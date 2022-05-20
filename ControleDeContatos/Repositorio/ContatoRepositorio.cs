using ControleDeContatos.Data;
using ControleDeContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Repositorio
{
    public class ContatoRepositorio
    {
        private readonly ControleDeContatosContext _context;

        public ContatoRepositorio(ControleDeContatosContext context)
        {
            _context = context;
        }

        public async Task Adicionar(Contato contato)
        {
            _context.Add(contato);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Contato>> BuscarTodos()
        {
            return await _context.Contatos.ToListAsync();
        }

        public async Task<Contato> ListaPorId(int id)
        {
            return await _context.Contatos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Altualizar(Contato obj)
        {
            bool contatoDB = await _context.Contatos.AnyAsync(x => x.Id == obj.Id);
            if (!contatoDB)
            {
                throw new Exception();
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }

        public async Task Excluir(int id)
        {
            var obj = await _context.Contatos.FindAsync(id);
            _context.Contatos.Remove(obj);
            await _context.SaveChangesAsync();
        }
    }
}
