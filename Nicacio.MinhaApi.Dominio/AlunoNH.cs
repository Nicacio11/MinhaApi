using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nicacio.MinhaApi.Dominio
{
	public class AlunoNH
	{
		public virtual int Id { get; set; }
		public virtual string Nome { get; set; }
		public virtual string Endereco { get; set; }
		public virtual decimal Mensalidade { get; set; }
	}
}
