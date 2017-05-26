using System.Collections.Generic;

namespace DBModel.Models
{
    public class User
    {
        /*public User()
        {
            Documents = new List<Document>();
        }*/
        private IList<Document> _documents;

        public virtual long Id { get; set; }

        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }

        public virtual IList<Document> Documents { get { return _documents ?? (_documents = new List<Document>()); } set { _documents = value; } }
    }
}
