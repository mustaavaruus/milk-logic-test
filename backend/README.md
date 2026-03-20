# [Название вашего API]

Разработка сервиса мониторинга данных с эмуляцией реального времени

## 🚀 Быстрый запуск
1. Установите .NET SDK 8.0/9.0.
2. Установить PostrgeSQL
3. Выполнить SQL-скрипт (ю/database/init.sql)
4. Клонируйте репозиторий. https://github.com/mustaavaruus/milk-logic-test  (https://github.com/mustaavaruus/milk-logic-test.git)
5. Перейдите в папку backend
6. Выполните команду `dotnet build`
7. Выполните команду `dotnet run`.
8. Перейдите по адресу `https://localhost:5100/swagger` для просмотра документации.

## 🛠 Технологии
- ASP.NET Core Web API
- Entity Framework Core (In-Memory/SQL Server)
- Swagger/OpenAPI
- Automapper

## 📖 Эндпоинты

| Метод | Путь                      | Описание                                                                                      |
|-------|---------------------------|-----------------------------------------------------------------------------------------------|
| GET   | /api/data                 | Возвращает данные за заданный период времени                                                  |
| GET   | /api/sensors/summary      | Возвращает агрегированные данные (среднее, максимум, минимум) за указанный период.            |
| POST  | /api/data                 | Принимает данные от эмулятора датчиков и сохраняет их в базе                                  |
| POST  | /api/validate-xml-data    | Создать новый элемент                                                                         |

## 🧪 Тестирование
Для тестирования можно использовать **Postman** или встроенный **Swagger UI**.


1. Возвращает данные за заданный период времени.
    Метод: GET 
    URL: http://localhost:5100/api/data?PeriodBegin=2026-03-20T06%3A08%3A52.525Z&PeriodEnd=2026-03-20T06%3A08%3A52.525Z

2. Возвращает агрегированные данные (среднее, максимум, минимум) за указанный период.
    Метод: GET
    URL: http://localhost:5100/api/sensors/summary?PeriodBegin=2026-03-20T06%3A09%3A55.830Z&PeriodEnd=2026-03-20T06%3A09%3A55.830Z

3. Принимает данные от эмулятора датчиков и сохраняет их в базе.
    Метод: POST
    URL: http://localhost:5100/api/data
    Body: 
        {
        "sensorId": 1,
        "value": 1
        }

4. Функционал для валидации данных, отправляемых с фронтенда в формате XML
    Метод: POST
    URL: http://localhost:5100/api/validate-xml-data
    Body (xml):
        <?xml version="1.0" encoding="UTF-8"?>
        <SensorDataXmlRequestContractModel>
            <signals>
                <sensorId>1</sensorId>
                <timestamp>2026-03-20T06:10:36.070Z</timestamp>
                <value>2</value>
            </signals>
        </SensorDataXmlRequestContractModel>