using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ControleDeContatos.Models;

namespace ControleDeContatos.Data
{
    public class ControleDeContatosContext : DbContext
    {
        public ControleDeContatosContext(DbContextOptions<ControleDeContatosContext> options)
            : base(options)
        {
        }

        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ControleDeContatos.Models.LoginModel>? LoginModel { get; set; }
        
    }
}
