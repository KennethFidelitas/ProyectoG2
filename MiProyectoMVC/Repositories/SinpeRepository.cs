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
                (IdCaja, TelefonoOrigen, NombreOrigen, TelefonoDestinatario,
                 NombreDestinatario, Monto, FechaDeRegistro, Descripcion, Estado)
                VALUES
                (@IdCaja, @TelefonoOrigen, @NombreOrigen, @TelefonoDestinatario,
                 @NombreDestinatario, @Monto, @FechaDeRegistro, @Descripcion, @Estado)";

    connection.Execute(sql, sinpe);
}

    public Comercio? ObtenerComercioPorTelefono(string telefono)
    {
        using var connection = new MySqlConnection(_connectionString);

        var sql = @"SELECT *
                    FROM Comercios
                    WHERE Telefono = @Telefono
                    AND Activo = true";

        return connection.QueryFirstOrDefault<Comercio>(sql, new { Telefono = telefono });
    }

    public Caja? ObtenerCajaAbierta(int comercioId)
    {
        using var connection = new MySqlConnection(_connectionString);

        var sql = @"SELECT *
                    FROM Cajas
                    WHERE ComercioId = @ComercioId
                    AND EstaAbierta = true
                    LIMIT 1";

        return connection.QueryFirstOrDefault<Caja>(sql, new { ComercioId = comercioId });
    }

    public void AgregarMontoACaja(int idCaja, decimal monto)
    {
        using var connection = new MySqlConnection(_connectionString);

        var sql = @"UPDATE Cajas
                    SET MontoFinal = IFNULL(MontoFinal,0) + @Monto
                    WHERE IdCaja = @IdCaja";

        connection.Execute(sql, new { IdCaja = idCaja, Monto = monto });
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

    public List<Sinpe> ObtenerPorCaja(int idCaja)
{
    using var connection = new MySqlConnection(_connectionString);

    var sql = @"SELECT *
                FROM Sinpes
                WHERE IdCaja = @IdCaja
                ORDER BY FechaDeRegistro DESC";

    return connection.Query<Sinpe>(sql, new { IdCaja = idCaja }).ToList();
}
}