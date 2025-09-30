using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

class Planet
{
    public string Name { get; set; }
    public string DiscoveryDate { get; set; }
    public double Radius { get; set; }
    public double x { get; set; }
    public double y { get; set; }
}

class Comet
{
    public string Name { get; set; }
    public double Luminosity { get; set; }
    public double Distance { get; set; }
    public string SpectralType { get; set; }
}

class Satellite
{
    public string Name { get; set; }
    public string PlanetName { get; set; }
    public double OrbitalRadius { get; set; }
    public double Mass { get; set; }
}

class Program
{
    static List<Planet> planets = new List<Planet>();
    static List<Comet> comets = new List<Comet>();
    static List<Satellite> satellites = new List<Satellite>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nВыберите тип объекта: Planet, Comet, Satellite или Exit для выхода");
            string index = Console.ReadLine()?.Trim();

            if (index == "Exit")
                break;

            while (index != "Planet" && index != "Comet" && index != "Satellite")
            {
                Console.WriteLine("Неверный ввод. Введите Planet, Comet, Satellite или Exit:");
                index = Console.ReadLine()?.Trim();
                if (index == "Exit")
                    return;
            }

            Console.WriteLine($"Вы выбрали: {index}");

            bool menuExit = false;
            while (!menuExit)
            {
                ShowMenu();
                string choice = Console.ReadLine()?.Trim();
                menuExit = ProcessMenuChoice(choice, index);
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("\n=== Панель управления ===");
        Console.WriteLine("1) Добавить объект");
        Console.WriteLine("2) Удалить объект");
        Console.WriteLine("3) Показать объекты");
        Console.WriteLine("4) Загрузить объекты из файла");
        Console.WriteLine("5) Планета с максимальным радиусом (только для Planet)");
        Console.WriteLine("6) Поиск планет между координатами (только для Planet)");
        Console.WriteLine("7) Вернуться к выбору типа объекта");
        Console.Write("Выберите пункт меню: ");
    }

    static bool ProcessMenuChoice(string choice, string type)
    {
        switch (choice)
        {
            case "1":
                AddObject(type);
                break;
            case "2":
                RemoveObject(type);
                break;
            case "3":
                PrintObjects(type);
                break;
            case "4":
                LoadFromFile(type);
                break;
            case "5":
                ShowMaxRadiusPlanet(type);
                break;
            case "6":
                FindPlanetsBetweenCoords(type);
                break;
            case "7":
                return true;
            default:
                Console.WriteLine("Некорректный выбор, попробуйте снова.");
                break;
        }
        return false;
    }

    static void AddObject(string type)
    {
        if (type == "Planet") AddPlanet();
        else if (type == "Comet") AddComet();
        else if (type == "Satellite") AddSatellite();
    }

    static void AddPlanet()
    {
        Console.WriteLine("Введите данные планеты в формате: \"Название\" ГГГГ.ММ.ДД Радиус X Y");
        var line = Console.ReadLine();
        var regex = new Regex("\"([^\"]+)\"\\s+(\\d{4}\\.\\d{2}\\.\\d{2})\\s+(\\d+(?:\\.\\d+)?)\\s+([-+]?[0-9]*\\.?[0-9]+)\\s+([-+]?[0-9]*\\.?[0-9]+)");
        var match = regex.Match(line);
        if (match.Success)
        {
            planets.Add(new Planet
            {
                Name = match.Groups[1].Value,
                DiscoveryDate = match.Groups[2].Value,
                Radius = double.Parse(match.Groups[3].Value.Replace('.', ',')),
                x = double.Parse(match.Groups[4].Value.Replace('.', ',')),
                y = double.Parse(match.Groups[5].Value.Replace('.', ','))
            });
            Console.WriteLine("Планета добавлена.");
        }
        else
        {
            Console.WriteLine("Неверный формат данных.");
        }
    }

