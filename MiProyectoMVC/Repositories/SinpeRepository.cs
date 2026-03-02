namespace MiProyectoMVC.Repositories;

using Dapper;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using MiProyectoMVC.Models;
using System.Collections.Generic;
using System.Linq;

public class SinpeRepository : ISinpeRepository
{
    private readonly string _connectionString;

    public SinpeRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public void Registrar(Sinpe sinpe)
    {
        using var connection = new MySqlConnection(_connectionString);

        var sql = @"INSERT INTO Sinpes
                    (TelefonoOrigen, NombreOrigen, TelefonoDestinatario,
                     NombreDestinatario, Monto, FechaDeRegistro, Descripcion, Estado)
                    VALUES
                    (@TelefonoOrigen, @NombreOrigen, @TelefonoDestinatario,
                     @NombreDestinatario, @Monto, @FechaDeRegistro, @Descripcion, @Estado)";

        connection.Execute(sql, sinpe);
    }

    public void Editar(Sinpe sinpe)
    {
        using var connection = new MySqlConnection(_connectionString);

        var sql = @"UPDATE Sinpes
                    SET Estado = @Estado
                    WHERE IdSinpe = @IdSinpe";

        connection.Execute(sql, sinpe);
    }

    public void Eliminar(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Execute("DELETE FROM Sinpes WHERE IdSinpe = @Id", new { Id = id });
    }

    public Sinpe? ObtenerPorId(int id)
    {
        using var connection = new MySqlConnection(_connectionString);

        return connection.QueryFirstOrDefault<Sinpe>(
            "SELECT * FROM Sinpes WHERE IdSinpe = @Id",
            new { Id = id });
    }

    public List<Sinpe> ObtenerTodos()
    {
        using var connection = new MySqlConnection(_connectionString);

        return connection.Query<Sinpe>(
            "SELECT * FROM Sinpes ORDER BY FechaDeRegistro DESC"
        ).ToList();
    }
}