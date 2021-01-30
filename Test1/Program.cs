using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace TestForCleverence.ru
{
    class Program
    {
        static void Main(string[] args)
        {

            Parallel.For(1, 10, x =>
            {
                Server.AddToCount(x);  //добовление к счетчику 
                var list = new List<int>();

                for (int i = 0; i < 10; ++i)
                {
                    list.Add(Server.GetCount().Result);
                }
            });

            Console.ReadLine();
        }
    }
}
/*
 * 
 * 
 *  Есть "сервер" в виде статического класса.  
У него есть переменная count (тип int) и два метода, которые позволяют эту переменную читать и писать: GetCount() и AddToCount(int value). 
К серверу стучатся множество параллельных клиентов, которые в основном читают, но некоторые добавляют значение к count. 

Нужно реализовать GetCount / AddToCount так, чтобы: 

читатели могли читать параллельно, без выстраивания в очередь по локу; 
писатели писали только последовательно и никогда одновременно; 
пока писатели добавляют и пишут, читатели должны ждать окончания записи. 
 */