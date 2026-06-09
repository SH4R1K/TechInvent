# TechInvent 🖥️

**Информационная система для инвентаризации компьютеров и оборудования**

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![ASP.NET Core MVC](https://img.shields.io/badge/ASP.NET_Core-MVC-512BD4?logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/core/mvc/)
[![ASP.NET Core Web API](https://img.shields.io/badge/ASP.NET_Core-Web_API-512BD4?logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/core/web-api/)
[![EF Core](https://img.shields.io/badge/EF_Core-8.0-512BD4?logo=entity-framework&logoColor=white)](https://learn.microsoft.com/ef/core/)
[![MySQL](https://img.shields.io/badge/MySQL-8.4-4479A1?logo=mysql&logoColor=white)](https://www.mysql.com/)
[![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?logo=docker&logoColor=white)](https://www.docker.com/)
[![JWT](https://img.shields.io/badge/Auth-JWT_Bearer-000000?logo=jsonwebtokens&logoColor=white)](https://jwt.io/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5-darkviolet?logo=bootstrap&logoColor=white)](https://getbootstrap.com/)
[![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?logo=swagger&logoColor=white)](https://swagger.io/)
[![ClosedXML](https://img.shields.io/badge/Excel-ClosedXML-217346?logo=microsoftexcel&logoColor=white)](https://closedxml.github.io/ClosedXML/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

---

## 📋 О проекте

Дипломный проект, реализующий полный цикл инвентаризации компьютерной техники — от автоматического сбора данных с рабочих станций до веб-интерфейса для учёта, поиска и формирования отчётности.

**Ключевые возможности:**
- ✅ Автоматический сбор конфигурации ПК (железо + ПО) через агента инвентаризации
- ✅ Веб-интерфейс для управления кабинетами, рабочими местами и оборудованием
- ✅ Загрузка данных через JSON (от агента) и Excel
- ✅ Генерация QR-кодов для рабочих мест
- ✅ Экспорт отчётов в Excel
- ✅ Система заявок на техническое обслуживание
- ✅ Ролевая модель: администратор, оператор, пользователь

---

## 🏗️ Архитектура

```
┌─────────────────────────────────────────────────────┐
│                    Docker Compose                   │
│  ┌─────────────┐  ┌──────────────┐  ┌────────────┐  │
│  │   WebMVC    │  │ TechInventAPI│  │   MySQL    │  │
│  │  :80/8080   │  │  :5239/8080  │  │   8.4      │  │
│  └──────┬──────┘  └──────┬───────┘  └─────┬──────┘  │
│         │                │                │         │
│         └─────────────────────────────────┘         │
│                                                     │
└─────────────────────────────────────────────────────┘
         ▲                          ▲
         │                          │
    ┌────┴─────┐           ┌───────┴────────┐
    │  Агент   │           │ Пользователь   │
    │инвентариз.│           │  (браузер)     │
    │ .exe     │           │                │
    └──────────┘           └────────────────┘
```

**Многослойная архитектура (.NET 8):**

```
TechInvent.sln
│
├── 🧱 TechInvent.DM        # Domain Models (сущности)
├── 🗄️  TechInvent.DAL      # Data Access Layer (EF Core, миграции)
├── ⚙️  TechInvent.BLL      # Business Logic Layer (DTO, конвертеры, сервисы)
│
├── 🌐 TechInventAPI        # ASP.NET Core Web API 
└── 🖥️  WebMVC              # ASP.NET Core MVC (веб-интерфейс)
```

---

## 🧱 TechInvent.DM — Domain Models

Сущности предметной области. Реализовано наследование **Table-per-Type (TPT)** через две иерархии:

### Иерархия `InventStuff`
```
InventStuff (база)
├── 🔹 SerialNumber, InventNumber, IsDecommissioned
├── 🖥️  Workplace — рабочее место
│     ├── ОС, GUID, компоненты, ПО
│     └── Привязка к кабинету
└── 📦 CabinetEquipment — оборудование кабинета
      ├── Тип, вендор
      └── Привязка к кабинету / рабочему месту
```

### Иерархия `Component`
```
Component (база)
├── 🧠 Processor     — процессор (ядра, частота)
├── 💾 Ram           — ОЗУ (объём, тип, частота, производитель)
├── 💽 Disk          — диск (модель, размер)
├── 🎮 Gpu           — видеокарта (VRAM, UUID)
├── 🔌 Mainboard     — материнская плата (серийный номер)
└── 🌐 NetAdapter    — сетевой адаптер (MAC, тип, производитель)
```

### Справочники
| Сущность                 | Назначение                     |
|--------------------------|--------------------------------|
| `Cabinet`                | Кабинет / помещение            |
| `CabinetEquipmentType`   | Тип оборудования (роутер, принтер и т.д.) |
| `Vendor`                 | Производитель оборудования      |
| `Manufacturer`           | Производитель компонентов       |
| `AdapterType`            | Тип сетевого адаптера           |
| `Os`                     | Операционная система            |
| `Software`               | Программное обеспечение         |
| `RequestType`            | Тип заявки на обслуживание      |
| `Role` / `User`          | Роли и пользователи             |

### Связующие сущности
- `InstalledSoftware` — Many-to-Many: Workplace ↔ Software
- `TechRequestWorkplace` — Many-to-Many: TechRequest ↔ Workplace
- `TechRequestComment` — Комментарии к заявкам

**Всего: 25 файлов моделей**

---

## 🗄️ TechInvent.DAL — Data Access Layer

```csharp
public partial class TechInventContext : DbContext
{
    // 22 DbSet'а для всех сущностей
    // TPT: modelBuilder.Entity<Component>().UseTptMappingStrategy()
    //      modelBuilder.Entity<InventStuff>().UseTptMappingStrategy()
}
```

**Состав:**
- **`TechInventContext`** — EF Core DbContext (534 строки) с полным fluent-маппингом
- **`InitialData`** — Seed-данные при первом запуске:
  - Администратор: `admin` / `1`
  - 3 роли: `admin`, `user`, `operator`
  - 5 типов заявок (неисправность ПК, периферии, ПО, помещения, прочее)
  - 5 типов оборудования (роутер, сервер, ноутбук, принтер, монитор)

**6 миграций** — полная эволюция схемы от начальной до финальной (MySQL, InnoDB, utf8mb4).

---

## ⚙️ TechInvent.BLL — Business Logic Layer

### DTO для агента инвентаризации

JSON-структура, принимаемая от агента (`TechInvent.exe`):

```
CabinetDto
└── WorkplaceDto
    ├── HardwareInfo
    │   ├── MainboardDto
    │   ├── List<ProcessorDto>
    │   ├── List<RamDto>
    │   ├── List<GpuDto>
    │   ├── List<NetDto>
    │   └── List<DiskDto>
    ├── List<SoftwareDto>
    └── Perifery (мониторы, мыши, клавиатуры, принтеры)
```

### DtoConverter
Конвертирует плоские DTO в граф связанных доменных сущностей с полным обходом и сохранением через `EntityCheckerService`.

### EntityCheckerService
Сервис **"Get-or-Create"** — для каждой справочной сущности проверяет наличие в БД и создаёт при отсутствии. Предотвращает дублирование при многократном импорте.

### DtoMVC
Лёгкие DTO (`CabinetNameIdDto`, `VendorNameIdDto`, `WorkplaceNameIdDto`, `WorkplaceNameUrlDto`) для передачи данных в Razor-представления.

---

## 🌐 TechInventAPI — Web API

| Метод | Эндпоинт         | Описание                           |
|-------|------------------|-------------------------------------|
| POST  | `/api/Invent`    | Приём JSON от агента инвентаризации |

```json
{
  "name": "Кабинет 301",
  "workplace": {
    "comp_name": "PC-301-01",
    "os_name": "Microsoft Windows 10 Pro",
    "version": "10.0.19045.3693",
    "hardware_info": {
      "mainboard": { "name": "ASUS PRIME B360-PLUS", "serial_number": "SN123456789" },
      "processor": [{ "name": "Intel Core i5-9400F", "physical_cores": 6, "logical_cores": 6, "max_clock_speed": 2901 }],
      "ram": [{ "name": "Kingston DDR4 8GB", "manufacturer": "Kingston", "speed": "2666", "capacity": "8 GB", "memory_type": 26, "part_number": "KHX2666C16/8G", "serial_number": "ABC123" }],
      "gpu": [{ "name": "NVIDIA GeForce GTX 1660", "adapter_ram": 6144.0 }],
      "net": [{ "name": "Realtek PCIe GbE Family Controller", "manufacturer": "Realtek", "mac_address": "AA:BB:CC:DD:EE:FF", "adapter_type": "Ethernet" }],
      "disk": [{ "model": "Samsung SSD 860 EVO 500GB", "size": 500 }]
    },
    "software": [
      { "name": "Microsoft Office Professional Plus 2019", "version": "16.0.10402.20025", "vendor": "Microsoft Corporation" },
      { "name": "Google Chrome", "version": "125.0.6422.142", "vendor": "Google LLC" }
    ]
  }
}
```

**Особенности:**
- 📖 Swagger UI в Development-режиме
- 🔄 Автоматический прогон миграций EF Core при запуске
- 🐇 RabbitMQ (заготовка: `RabbitMqListener` + `IRabbitMqService`, ожидает активации)

---

## 🖥️ WebMVC — Веб-интерфейс

### 🎭 Ролевая модель

| Роль       | Права                                                              |
|------------|---------------------------------------------------------------------|
| 👑 **admin** | Полный доступ: управление пользователями, редактирование, удаление |
| 🔧 **operator** | Управление оборудованием, кабинетами, заявками, импорт           |
| 👤 **user** | Просмотр, создание заявок, просмотр деталей рабочего места         |

Аутентификация через **JWT Bearer** (токен хранится в cookie `A`, срок — 30 минут).

### 📂 Контроллеры

| Контроллер                     | Доступ          | Описание                                          |
|--------------------------------|-----------------|---------------------------------------------------|
| `AuthController`               | ✳️ Все         | Вход / выход                                      |
| `HomeController`               | 🔐 Авториз.    | Главная, страница 404                             |
| `CabinetsController`           | 🔐 Авториз.    | CRUD кабинетов, QR-коды, Excel-отчёты             |
| `WorkplacesController`         | 🔐 Авториз.    | Рабочие места (детали с компонентами), экспорт    |
| `CabinetEquipmentsController`  | 🔧 operator+   | CRUD оборудования                                  |
| `CabinetEquipmentTypesController` | 🔧 operator+ | CRUD типов оборудования                             |
| `VendorsController`            | 🔧 operator+   | CRUD производителей                                |
| `InventStuffController`        | 🔐 Авториз.    | Поиск, списание, просмотр списанного               |
| `ImportController`             | 🔧 operator+   | Импорт JSON / Excel, скачивание агента             |
| `TechRequestController`        | 🔐 Авториз.    | Заявки на обслуживание (CRUD + комментарии)        |
| `UsersController`              | 👑 admin       | Управление пользователями                          |

### 🛠️ Сервисы

| Сервис               | Технологии                                | Назначение                                |
|-----------------------|--------------------------------------------|-------------------------------------------|
| `JWTokenService`      | HMAC-SHA512, JwtSecurityToken              | Генерация JWT-токенов                     |
| `QRService`           | QRCoder + DinkToPdf                       | QR-коды и PDF для печати                  |
| `ExcelService`        | ClosedXML                                  | Экспорт отчётов в Excel                    |
| `ExcelServiceImport`  | ClosedXML + EntityCheckerService           | Импорт оборудования из Excel              |
| `UserService`         | HttpContextAccessor                        | Получение текущего пользователя           |

### 🎨 Фронтенд

- **Bootstrap 5** (тёмная тема `data-bs-theme="dark"`)
- **jQuery** + **jQuery UI** (автодополнение поиска)
- **Select2** (множественный выбор рабочих мест в заявках)
- **Toastify** (всплывающие уведомления)
- **Bootstrap Icons**
- **Кастомные CSS-стили** (colors, buttons, cards, navbar, modal, scrollbar, pagination и др.)

---

## 📦 Docker Compose

```yaml
services:
  mvc:          # techinvent_mvc   — порт 80 → 8080
  api:          # techinvent_api   — порт 5239 → 8080
  mysql_dbms:   # mysql_dbms       — MySQL 8.4 + healthcheck
```

Все сервисы объединены в сеть `backend` (bridge).

---

## 🚀 Запуск

### Docker Compose (рекомендуется)

```bash
# 1. Скопировать и настроить .env файлы
cp environment/default.env_example environment/default.env
cp environment/mysql.env_example environment/mysql.env

# 2. Запустить
docker compose up -d

# 3. Открыть в браузере
start http://localhost           # Веб-интерфейс
start http://localhost:5239/swagger  # Swagger API
```

> **Учётные данные по умолчанию:** `admin` / `1`

### Без Docker

```bash
# Требуется: .NET 8 SDK, MySQL сервер

# Настроить строку подключения через переменную окружения:
set MySQLConnString="server=localhost;port=3306;user=root;password=pass;database=techinvent"

# Запустить API (миграции применяются автоматически)
dotnet run --project TechInventAPI

# Запустить веб-интерфейс
dotnet run --project WebMVC
```

### Переменные окружения

**`environment/default.env`:**
```env
MySQLConnString="server=mysql_dbms;port=3306;user=user;password=pass;database=techinvent"
JWTKey="SUPER_SECRET_KEY_MIN_32_CHARS_LONG_HERE"
JWTIssuer="techinvent"
JWTAudience="techinvent"
```

**`environment/mysql.env`:**
```env
MYSQL_ROOT_PASSWORD=root_password
MYSQL_DATABASE=techinvent
MYSQL_USER=user
MYSQL_PASSWORD=pass
```

---

## 🗃️ Схема базы данных

MySQL 8.4, InnoDB, utf8mb4. 22 таблицы, нормализованная схема с TPT-наследованием.

```
adapter_type
├── id_adapter_type (PK)
└── name (UQ)

cabinet
├── id_cabinet (PK)
└── name (UQ)

invent_stuff (TPT base)
├── id_invent_stuff (PK)
├── name
├── invent_number (UQ)
├── serial_number (UQ)
├── is_decommissioned
├── decommission_comment
└── decommission_date

├── workplace
│   ├── guid (UQ)
│   ├── last_update
│   └── id_cabinet (FK → cabinet)
│       id_os (FK → os)

├── cabinet_equipment
│   ├── id_cabinet (FK → cabinet, SET NULL)
│   ├── id_workplace (FK → workplace, SET NULL)
│   ├── id_cabinet_equipment_type (FK → cabinet_equipment_type)
│   └── id_vendor (FK → vendor)

component (TPT base)
├── id_component (PK)
├── name
└── id_workplace (FK → workplace, CASCADE)

├── processor      │  ├── gpu
├── ram            │  ├── net_adapter
├── disk           │  └── mainboard
```

**Полный список таблиц:**

| Таблица                  | Назначение                     |
|--------------------------|--------------------------------|
| `cabinet`                | Кабинеты                       |
| `workplace`              | Рабочие места                  |
| `cabinet_equipment`      | Оборудование                   |
| `cabinet_equipment_type` | Типы оборудования              |
| `invent_stuff`           | Базовая таблица инвентаря      |
| `component`              | Базовая таблица компонентов    |
| `processor` / `ram` / `disk` / `gpu` / `mainboard` / `net_adapter` | Компоненты |
| `manufacturer`           | Производители                  |
| `adapter_type`           | Типы сетевых адаптеров         |
| `vendor`                 | Вендоры оборудования           |
| `os`                     | Операционные системы           |
| `software`               | Программное обеспечение        |
| `installed_software`     | Связь ПК ↔ ПО (M:N)           |
| `user` / `role`          | Пользователи и роли            |
| `tech_request`           | Заявки на обслуживание         |
| `request_type`           | Типы заявок                    |
| `tech_request_comment`   | Комментарии к заявкам          |
| `techrequest_has_workplace` | Связь заявка ↔ рабочее место |

---

## 🧪 Технологический стек

| Категория           | Технология                                                     |
|---------------------|----------------------------------------------------------------|
| **Язык**            | [![C#](https://img.shields.io/badge/C%23_12-239120?logo=csharp)](https://learn.microsoft.com/dotnet/csharp/) |
| **Платформа**       | [![.NET 8](https://img.shields.io/badge/.NET_8-512BD4?logo=dotnet)](https://dotnet.microsoft.com/) |
| **ORM**             | [![EF Core 8](https://img.shields.io/badge/EF_Core_8-512BD4?logo=entity-framework)](https://learn.microsoft.com/ef/core/) |
| **СУБД**            | [![MySQL 8.4](https://img.shields.io/badge/MySQL_8.4-4479A1?logo=mysql)](https://www.mysql.com/) |
| **Веб-фреймворк**   | [![ASP.NET Core MVC + API](https://img.shields.io/badge/ASP.NET_Core_8-512BD4?logo=dotnet)](https://learn.microsoft.com/aspnet/core/) |
| **Аутентификация**  | [![JWT Bearer](https://img.shields.io/badge/JWT_Bearer-000000?logo=jsonwebtokens)](https://jwt.io/) |
| **Фронтенд**        | [![Bootstrap 5](https://img.shields.io/badge/Bootstrap_5-7952B3?logo=bootstrap)](https://getbootstrap.com/) [![jQuery](https://img.shields.io/badge/jQuery-0769AD?logo=jquery)](https://jquery.com/) [![Select2](https://img.shields.io/badge/Select2-2C3E50?logo=javascript)](https://select2.org/) |
| **Excel**           | [![ClosedXML](https://img.shields.io/badge/ClosedXML-217346?logo=microsoftexcel)](https://closedxml.github.io/) |
| **PDF**             | [![DinkToPdf](https://img.shields.io/badge/DinkToPdf-0892D0)](https://github.com/rdvojmoc/DinkToPdf) |
| **QR-коды**         | [![QRCoder](https://img.shields.io/badge/QRCoder-FF6A00)](https://github.com/codebude/QRCoder) |
| **Документация API**| [![Swagger](https://img.shields.io/badge/Swagger-85EA2D?logo=swagger)](https://swagger.io/) |
| **Очереди**         | [![RabbitMQ](https://img.shields.io/badge/RabbitMQ-FF6600?logo=rabbitmq)](https://www.rabbitmq.com/) |
| **Контейнеризация** | [![Docker](https://img.shields.io/badge/Docker-2496ED?logo=docker)](https://www.docker.com/) |

---

## 📁 Структура решения

```
TechInvent/
├── 📄 TechInvent.sln
├── 📄 docker-compose.yaml
├── 📄 .dockerignore / .gitignore
│
├── 📁 environment/                        # Переменные окружения
│   ├── default.env_example
│   └── mysql.env_example
│
├── 📁 TechInvent.DM/                      # 🧱 Domain Models
│   └── Models/                            # 25 сущностей
│       ├── InventStuff.cs                 # Базовый класс инвентаря
│       ├── Workplace.cs / CabinetEquipment.cs
│       ├── Component.cs                   # Базовый класс компонентов
│       ├── Processor.cs / Ram.cs / Disk.cs / Gpu.cs / Mainboard.cs / NetAdapter.cs
│       ├── Cabinet.cs / CabinetEquipmentType.cs / Vendor.cs
│       ├── Manufacturer.cs / AdapterType.cs / Os.cs / Software.cs
│       ├── InstalledSoftware.cs
│       ├── User.cs / Role.cs
│       └── TechRequest.cs / TechRequestComment.cs / TechRequestWorkplace.cs / RequestType.cs
│
├── 📁 TechInvent.DAL/                     # 🗄️ Data Access Layer
│   ├── Data/
│   │   ├── TechInventContext.cs            # DbContext (22 DbSet)
│   │   └── InitialData.cs                 # Seed data
│   └── Migrations/                        # 6 миграций
│       ├── 20250516101728_Initial.cs
│       ├── 20250517194659_cabinet-equipment-workplace.cs
│       ├── 20250518103801_vendorserialnumberupdate.cs
│       ├── 20250519093344_removemonitors.cs
│       ├── 20250522204648_updateinventstuffuser.cs
│       └── 20250523092149_initialcabeqtypes.cs
│
├── 📁 TechInvent.BLL/                     # ⚙️ Business Logic Layer
│   ├── Dto/
│   │   └── DtoConverter.cs                # DTO ↔ Domain конвертер
│   ├── DtoModels/                         # 14 DTO
│   │   ├── CabinetDto.cs / WorkplaceDto.cs / HardwareInfo.cs
│   │   ├── ProcessorDto.cs / RamDto.cs / DiskDto.cs / GpuDto.cs
│   │   ├── MainboardDto.cs / NetDto.cs / SoftwareDto.cs
│   │   ├── Monitor.cs / Mouse.cs / Keyboard.cs / Printer.cs / Perifery.cs
│   │   └── DtoMVC/                        # 4 DTO для MVC
│   ├── Extensions/
│   │   └── StringExtension.cs
│   └── Service/
│       └── EntityCheckerService.cs         # Get-or-Create сервис
│
├── 📁 TechInventAPI/                      # 🌐 Web API
│   ├── Controllers/
│   │   └── InventController.cs            # POST /api/Invent
│   ├── RabbitMQ/
│   │   ├── IRabbitMqService.cs
│   │   └── RabbitMqListener.cs            # BackgroundService (закомментирован)
│   ├── Program.cs
│   ├── Dockerfile
│   └── Properties/launchSettings.json
│
└── 📁 WebMVC/                             # 🖥️ Веб-интерфейс
    ├── Controllers/                       # 11 контроллеров
    │   ├── AuthController.cs
    │   ├── HomeController.cs
    │   ├── CabinetsController.cs
    │   ├── WorkplacesController.cs
    │   ├── CabinetEquipmentsController.cs
    │   ├── CabinetEquipmentTypesController.cs
    │   ├── VendorsController.cs
    │   ├── InventStuffController.cs
    │   ├── ImportController.cs
    │   ├── TechRequestController.cs
    │   └── UsersController.cs
    ├── Services/                          # 5 сервисов
    │   ├── JWTokenService.cs
    │   ├── QRService.cs
    │   ├── ExcelService.cs (+ ExcelServiceImport.cs)
    │   └── UserService.cs
    ├── ViewComponents/
    │   └── SearchViewComponent.cs
    ├── Views/                             # 30+ Razor-представлений
    │   ├── Shared/_Layout.cshtml
    │   ├── Auth/ / Home/ / Cabinets/ / Workplaces/
    │   ├── CabinetEquipments/ / CabinetEquipmentTypes/ / Vendors/
    │   ├── InventStuff/ / Import/ / TechRequest/ / Users/
    │   └── Shared/Components/Search/
    ├── wwwroot/
    │   ├── css/ (site.css + custom_css/)
    │   ├── js/ / lib/ / select2/ / images/ / files/
    │   └── TechInvent.exe                 # Агент инвентаризации
    ├── Program.cs
    └── Dockerfile
```

---

<div align="center">
    <br>
    <sub>© 2026 — Дипломный проект по разработке информационной системы для инвентаризации компьютеров</sub>
    <br>
    <sub>Made with ❤️ and .NET</sub>
</div>
