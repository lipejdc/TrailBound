# TrailBound

**TrailBound** is a personal activity and trip tracking project for managing my hikes, cycling, and mountaineering adventures. It provides a **Web API** to organize activities and trips, store metadata like distance, elevation, and location, and optionally attach GPX or Komoot routes.  

The project is built with **C#/.NET**, **ASP.NET Core Web API**, and **Entity Framework Core**, following a clean architecture pattern with separate **Domain**, **Application**, **Infrastructure**, and **API** layers.  

---

## Technology Stack

- **C# / .NET 10**  
- **ASP.NET Core Web API**  
- **Entity Framework Core**  
- **SQL Server** (or any EF Core compatible database)  

---

## Features

- CRUD operations for **Activities** and **Trips**
- Filter activities by **year** or **month**
- Enum-based activity **type** and **status** (Hiking, Cycling, Mountaineering / Planned, Completed)
- Assign activities to trips with optional metadata
- EF Core **value object mapping** for locations
- Async repository pattern for database operations

---

## Future Frontend

A frontend will be built later using:  

- **React** + **Next.js**  
- **TypeScript**  
- **Tailwind CSS**  

This will provide a personal website interface to view and manage all activities and trips.  
