using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class BookModel
    {
        [Required]
        public string StudentName { get; set; }

        public string author { get;set;}

        [Required]
        public string title { get;set;}
        
        public string genre { get; set; }
      

        public string description { get; set; }

        public string bookAvail { get; set; }
    }
}