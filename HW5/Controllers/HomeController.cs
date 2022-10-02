using HW5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HW5.Controllers
{
    public class HomeController : Controller
    {
        //Step 1: Create Service Model 
        private Service Service = new Service();
        public ActionResult Index()
        {
            
            BooksListVM booksListVM = new BooksListVM();
            booksListVM.Books = Service.GetAllBooks();
            booksListVM.Authors = Service.GetAllAuthors();
            booksListVM.Types = Service.GetAllTypes();
            return View(booksListVM);
        }
        //Step : Service Model Returned 
        public ActionResult BookDetails(int id)
        {
            //Step3: Create BookDetails Model and return information 
            BookDetailsVM bookDetailsVM = new BookDetailsVM();  
            bookDetailsVM.BorrowedBooks = Service.GetAllBorowedBooks(id);
            bookDetailsVM.Book = Service.GetAllBooks().Where(b => b.ID == id).FirstOrDefault();
            return View(bookDetailsVM);
        }
        public ActionResult Search( int type = 0, int author = 0, string name = null)
        {
            //Step 4: Create BookLists Model and return information 
            BooksListVM booksListVM = new BooksListVM();
            booksListVM.Books = Service.Search(name,type, author);
            booksListVM.Authors = Service.GetAllAuthors();
            booksListVM.Types = Service.GetAllTypes();               
            return View("Index", booksListVM);
        }

        public ActionResult Students(int bookId)
        {
            //Add list for Students from database 
           //Display whether book is borrowed/not borrowed
            StudentsListVM studentsListVM = new StudentsListVM();
            List<Student> students = Service.GetAllStudents();
            List<BorrowedBook> books = Service.GetAllBorowedBooks(bookId);
            foreach(var student in students)
            {
                for (int i = 0; i < books.Count(); i++)
                {
                    string name = student.Name + " " + student.Surname;
                    if (books[i].StudentName == name && (books[i].BroughtDate == "" || books[i].BroughtDate == null))
                    {
                        student.Book = true;
                    }
                    else
                    {
                        student.Book = false;
                             
                    }
                }   
            }
            //Create Students Model and return information. 
            studentsListVM.Students = students;
            studentsListVM.Book = Service.GetAllBooks().Where(b => b.ID == bookId).FirstOrDefault();
            studentsListVM.Class = Service.GetAllClases();
            return View(studentsListVM);
        }

        //Create BookDetails View Model and return information to display 
     
        public ActionResult ReturnBook(int bookId, int studentId)
        {
            Service.ReturnBook(bookId, studentId);

            BookDetailsVM bookDetailsVM = new BookDetailsVM();
            bookDetailsVM.BorrowedBooks = Service.GetAllBorowedBooks(bookId);
            bookDetailsVM.Book = Service.GetAllBooks().Where(b => b.ID == bookId).FirstOrDefault();
            return View("BookDetails", bookDetailsVM);
            
        }

        //Create BorrowedBooks model and return the information to display 
        public ActionResult BorrowBook(int bookId, int studentId)
        {
            Service.BorrowBook(bookId, studentId);
            BookDetailsVM bookDetailsVM = new BookDetailsVM();
            bookDetailsVM.BorrowedBooks = Service.GetAllBorowedBooks(bookId);
            bookDetailsVM.Book = Service.GetAllBooks().Where(b => b.ID == bookId).FirstOrDefault();
            return View("BookDetails", bookDetailsVM);
        }
        
        public ActionResult StudentSearch(int bookId, string name = "none", string _class = "none")
        {
            //Add Student List from database 
            //Display Student List 
            StudentsListVM studentsListVM = new StudentsListVM
            {
                Students = Service.SearchStudent(name, _class),
                Book = Service.GetAllBooks().Where(b => b.ID == bookId).FirstOrDefault(),
                Class = Service.GetAllClases()

            };
            return View("Students", studentsListVM);
        }

        //Information can be null 

    }
}