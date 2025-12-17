using System;
using BusinessLogic;
using Model;
using System.Text.RegularExpressions;
using System.Linq;

namespace View
{
    public class ConsoleView
    {
    
        private readonly SpaceObjectManager manager = new SpaceObjectManager();

        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                ShowMenu();
                var choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1": AddPlanet(); break;
                    case "2": AddComet(); break;
                    case "3": AddSatellite(); break;
                    case "4": RemoveObject(); break;
                    case "5": ShowAllObjects(); break;
                    case "6": LoadFromFile(); break;
                    case "7": ShowMaxRadiusPlanet(); break;
                    case "8": exit = true; break;
                    default: Console.WriteLine("Некорректный выбор, попробуйте снова."); break;
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("=== Главное меню ===");
            Console.WriteLine("1) Добавить планету");
            Console.WriteLine("2) Добавить комету");
            Console.WriteLine("3) Добавить спутник");
            Console.WriteLine("4) Удалить объект");
            Console.WriteLine("5) Показать все объекты");
            Console.WriteLine("6) Загрузить объекты из файла");
            Console.WriteLine("7) Планета с максимальным радиусом");
            Console.WriteLine("8) Выход");
            Console.Write("Выберите пункт меню: ");
        }

        private void AddPlanet()
        {
            Console.WriteLine("Введите данные планеты: \"Название\" ГГГГ.ММ.ДД Радиус X Y");
            var line = Console.ReadLine();
            var regex = new Regex("\"([^\"]+)\"\\s+(\\d{4}\\.\\d{2}\\.\\d{2})\\s+(\\d+(?:\\.\\d+)?)\\s+([-+]?\\d*\\.?\\d+)\\s+([-+]?\\d*\\.?\\d+)");
            var match = regex.Match(line);
            if (!match.Success) { Console.WriteLine("Неверный формат данных."); return; }
            try
            {
                var planet = new Planet
                {
                    Name = match.Groups[1].Value,
                    DiscoveryDate = match.Groups[2].Value,
                    Radius = double.Parse(match.Groups[3].Value.Replace('.', ',')),
                    X = double.Parse(match.Groups[4].Value.Replace('.', ',')),
                    Y = double.Parse(match.Groups[5].Value.Replace('.', ','))
                };
                manager.AddObject(planet);
                Console.WriteLine("Планета добавлена.");
            }
            catch { Console.WriteLine("Ошибка в формате числовых данных."); }
        }

        private void AddComet()
        {
            Console.WriteLine("Введите данные кометы: \"Название\" Светимость Расстояние СпектральныйТип");
            var line = Console.ReadLine();
            var regex = new Regex("\"([^\"]+)\"\\s+(\\d+(?:\\.\\d+)?)\\s+(\\d+(?:\\.\\d+)?)\\s+(\\w+)");
            var match = regex.Match(line);
            if (!match.Success) { Console.WriteLine("Неверный формат данных."); return; }
            try
            {
                var comet = new Comet
                {
                    Name = match.Groups[1].Value,
                    Luminosity = double.Parse(match.Groups[2].Value.Replace('.', ',')),
                    Distance = double.Parse(match.Groups[3].Value.Replace('.', ',')),
                    SpectralType = match.Groups[4].Value
                };
                manager.AddObject(comet);
                Console.WriteLine("Комета добавлена.");
            }
            catch { Console.WriteLine("Ошибка в формате числовых данных."); }
        }

        private void AddSatellite()
        {
            Console.WriteLine("Введите данные спутника: \"Название\" Планета ОрбитальныйРадиус Масса");
            var line = Console.ReadLine();
            var regex = new Regex("\"([^\"]+)\"\\s+(\\w+)\\s+(\\d+(?:\\.\\d+)?)\\s+(\\d+(?:\\.\\d+)?)");
            var match = regex.Match(line);
            if (!match.Success) { Console.WriteLine("Неверный формат данных."); return; }
            try
            {
                var satellite = new Satellite
                {
                    Name = match.Groups[1].Value,
                    PlanetName = match.Groups[2].Value,
                    OrbitalRadius = double.Parse(match.Groups[3].Value.Replace('.', ',')),
                    Mass = double.Parse(match.Groups[4].Value.Replace('.', ','))
                };
                manager.AddObject(satellite);
                Console.WriteLine("Спутник добавлен.");
            }
            catch { Console.WriteLine("Ошибка в формате числовых данных."); }
        }

        private void RemoveObject()
        {
            Console.WriteLine("Введите название объекта для удаления:");
            var name = Console.ReadLine();
            if (manager.RemoveByName(name))
                Console.WriteLine("Объект удален.");
            else
                Console.WriteLine("Объект с таким названием не найден.");
        }

        private void ShowAllObjects()
        {
            var objects = manager.GetAllObjects();
            if (objects == null || !objects.Any())
            {
                Console.WriteLine("Объекты не найдены.");
                return;
            }
            foreach (var obj in objects)
            {
                switch (obj)
                {
                    case Planet p:
                        Console.WriteLine($"Планета: \"{p.Name}\" {p.DiscoveryDate} {p.Radius} {p.X} {p.Y}");
                        break;
                    case Comet c:
                        Console.WriteLine($"Комета: \"{c.Name}\" {c.Luminosity} {c.Distance} {c.SpectralType}");
                        break;
                    case Satellite s:
                        Console.WriteLine($"Спутник: \"{s.Name}\" {s.PlanetName} {s.OrbitalRadius} {s.Mass}");
                        break;
                }
            }
        }

        private void LoadFromFile()
        {
            Console.WriteLine("Введите тип объектов для загрузки (Planet, Comet, Satellite):");
            string type = Console.ReadLine()?.Trim();
            if (type != "Planet" && type != "Comet" && type != "Satellite")
            {
                Console.WriteLine("Неверный тип.");
                return;
            }
            try
            {
                manager.LoadObjectsFromFile("1.txt", type);
                Console.WriteLine("Загрузка завершена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки: {ex.Message}");
            }
        }

        private void ShowMaxRadiusPlanet()
        {
            var planet = manager.GetPlanetWithMaxRadius();
            if (planet != null)
                Console.WriteLine($"Планета с максимальным радиусом: {planet.Name} ({planet.Radius})");
            else
                Console.WriteLine("Планеты не найдены.");
        }
    }
}
