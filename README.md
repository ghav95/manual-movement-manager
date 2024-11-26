# Manual Movement Manager

## 🎯 Objective

Build a robust and scalable microsservice, using Clean Architecture, CQRS, Entity Framework and Docker. This MVP serve as a foundation for creating high-quality, maintainable, and testable, with a focus on simplicity.

## 🚀 Getting Started

### 📝 Prerequisites

- **Docker** 
- **.NET Framework 4.5**
---
1. **Clone the Repository**  

   ```bash
   git clone https://github.com/ghav95/manual-movement-manager.git
   ```
2. Open a terminal in the root of repository:
   ```bash
    cd ./manual-movement-manager
   ```

 3. Build docker contaniers
    ```bash
    docker compose up --build -d
    ```
4. Run the .Net project (You can use VS Code, Visual Studio or `msbuild`)
   - You can check if the backend is working in swagger http://localhost:49448/swagger/ui/index
5. Go to http://localhost:4200/
   
---

## 🔑 Keywords
- **Microsservice**
- **DDD**
- **.Net Framework 4.5**
- **SQL Server**
- **Angular**

## 🏛️ Architecture

The project follows the **Clean Architecture** structure to promote modularity, separation of concerns, and ease of testing. The main layers include:

1. **Domain**: Core business logic and entities.
2. **Application**: Application services, use cases, and MediatR handlers.
3. **Infrastructure**: Data access implementations, including Entity Framework.
4. **WebAPI**: API layer to expose endpoints for client applications.
5. **Tests**: Unit and integration tests to ensure reliability and maintainability.

## 🧩  Design Patterns
- **Mediator**
- **CQRS**
- **Repository**

## 🛠️ Technologies Used
- **.NET Framework 4.5**
- **OpenAPI**
- **EF**
- **Unity**
- **Docker**
- **Angular**
- **MediatoR**
