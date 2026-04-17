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
                    AddPlant(parts);
                    break;
                case "REM":
                    RemovePlant(parts);
                    break;
                case "PRINT":
                    PrintPlants();
                    break;
                default:
                    Console.WriteLine("Неизвестная команда");
                    break;
            }
        }

        static void AddPlant (string[] parts)
        {
            try
            {
                string type = parts[1].ToLower();
                string name = parts[2];

                switch (type)
                {
                    case "дерево":
                        long age = long.Parse(parts[3]);
                        plants.Add(new Tree(name, age));
                        break;
                    case "кустарник":
                        if (!Enum.TryParse(parts[3].Trim(), true, out Month month))
                        {
                            Console.WriteLine($"Неправильный месяц {month}");
                        }
                        plants.Add(new Bush(name, month));  
                        break;
                    case "кактус":
                        double length = double.Parse(parts[3]);
                        plants.Add(new Cactus(name, length));
                        break;
                }

            }
            catch
            {
                Console.WriteLine("Ошибка в добавлении");
            }
        }

        static bool CompareStrings(string a, string op, string b)
        {
            op = op.Trim();

            switch (op)
            {
                case "=": return a.Equals(b, StringComparison.OrdinalIgnoreCase);
                case "!=": return !a.Equals(b, StringComparison.OrdinalIgnoreCase);
                default: return false;
                    
            }

        }

        static bool CompareNumbers(double a, string op, double b)
        {
            op = op.Trim();

            switch (op)
            {
                case ">": return a > b;
                case "<": return a < b;
                case ">=": return a >= b;
                case "<=": return a <= b;
                case "=": return a == b;
                case "!=": return a != b;
                default: return false;
            }
        }

        static bool CheckCondition (Plant plant, string field, string op, string value)
        {
            field = field.ToLower();
            switch (field)
            {
                case "название":
                    return CompareStrings(plant.Name, op, value);
                case "возраст":
                    if (plant is Tree tree)
                        return CompareNumbers(tree.Age, op, double.Parse(value));
                    break;
                case "длинаколючек":
                    if (plant is Cactus cactus)
                        return CompareNumbers(cactus.SpineLength, op, double.Parse(value));
                    break;
                case "месяццветения":
                    if (plant is Bush bush)
                    {
                        string monthString = value.Trim();

                        if(!Enum.TryParse(monthString, true, out Month month))
                            return false;
                        return CompareNumbers((int)bush.FloweringMonth, op, (int)month);
                    }
                    break;
            }

            return false;
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

            plants = plants.Where(p => !CheckCondition(p, field, op, value)).ToList();
        }

        static void PrintPlants()
        {
            Console.WriteLine("------Текущее состояние контейнера:------");
            if(plants.Count == 0)
            {
                Console.WriteLine("Нет растений");
                return;
            }

            foreach (var plant in plants)
            {
                Console.WriteLine(plant.GetInfo());
            }
            Console.WriteLine("-----------------------------------------");
        }
    }
}
