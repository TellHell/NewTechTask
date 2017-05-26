using DBModel.Interfaces;
using System;
using System.Collections.Generic;
using DBModel.Models;
using NHibernate;
using DBModel.Helpers;
using System.Linq;
using NHibernate.Linq;

namespace DBModel.Managers
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<User>().List<User>();
            }
        }

        public void Save(User entity)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }

        public bool Validate(string userName, string password)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<User>()
                              .Where(user => user.UserName == userName && user.Password == password)
                              .SingleOrDefault() != null;
            }
        }
    }
}
