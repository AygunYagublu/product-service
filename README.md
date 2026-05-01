# Product Service

A RESTful microservice built with ASP.NET Core 8 for managing products.

![Language](https://img.shields.io/badge/language-C%23-blue)
![Framework](https://img.shields.io/badge/framework-ASP.NET%20Core%208-purple)
![License](https://img.shields.io/badge/license-MIT-green)

## Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/products | Get all products |
| GET | /api/products/{id} | Get product by ID |
| POST | /api/products | Create new product |
| PUT | /api/products/{id} | Update product |
| DELETE | /api/products/{id} | Delete product |

## Run

```bash
git clone https://github.com/AygunYagublu/product-service.git
cd product-service
dotnet run
```

## Test

```bash
# Get all products
curl http://localhost:5213/api/products

# Create product
curl -X POST http://localhost:5213/api/products \
  -H "Content-Type: application/json" \
  -d '{"name":"Monitor","description":"4K Display","price":350.00,"stock":15}'

# Delete product
curl -X DELETE http://localhost:5213/api/products/1
```

## Tech Stack
- C# 12
- ASP.NET Core 8
- REST API
- In-memory repository pattern
