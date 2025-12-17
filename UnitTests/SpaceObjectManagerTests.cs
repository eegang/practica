using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;
using Model;

namespace UnitTests
{
    [TestClass]
    public class SpaceObjectManagerTests
    {
        private string testFilePath;

        [TestInitialize]
        public void Setup()
        {
            testFilePath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.txt");
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(testFilePath))
            {
                File.Delete(testFilePath);
            }
        }

        [TestMethod]
        public void LoadObjectsFromFile_LoadsObjects_FromFile()
        {
            // Arrange
            var manager = new SpaceObjectManager();
            var testContent = "\"Меркурий\" 1631.11.07 2439.7 4 8\n\"Венера\" 1761.06.06 6051.8 45 7";
            File.WriteAllText(testFilePath, testContent);

            // Act
            manager.LoadObjectsFromFile(testFilePath, "Planet");

            // Assert
            var objects = manager.GetAllObjects();
            var count = 0;
            foreach (var obj in objects)
            {
                if (obj is Planet)
                    count++;
            }
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void RemoveByName_RemovesObject()
        {
            // Arrange
            var manager = new SpaceObjectManager();
            var planet = new Planet { Name = "Тестовая планета" };
            manager.AddObject(planet);

            // Act
            var result = manager.RemoveByName("Тестовая планета");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetPlanetWithMaxRadius_ReturnsPlanet_WithMaximumRadius()
        {
            // Arrange
            var manager = new SpaceObjectManager();
            var planet1 = new Planet { Name = "Маленькая", Radius = 100.0 };
            var planet2 = new Planet { Name = "Большая", Radius = 1000.0 };
            manager.AddObject(planet1);
            manager.AddObject(planet2);

            // Act
            var result = manager.GetPlanetWithMaxRadius();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Большая", result.Name);
        }
    }
}

