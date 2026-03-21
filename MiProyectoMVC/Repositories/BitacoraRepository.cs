namespace MiProyectoMVC.Repositories;

using Dapper;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using MiProyectoMVC.Models;
using System.Collections.Generic;
using System.Linq;

public class BitacoraRepository : IBitacoraRepository
{
    private readonly string _connectionString;

    public BitacoraRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public void RegistrarEvento(BitacoraEvento evento)
    {
        using var connection = new MySqlConnection(_connectionString);

        var sql = @"INSERT INTO Bitacora_Eventos
                    (TablaDeEvento, TipoDeEvento, FechaDeEvento,
                     DescripcionDeEvento, StackTrace,
                     DatosAnteriores, DatosPosteriores)
                    VALUES
                    (@TablaDeEvento, @TipoDeEvento, @FechaDeEvento,
                     @DescripcionDeEvento, @StackTrace,
                     @DatosAnteriores, @DatosPosteriores)";

        connection.Execute(sql, evento);
    }

    public List<BitacoraEvento> ObtenerEventos()
    {
        using var connection = new MySqlConnection(_connectionString);

        return connection.Query<BitacoraEvento>(
            @"SELECT *
              FROM Bitacora_Eventos
              ORDER BY FechaDeEvento DESC"
        ).ToList();
    }
}