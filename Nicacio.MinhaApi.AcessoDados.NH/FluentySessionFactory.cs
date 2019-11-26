using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nicacio.MinhaApi.AcessoDados.NH
{
	public class FluentySessionFactory : IDisposable
	{
		//public ISession session;
		//public ITransaction Transaction;
		private static string ConnectionString = "";
		private static ISessionFactory SessionFactory;

		public FluentySessionFactory()
		{
			SessionFactory = CreateSessionFactory();
			//session = SessionFactory.OpenSession();
			//Transaction = session.BeginTransaction();

		}
		private static ISessionFactory CreateSessionFactory()
		{
			if (SessionFactory != null)
				return SessionFactory;
			var pathScriptBanco = System.Configuration.ConfigurationManager.AppSettings["PathScriptDataBase"];
			var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["defaultNH"].ConnectionString;

			var configMap = Fluently.Configure().Database(MsSqlConfiguration.MsSql2008
				.ConnectionString(c => c.FromConnectionStringWithKey("defaultNH")).ShowSql())
				 .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
				 .ExposeConfiguration(cfg => new SchemaExport(cfg).SetOutputFile(pathScriptBanco.ToString())
				 .Execute(true, true, false));

			SessionFactory = configMap.BuildSessionFactory();
			return SessionFactory;
		}
		public static ISession AbrirSession()
		{
			return CreateSessionFactory().OpenSession();
		}

		public void Dispose()
		{
			if (SessionFactory != null)
			{
				SessionFactory.Dispose();
			}
		}
	}
}
