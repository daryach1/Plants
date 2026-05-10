using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantsApp
{
    public class CommandParser
    {
        public IPlant CreatePlant(string[] parts)
        {
            if (parts.Length < 3)
                throw new ArgumentException("Недостаточно параметров для создания растения");

            string type = parts[1].ToLower();
            string name = parts[2];

            switch (type)
            {
                case "дерево":
                    if (!long.TryParse(parts[3], out long age))
                        throw new FormatException("Возраст должен быть целым числом");
                    return new Tree (name, age);
                case "кустарник":
                    if (!Enum.TryParse(parts[3], true, out Month month))
                        throw new FormatException("Неизвестный месяц");
                    return new Bush(name, month);
                case "кактус":
                    if (!double.TryParse(parts[3], out double length))
                        throw new FormatException("Длина колючек должна быть числом");
                    return new Cactus(name, length);
                default:
                    throw new ArgumentException("Неизвестный тип растения");
            }
        }
    }

    public interface IComparer
    {
        bool Compare (IPlant plant, string field, string op, string value);
    }

    public class PlantComparer : IComparer
    {
        public bool Compare (IPlant plant, string field, string op, string value)
        {
            field = field.ToLower();
            op = op.Trim();

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

                        if (!Enum.TryParse(monthString, true, out Month month))
                            return false;
                        return CompareNumbers((int)bush.FloweringMonth, op, (int)month);
                    }
                    break;
            }

            return false;
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
    }
}
