using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modal;
using Logic;
using Interface;
using Common;
using PagedList;

namespace UI.Controllers
{
    public class UserController : Controller
    {                
        BaseLogic baseLogic = new UserLogic();
        UserLogic userLogic = new UserLogic();
        TalkLogic talklogic = new TalkLogic();
        Code validationcode = new Code();       
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }
        private void SetRegistSkin() 
        {
            ViewData["RegistImg"] = ComHelper.GetRegistImgPath();
        }

        private void SetLoginSkin() 
        {
            ViewData["LoginImg"] = ComHelper.GetLoginImgPath();
        }
        [HttpGet]
        public ActionResult Regist() 
        {

            SetRegistSkin();
            return View();
        }


        [HttpPost]
        public ActionResult Regist(User user) 
        {
            IMessageEntity msg = new MessageEntity();
            string result = "";

            string code = Request["code"];
             
            #region 对user赋值
            user.Upwd = Md5Helper.GetMd5(user.Upwd, 16);
            user.Id = CommonUtil.NewShortGuid();
            user.Cdate = DateTime.Now;
            #endregion

            if (Session["code"].ToString() == code)
            {
                if (!string.IsNullOrEmpty(user.Uname) && !string.IsNullOrEmpty(user.Upwd))
                {
                    msg = userLogic.Add<User>("user",user);
                    if (msg.Msgflag)
                    {
                        result = "注册成功";

                    }
                    else
                    {
                        result = "注册失败";

                    }
                }
                else
                {
                    result = "请输入用户名和密码";

                }
            }
            else
            {
                result = "验证码输入错误";
            }
     

            if (msg.Msgflag)
            {
                return RedirectToAction("Login", "User");
            }
            else 
            {
                SetRegistSkin();
                ViewData["result"] = result;
                return View();
            }


            //return Content(result);//往前台输出字符串


        }



     


