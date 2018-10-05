using System;
using System.Collections.Generic;
using System.Text;

namespace BookService.Data
{
    public class Author
    {
        public Guid Id;
        public string Name;
        public string Surname;
        public DateTime CreatedOn;

        public List<Book> Books;
    }
}
