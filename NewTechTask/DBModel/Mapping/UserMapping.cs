using DBModel.Models;
using FluentNHibernate.Mapping;

namespace DBModel.Mapping
{
    public class UserMapping : ClassMap<User>
    {
        public UserMapping()
        {
            Id(x => x.Id);
            Map(x => x.UserName);
            Map(x => x.Password);
            HasMany(x => x.Documents).Inverse();
            Table("dbo.Users");
        }
    }
}
