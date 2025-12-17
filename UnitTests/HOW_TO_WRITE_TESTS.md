# Как писать Unit тесты

## Что такое Unit тесты?

**Unit тесты** (модульные тесты) - это автоматические тесты, которые проверяют работу отдельных функций (юнитов) кода. Они помогают:
- Убедиться, что код работает правильно
- Найти ошибки до того, как код попадет в продакшн
- Упростить рефакторинг (изменение кода без страха что-то сломать)
- Документировать, как должен работать код

---

## Структура Unit теста

Каждый тест состоит из трех частей (паттерн **AAA**):

### 1. **Arrange** (Подготовка)
Подготовка данных и объектов для теста

### 2. **Act** (Действие)
Вызов тестируемой функции

### 3. **Assert** (Проверка)
Проверка результата

---

## Пример из вашего проекта

```csharp
[TestMethod]
public void RemoveByName_RemovesObject()
{
    // Arrange - Подготовка
    var manager = new SpaceObjectManager();
    var planet = new Planet { Name = "Тестовая планета" };
    manager.AddObject(planet);

    // Act - Действие
    var result = manager.RemoveByName("Тестовая планета");

    // Assert - Проверка
    Assert.IsTrue(result);
}
```

**Разбор:**
- **Arrange**: Создаем менеджер, добавляем планету
- **Act**: Вызываем метод `RemoveByName`
- **Assert**: Проверяем, что метод вернул `true`

---

## Атрибуты MSTest

### `[TestClass]`
Помечает класс как тестовый класс. Все тесты должны быть внутри такого класса.

```csharp
[TestClass]
public class SpaceObjectManagerTests
{
    // тесты здесь
}
```

### `[TestMethod]`
Помечает метод как тестовый метод. Каждый тест - это отдельный метод.

```csharp
[TestMethod]
public void MyTest()
{
    // код теста
}
```

### `[TestInitialize]`
Метод, который выполняется **перед каждым** тестом. Используется для подготовки данных.

```csharp
[TestInitialize]
public void Setup()
{
    // Подготовка перед каждым тестом
    testFilePath = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.txt");
}
```

### `[TestCleanup]`
Метод, который выполняется **после каждого** теста. Используется для очистки (удаление временных файлов, освобождение ресурсов).

```csharp
[TestCleanup]
public void Cleanup()
{
    // Очистка после каждого теста
    if (File.Exists(testFilePath))
    {
        File.Delete(testFilePath);
    }
}
```

---

## Методы Assert (Проверки)

### `Assert.AreEqual(expected, actual)`
Проверяет, что два значения равны.

```csharp
Assert.AreEqual(2, count); // Проверяет, что count равен 2
Assert.AreEqual("Большая", result.Name); // Проверяет, что имя равно "Большая"
```

### `Assert.IsTrue(condition)`
Проверяет, что условие истинно.

```csharp
Assert.IsTrue(result); // Проверяет, что result == true
```

### `Assert.IsFalse(condition)`
Проверяет, что условие ложно.

```csharp
Assert.IsFalse(result); // Проверяет, что result == false
```

### `Assert.IsNull(object)`
Проверяет, что объект равен `null`.

```csharp
Assert.IsNull(result); // Проверяет, что result == null
```

### `Assert.IsNotNull(object)`
Проверяет, что объект не равен `null`.

```csharp
Assert.IsNotNull(result); // Проверяет, что result != null
```

### `Assert.ThrowsException<T>(action)`
Проверяет, что метод выбрасывает исключение определенного типа.

```csharp
Assert.ThrowsException<FileNotFoundException>(() =>
{
    manager.LoadObjectsFromFile("несуществующий_файл.txt", "Planet");
});
```

---

## Именование тестов

Хорошее имя теста должно описывать:
- **Что** тестируется
- **При каких условиях**
- **Какой результат ожидается**

**Формат:** `MethodName_Scenario_ExpectedResult`

**Примеры:**
```csharp
// ✅ Хорошо
RemoveByName_RemovesObject_WhenExists()
GetPlanetWithMaxRadius_ReturnsNull_WhenNoPlanets()
LoadObjectsFromFile_ThrowsException_WhenFileNotFound()

// ❌ Плохо
Test1()
RemoveTest()
TestRemove()
```

