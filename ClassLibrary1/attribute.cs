using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using isRock.Framework.AOP;

namespace ClassLibrary1
{

    /// <summary>
    /// 客製化 attribute 必須繼承 PolicyInjectionAttributeBase
    /// </summary>
    public class ExceptionNotify : PolicyInjectionAttributeBase
    {
        //指定Log File Name
        public string LogFileName { get; set; }
        //override OnException方法
        public override void OnException(object sender, PolicyInjectionAttributeEventArgs e)
        {
            var msg = $"\r\n exception({DateTime.Now.ToString()}) : \r\n{e.Exception.Message }";
            //Console.Write(msg);
            SaveLog(msg);
        }

        //寫入Log
        private void SaveLog(string msg)
        {
            if (System.IO.File.Exists(LogFileName))
            {
                System.IO.File.AppendAllText(LogFileName, msg);
            }
            else
            {
                System.IO.File.WriteAllText(LogFileName, msg);
            }
        }
    }
    public class Logging : PolicyInjectionAttributeBase
    {
        //指定Log File Name
        public string LogFileName { get; set; }
        //override AfterInvoke方法
        public override void AfterInvoke(object sender, PolicyInjectionAttributeEventArgs e)
        {
            var msg = $"\r\n Method '{e.MethodBase.Name}' has been called - {DateTime.Now.ToString()} ";
            SaveLog(msg);
        }

 
       //寫入Log
       private void SaveLog(string msg)
        {
            if (System.IO.File.Exists(LogFileName))
            {
                System.IO.File.AppendAllText(LogFileName, msg);
            }
            else
            {
                System.IO.File.WriteAllText(LogFileName, msg);
            }
        }
    }

}
