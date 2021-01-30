using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test2
{
    class Program
    {
        static void Main(string[] args)
        {
            var ms = new Questions().Question<int>("Тест 2. Введите необходимую задержку (у метода 5000 мс): ",
                new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', });

            var test2 = new TestInvoke();
            bool isCompleted = test2.Start(int.Parse(ms));
            Console.WriteLine("");
            Console.WriteLine("Время исполнения метода " + (isCompleted ? "удовлетворяет требованию." : "не удовлетворяет требованию."));
            Console.ReadLine();
        }
    }

    class TestInvoke
    {
        public bool Start(int ms)
        {
            EventHandler h = new EventHandler(this.myEventHandler);
            var ac = new AsyncCaller(h);
            bool completedOK = ac.Invoke(ms, null, EventArgs.Empty);

            return completedOK;
        }

        private void myEventHandler(object sender, EventArgs e)
        {
            Thread.Sleep(5000);
        }

    }

    /// <summary>
    /// Класс запроса данных у пользователя
    /// </summary>
    class Questions
    {
        /// <summary>
        /// Перевести первый символ в заглавный
        /// </summary>
        /// <param name="text">Корректируемый текст</param>
        /// <returns></returns>
        public string FirstUpper(string text) => text.Substring(0, 1).ToUpper() + (text.Length > 1 ? text.Substring(1) : "");

        /// <summary>
        /// Запрос данных у пользователя
        /// </summary>
        /// <typeparam name="T">Тип выводимых значений (При отличных от стринг типах данных происходить замена точек на запятые)</typeparam>
        /// <param name="text">Текст запроса значения у пользователя</param>
        /// <param name="arraySym">Массив допустимых вводимых символов пользователем</param>
        /// <returns></returns>
        public string Question<T>(string text, HashSet<char> arraySym)
        {
            Console.Write(text);
            var textAnswer = new StringBuilder();
            while (true)
            {
                var symbol = Console.ReadKey(true);
                if (arraySym.Contains(symbol.KeyChar))
                {
                    textAnswer.Append(symbol.KeyChar.ToString());
                    Console.Write(symbol.KeyChar.ToString());
                }

                if (symbol.Key == ConsoleKey.Backspace && textAnswer.Length > 0)
                {
                    textAnswer.Remove(textAnswer.Length - 1, 1);
                    Console.Write(symbol.KeyChar.ToString());
                    Console.Write(" ");
                    Console.Write(symbol.KeyChar.ToString());
                }

                if (typeof(T) == typeof(string))
                {
                    if (symbol.Key == ConsoleKey.Enter && textAnswer.Length > 0)
                        break;
                }
                else
                    if (symbol.Key == ConsoleKey.Enter &&
                        double.TryParse(textAnswer.ToString()
                            .Replace(".", ","),
                            out var number))
                    break;
            }
            Console.Write("");
            return textAnswer.ToString();
        }
    }
}
/*  
Задача #2
В .net есть возможность звать делегаты как синхронно: 
EventHandler h = new EventHandler(this.myEventHandler); 
h.Invoke(null, EventArgs.Empty); 
так и асинхронно:
var res = h.BeginInvoke(null, EventArgs.Empty, null, null);

Нужно реализовать возможность полусинхронного вызова делегата (написать реализацию класса AsyncCaller), который бы работал таким образом: 

EventHandler h = new EventHandler(this.myEventHandler); 
ac = new AsyncCaller(h); 
bool completedOK = ac.Invoke(5000, null, EventArgs.Empty);

"Полусинхронного" в данном случае означает, что делегат будет вызван, и вызывающий поток будет ждать, пока вызов не выполнится.  Но если выполнение делегата займет больше 5000 миллисекунд, то ac.Invoke выйдет и вернет в completedOK значение false.
 */