using ControleDeContatos.Data;
using ControleDeContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Repositorio
{
    public class UsuarioRepositorio
    {
        private readonly ControleDeContatosContext _context;

        public UsuarioRepositorio(ControleDeContatosContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> TrazerTodos()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> BuscarPorId(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Adicionar(Usuario usuario)
        {

            await _context.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Usuario usuario)
        {
            bool obj = await _context.Usuarios.AnyAsync(x => x.Id == usuario.Id);
            if (!obj)
            {
                throw new Exception();
            }
            try
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }
        public async Task Excluir(int id)
        {
            var obj = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> BuscarLogin(string login)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Login.ToUpper() == login.ToUpper());
        }

    }
}
