using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nicacio.Comum.Repositorio.Interfaces
{
    public interface IRepositorioMinhaApi<TDominio, TKey>
		where TDominio : class
    {
		List<TDominio> GetAll(Expression<Func<TDominio, bool>> expression = null);
		TDominio GetById(TKey id);
		void Inserir(TDominio dominio);
		void Update(TDominio dominio);
		void Delete(TDominio dominio);
		void DeleteById(TKey id);
 }
}
