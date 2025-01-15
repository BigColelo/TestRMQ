# TestRMQ

A .NET 6.0 Web API project demonstrating RabbitMQ integration with Docker containerization. This project provides a simple message queue management system with basic operations for queue creation, message publishing, and message consumption.

## Prerequisites

- Docker Desktop
- .NET 6.0 SDK (for local development)
- Visual Studio 2022 or VS Code (optional)

## Technologies Used

- .NET 6.0
- RabbitMQ
- Docker
- Swagger/OpenAPI
- Docker Compose

## Quick Start

1. Clone the repository:
```bash
git clone https://github.com/BigColelo/TestRMQ.git
cd TestRMQ
```

2. Run the application using Docker Compose:
```bash
docker-compose up --build
```

The application will be available at:
- API and Swagger UI: http://localhost:5001
- RabbitMQ Management UI: http://localhost:15672 (credentials: user/password)

## Project Structure

```
TestRMQ/
├── Controllers/
│   └── RabbitMQController.cs
├── BL/
│   ├── IRabbitMQBusinessLogic.cs
│   └── RabbitMQBusinessLogic.cs
├── DAO/
│   ├── IRabbitMQDAO.cs
│   └── RabbitMQDAO.cs
└── docker-compose.yml
```

## API Endpoints

### Queue Management
- `POST /api/v1/RabbitMQ/createQueue` - Create a new queue
- `DELETE /api/v1/RabbitMQ/deleteQueue` - Delete an existing queue
- `POST /api/v1/RabbitMQ/sendMessage` - Publish a message to the queue
- `GET /api/v1/RabbitMQ/getMessages` - Retrieve messages from a queue

## Configuration

The application can be configured through:
- `appsettings.json`
- `appsettings.Development.json`
- Environment variables in docker-compose.yml

### RabbitMQ Configuration

```json
{
  "RabbitMQ": {
    "HostName": "rabbitmq",
    "Port": 5672,
    "UserName": "user",
    "Password": "password"
  }
}
```

## Docker Configuration

The project includes:
- `Dockerfile` for the .NET application
- `docker-compose.yml` for orchestrating the application and RabbitMQ services

### Ports
- API: 5001
- RabbitMQ: 5672 (AMQP), 15672 (Management UI)

## Architecture

The project follows a three-layer architecture:
- Controller Layer (API endpoints)
- Business Logic Layer (BL)
- Data Access Layer (DAO)

## License

This project is licensed under the MIT License - see the LICENSE file for details.
