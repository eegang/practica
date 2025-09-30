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
        bool exit = false;
        while (!exit)
        {
            ShowMainMenu();
            string choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    {
                        AddPlanet();
                        break;
                    }
                case "2":
                    {
                        AddComet();
                        break;
                    }
                case "3":
                    {
                        AddSatellite();
                        break;
                    }
                case "4":
                    {
                        RemovePlanet();
                        break;
                    }
                case "5":
                    {
                        RemoveComet();
                        break;
                    }
                case "6":
                    {
                        RemoveSatellite();
                        break;
                    }
                case "7":
                    {
                        PrintAllObjects();
                        break;
                    }
                case "8":
                    {
                        LoadFromFileMenu();
                        break;
                    }
                case "9":
                    {
                        ShowMaxRadiusPlanet();
                        break;
                    }
                case "10":
                    {
                        FindPlanetsBetweenCoords();
                        break;
                    }
                case "11":
                    {
                        exit = true;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Некорректный выбор, попробуйте снова.");
                        break;
                    }
            }
        }
    }

    static void ShowMainMenu()
    {
        Console.WriteLine("\n=== Главное меню ===");
        Console.WriteLine("1) Добавить планету");
        Console.WriteLine("2) Добавить комету");
        Console.WriteLine("3) Добавить спутник");
        Console.WriteLine("4) Удалить планету");
        Console.WriteLine("5) Удалить комету");
        Console.WriteLine("6) Удалить спутник");
        Console.WriteLine("7) Показать все объекты");
        Console.WriteLine("8) Загрузить объекты из файла");
        Console.WriteLine("9) Планета с максимальным радиусом");
        Console.WriteLine("10) Поиск планет между координатами");
        Console.WriteLine("11) Выход");
        Console.Write("Выберите пункт меню: ");
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
        else
        {
            Console.WriteLine("Планета не найдена.");
        }
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
        else
        {
            Console.WriteLine("Комета не найдена.");
        }
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
        else
        {
            Console.WriteLine("Спутник не найден.");
        }
    }

    static void PrintAllObjects()
    {
        Console.WriteLine("\nВсе планеты:");
        if (planets.Count == 0)
        {
            Console.WriteLine("Планеты не найдены.");
        }
        else
        {
            foreach (var p in planets)
            {
                Console.WriteLine($"\"{p.Name}\" {p.DiscoveryDate} {p.Radius} {p.x} {p.y}");
            }
        }

        Console.WriteLine("\nВсе кометы:");
        if (comets.Count == 0)
        {
            Console.WriteLine("Кометы не найдены.");
        }
        else
        {
            foreach (var c in comets)
            {
                Console.WriteLine($"\"{c.Name}\" {c.Luminosity} {c.Distance} {c.SpectralType}");
            }
        }

        Console.WriteLine("\nВсе спутники:");
        if (satellites.Count == 0)
        {
            Console.WriteLine("Спутники не найдены.");
        }
        else
        {
            foreach (var s in satellites)
            {
                Console.WriteLine($"\"{s.Name}\" {s.PlanetName} {s.OrbitalRadius} {s.Mass}");
            }
        }
    }

    static void LoadFromFileMenu()
    {
        Console.WriteLine("Выберите тип объектов для загрузки из файла: Planet, Comet, Satellite");
        string type = Console.ReadLine()?.Trim();
        if (type == "Planet" || type == "Comet" || type == "Satellite")
        {
            LoadFromFile(type);
        }
        else
        {
            Console.WriteLine("Неверный тип объекта.");
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

        int countAdded = 0;

        switch (type)
        {
            case "Planet":
                countAdded = LoadPlanetsFromLines(lines);
                break;
            case "Comet":
                countAdded = LoadCometsFromLines(lines);
                break;
            case "Satellite":
                countAdded = LoadSatellitesFromLines(lines);
                break;
        }

        Console.WriteLine($"{countAdded} объектов типа {type} загружено из файла.");
    }

    static int LoadPlanetsFromLines(string[] lines)
    {
        var regex = new Regex("\"([^\"]+)\"\\s+(\\d{4}\\.\\d{2}\\.\\d{2})\\s+(\\d+(?:\\.\\d+)?)(?:\\s+([-+]?[0-9]*\\.?[0-9]+)\\s+([-+]?[0-9]*\\.?[0-9]+))?");
        int count = 0;
        foreach (var line in lines)
        {
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
                count++;
            }
        }
        return count;
    }

    static int LoadCometsFromLines(string[] lines)
    {
        var regex = new Regex("\"([^\"]+)\"\\s+(\\d+(?:\\.\\d+)?)\\s+(\\d+(?:\\.\\d+)?)\\s+(\\w+)");
        int count = 0;
        foreach (var line in lines)
        {
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
                count++;
            }
        }
        return count;
    }

    static int LoadSatellitesFromLines(string[] lines)
    {
        var regex = new Regex("\"([^\"]+)\"\\s+(\\w+)\\s+(\\d+(?:\\.\\d+)?)\\s+(\\d+(?:\\.\\d+)?)");
        int count = 0;
        foreach (var line in lines)
        {
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
                count++;
            }
        }
        return count;
    }

    static void ShowMaxRadiusPlanet()
    {
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

    static void FindPlanetsBetweenCoords()
    {
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
