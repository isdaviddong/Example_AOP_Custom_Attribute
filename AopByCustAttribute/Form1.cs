using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AopByCustAttribute
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //invoke method
            invoke(this.textBox1.Text, this.textBox2.Text);
        }

        string ChannelAccessToken = "___請換成自己line bot的token___"; //todo:請換成自己line bot的token

        //invoke method
        void invoke(string MethodName, string Para)
        {
            //只針對BO這個class
            BO bo = new BO();
            //判斷是否有該method
            var method = bo.GetType().GetMethod(MethodName);
            if (method == null)
            {
                MessageBox.Show($"No '{MethodName}'");
                return;
            }

            try
            {
                //如何處理 Logging attribute ?

                //執行特定method
                var ret = method.Invoke(bo, new object[] { Para });
                MessageBox.Show("result:" + ret);
            }
            catch (Exception ex)
            {
                //如果有exception

                //判斷是否有掛ExceptionNotify
                var attrs = method.GetCustomAttributes(typeof(ExceptionNotify), true);
                //如果有的話，就處理
                foreach (var item in attrs)
                {
                    var ExceptionNotify = (ExceptionNotify)item;
                    if (ExceptionNotify != null)
                    {
                        //send line(示意)
                        if (!string.IsNullOrEmpty(ExceptionNotify.LineTo))
                        {
                            PushLineMessage(ExceptionNotify.LineTo, ChannelAccessToken,
                                "發生錯誤:" + ex.InnerException.Message);
                        }
                        //send line(示意)
                        if (!string.IsNullOrEmpty(ExceptionNotify.MailTo))
                            //發mail(示意)
                            MessageBox.Show("MailTo:" + ((ExceptionNotify)item).MailTo);
                    }
                }
                //還是得丟出去，不能吃掉
                throw ex;
            }
        }

        void PushLineMessage(string ToLineId, string token, string Message)
        {
            isRock.LineBot.Bot bot = new isRock.LineBot.Bot(token);
            bot.PushMessage(ToLineId, Message);
        }
    }
}
