﻿using System.Collections.Generic;

namespace my_books.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public List<Book_Author> Book_Authors { get; set; }
    }
}