---

## Примеры тестов из вашего проекта

### Пример 1: Тест загрузки из файла

```csharp
[TestMethod]
public void LoadObjectsFromFile_LoadsObjects_FromFile()
{
    // Arrange - Подготовка
    var manager = new SpaceObjectManager();
    var testContent = "\"Меркурий\" 1631.11.07 2439.7 4 8\n\"Венера\" 1761.06.06 6051.8 45 7";
    File.WriteAllText(testFilePath, testContent);

    // Act - Действие
    manager.LoadObjectsFromFile(testFilePath, "Planet");

    // Assert - Проверка
    var objects = manager.GetAllObjects();
    var count = 0;
    foreach (var obj in objects)
    {
        if (obj is Planet)
            count++;
    }
    Assert.AreEqual(2, count); // Должно быть загружено 2 планеты
}
```

### Пример 2: Тест поиска максимума

```csharp
[TestMethod]
public void GetPlanetWithMaxRadius_ReturnsPlanet_WithMaximumRadius()
{
    // Arrange - Подготовка
    var manager = new SpaceObjectManager();
    var planet1 = new Planet { Name = "Маленькая", Radius = 100.0 };
    var planet2 = new Planet { Name = "Большая", Radius = 1000.0 };
    manager.AddObject(planet1);
    manager.AddObject(planet2);

    // Act - Действие
    var result = manager.GetPlanetWithMaxRadius();

    // Assert - Проверка
    Assert.IsNotNull(result); // Результат не должен быть null
    Assert.AreEqual("Большая", result.Name); // Должна вернуться планета с большим радиусом
}
```

---

## Лучшие практики

### ✅ Делайте тесты независимыми
Каждый тест должен работать самостоятельно, не зависеть от других тестов.

### ✅ Используйте понятные имена
Имя теста должно ясно описывать, что он проверяет.

### ✅ Тестируйте один сценарий за раз
Один тест = одна проверка.

### ✅ Используйте Setup и Cleanup
Для подготовки и очистки данных используйте `[TestInitialize]` и `[TestCleanup]`.

### ✅ Тестируйте граничные случаи
- Пустые коллекции
- Null значения
- Несуществующие файлы
- Некорректные данные

### ✅ Проверяйте исключения
Если метод должен выбрасывать исключение, проверьте это.

---

## Шаблон нового теста

```csharp
[TestMethod]
public void MethodName_Scenario_ExpectedResult()
{
    // Arrange - Подготовка данных
    var объект = new Класс();
    // ... подготовка ...

    // Act - Вызов тестируемого метода
    var результат = объект.Метод(параметры);

    // Assert - Проверка результата
    Assert.AreEqual(ожидаемоеЗначение, результат);
}
```

---

## Что тестировать?

### ✅ Тестируйте:
- Бизнес-логику
- Сложные вычисления
- Условия и ветвления (if/else)
- Обработку ошибок
- Граничные случаи

### ❌ Не тестируйте:
- Простые геттеры/сеттеры
- Фреймворки (они уже протестированы)
- Внешние зависимости (базы данных, API) - используйте моки

---

## Запуск тестов

1. **В Visual Studio:**
   - Откройте **Обозреватель тестов**: Тест → Обозреватель тестов
   - Нажмите **"Выполнить все"** (Run All)

2. **Горячие клавиши:**
   - `Ctrl+R, A` - Запустить все тесты
   - `Ctrl+R, T` - Запустить текущий тест

3. **Результаты:**
   - ✅ Зеленая галочка - тест прошел
   - ❌ Красный крестик - тест упал (показывает детали ошибки)

---

## Полезные советы

1. **Пишите тесты параллельно с кодом** - это помогает лучше понять требования
2. **Тесты должны быть быстрыми** - запускайте их часто
3. **Тесты должны быть стабильными** - один и тот же тест должен давать один и тот же результат
4. **Используйте комментарии** - особенно в сложных тестах
5. **Рефакторьте тесты** - если тест стал сложным, упростите его

---

## Дополнительные ресурсы

- [Документация MSTest](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest)
- [Паттерн AAA](https://en.wikipedia.org/wiki/Arrange-Act-Assert)
- [Test-Driven Development (TDD)](https://en.wikipedia.org/wiki/Test-driven_development)



