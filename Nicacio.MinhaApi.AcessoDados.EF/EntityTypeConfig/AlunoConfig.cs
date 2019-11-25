using Nicacio.MinhaApi.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nicacio.MinhaApi.AcessoDados.EF.EntityTypeConfig
{
	class AlunoConfig : EntityTypeConfiguration<Aluno>
	{
		public AlunoConfig()
		{
			ToTable("Aluno");
			HasKey(x => x.Id);
			Property(x => x.Id)
				.IsRequired()
				.HasColumnName("aln_int_id")
				.HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
			Property(x => x.Nome)
				.HasColumnName("aln_str_nome");
			Property(x => x.Endereco)
				.HasColumnName("aln_str_endereco");
			Property(x => x.Mensalidade)
				.HasColumnName("aln_dec_mensalidade");
		}
	}
}