    static void AddComet()
    {
        Console.WriteLine("Введите данные кометы в формате: \"Название\" Светимость Расстояние СпектральныйТип");
        var line = Console.ReadLine();
        var regex = new Regex("\"([^\"]+)\"\\s+(\\d+(?:\\.\\d+)?)\\s+(\\d+(?:\\.\\d+)?)\\s+(\\w+)");
        var match = regex.Match(line);
        if (match.Success)
        {
            comets.Add(new Comet
            {
                Name = match.Groups[1].Value,
                Luminosity = double.Parse(match.Groups[2].Value.Replace('.', ',')),
                Distance = double.Parse(match.Groups[3].Value.Replace('.', ',')),
                SpectralType = match.Groups[4].Value
            });
            Console.WriteLine("Комета добавлена.");
        }
        else
        {
            Console.WriteLine("Неверный формат данных.");
        }
    }

    static void AddSatellite()
    {
        Console.WriteLine("Введите данные спутника в формате: \"Название\" Планета ОрбитальныйРадиус Масса");
        var line = Console.ReadLine();
        var regex = new Regex("\"([^\"]+)\"\\s+(\\w+)\\s+(\\d+(?:\\.\\d+)?)\\s+(\\d+(?:\\.\\d+)?)");
        var match = regex.Match(line);
        if (match.Success)
        {
            satellites.Add(new Satellite
            {
                Name = match.Groups[1].Value,
                PlanetName = match.Groups[2].Value,
                OrbitalRadius = double.Parse(match.Groups[3].Value.Replace('.', ',')),
                Mass = double.Parse(match.Groups[4].Value.Replace('.', ','))
            });
            Console.WriteLine("Спутник добавлен.");
        }
        else
        {
            Console.WriteLine("Неверный формат данных.");
        }
    }

    static void RemoveObject(string type)
    {
        if (type == "Planet") RemovePlanet();
        else if (type == "Comet") RemoveComet();
        else if (type == "Satellite") RemoveSatellite();
    }

    static void RemovePlanet()
    {
        Console.WriteLine("Введите название планеты для удаления:");
        var name = Console.ReadLine();
        var planet = planets.FirstOrDefault(p => p.Name == name);
        if (planet != null)
        {
            planets.Remove(planet);
            Console.WriteLine("Планета удалена.");
        }
        else Console.WriteLine("Планета не найдена.");
    }

    static void RemoveComet()
    {
        Console.WriteLine("Введите название кометы для удаления:");
        var name = Console.ReadLine();
        var comet = comets.FirstOrDefault(c => c.Name == name);
        if (comet != null)
        {
            comets.Remove(comet);
            Console.WriteLine("Комета удалена.");
        }
        else Console.WriteLine("Комета не найдена.");
    }

    static void RemoveSatellite()
    {
        Console.WriteLine("Введите название спутника для удаления:");
        var name = Console.ReadLine();
        var satellite = satellites.FirstOrDefault(s => s.Name == name);
        if (satellite != null)
        {
            satellites.Remove(satellite);
            Console.WriteLine("Спутник удален.");
        }
        else Console.WriteLine("Спутник не найден.");
    }

    static void PrintObjects(string type)
    {
        if (type == "Planet")
        {
            if (!planets.Any()) Console.WriteLine("Планеты не найдены.");
            else foreach (var p in planets) Console.WriteLine($"\"{p.Name}\" {p.DiscoveryDate} {p.Radius} {p.x} {p.y}");
        }
        else if (type == "Comet")
        {
            if (!comets.Any()) Console.WriteLine("Кометы не найдены.");
            else foreach (var c in comets) Console.WriteLine($"\"{c.Name}\" {c.Luminosity} {c.Distance} {c.SpectralType}");
        }
        else if (type == "Satellite")
        {
            if (!satellites.Any()) Console.WriteLine("Спутники не найдены.");
            else foreach (var s in satellites) Console.WriteLine($"\"{s.Name}\" {s.PlanetName} {s.OrbitalRadius} {s.Mass}");
        }
    }

