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
    }
}
