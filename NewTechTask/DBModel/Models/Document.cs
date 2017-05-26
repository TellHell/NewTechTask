using System;

namespace DBModel.Models
{
    public class Document
    {
        public virtual long Id { get; set; }

        public virtual string DocumentName { get; set; }

        public virtual User Author { get; set; }

        public virtual DateTime DocumentAddDate { get; set; }

        public virtual string LinkToUploadedFile { get; set; }

        public virtual string FileName { get; set; }
    }
}
