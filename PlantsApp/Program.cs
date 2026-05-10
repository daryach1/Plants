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
        static List<Plant> plants = new List<Plant>();
        static void Main(string[] args)
        {
            string path = @"C:\Users\Dasha\Desktop\plants.txt";
            ReadFile(path);
        }

        static void ReadFile(string path)
        {
            if(!File.Exists(path))
            {
                Console.WriteLine("Файл не найден");
                return;
            }
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                ProcessCommand(line);
            }
        }

        static void ProcessCommand(string command)
        {
            var parts = command.Split(' ');
            switch (parts[0].ToUpper())
            {
                case "ADD":
                    //AddPlant(parts);
                    break;
                case "REM":
                    RemovePlant(parts);
                    break;
                case "PRINT":
                    //PrintPlants();
                    break;
                default:
                    Console.WriteLine("Неизвестная команда");
                    break;
            }
        }

      

        



        static void RemovePlant(string[] parts)
        {
            if(parts.Length < 4)
            {
                Console.WriteLine("Недопустимая команда REM");
                return;
            }

            string field = parts[1];
            string op = parts[2];
            string value = parts[3];

            //plants = plants.Where(p => !CheckCondition(p, field, op, value)).ToList();
        }
    }
}
