using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modal;
using Logic;
using Common;
using Interface;
using PagedList;


namespace UI.Controllers
{
    public class HomeController : Controller
    {
        #region
        ///// <summary>
        ///// 对talk初始加载内容进行16位md5加密后字符串 全局变量
        ///// </summary>
        //private static string contentMd5;

        ///// <summary>
        ///// 对talk初始加载所有数据(主题、标题、内容)进行16位md5加密后字符串 全局变量
        ///// </summary>
        //private static string allcontentMd5;

        #endregion
        //
        // GET: /Home/
        TalkLogic talklogic = new TalkLogic();


        UserLogic userlogic = new UserLogic();

        Disable_wordLogic dislogic = new Disable_wordLogic();

        public ActionResult Index(int? page)
        {
            ViewData["address"] = ComHelper.GetAddress();
            IMessageEntity msguser = null;
            User user=null;
            ViewData["flag"] = "";
            ViewData["img"] = "";

            #region
            if (Session["user"] != null ) 
            {
                string ulogin = Session["user"].ToString() == null ? "" : Session["user"].ToString();
                if (!string.IsNullOrEmpty(ulogin))
                {
                    if (ComHelper.GetDate() == "早上")
                    {
                        ViewData["img"] = ComHelper.GetMorning();
                    }
                    else if (ComHelper.GetDate() == "下午")
                    {
                        ViewData["img"] = ComHelper.GetNoon();
                    }
                    else
                    {
                        ViewData["img"] = ComHelper.GetNight();
                    }
                }
            }

            #endregion

            


            #region 非分页形式
            //List<Talk> talks = new List<Talk>();
            //IMessageEntity msg = new MessageEntity();
            //msg = talklogic.SelectTalk();
            //if (msg.Msgflag)
            //{
            //    talks = (List<Talk>)msg.Msgvalue;
            //}
            //ViewData.Model = talks;
            #endregion

            if (Session["user"] != null)
            {
                msguser = userlogic.SelectUser(Session["user"].ToString());
                if (msguser.Msgflag)
                {
                    user = (User)msguser.Msgvalue;
                }

                if (user.Des != "")
                {
                    ViewData["flag"] = user.Des;
                }
                else
                {
                    ViewData["flag"] = ComHelper.GetFlag();
                }

                
            }
            else
            {
                ViewData["flag"] = ComHelper.GetFlag();
            }



            


            #region 分页形式

            List<Talk> talks = new List<Talk>();
            IMessageEntity msg = new MessageEntity();
            msg = talklogic.SelectTalk();
            if (msg.Msgflag)
            {
                talks = (List<Talk>)msg.Msgvalue;
            }


            int pageIndex = page ?? 1;//第几页
            int pageSize = ComHelper.PageSize();//每页显示几条数据
            //通过ToPagedList扩展方法进行分页  
            IPagedList<Talk> talkPagedList = talks.ToPagedList(pageIndex, pageSize);
            
            


            #endregion


            return View(talkPagedList);
        }

        /// <summary>
        /// 显示信息详情
        /// </summary>
        /// <param name="id">信息id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail(int id)
        {
            ViewData["updateflag"] = "";
            IMessageEntity msg = new MessageEntity();
            Talk t = new Talk();
            msg = talklogic.SelectTalk(id);
            if (msg.Msgflag) 
            {
                t = (Talk)msg.Msgvalue;
            }

            //Session["contentmd5"] = Md5Helper.GetMd5(t.Content, 16);//给talk的content进行md5加密

            //Session["allcontentmd5"] = Md5Helper.GetMd5(t.Theme + t.Title + t.Content, 16);//给talk的theme、title、content加密

            ViewData.Model = t;

            

            return View();
        }


        

        



