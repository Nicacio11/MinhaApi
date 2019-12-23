using Nicacio.MinhaApi.AcessoDados.EF.EntityTypeConfig;
using Nicacio.MinhaApi.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nicacio.MinhaApi.AcessoDados.EF
{
    public class Contexto : DbContext
    {
		public DbSet<Aluno> Alunos { get; set; }
		public Contexto() : base("ContextoJob")
		{
			Configuration.ProxyCreationEnabled = false;
			Configuration.LazyLoadingEnabled = false;
		}
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new AlunoConfig());
		}
	}
}
