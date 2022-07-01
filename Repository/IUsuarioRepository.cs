using usuario.Models;

namespace usuarios.Repository
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> BuscarUsuarios();
        Task<Usuario> BuscarUsuario(int id);

        void AdicionaUsuario(Usuario usuario);
        void AtualizaUsuario(Usuario usuario);
        void DeletaUsuario(Usuario usuario);
        Task<bool> SaveChangesAsync();
    }
}