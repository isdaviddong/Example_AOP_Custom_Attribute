using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;
using isRock.Framework.AOP;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //BMIProcessor BMI =  new BMIProcessor();
            //上面這行改為底下這樣
            BMI bmi = PolicyInjection.Create<BMI>();

            //其餘程式碼完全不變
            bmi.Height = 170;
            bmi.Weight = 70;
            //計算BMI
            var ret = bmi.Calculate();
            Console.WriteLine($"\nBMI : {ret}");
            Console.ReadKey();

            //測試exception
            bmi.Height = 0;
            bmi.Weight = 0;

            //計算BMI
            ret = bmi.Calculate();

            Console.WriteLine($"\nBMI : {ret}");
            Console.ReadKey();
        }
    }


    /// <summary>
    /// BMI類別必須繼承 PolicyInjectionComponentBase
    /// </summary>
    public class BMI : PolicyInjectionComponentBase
    {
        public int Weight { get; set; }
        public int Height { get; set; }
        public Decimal Value
        {
            get
            {
                return Calculate();
            }
        }

        [ExceptionNotify(LogFileName = "log.txt")]
        [Logging(LogFileName = "log.txt")]
        //計算BMI (被掛上了客製化的attribute)
        public Decimal Calculate()
        {
            Decimal result = 0;
            Decimal height = (Decimal)Height / 100;
            result = Weight / (height * height);

            return result;
        }
    }
}
