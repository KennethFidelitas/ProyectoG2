using Dapper;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using MiProyectoMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace MiProyectoMVC.Repositories
{
    public class ComercioRepository : IComercioRepository
    {
        private readonly string _connectionString;

        public ComercioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public List<Comercio> ObtenerTodos()
{
    using var connection = new MySqlConnection(_connectionString);

    var sql = @"SELECT 
                IdComercio,
                Nombre,
                Direccion,
                Telefono,
                Email,
                Activo
                FROM Comercios";

    return connection.Query<Comercio>(sql).ToList();
}

        public Comercio? ObtenerPorId(int id)
{
    using var connection = new MySqlConnection(_connectionString);

    var sql = @"SELECT 
                IdComercio,
                Nombre,
                Direccion,
                Telefono,
                Email,
                Activo
                FROM Comercios
                WHERE IdComercio = @Id";

    return connection.QueryFirstOrDefault<Comercio>(sql, new { Id = id });
}

        public void Registrar(Comercio comercio)
{
    using var connection = new MySqlConnection(_connectionString);

    var sql = @"INSERT INTO Comercios
                (Nombre, Direccion, Telefono, Email, Activo)
                VALUES
                (@Nombre, @Direccion, @Telefono, @Email, @Activo)";

    connection.Execute(sql, comercio);
}

        public void Editar(Comercio comercio)
{
    using var connection = new MySqlConnection(_connectionString);

    var sql = @"UPDATE Comercios
                SET Nombre = @Nombre,
                    Activo = @Activo
                WHERE IdComercio = @IdComercio";

    connection.Execute(sql, comercio);
}

        public void Eliminar(int id)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = @"DELETE FROM Comercios
                        WHERE IdComercio = @Id";

            connection.Execute(sql, new { Id = id });
        }
    }
}