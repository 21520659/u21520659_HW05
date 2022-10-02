using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW5.Models
{
    public class BorrowedBook
    {
        //Declare Variables 
        //Naming convention PDF
        public int BorrowID { get; set; }
        public int BookID { get; set; }
        public string TakenDate { get; set; }
        public string BroughtDate { get; set; }
        public string StudentName { get; set; }
    }
}