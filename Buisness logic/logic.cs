using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessLogic
{
    public class SpaceObjectManager
    {
        private List<SpaceObject> spaceObjects = new List<SpaceObject>();

        public void AddObject(SpaceObject obj) => this.spaceObjects.Add(obj);

        public bool RemoveByName(string name)
        {
            var obj = this.spaceObjects.FirstOrDefault(o => o.Name == name);
            if (obj != null)
            {
                this.spaceObjects.Remove(obj);
                return true;
            }
            return false;
        }

        public IEnumerable<SpaceObject> GetAllObjects() => this.spaceObjects;

        public Planet GetPlanetWithMaxRadius()
        {
            return this.spaceObjects.OfType<Planet>().OrderByDescending(p => p.Radius).FirstOrDefault();
        }

       
        public void LoadObjectsFromFile(string path, string type)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Файл {path} не найден");

            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                switch (type)
                {
                    case "Planet":
                        if (TryParsePlanet(line, out Planet planet)) this.AddObject(planet); break;
                    case "Comet":
                        if (TryParseComet(line, out Comet comet)) this.AddObject(comet); break;
                    case "Satellite":
                        if (TryParseSatellite(line, out Satellite satellite)) this.AddObject(satellite); break;
                }
            }
        }

        private bool TryParsePlanet(string line, out Planet planet)
        {
            planet = null;
            var regex = new Regex("\"([^\"]+)\"\\s+(\\d{4}\\.\\d{2}\\.\\d{2})\\s+(\\d+(?:\\.\\d+)?)\\s+([-+]?\\d*\\.?\\d+)\\s+([-+]?\\d*\\.?\\d+)");
            var match = regex.Match(line);
            if (!match.Success) return false;
            try
            {
                planet = new Planet
                {
                    Name = match.Groups[1].Value,
                    DiscoveryDate = match.Groups[2].Value,
                    Radius = double.Parse(match.Groups[3].Value.Replace('.', ',')),
                    X = double.Parse(match.Groups[4].Value.Replace('.', ',')),
                    Y = double.Parse(match.Groups[5].Value.Replace('.', ','))
                };
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TryParseComet(string line, out Comet comet)
        {
            comet = null;
            var regex = new Regex("\"([^\"]+)\"\\s+(\\d+(?:\\.\\d+)?)\\s+(\\d+(?:\\.\\d+)?)\\s+(\\w+)");
            var match = regex.Match(line);
            if (!match.Success) return false;
            try
            {
                comet = new Comet
                {
                    Name = match.Groups[1].Value,
                    Luminosity = double.Parse(match.Groups[2].Value.Replace('.', ',')),
                    Distance = double.Parse(match.Groups[3].Value.Replace('.', ',')),
                    SpectralType = match.Groups[4].Value
                };
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TryParseSatellite(string line, out Satellite satellite)
        {
            satellite = null;
            var regex = new Regex("\"([^\"]+)\"\\s+(\\w+)\\s+(\\d+(?:\\.\\d+)?)\\s+(\\d+(?:\\.\\d+)?)");
            var match = regex.Match(line);
            if (!match.Success) return false;
            try
            {
                satellite = new Satellite
                {
                    Name = match.Groups[1].Value,
                    PlanetName = match.Groups[2].Value,
                    OrbitalRadius = double.Parse(match.Groups[3].Value.Replace('.', ',')),
                    Mass = double.Parse(match.Groups[4].Value.Replace('.', ','))
                };
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