        [HttpGet]
        public ActionResult Login() 
        {
            SetLoginSkin();
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user) 
        {
            //登录成功后将用户名保存在Session["user"]对象中
            IMessageEntity result = new MessageEntity();

            string code = Request["code"];


            if (Session["code"].ToString() == code)
            {
                if (!string.IsNullOrEmpty(user.Uname) && !string.IsNullOrEmpty(user.Upwd))
                {
                    user.Upwd = Md5Helper.GetMd5(user.Upwd, 16);

                    string query = string.Format(" uname='{0}' and upwd='{1}' ", user.Uname, user.Upwd);
                    result = userLogic.GetInfo<User>("user", query);
                }
                else
                {
                    result.Msgvalue = "请输入用户名和密码";
                }
            }
            else
            {
                result.Msgvalue = "验证码输入有误";
            }        

            if (result.Msgflag)
            {
                Session["user"] = user.Uname;

                if (user.Uname == "admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

               
            }
            else 
            {
                SetLoginSkin();
                ViewData["result"] = result.Msgvalue;
                return View();
            }
            
        }



        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns>返回验证码</returns>
        public ActionResult ValidateCode()
        {
            #region code2
            Code code = new Code();
            string strcode = code.GetValidationCode();

            Session["code"] = strcode;
            byte[] bytecode = code.CreateValidateGraphic(strcode);
            return File(bytecode, @"image/jpeg");            
            #endregion

           
        }


        public ActionResult SearchInfo(int? page) 
        {
            string keyword = Request["keyword"];
            List<Talk> talks = new List<Talk>();
            IMessageEntity msg = new MessageEntity();

            #region  分页形式

           

            msg = talklogic.GetBigInfo<Talk>("talk", string.Format("(theme like'%{0}%' or title like'%{0}%' or content like'%{0}%') and display=1 order by cdate desc", keyword));

            if (msg.Msgflag)
            {
                talks = (List<Talk>)msg.Msgvalue;
            }

            int pageIndex = page ?? 1;//第几页
            int pageSize = ComHelper.PageSize();//每页显示几条数据
            //通过ToPagedList扩展方法进行分页  
            IPagedList<Talk> talkPagedList = talks.ToPagedList(pageIndex, pageSize);
            #endregion


            if (talks.Count <=0)
            {
                ViewData["nr"] = "信息为空";           
                return View(talkPagedList);
            }

            else
            {
                ViewData.Model = talks;
                return View(talkPagedList);
            }
            
        }

        public ActionResult Informations(int? page) 
        {
            string theme = Request["Theme"];
            List<Talk> talks = new List<Talk>();
            IMessageEntity msg = new MessageEntity();

            #region  分页
            //msg = talklogic.SelectTalkByTheme(theme);
            msg = talklogic.GetBigInfo<Talk>("talk", string.Format("theme='{0}' and display=1", theme));

            if (msg.Msgflag)
            {
                talks = (List<Talk>)msg.Msgvalue;
            }

            int pageIndex = page ?? 1;//第几页
            int pageSize = ComHelper.PageSize();//每页显示几条数据
            //通过ToPagedList扩展方法进行分页  
            IPagedList<Talk> talkPagedList = talks.ToPagedList(pageIndex, pageSize);
            #endregion



            if (talks.Count <= 0)
            {
                ViewData["nr"] = "信息为空";
                return View(talkPagedList);
            }

            else
            {
                ViewData.Model = talks;
                return View(talkPagedList);
            }


           
        }


        public ActionResult TalkDetail(string id) 
        {
            return Redirect("/Home/Detail/" + id);
        }


        [HttpGet]
        public ActionResult ReSetPwd() 
        {

            return View();
        }


        [HttpPost]
        public ActionResult ReSetPwd(string email,string code,string pwd1,string pwd2) 
        {
            
            string em = "";
            if (Session["code"].ToString() == code)
            {
                if (string.IsNullOrEmpty(email))
                {
                    em = "请输入邮箱";
                }
                else
                {
                    IMessageEntity msg = userLogic.IsUserExist(email, 2);

                    if (!string.IsNullOrEmpty(pwd1) && !string.IsNullOrEmpty(pwd2))
                    {
                        if (pwd1 == pwd2)
                        {
                            if (msg.Msgflag) //该用户存在
                            {
                                //调用
                                //pwd = validationcode.GetValidationCode();//随机生成的6位字母+数字验证码
                                if (userLogic.UpdateUser(email, pwd1).Msgflag)
                                {
                                    em = "密码更新成功，新密码为：" + pwd1 + "请您妥善保管";
                                }
                                else
                                {
                                    em = "密码更新失败";
                                }

                            }
                            else
                            {
                                em = "邮箱不存在";
                            }
                        }
                        else
                        {
                            em = "两次输入的密码不一致";
                        }
                    }
                    else
                    {
                        em = "请输入密码获取确认密码";
                    }

                    
                    
                    
                }
            }
            else
            {
                em = "验证码输入有误";
            }

            
            
            

            ViewData["em"] = em;

            return View();
        }




        [HttpGet]
        public ActionResult UserCenter(int? page) 
        {
            List<Talk> talks = new List<Talk>();
            IMessageEntity msg = new MessageEntity();
            IPagedList<Talk> talkPagedList=null;

            #region 分页方式
            if (Session["user"] != null)
            {
                string cname = Session["user"].ToString();
                string query = string.Format("cname='{0}' ", cname);
                msg = talklogic.GetBigInfo<Talk>("talk", query);
                if (msg.Msgflag && msg.Msgvalue!=null)
                {
                    talks = (List<Talk>)msg.Msgvalue;
                }
                int pageIndex = page ?? 1;//第几页
                int pageSize = ComHelper.PageSize();//每页显示几条数据
                //通过ToPagedList扩展方法进行分页  
                 talkPagedList= talks.ToPagedList(pageIndex, pageSize);
            }
            #endregion

            return View(talkPagedList);
            
        }

        [HttpGet]
        public ActionResult UserInfo() 
        {
            User user = null;
            string Uname = Session["user"].ToString();
            string query = string.Format("Uname='{0}' ", Uname);
            IMessageEntity msgUser = userLogic.GetInfo<User>("user", query);
            List<User> a = new List<User>();
            if (msgUser.Msgflag) 
            {
                user = (User)((List<User>)msgUser.Msgvalue).FirstOrDefault();
            }
            ViewData.Model = user;         
            return View();
        }

     

        /// <summary>
        /// 用户退出
        /// </summary>
        /// <returns></returns>
        public ActionResult UserExit() 
        {
            Session["user"] = null;
            return Redirect("/User/UserCenter");
        }

        /// <summary>
        ///加载用户中心
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadUserTalk() 
        {
            return Redirect("/User/UserCenter");
        }



        public ActionResult DeleteTalk(int Id) 
        {
            talklogic.DeleteTalk(Id);
            return Redirect("/User/UserCenter");
        }


        


        

    }
}
