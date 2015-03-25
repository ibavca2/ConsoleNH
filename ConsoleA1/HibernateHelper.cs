using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace ConsoleA1
{
    internal class HibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializerSessionFactory();
                return _sessionFactory;
            }
        }

        private static void InitializerSessionFactory()
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Белоногов\ConsoleA1\ConsoleA1\myLocal.mdf;Integrated Security=True")
                .ShowSql())
                 .Mappings(m =>
                    m.FluentMappings
                    .AddFromAssemblyOf<Car>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                .Create(true, true))
                .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
