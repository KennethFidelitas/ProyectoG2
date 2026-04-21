using MiProyectoMVC.Models;

namespace MiProyectoMVC.Repositories
{
    public class UsuarioRepository
    {
        private readonly TuDbContext _context;

        public UsuarioRepository(TuDbContext context)
        {
            _context = context;
        }

        public List<Usuario> GetAll()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario GetById(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public Usuario GetByIdentificacion(string identificacion)
        {
            return _context.Usuarios
                .FirstOrDefault(u => u.Identificacion == identificacion);
        }

        public void Add(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }
    }
}
