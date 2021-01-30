using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestForCleverence.ru
{
    static class Server
    {
        private static object _lock = new object();

        private static int count;

        public static async Task<int> GetCount() => await Task.Run(() => GetCountAsync());

        static int GetCountAsync()
        {
            Console.WriteLine($"Чтение - {count}");
            return count;
        }

        public static async void AddToCount(int value) => await Task.Run(() => AddToCountAsync(value));

        static void AddToCountAsync(int value)
        {
            lock (_lock)
            {
                Console.WriteLine($"Запись - {value}");
                //Thread.Sleep(1000); //Для проверки работоспособнности lock задержка в 1000 мс
                count = value;
            }
        }
    }
}
