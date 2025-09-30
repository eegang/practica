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

                switch (choice)
                {
                    case "1":
                        AddObject(index);
                        break;
                    case "2":
                        RemoveObject(index);
                        break;
                    case "3":
                        PrintObjects(index);
                        break;
                    case "4":
                        LoadFromFile(index);
                        break;
                    case "5":
                        if (index == "Planet")
                            GetMax(planets);
                        else
                            Console.WriteLine("Функция доступна только для планет.");
                        break;
                    case "6":
                        if (index == "Planet")
                            FindPlanetsBetweenCoords();
                        else
                            Console.WriteLine("Функция доступна только для планет.");
                        break;
                    case "7":
                        menuExit = true;
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор, попробуйте снова.");
                        break;
                }
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("\nМеню");
        Console.WriteLine("1) Добавить объект");
        Console.WriteLine("2) Удалить объект");
        Console.WriteLine("3) Показать объекты");
        Console.WriteLine("4) Загрузить объекты из файла");
        Console.WriteLine("5) Планета с максимальным радиусом (только для Planet)");
        Console.WriteLine("6) Поиск планет между координатами (только для Planet)");
        Console.WriteLine("7) Вернуться к выбору типа объекта");
        Console.Write("Выберите пункт меню: ");
    }

    static void AddObject(string type)
    {
        switch (type)
        {
            case "Planet":
                Console.WriteLine("Введите данные планеты в формате: \"Название\" ГГГГ.ММ.ДД Радиус X Y");
                var planetLine = Console.ReadLine();
                var planetRegex = new Regex("\"([^\"]+)\"\\s+(\\d{4}\\.\\d{2}\\.\\d{2})\\s+(\\d+(?:\\.\\d+)?)\\s+([-+]?[0-9]*\\.?[0-9]+)\\s+([-+]?[0-9]*\\.?[0-9]+)");
                var pm = planetRegex.Match(planetLine);
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
                    Console.WriteLine("Планета добавлена.");
                }
                else
                {
                    Console.WriteLine("Неверный формат данных.");
                }
                break;
            case "Comet":
                Console.WriteLine("Введите данные кометы в формате: \"Название\" Светимость Расстояние СпектральныйТип");
                var cometLine = Console.ReadLine();
                var cometRegex = new Regex("\"([^\"]+)\"\\s+(\\d+(?:\\.\\d+)?)\\s+(\\d+(?:\\.\\d+)?)\\s+(\\w+)");
                var cm = cometRegex.Match(cometLine);
                if (cm.Success)
                {
                    comets.Add(new Comet
                    {
                        Name = cm.Groups[1].Value,
                        Luminosity = double.Parse(cm.Groups[2].Value.Replace('.', ',')),
                        Distance = double.Parse(cm.Groups[3].Value.Replace('.', ',')),
                        SpectralType = cm.Groups[4].Value
                    });
                    Console.WriteLine("Комета добавлена.");
                }
                else
                {
                    Console.WriteLine("Неверный формат данных.");
                }
                break;
            case "Satellite":
                Console.WriteLine("Введите данные спутника в формате: \"Название\" Планета ОрбитальныйРадиус Масса");
                var satLine = Console.ReadLine();
                var satRegex = new Regex("\"([^\"]+)\"\\s+(\\w+)\\s+(\\d+(?:\\.\\d+)?)\\s+(\\d+(?:\\.\\d+)?)");
                var sm = satRegex.Match(satLine);
                if (sm.Success)
                {
                    satellites.Add(new Satellite
                    {
                        Name = sm.Groups[1].Value,
                        PlanetName = sm.Groups[2].Value,
                        OrbitalRadius = double.Parse(sm.Groups[3].Value.Replace('.', ',')),
                        Mass = double.Parse(sm.Groups[4].Value.Replace('.', ','))
                    });
                    Console.WriteLine("Спутник добавлен.");
                }
                else
                {
                    Console.WriteLine("Неверный формат данных.");
                }
                break;
        }
    }

    static void RemoveObject(string type)
    {
        switch (type)
        {
            case "Planet":
                Console.WriteLine("Введите название планеты для удаления:");
                var pname = Console.ReadLine();
                var p = planets.FirstOrDefault(pl => pl.Name == pname);
                if (p != null)
                {
                    planets.Remove(p);
                    Console.WriteLine("Планета удалена.");
                }
                else
                {
                    Console.WriteLine("Планета не найдена.");
                }
                break;
            case "Comet":
                Console.WriteLine("Введите название кометы для удаления:");
                var cname = Console.ReadLine();
                var c = comets.FirstOrDefault(co => co.Name == cname);
                if (c != null)
                {
                    comets.Remove(c);
                    Console.WriteLine("Комета удалена.");
                }
                else
                {
                    Console.WriteLine("Комета не найдена.");
                }
                break;
            case "Satellite":
                Console.WriteLine("Введите название спутника для удаления:");
                var sname = Console.ReadLine();
                var s = satellites.FirstOrDefault(sa => sa.Name == sname);
                if (s != null)
                {
                    satellites.Remove(s);
                    Console.WriteLine("Спутник удален.");
                }
                else
                {
                    Console.WriteLine("Спутник не найден.");
                }
                break;
        }
    }

    static void PrintObjects(string type)
    {
        switch (type)
        {
            case "Planet":
                if (planets.Count == 0)
                    Console.WriteLine("Планеты не найдены.");
                else
                    foreach (var p in planets)
                        Console.WriteLine($"\"{p.Name}\" {p.DiscoveryDate} {p.Radius} {p.x} {p.y}");
                break;

            case "Comet":
                if (comets.Count == 0)
                    Console.WriteLine("Кометы не найдены.");
                else
                    foreach (var c in comets)
                        Console.WriteLine($"\"{c.Name}\" {c.Luminosity} {c.Distance} {c.SpectralType}");
                break;

            case "Satellite":
                if (satellites.Count == 0)
                    Console.WriteLine("Спутники не найдены.");
                else
                    foreach (var s in satellites)
                        Console.WriteLine($"\"{s.Name}\" {s.PlanetName} {s.OrbitalRadius} {s.Mass}");
                break;
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

    static void GetMax(List<Planet> planets)
    {
        if (planets.Count > 0)
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
