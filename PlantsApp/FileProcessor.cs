using System;
using System.IO;

namespace PlantsApp
{
    public class FileProcessor
    {
        private PlantManager _manager;
        private CommandParser _parser;

        public FileProcessor(PlantManager plantManager)
        {
            _manager = plantManager;
            _parser = new CommandParser();
        }

        public void ReadFile(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException("Файл не найден");
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line)) ProcessCommand(line);
            }
        }

        private void ProcessCommand(string command)
        {
            try
            {
                var parts = command.Split(' ');
                switch (parts[0].ToUpper())
                {
                    case "ADD":
                        HandleAddCommand(parts);
                        break;
                    case "REM":
                        HandleRemoveCommand(parts);
                        break;
                    case "PRINT":
                        _manager.PrintPlants();
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
            catch (Exception ex) { Console.WriteLine($"Ошибка при обработке команды: {ex.Message}"); }
            
        }

        private void HandleAddCommand(string[] parts)
        {
            try
            {
                IPlant plant = _parser.CreatePlant(parts);
                if (plant != null) _manager.AddPlant(plant);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении: {ex.Message}");
            }
        }

        private void HandleRemoveCommand(string[] parts)
        {
            if (parts.Length < 4)
            {
                Console.WriteLine("Недопустимая команда REM");
                return;
            }

            string field = parts[1];
            string op = parts[2];
            string value = parts[3];

            _manager.RemovePlant(field, op, value);
        }
    }
}
