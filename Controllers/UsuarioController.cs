using Microsoft.AspNetCore.Mvc;
using usuario.Models;
using usuarios.Repository;

namespace usuarios.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository repository;

        public UsuarioController(IUsuarioRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {

            var usuarios = await this.repository.BuscarUsuarios();
            return usuarios.Any() ? Ok(usuarios) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var usuario = await this.repository.BuscarUsuario(id);
            return usuario != null ? Ok(usuario) : NotFound("Usuario não encontrado");

        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Usuario usuario)
        {
            this.repository.AdicionaUsuario(usuario);

            return await this.repository.SaveChangesAsync()
            ? Ok("Usuario salvado com sucesso")
            : BadRequest("Erro ao salvar usuario");
        }


        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int id, Usuario usuario)
        {
            var usuarioBanco = await this.repository.BuscarUsuario(id);
            if (usuarioBanco == null) return NotFound("Usuario nao encontrado!");

            usuarioBanco.Nome = usuario.Nome ?? usuarioBanco.Nome;

            usuarioBanco.DataNascimento = usuario.DataNascimento != new DateTime()
            ? usuario.DataNascimento : usuarioBanco.DataNascimento;


            this.repository.AtualizaUsuario(usuarioBanco);
            return await this.repository.SaveChangesAsync()
                     ? Ok("Usuario atualizado com sucesso")
                     : BadRequest("Erro ao atualizar usuario");
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id, Usuario usuario)
        {
            var usuarioBanco = await this.repository.BuscarUsuario(id);
            if (usuarioBanco == null) return NotFound("Usuario nao encontrado!");

            this.repository.DeletaUsuario(usuarioBanco);
            return await this.repository.SaveChangesAsync()
                            ? Ok("Usuario deletado com sucesso")
                            : BadRequest("Erro ao deletar usuario");
        }
    }

}