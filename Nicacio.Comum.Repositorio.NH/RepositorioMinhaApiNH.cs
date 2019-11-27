using NHibernate;
using Nicacio.Comum.Repositorio.Interfaces;
using Nicacio.MinhaApi.AcessoDados.NH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nicacio.Comum.Repositorio.NH
{
	public abstract class RepositorioMinhaApiNH<TDominio, TKey> : IRepositorioMinhaApi<TDominio, TKey>
		where TDominio : class
	{
		public void Delete(TDominio dominio)
		{
			using (ISession session = FluentySessionFactory.AbrirSession())
			{
				using (ITransaction transaction = session.BeginTransaction())
				{
					try
					{
						//session.Merge(dominio);
						session.Delete(dominio);
						transaction.Commit();
					}
					catch (Exception)
					{
						if (!transaction.WasCommitted)
							transaction.Rollback();
						throw;
					}
				}

			}
		}

		public void DeleteById(TKey id)
		{
			TDominio dominio = GetById(id);
			Delete(dominio);
		}

		public List<TDominio> GetAll(Expression<Func<TDominio, bool>> expression = null)
		{
			using (ISession session = FluentySessionFactory.AbrirSession())
			{
				if (expression != null)
					return session.Query<TDominio>().Where(expression).ToList();
				return session.Query<TDominio>().ToList();		
   }
		}

		public TDominio GetById(TKey id)
		{
			using (ISession session = FluentySessionFactory.AbrirSession())
			{
				return session.Get<TDominio>(id);
			}
		}

		public void Inserir(TDominio dominio)
		{

			using (ISession session = FluentySessionFactory.AbrirSession())
			{
				using (ITransaction transaction = session.BeginTransaction())
				{
					try
					{
						session.Save(dominio);
						transaction.Commit();
					}
					catch (Exception)
					{
						if (!transaction.WasCommitted)
							transaction.Rollback();
						throw;
					}
				}

			}
		}

		public void Update(TDominio dominio)
		{
			using (ISession session = FluentySessionFactory.AbrirSession())
			{
				using (ITransaction transaction = session.BeginTransaction())
				{
					try
					{
						//session.Merge(dominio);
						session.Update(dominio);
						transaction.Commit();
					}
					catch (Exception)
					{
						if (!transaction.WasCommitted)
							transaction.Rollback();
						throw;
					}
				}

			}
		}
	}
}