        /// <summary>
        /// 其他用户更新和发布者本人更新talk的方法
        /// </summary>
        /// <param name="talk"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Detail(Talk talk) 
        {

            IMessageEntity msginfo = new MessageEntity();
            Talk t = new Talk();           
             IMessageEntity msg = new MessageEntity();
            string flag = "";
            ViewData["updateflag"] = "";

            #region 查询talk实体
            msginfo = talklogic.SelectTalk(talk.Id);
            if (msginfo.Msgflag)
            {
                t = (Talk)msginfo.Msgvalue;
            }

            ViewData.Model = t;
            #endregion

            #region 检查talks的主题/标题/内容是否为空 /检查内容中是否包含敏感字
            if (string.IsNullOrEmpty(talk.Theme.Trim()) || string.IsNullOrEmpty(talk.Title.Trim()) || string.IsNullOrEmpty(talk.Content.Trim())) 
            {
                ViewData["updateflag"] = "更新失败：主题/标题/内容不能为空";
                return View();
            }
            List<string> dis_words = (List<string>)dislogic.SelectContent().Msgvalue;
            if (ComHelper.ValidateStrs(dis_words, talk.Content)) 
            {
                ViewData["updateflag"] = "更新失败：内容存在敏感词";
                return View();
            }
            
            #endregion



            #region 发布人和普通用户更新talk时采用不同方法
            //if (talk.CName == session["user"].toString())
            //{
            //    msg = talklogic.UpdateTalk(talk.UName, talk);//发布人更新自己的talk
            //}
            //else
            //{
            //    msg = talklogic.UpdateTalk(talk.UName, talk);//非发布用户更改自己的talk
            //}
            #endregion

            #region  发布人和普通用户更新talk时采用相同方法
            msg = talklogic.UpdateTalk(talk.UName, talk);//发布人更新自己的talk
            #endregion
            #region 发布人和当前登录用户一致和不一致时做出不同的判断
            //if (talk.CName == Session["user"].ToString())//创建人修改自己的talk
            //{
            //     currentallMd5 = Md5Helper.GetMd5(talk.Theme + talk.Title + talk + talk.Content, 16);
            //    if (currentallMd5 != Session["allcontentmd5"].ToString())
            //    {
            //        msg = talklogic.UpdateTalk(talk.UName, talk);//执行更新操作
            //    }
            //    else
            //    {
            //        flag = "系统检测到你未修改内容";
            //    }
            //}
            //else
            //{
            //    currentMd5 = Md5Helper.GetMd5(talk.Content, 16);
            //    if (currentMd5 != Session["contentmd5"].ToString()) 
            //    {
            //        msg = talklogic.UpdateTalk(talk.UName, talk);//执行更新操作
            //    }
            //}
            #endregion

            if (msg.Msgflag)
            {
                flag = "";
            }
            else
            {
                flag = "更新失败：" + msg.Msgvalue;

                #region
                //if (talk.CName == Session["user"].ToString())
                //{
                //    if (currentallMd5 == allcontentMd5) 
                //    {
                //        flag = "更新失败：系统检测到未修改内容";
                //    }
                //}
                //else
                //{
                //    if (currentMd5 == contentMd5) 
                //    {
                //        flag = "更新失败：系统检测到未修改内容";
                //    }
                //}
                #endregion

            }

            ViewData["updateflag"] = flag;


            #region 查询talk实体
            msginfo = talklogic.SelectTalk(talk.Id);
            if (msginfo.Msgflag)
            {
                t = (Talk)msginfo.Msgvalue;
            }

            ViewData.Model = t;
            #endregion
           

            return View();
        }



        [HttpGet]
        public ActionResult AddTalk() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTalk(Talk talk)
        {
            ViewData["flag"] = "";
            if (string.IsNullOrEmpty(talk.Theme) || string.IsNullOrEmpty(talk.Title) || string.IsNullOrEmpty(talk.Content))
            {
                ViewData["flag"] = "请输入 主题/标题/内容";
                return View();
            }


            

            

            #region  赋值之前检查talk中是否包含敏感词
            List<string> dislists = (List<string>)dislogic.SelectContent().Msgvalue;//在数据库中查询出所有敏感词

            if (ComHelper.ValidateStrs(dislists, talk.Content)) 
            {
                ViewData["flag"] = "操作失败：内容包含敏感词";
                return View();
            }

            #endregion


            #region 给talk属性赋值
            IMessageEntity msguid = new MessageEntity();
            string cname = "";
            int uid = 0;
            if (Session["user"] != null && Session["user"].ToString() !="") 
            {
                cname = Session["user"].ToString();
                msguid = userlogic.SelectUserId(cname);
                if (msguid.Msgflag) 
                {
                    uid = Convert.ToInt32(msguid.Msgvalue);
                }

            }
            talk.CName = cname;
            talk.UId = uid;


            



            
            #endregion




            #region 插入talk操作

            


            IMessageEntity msgtalk = new MessageEntity();
            msgtalk = talklogic.AddTalk(talk);




            if (msgtalk.Msgflag)
            {
                return RedirectToAction("UserCenter", "User");
            }
            else
            {
                ViewData["flag"] = "操作失败：" + msgtalk.Msgvalue.ToString();
            }
            #endregion

            return View();
        }


        /// <summary>
        /// 显示talk
        /// </summary>
        /// <param name="talkid">talk的id</param>
        /// <returns></returns>
        public ActionResult ShowTalk(int Id)
        {
            talklogic.ShowTalk(Id);         
            return Redirect("/User/UserCenter");
        }


        /// <summary>
        /// 隐藏talk
        /// </summary>
        /// <param name="talkid">talk的id</param>
        /// <returns></returns>
        public ActionResult HideTalk(int Id)
        {
            talklogic.HideTalk(Id);
            return Redirect("/User/UserCenter");
        }

    }
}
