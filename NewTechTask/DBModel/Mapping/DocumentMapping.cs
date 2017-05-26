using DBModel.Models;
using FluentNHibernate.Mapping;

namespace DBModel.Mapping
{
    public class DocumentMapping : ClassMap<Document>
    {
        public DocumentMapping()
        {
            Id(x => x.Id);
            Map(x => x.DocumentName);
            References(x => x.Author).Cascade.SaveUpdate().Fetch.Join(); 
            Map(x => x.DocumentAddDate);
            Map(x => x.LinkToUploadedFile);
            Map(x => x.FileName);
            Table("dbo.Documents");
        }
    }
}
