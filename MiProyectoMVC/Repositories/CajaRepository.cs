namespace MiProyectoMVC.Repositories;

using Dapper;
using Microsoft.Extensions.Configuration;
using MiProyectoMVC.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

public class CajaRepository : ICajaRepository
{
    private readonly string _connectionString;

    public CajaRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public void Registrar(Caja caja)
    {
        using var connection = new MySqlConnection(_connectionString);

        var sql = @"INSERT INTO Cajas
                    (ComercioId, FechaApertura, EstaAbierta)
                    VALUES
                    (@ComercioId, @FechaApertura, @EstaAbierta)";

        connection.Execute(sql, caja);
    }

    public void Editar(Caja caja)
    {
        using var connection = new MySqlConnection(_connectionString);

        var sql = @"UPDATE Cajas
                    SET ComercioId = @ComercioId
                    WHERE IdCaja = @IdCaja";

        connection.Execute(sql, caja);
    }

    public void Eliminar(int id)
    {
        using var connection = new MySqlConnection(_connectionString);

        connection.Execute("DELETE FROM Cajas WHERE IdCaja = @Id", new { Id = id });
    }

    public void CerrarCaja(int id, decimal montoFinal)
    {
        using var connection = new MySqlConnection(_connectionString);

        var sql = @"UPDATE Cajas
                    SET EstaAbierta = false,
                        FechaCierre = @FechaCierre,
                        MontoFinal = @MontoFinal
                    WHERE IdCaja = @Id";

        connection.Execute(sql, new
        {
            Id = id,
            FechaCierre = DateTime.Now,
            MontoFinal = montoFinal
        });
    }

    public Caja? ObtenerPorId(int id)
    {
        using var connection = new MySqlConnection(_connectionString);

        var sql = @"SELECT c.*, co.Nombre AS NombreComercio
                    FROM Cajas c
                    INNER JOIN Comercios co ON co.IdComercio = c.ComercioId
                    WHERE c.IdCaja = @Id";

        return connection.QueryFirstOrDefault<Caja>(sql, new { Id = id });
    }

    public List<Caja> ObtenerTodos()
    {
        using var connection = new MySqlConnection(_connectionString);

        var sql = @"SELECT c.*, co.Nombre AS NombreComercio
                    FROM Cajas c
                    INNER JOIN Comercios co ON co.IdComercio = c.ComercioId
                    ORDER BY c.FechaApertura DESC";

        return connection.Query<Caja>(sql).ToList();
    }
}