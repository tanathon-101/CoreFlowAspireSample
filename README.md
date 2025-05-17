# CoreFlowAspireSample

## Project Overview

CoreFlowAspireSample เป็นโปรเจกต์ตัวอย่าง .NET Aspire (.NET 8.0) สำหรับทำ Distributed Application ที่ประกอบด้วย Web API และ SQL Server (เชื่อมต่อด้วย Dapper)

## Structure

- `CoreFlowAspire.AppHost` – ตัว Application Host สำหรับจัดการ Distributed Application (Aspire Dashboard)
- `CoreFlowAspire.Web` – Minimal API สำหรับรับข้อมูลแล้วบันทึกลงฐานข้อมูล
- `CoreFlowAspire.ApiService` – สำหรับ Service ในอนาคต
- `CoreFlowAspire.ServiceDefaults` – สำหรับจัดเก็บ configuration กลาง

## How to Run

1. ติดตั้ง .NET Aspire workload

    ```bash
    dotnet workload install aspire
    ```

2. รันโปรเจกต์ AppHost

    ```bash
    dotnet run --project CoreFlowAspire.AppHost
    
    ```

3. หรือถ้าต้องการรันเฉพาะ Web API

    ```bash
    dotnet run --project CoreFlowAspire.Web
    ```


CREATE TABLE SampleData (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    CreatedAt DATETIME
);

curl --location 'https://localhost:7033/publish' \
--header 'Content-Type: application/json' \
--header 'Cookie: .Aspire.Dashboard.Antiforgery=CfDJ8IWPA4aAO0FJsV1uY4n9DgMPqpQCUnFUQUEFgzAGjJh_vXGAlAF-n7_suh3wVwrESuv0iLpB7uMO9qFiAjqYiGfsb3nDOnUVANhiOBcvLeclONQ66ObG0WKf4qrw1mp_-v_KPt6cE5hu1VywEn58gsk' \
--data '{"name":"Josh Aspire"}'
