# A Microservices E-Commerce Platform

![High-Level System Architecture](https://github.com/user-attachments/assets/403dbc04-6a59-4673-950c-373aaa4d4dde)

## Project Overview
This project implements an e-commerce platform with modular microservices (Catalog, Basket, Discount, Ordering, API Gateway, and WebUI). It integrates event-driven messaging, polyglot persistence, and full observability (metrics, logs, and traces) using OpenTelemetry and modern observability tools.

## Modules and Features
#### Catalog Microservice
* Built with ASP.NET Core Minimal APIs leveraging the latest features of .NET 8 and C# 12.
* Implements Vertical Slice Architecture with feature folders.
* CQRS implementation using MediatR library for command and query separation.
* CQRS Validation Pipeline Behaviors with MediatR and FluentValidation.
* Uses Marten library for .NET Transactional Document DB on PostgreSQL.
* Uses Carter for Minimal API endpoint definition.
* Features cross-cutting concerns like Logging, Global Exception Handling, and Health Checks.

#### Basket Microservice
* ASP.NET 8 Web API application, Following REST API principles, CRUD.
* Utilizes Redis as a Distributed Cache for basket data.
* Implements design patterns: Proxy, Decorator, and Cache-aside.
* Syncs with the Discount microservice using gRPC for pricing calculations.
* Publishes BasketCheckout events to RabbitMQ using MassTransit.
  
#### Discount Microservice
* High-performance gRPC Server for inter-service communication with the Basket microservice.
* Defines Protobuf messages for gRPC services.
* Manages data with SQLite and Entity Framework Core, including database migrations.
* Fully containerized for seamless deployment.

#### Ordering Microservice
* Implementing DDD, CQRS, and Clean Architecture.
* Features CQRS with MediatR, FluentValidation, and Mapster.
* Consumes RabbitMQ BasketCheckout events with MassTransit.
* Manages data with SQL Server and EF Core, including automatic migrations on startup.

#### Microservices Communication
* Synchronous Communication: gRPC-based inter-service calls.
* Asynchronous Communication: RabbitMQ Message-Broker.
  * Implements Publish/Subscribe Topic Exchange Model.
  * Manages events with MassTransit.
* Shared EventBus Messages Library for consistent event contracts across microservices.

#### Yarp API Gateway Microservice
* Built using YARP Reverse Proxy, applying the Gateway Routing Pattern.
* Configurable with routes, clusters, transforms, and rate limiting via FixedWindowLimiter.

#### WebUI ShoppingApp Microservice
* ASP.NET Core web application styled with Bootstrap 4 and Razor templates.
* Consumes APIs from microservices via YARP using Refit HttpClientFactory.

#### Docker Compose Setup
* Containerizes microservices and databases.

---

## Observability & Monitoring

This project implements a comprehensive **observability stack** using OpenTelemetry for unified telemetry data collection, providing full visibility into metrics, traces, and logs across all microservices.

### Architecture Overview
The observability architecture follows the **OpenTelemetry Collector pattern** where:
- **All microservices** export instrumentation data (metrics, traces, logs) to a centralized **OpenTelemetry Collector**
- **OpenTelemetry Collector** acts as a telemetry data pipeline, receiving, processing, and routing observability data to appropriate backends
- **Jaeger** receives distributed traces for trace analysis
- **Loki** receives structured logs for log aggregation and search
- **Prometheus** scrapes metrics from the OpenTelemetry Collector for monitoring
- **Grafana** provides unified dashboards for visualizing metrics and logs with automatic correlation
![Observability Architecture](https://github.com/user-attachments/assets/83588ffa-8e5d-4c7c-895a-b8a0ab99267b)
  
### Metrics (OpenTelemetry → Prometheus → Grafana)
* **OpenTelemetry** collects metrics from all microservices using auto-instrumentation
* **OpenTelemetry Collector** processes and exposes metrics to **Prometheus**
* **Prometheus** scrapes metrics from the OpenTelemetry Collector and stores them as time-series metrics data
* **Grafana** provides rich dashboards for service performance visualization
![Grafana Metrics](https://github.com/user-attachments/assets/bddd9c51-0707-481f-9bc8-b14415211006)
---

### Tracing (OpenTelemetry → Jaeger)
* **OpenTelemetry** generates distributed traces across all microservice interactions
* **OpenTelemetry Collector** receives and exports traces to **Jaeger**
* **Jaeger** provides distributed tracing analysis with service maps and trace visualization
* Full request flow tracking from API Gateway through all downstream services
![Jaeger Traces](https://github.com/user-attachments/assets/94ae24aa-770f-4dd0-a876-9dfd2da29b87)
---

### Logging (OpenTelemetry → Loki → Grafana)
* **OpenTelemetry** collects structured logs from all microservices with automatic context enrichment
* **OpenTelemetry Collector** processes and forwards logs to **Loki** using native OTLP support
* **Loki** stores and indexes logs with automatic label extraction from OpenTelemetry attributes
* **Grafana** provides log exploration and correlation with metrics and traces
![Loki Logs](https://github.com/user-attachments/assets/4f9c1c45-b8c7-4a19-ad65-91dce1ec5653)
---

## Run The Project
### Prerequisites
Ensure the following tools are installed on your system:
* [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
* [.Net Core 8 or later](https://dotnet.microsoft.com/download/dotnet-core/8)
* [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Installation Steps
1. Clone the repository.
2. Start Docker Desktop.
3. Run the project:
    * Open the solution in Visual Studio, set docker-compose as the startup project, and start without debugging.
    * Alternatively, execute the following command in the root directory:
    ```bash
    docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
    ```
4. Access the Web UI:
    * Open your browser and navigate to https://localhost:6065.
    * Explore the ShoppingApp to interact with microservices via the YARP API Gateway.

---
## 🔗 Quick Access (Default Ports)
| Service          | URL                                   | Description                    |
|------------------|---------------------------------------|--------------------------------|
| WebUI ShoppingApp| https://localhost:6065                | Main e-commerce application    |
| Grafana          | http://localhost:3000 (admin/admin)   | Metrics & logs dashboards      |
| Jaeger           | http://localhost:16686                | Distributed tracing UI         |
| Prometheus       | http://localhost:9090                 | Metrics storage & queries      |
