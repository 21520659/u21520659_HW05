using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW5.Models
{
    public class BookDetailsVM
    {
        //Declare Variables
        public Book Book { get; set; }
        //Add List from database
        public List<BorrowedBook> BorrowedBooks { get; set; }
    }
}