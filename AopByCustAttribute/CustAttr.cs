using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopByCustAttribute
{
    public class ExceptionNotify : System.Attribute
    {
        public string LineTo { get; set; }
        public string MailTo { get; set; }
    }

    public enum LoggingType
    {
        database, file, AppInsight
    }

    public class Logging : System.Attribute
    {
        public LoggingType LoggingType { get; set; }
        public bool logUserInfo{ get; set; }
        public bool logDateTime { get; set; }
        public bool logParameters{ get; set; }
    }

}