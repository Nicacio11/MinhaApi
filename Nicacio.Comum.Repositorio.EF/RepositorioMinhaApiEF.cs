using Nicacio.Comum.Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nicacio.Comum.Repositorio.EF
{
	public abstract class RepositorioMinhaApiEF<TDominio, TChave> : IRepositorioMinhaApi<TDominio, TChave>
		where TDominio : class
	{
		protected DbContext contexto;
		public RepositorioMinhaApiEF(DbContext contexto)
		{
			this.contexto = contexto;
		}
		public void Delete(TDominio dominio)
		{
			contexto.Set<TDominio>().Attach(dominio);
			contexto.Entry(dominio).State = EntityState.Deleted;
			contexto.SaveChanges();
		}

		public void DeleteById(TChave id)
		{
			TDominio dominio = GetById(id);
			Delete(dominio);
		}

		public virtual List<TDominio> GetAll(Expression<Func<TDominio, bool>> expression = null)
		{
			if(expression != null)
				return contexto.Set<TDominio>().Where(expression).ToList();
			return contexto.Set<TDominio>().ToList();
		}

		public virtual TDominio GetById(TChave id)
		{
			return contexto.Set<TDominio>().Find(id);
		}

		public void Inserir(TDominio dominio)
		{
			contexto.Set<TDominio>().Add(dominio);
			contexto.SaveChanges();
		}

		public virtual void Update(TDominio dominio)
		{
			contexto.Set<TDominio>().Attach(dominio);
			contexto.Entry(dominio).State = EntityState.Modified;
			contexto.SaveChanges();
		}
	}
}
