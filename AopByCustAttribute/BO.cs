using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopByCustAttribute
{
    public class BO
    {
        //如果發生未處裡的exception，則send line
        [ExceptionNotify(LineTo = "___請置換為要收Line訊息者的Id___")]   //todo:請置換為要收Line訊息者的Id
        public string MethodA(string para)
        {
            //當para==11就會exception，其餘沒事
            if (para == "11") throw new Exception("para==11");
            return para.ToString();
        }

        [Logging(LoggingType = LoggingType.file, logDateTime = true, logUserInfo = true, logParameters = true)]
        public string MethodB(string para)
        {
            //當para==11就會exception，其餘沒事
            if (para == "11") throw new Exception("para==11");

            return para + " hello ";
        }
    }
}
