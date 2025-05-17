using Dapper;
using Microsoft.Data.SqlClient;

namespace CoreFlowAspire.ApiService.GraphQL;

/// <summary>
/// GraphQL Mutation class — สำหรับเปลี่ยนแปลงข้อมูล เช่น insert, update, delete
/// </summary>
public class Mutation
{
    private readonly SqlConnection _connection;

    // ใช้ Dependency Injection รับ SqlConnection ที่ผูกไว้ใน Program.cs
    public Mutation(SqlConnection connection)
    {
        _connection = connection;
    }

    /// <summary>
    /// Insert ข้อมูลใหม่ลงตาราง SampleData
    /// </summary>
    /// <param name="name">ชื่อที่ต้องการบันทึก</param>
    /// <returns>ข้อความบอกผลลัพธ์</returns>
    public async Task<string> AddSampleData(string name)
    {
        var sql = "INSERT INTO SampleData (Name, CreatedAt) VALUES (@Name, GETDATE())";

        var result = await _connection.ExecuteAsync(sql, new { Name = name });

        return result > 0 ? "Insert successful" : "Insert failed";
    }
}
