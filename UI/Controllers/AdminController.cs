using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logic;
using Modal;
using Interface;
using Common;
using PagedList;

namespace UI.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/ 管理员控制器
        //1.用户信息管理
        //2.用户talk管理
        //3.用户评论管理
        //4.上传图片

        Disable_wordLogic dis = new Disable_wordLogic();

        TalkLogic talklogic = new TalkLogic();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddDisableword(string disable) 
        {
            string nr = disable.Trim();
            string[] strs;
            if (!string.IsNullOrEmpty(nr))
            {
                strs = nr.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                Disable_word word = new Disable_word();
                for (int i = 0; i < strs.Length; i++) 
                {
                    word.Content = strs[i];
                    dis.AddDisable_word(word);
                }
            }
            return Redirect("/Admin/Index");

        }

        public ActionResult AdminTalk(int? page)
        {
            //查询所有talk信息
            #region 分页查询所有talks信息
            List<Talk> talks = new List<Talk>();
            IMessageEntity msg = new MessageEntity();

            msg = talklogic.GetBigInfo<Talk>("talk", null);

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

        public ActionResult AdminUser() 
        {
            return View();
        }

        public ActionResult TalkDetail(int id) 
        {
            return Redirect("/Home/Detail/" + id);
        }


        public ActionResult ShowTalk(string Id) 
        {
            talklogic.ShowTalk(Id);
            //return Redirect("/Admin/AdminTalk");
            return RedirectToAction("AdminTalk");
        }

        public ActionResult HideTalk(string Id) 
        {
            talklogic.HideTalk(Id);
            //return Redirect("/Admin/AdminTalk");
            return RedirectToAction("AdminTalk");
        }

        public ActionResult DeleteTalk(int Id) 
        {
            talklogic.DeleteTalk(Id);
            return Redirect("/Admin/AdminTalk");
        }

    }
}
