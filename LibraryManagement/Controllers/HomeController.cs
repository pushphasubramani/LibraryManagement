using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller 
    {

        List<BookModel> Books = new List<BookModel>();
        Dictionary<string, string> BooksDictionary = new Dictionary<string, string>();
        Dictionary<string, string> modifiedDictionary = new Dictionary<string, string>();
        Dictionary<string, string> tempDictionary = new Dictionary<string, string>();
        Dictionary<string, string> finalDictionary = new Dictionary<string, string>();

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult LendaBook()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("C:/Users/ADMIN/Desktop/Pooja-Personal/LibraryManagement/LibraryManagement/Books.xml");

            foreach (XmlNode node in doc.SelectNodes("/catalog/book"))
            {
                Books.Add(new BookModel
                {
                    author = node.SelectSingleNode("author").InnerText.Trim(),
                    title = node.SelectSingleNode("title").InnerText.Trim(),
                    genre = node.SelectSingleNode("genre").InnerText.Trim(),
                    description = node.SelectSingleNode("description").InnerText.Trim(),
                });
            }

            var bookTitleList = new List<string> ();
            int i= 0;
            int totalBooks = Books.Count-1;
            foreach (var ps in Books)
            {
                if (i< totalBooks) 
                {
                    i++;
                    bookTitleList.Add(Books[i].title); 
                }
            }
            ViewBag.list=bookTitleList;

            string[] studentnames = { "Rafael", "Jane", "Nikolina" };
            ViewBag.list1 = studentnames;
            return View();
        }

        [HttpPost]
        public ActionResult LendaBook(BookModel values)
        {
            int j = 0;
            string author= string.Empty;
            string Title=string.Empty;
            string description= string.Empty;
            string genre= string.Empty;


            if (ModelState.IsValid)
            {
                int checker = 0;
                LendaBook();
                string lender = string.Empty;
           

                if (TempData["myDictionary"] != null)
                {
                    ViewBag.myDictionary = TempData["myDictionary"];

                    finalDictionary = ViewBag.myDictionary;
                    TempData.Keep("myDictionary");

                    foreach (KeyValuePair<string, string> dict in finalDictionary) 
                    {
                        checker ++;
                        if (dict.Key == values.title)
                        {
                            checker = checker - 1;

                            if (!string.IsNullOrEmpty(finalDictionary[values.title]))
                            {
                                string lenderPerson = finalDictionary[values.title];
                                ViewBag.Message = "SORRY!, "+ "\n" + " \n" + " \n" + " \n"+"Book is availed by" + "\n" + " \n" + " \n" + " \n" + lenderPerson;
                            }

                        }

                    }
                }

                if (string.IsNullOrEmpty(ViewBag.Message) )
                    {
                    foreach (var tempn in Books) //dictionary used to store the book title & lender name
                    {
                        if (tempn.title == values.title) { lender = values.StudentName; }
                        else { lender = string.Empty; }
                        BooksDictionary.Add(tempn.title, lender);
                    }

                        TempData["myDictionary"] = BooksDictionary;
                        TempData.Keep("myDictionary");
           

                    XmlDocument doc = new XmlDocument();
                    doc.Load("C:/Users/ADMIN/Desktop/Pooja-Personal/LibraryManagement/LibraryManagement/Books.xml");

                    int totalBooks = Books.Count - 1;
                    TempData.Keep("myDictionary");

                    foreach (var den in Books)
                    {

                        j++;
                        if (den.title == values.title)
                        {
                            int bookbb = j - 1;
                            author = Books[bookbb].author;
                            Title = Books[bookbb].title;
                            description = Books[bookbb].description;
                            genre = Books[bookbb].genre;
                            ViewBag.Message = "BOOK AVAILABLE !!"+ "\n" +" \n"+" \n"+ " \n"+"Author:" + author + "\n" + " \n" + " \n" + " \n" + "Description:" + description + "\n" + " \n" + " \n" + " \n" +  "genre:" + genre + "\n" + " \n" + " \n" + " \n" + "Congratulations! You have successfully availed this book";

                        }



                    } }

            };

            return View();
            
        }

        [HttpGet]
        public ActionResult Contact()
        {

            LendaBook();
            return View();
        }

        [HttpPost]
        public ActionResult Contact(BookModel values)
        {
            if (ModelState.IsValid)
            {
                int counter = 0;
                LendaBook();
                string lender = string.Empty;
                ViewBag.myDictionary = TempData["myDictionary"];
                TempData.Keep("myDictionary");
                
                if (ViewBag.myDictionary!=null) {
                    foreach (KeyValuePair<string, string> dict in ViewBag.myDictionary) //dictionary used to store the book title & lender name
                    {
                        counter++;
                        if (dict.Key == values.title)
                        {
                            modifiedDictionary = ViewBag.myDictionary;
                            lender = String.Empty;
                            modifiedDictionary[values.title] = string.Empty;
                            TempData.Remove("myDictionary");
                            ViewBag.myDictionary = TempData["modifiedDictionary"];
                            TempData.Keep("myDictionary");
                            ViewBag.Message = "BOOK RETURNED SUCCESSFULLY!!";
                            break;
                        }
                        else { }

                    } 
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }

            return View();

        }

}
}