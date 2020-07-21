using FenbuDuxie.Dal;
using System;
using System.Dynamic;

namespace FenbuDuxie
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("欢迎进入朝夕教育：");
                {
                    SqlHelper sqlHelper = new SqlHelper();
                    Company company = sqlHelper.Find<Company>(1);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message); ;
            }
            Console.Read();
        }
    }
}
