using FluentNHibernate.Mapping;
using Nicacio.MinhaApi.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nicacio.MinhaApi.AcessoDados.NH.Mapping
{
	class AlunoMapping : ClassMap<AlunoNH>
	{
		public AlunoMapping()
		{
			Table("Aluno");
			Id(x => x.Id, "aln_int_id")
				.GeneratedBy.Identity();
			Map(x => x.Nome, "aln_str_nome");
			Map(x => x.Mensalidade, "aln_str_mensalidade");
			Map(x => x.Endereco, "aln_str_endereco");

		}
	}
}
