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
    public class DocumentRepository : IDocumentRepository
    {
        public IEnumerable<Document> GetAll()
        {
            //var result = new List<Document>();

            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<Document>().List<Document>();
            }

            
        }

        public IEnumerable<Document> GetAllSearched(string searchText, string searchVariant)
        {
            List<Document> searchedDocuments = new List<Document>();

            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (searchVariant == "DocumentName")
                    searchedDocuments = session.Query<Document>().Where(x => x.DocumentName.Contains(searchText)).ToList();
                else if (searchVariant == "Author")
                    searchedDocuments = session.Query<Document>().Where(x => x.Author.UserName.Contains(searchText)).ToList();
                else if (searchVariant == "DocumentAddDate")
                    searchedDocuments = session.Query<Document>().Where(x => x.DocumentAddDate.ToString().Contains(searchText)).ToList();
                
                return searchedDocuments;
            }
        }

        public void Save(Document entity)
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
    }
}
