using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewTechTask.Models
{
    public class AddDocumentViewModel
    {
        [Required]
        [Display(Name = "Имя документа")]
        public string Name { get; set; }
    }
}