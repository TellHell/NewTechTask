using DBModel.Models;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace DBModel.Helpers
{
    public class NHibernateHelper
    {
        public NHibernateHelper()
        {
            InitializeSesseionFacory();
        }

        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSesseionFacory();

                return _sessionFactory;
            }
        }

        private static void InitializeSesseionFacory()
        {
            _sessionFactory = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2008.ConnectionString(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Near\Docs\NewTechTask\NewTechTask\NewTechTask\DBModel\App_Data\TechTaskStorage.mdf"";Integrated Security=True").ShowSql())
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernateHelper>())
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
            .BuildSessionFactory();
        }

        /*public NHibernateHelper()
        {
            _sessionFactory = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2008.ConnectionString(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Near\Docs\NewTechTask\NewTechTask\NewTechTask\DBModel\App_Data\TechTaskStorage.mdf"";Integrated Security=True").ShowSql())
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernateHelper>())
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
            .BuildSessionFactory();
        }*/

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
