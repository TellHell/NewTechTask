using DBModel.Models;
using System.Collections.Generic;

namespace DBModel.Interfaces
{
    public interface IDocumentRepository : IBaseRepository<Document>
    {
        IEnumerable<Document> GetAllSearched(string searchText, string searchVariant);
    }
}
