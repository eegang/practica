# Инструкция по установке и запуску тестов

## Способ 1: Установка через NuGet (рекомендуется)

### Шаг 1: Установите NuGet пакеты

**Вариант А: Через Package Manager Console**
1. Откройте **Package Manager Console**: Tools → NuGet Package Manager → Package Manager Console
2. Выполните команды:
```powershell
Install-Package MSTest.TestFramework -Version 3.1.1 -ProjectName UnitTests
Install-Package MSTest.TestAdapter -Version 3.1.1 -ProjectName UnitTests
```

**Вариант Б: Через UI Visual Studio**
1. Правый клик на проекте **UnitTests** → **Manage NuGet Packages**
2. Перейдите на вкладку **Browse**
3. Найдите и установите:
   - `MSTest.TestFramework` (версия 3.1.1)
   - `MSTest.TestAdapter` (версия 3.1.1)

### Шаг 2: Добавьте ссылки на сборки

После установки пакетов Visual Studio должен автоматически добавить ссылки. Если нет:
1. Правый клик на проекте **UnitTests** → **Add** → **Reference**
2. Нажмите **Browse** и найдите файлы в папке `packages`:
   - `packages\MSTest.TestFramework.3.1.1\lib\net45\Microsoft.VisualStudio.TestPlatform.TestFramework.dll`
   - `packages\MSTest.TestAdapter.3.1.1\lib\net45\Microsoft.VisualStudio.TestPlatform.TestFramework.Extensions.dll`

## Способ 2: Добавление ссылок вручную (если NuGet не работает)

1. Правый клик на проекте **UnitTests** → **Add** → **Reference**
2. Нажмите **Browse**
3. Найдите файлы MSTest в папке установки Visual Studio:
   - Обычно находятся в: `C:\Program Files\Microsoft Visual Studio\2022\[Edition]\Common7\IDE\PublicAssemblies\`
   - Или: `C:\Program Files (x86)\Microsoft Visual Studio\2019\[Edition]\Common7\IDE\PublicAssemblies\`
4. Добавьте:
   - `Microsoft.VisualStudio.TestPlatform.TestFramework.dll`
   - `Microsoft.VisualStudio.TestPlatform.TestFramework.Extensions.dll`

## Запуск тестов

1. **Соберите решение**: Build → Build Solution (Ctrl+Shift+B)
2. **Откройте Test Explorer**: Test → Test Explorer (или Ctrl+E, T)
3. **Запустите тесты**: в Test Explorer нажмите **"Run All"**

## Примечание

Ошибка CS5001 (отсутствие метода Main) - это нормально для тестового проекта, так как это библиотека, а не исполняемое приложение.

