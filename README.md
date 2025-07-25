# HWID Checker

**HWID Checker** — это консольная утилита для Windows, которая собирает уникальный идентификатор оборудования (HWID) вашей системы на основе данных о процессоре, материнской плате и жёстком диске. Программа также выводит подробную информацию о системе и позволяет сохранить HWID в файл.

## Возможности

- Получение уникального HWID на основе аппаратных компонентов.
- Отображение информации о процессоре, материнской плате, жёстких дисках и ОС.
- Сохранение HWID в файл `hwid.txt` по желанию пользователя.
- Удобный консольный интерфейс.

## Требования

- **ОС:** Windows 10/11 (x64)
- **.NET:** .NET 9.0 Runtime (или SDK для сборки)
- **Права:** Запуск от имени пользователя с правами на доступ к WMI

## Скачивание и запуск

### Готовый релиз

1. Перейдите в папку `bin/Release/net9.0/win-x64/`.
2. Скачайте архив `net 9.0.7z`.
3. Распакуйте архив в удобное место.
4. Запустите `Hwid.exe` двойным кликом или через командную строку.

**Примечание:** Для работы программы в папке должны находиться файлы:
- `Hwid.exe`
- `SQLite.Interop.dll`

### Сборка из исходников

1. Установите [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0).
2. Клонируйте репозиторий:
   ```sh
   git clone <адрес_репозитория>
   cd HwidChecker1
   ```
3. Соберите проект:
   ```sh
   dotnet publish -c Release -r win-x64 --self-contained true
   ```
   Готовые файлы появятся в `bin/Release/net9.0/win-x64/publish/`.

## Использование

1. Запустите программу.
2. Ознакомьтесь с информацией о системе и вашим HWID.
3. При желании сохраните HWID в файл, нажав `y` при соответствующем запросе.
4. Используйте HWID для активации ПО или других целей.

## Пример вывода

```
╔═══════════════════════════════════════════╗
║                                           ║
║             Хвид Чекер v11111             ║
║                                           ║
╚═══════════════════════════════════════════╝
Этот инструмент собирает и отображает уникальный HWID вашей системы
Используйте этот HWID для запроса активации программного обеспечения

===== ИНФОРМАЦИЯ О СИСТЕМЕ =====
ЦП: Intel(R) Core(TM) i7-9700K CPU @ 3.60GHz
  ID процессора: BFEBFBFF000906EA
Материнская плата: ASUSTeK COMPUTER INC. PRIME Z390-A
  Серийный номер: 1234567890
Жесткие диски:
  Samsung SSD 970 EVO 1TB
    Серийный номер: S4EVNF0M123456A
    Размер: 931.51 GB
Операционная система: Microsoft Windows 10 Pro 10.0.19045

====== ВАШ HWID ======
1bee09adfb8e7d41b90a43250ad4edc45bf1d52e5af79640e1520f3231d175e1
=====================
Сохранить HWID в файл? (y/n):
```

## Лицензия

MIT Licence