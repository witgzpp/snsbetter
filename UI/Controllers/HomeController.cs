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

            if (Session["user"] != null)
            {
                string query = string.Format("uname='{0}' ", Session["user"].ToString());
                msguser = userlogic.GetInfo<User>("user",query);
                if (msguser.Msgflag)
                {
                    user = (User)((List<User>)msguser.Msgvalue).FirstOrDefault();
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
            msg = talklogic.GetBigInfo<Talk>("talk", "display=1");
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
        public ActionResult Detail(string id)
        {
            ViewData["updateflag"] = "";
            IMessageEntity msg = new MessageEntity();
            Talk t = new Talk();
            //msg = talklogic.SelectTalk(id);
            string query = string.Format("id='{0}' ", id);
            msg = talklogic.GetInfo<Talk>("talk", query);
            if (msg.Msgflag) 
            {
                if (msg.Msgvalue != null) 
                {
                    t = (Talk)((List<Talk>)msg.Msgvalue)[0];
                }
            }

            //Session["contentmd5"] = Md5Helper.GetMd5(t.Content, 16);//给talk的content进行md5加密

            //Session["allcontentmd5"] = Md5Helper.GetMd5(t.Theme + t.Title + t.Content, 16);//给talk的theme、title、content加密

            ViewData.Model = t;
            ViewData["title"] = t.Title;
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
            //msginfo = talklogic.SelectTalk(talk.Id);

            msginfo = talklogic.GetInfo<Talk>("talk", string.Format("id='{0}' ", talk.Id));
            if (msginfo.Msgflag)
            {
                t = ((List<Talk>)msginfo.Msgvalue).FirstOrDefault();
            }

            ViewData.Model = t;
            #endregion

            #region 检查talks的主题/标题/内容是否为空 /检查内容中是否包含敏感字
            if (string.IsNullOrEmpty(talk.Theme.Trim()) || string.IsNullOrEmpty(talk.Title.Trim()) || string.IsNullOrEmpty(talk.Content.Trim())) 
            {
                ViewData["updateflag"] = "更新失败：主题/标题/内容不能为空";
                return View();
            }
            //List<string> dis_words = (List<string>)dislogic.SelectContent().Msgvalue;
            List<string> dis_words = (List<string>)dislogic.GetListField<string>("disable_word", "content", null).Msgvalue;
            if (ComHelper.ValidateStrs(dis_words, talk.Content)) 
            {
                ViewData["updateflag"] = "更新失败：内容存在敏感词";
                return View();
            }
            
            #endregion



            



            #region  发布人和普通用户更新talk时采用相同方法

            talk.Udate = DateTime.Now;
            
            
           

            if (talk.Cname == Session["user"].ToString()) //修改人为作者
            {
                
            }
            else //修改人不是作者
            {
                if (string.IsNullOrEmpty(talk.Unames))
                {
                    talk.Unames = Session["user"].ToString();
                }
                else 
                {
                    talk.Unames = talk.Unames + ";" + Session["user"].ToString();
                }
                
            }

            //msg = talklogic.UpdateTalk(talk.Uname, talk);//发布人更新自己的talk

            msg = talklogic.Update<Talk>("talk", talk);
            #endregion

            if (msg.Msgflag)
            {
                flag = "";
            }
            else
            {
                flag = "更新失败：" + msg.Msgvalue;

               

            }

            ViewData["updateflag"] = flag;


            #region 查询talk实体
            //msginfo = talklogic.SelectTalk(talk.Id);

            msginfo = talklogic.GetInfo<Talk>("talk", string.Format("id='{0}'", talk.Id));
            if (msginfo.Msgflag)
            {
                t = ((List<Talk>)msginfo.Msgvalue).FirstOrDefault();
            }

            ViewData.Model = t;
            #endregion

            ViewData["title"] = t.Title;

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
            List<string> dislists = (List<string>)dislogic.GetListField<string>("disable_word", "content", string.Empty).Msgvalue;//在数据库中查询出所有敏感词
            var listResult= dislists.Where(item => talk.Content.Contains(item)).Select(item => item);
            if (listResult != null && listResult.Count() >= 1) 
            {
                ViewData["flag"] = "操作失败：内容包含敏感词";
                return View();
            }
            #endregion
            #region 插入talk操作
            #region 给talk属性赋值
            IMessageEntity msguser = new MessageEntity();
            string cname = "";

            if (Session["user"] != null && Session["user"].ToString() != "")
            {
                cname = Session["user"].ToString();
                msguser = userlogic.GetInfo<User>("user", string.Format("uname='{0}' ", cname));
                if (msguser.Msgflag && msguser.Msgvalue != null)
                {
                    talk.Cid = ((List<User>)msguser.Msgvalue)[0].Id;
                }

            }
            talk.Cname = cname;
            talk.Cdate = DateTime.Now;
            talk.Id = CommonUtil.NewShortGuid();
            #endregion
            IMessageEntity msgtalk = new MessageEntity();
            msgtalk = talklogic.Add<Talk>("talk", talk);
            if (msgtalk.Msgflag)//添加成功后执行跳转操作
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
        public ActionResult ShowTalk(string Id)
        {
            talklogic.ShowTalk(Id);
            return Redirect("/User/UserCenter");
        }


        /// <summary>
        /// 隐藏talk
        /// </summary>
        /// <param name="talkid">talk的id</param>
        /// <returns></returns>
        public ActionResult HideTalk(string Id)
        {
            talklogic.HideTalk(Id);
            return Redirect("/User/UserCenter");
        }

    }
}
