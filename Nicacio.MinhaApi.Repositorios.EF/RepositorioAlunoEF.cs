using Nicacio.Comum.Repositorio.EF;
using Nicacio.MinhaApi.AcessoDados.EF;
using Nicacio.MinhaApi.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nicacio.MinhaApi.Repositorio.EF
{
	public class RepositorioAlunoEF : RepositorioMinhaApiEF<Aluno, int>
	{
		public RepositorioAlunoEF(Contexto contexto) : base(contexto)
		{
		}
	}
}
