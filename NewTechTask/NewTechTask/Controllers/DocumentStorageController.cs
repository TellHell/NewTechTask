using DBModel.Interfaces;
using DBModel.Managers;
using DBModel.Models;
using NewTechTask.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewTechTask.Controllers
{
    public class DocumentStorageController : Controller
    {
        #region
        private IDocumentRepository DocumentRepository { get; set; }

        private IUserRepository UserRepository { get; set; }

        private const string saveFilePath = @"C:\Near\Docs\NewTechTask\NewTechTask\NewTechTask\NewTechTask\App_Data\Content\Files\";

        public List<SelectListItem> searchVariants;
        #endregion

        public DocumentStorageController()
        {
            DocumentRepository = new DocumentRepository();
            UserRepository = new UserRepository();

            searchVariants = new List<SelectListItem>();
            searchVariants.Add(new SelectListItem { Text = "По имени документа", Value = "DocumentName", Selected = true });
            searchVariants.Add(new SelectListItem { Text = "По автору", Value = "Author", Selected = false });
            searchVariants.Add(new SelectListItem { Text = "По дате", Value = "DocumentAddDate", Selected = false });
            ViewData["SearchVariants"] = searchVariants;
        }

        private User GetCurrentUser()
        {
            return UserRepository.GetAll().FirstOrDefault(u => u.UserName == HttpContext.User.Identity.Name);
        }

        [HttpGet]
        public ActionResult AddDocument()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDocument(HttpPostedFileBase file, AddDocumentViewModel model)
        {
            if (file == null)
            {
                ModelState.AddModelError("", "Выберите файл");
                return View();
            }
            else
            {
                var document = new Document()
                {
                    DocumentName = model.Name,
                    Author = GetCurrentUser(),
                    DocumentAddDate = DateTime.Now,
                    LinkToUploadedFile = $"{Path.GetFileName(file.FileName)}{DateTime.Now.ToString().Replace(':','-')}{GetCurrentUser().Id.ToString()}",
                    FileName = Path.GetFileName(file.FileName)
                };

                file.SaveAs($"{saveFilePath}{document.LinkToUploadedFile}");

                DocumentRepository.Save(document);

                ModelState.AddModelError("", "Документ добавлен");

                return View();
            }
        }

        public ActionResult ListOfDocuments(SortState sortOrder = SortState.NameAsc)
        {
            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["AuthorSort"] = sortOrder == SortState.AuthorAsc ? SortState.AuthorDesc : SortState.AuthorAsc;
            ViewData["DateSort"] = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;

            var documents = DocumentRepository.GetAll();

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    documents = documents.OrderByDescending(s => s.DocumentName);
                    break;
                case SortState.AuthorAsc:
                    documents = documents.OrderBy(s => s.Author.UserName);
                    break;
                case SortState.AuthorDesc:
                    documents = documents.OrderByDescending(s => s.Author.UserName);
                     break;
                case SortState.DateAsc:
                    documents = documents.OrderBy(s => s.DocumentAddDate);
                    break;
                case SortState.DateDesc:
                    documents = documents.OrderByDescending(s => s.DocumentAddDate);
                    break;
                default:
                    documents = documents.OrderBy(s => s.DocumentName);
                    break;
            }
            return View(documents);
        }

        [HttpPost]
        public ActionResult ListOfDocuments(string SearchText)
        {
            var documents = DocumentRepository.GetAllSearched(SearchText, Request.Params["SearchVariants"]);

            return View(documents);
        }

        public FileResult Download(string link, string file)
        {
            string file_path = $"{saveFilePath}{link}";
            return File(file_path, System.Net.Mime.MediaTypeNames.Application.Octet, file);
        }
    }
}