    static void LoadFromFile(string type)
    {
        if (!File.Exists("1.txt"))
        {
            Console.WriteLine("Файл 1.txt не найден.");
            return;
        }
        var lines = File.ReadAllLines("1.txt");

        var regexForPlanet = new Regex("\"([^\"]+)\"\\s+(\\d{4}\\.\\d{2}\\.\\d{2})\\s+(\\d+(?:\\.\\d+)?)(?:\\s+([-+]?[0-9]*\\.?[0-9]+)\\s+([-+]?[0-9]*\\.?[0-9]+))?");
        var regexForComet = new Regex("\"([^\"]+)\"\\s+(\\d+(?:\\.\\d+)?)\\s+(\\d+(?:\\.\\d+)?)\\s+(\\w+)");
        var regexForSatellite = new Regex("\"([^\"]+)\"\\s+(\\w+)\\s+(\\d+(?:\\.\\d+)?)\\s+(\\d+(?:\\.\\d+)?)");

        int countAdded = 0;

        foreach (var line in lines)
        {
            switch (type)
            {
                case "Planet":
                    var pm = regexForPlanet.Match(line);
                    if (pm.Success)
                    {
                        planets.Add(new Planet
                        {
                            Name = pm.Groups[1].Value,
                            DiscoveryDate = pm.Groups[2].Value,
                            Radius = double.Parse(pm.Groups[3].Value.Replace('.', ',')),
                            x = double.Parse(pm.Groups[4].Value.Replace('.', ',')),
                            y = double.Parse(pm.Groups[5].Value.Replace('.', ','))
                        });
                        countAdded++;
                    }
                    break;
                case "Comet":
                    var cm = regexForComet.Match(line);
                    if (cm.Success)
                    {
                        comets.Add(new Comet
                        {
                            Name = cm.Groups[1].Value,
                            Luminosity = double.Parse(cm.Groups[2].Value.Replace('.', ',')),
                            Distance = double.Parse(cm.Groups[3].Value.Replace('.', ',')),
                            SpectralType = cm.Groups[4].Value
                        });
                        countAdded++;
                    }
                    break;
                case "Satellite":
                    var sm = regexForSatellite.Match(line);
                    if (sm.Success)
                    {
                        satellites.Add(new Satellite
                        {
                            Name = sm.Groups[1].Value,
                            PlanetName = sm.Groups[2].Value,
                            OrbitalRadius = double.Parse(sm.Groups[3].Value.Replace('.', ',')),
                            Mass = double.Parse(sm.Groups[4].Value.Replace('.', ','))
                        });
                        countAdded++;
                    }
                    break;
            }
        }
        Console.WriteLine($"{countAdded} объектов типа {type} загружено из файла.");
    }

    static void ShowMaxRadiusPlanet(string type)
    {
        if (type != "Planet")
        {
            Console.WriteLine("Функция доступна только для планет.");
            return;
        }
        if (planets.Any())
        {
            var maxPlanet = planets.OrderByDescending(p => p.Radius).First();
            Console.WriteLine($"Планета с наибольшим радиусом: \"{maxPlanet.Name}\" Дата: {maxPlanet.DiscoveryDate} Радиус: {maxPlanet.Radius}, X: {maxPlanet.x}, Y: {maxPlanet.y}");
        }
        else
        {
            Console.WriteLine("Данные о планетах не найдены.");
        }
    }

    static void FindPlanetsBetweenCoords(string type)
    {
        if (type != "Planet")
        {
            Console.WriteLine("Функция доступна только для планет.");
            return;
        }
        Console.WriteLine("Введите координаты для поиска планет в формате: Space_x Space_x1 Space_y Space_y1");
        string input = Console.ReadLine();
        var parts = input.Split(' ');
        if (parts.Length != 4 || !double.TryParse(parts[0], out double Space_x) || !double.TryParse(parts[1], out double Space_x1)
            || !double.TryParse(parts[2], out double Space_y) || !double.TryParse(parts[3], out double Space_y1))
        {
            Console.WriteLine("Неверный формат координат.");
            return;
        }

        bool found = false;
        foreach (var planet in planets)
        {
            if (Space_x < planet.x && Space_x1 > planet.x && Space_y > planet.y && Space_y1 < planet.y)
            {
                Console.WriteLine($"Планета {planet.Name} находится внутри координат.");
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("Планеты в указанных координатах не найдены.");
        }
    }
}
