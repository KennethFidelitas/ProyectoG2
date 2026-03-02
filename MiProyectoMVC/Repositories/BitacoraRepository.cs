namespace MiProyectoMVC.Repositories;

using Dapper;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using MiProyectoMVC.Models;

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
}