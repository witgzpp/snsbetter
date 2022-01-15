using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    /// <summary>
    /// 对log_error表的封装
    /// </summary>
    [Serializable]
    public class Log_error
    {
        private int id;
        private string content;
        private DateTime cdate;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        public DateTime CDate
        {
            get { return cdate; }
            set { cdate = value; }
        }
    }
}