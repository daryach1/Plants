using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlantsApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Создаем менеджер растений
                var manager = new PlantManager();

                // Передаем путь к файлу через конструктор
                var processor = new FileProcessor(manager);
                processor.ReadFile(@"C:\Users\Dasha\Desktop\plants.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
