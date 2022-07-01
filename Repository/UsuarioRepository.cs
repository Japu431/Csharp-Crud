using Microsoft.EntityFrameworkCore;
using usuario.Models;
using usuarios.Data;

namespace usuarios.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuarioContext context;

        public UsuarioRepository(UsuarioContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Usuario>> BuscarUsuarios()
        {
            return await this.context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> BuscarUsuario(int id)
        {
            return await this.context.Usuarios
            .Where(x => x.Id == id).FirstOrDefaultAsync();

        }
        public void AdicionaUsuario(Usuario usuario)
        {
            this.context.Add(usuario);
        }

        public void AtualizaUsuario(Usuario usuario)
        {
            this.context.Update(usuario);
        }

        public void DeletaUsuario(Usuario usuario)
        {
            this.context.Remove(usuario);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }
    }
}