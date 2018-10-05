using System;
using System.Collections.Generic;
using System.Text;

namespace BookService.Data
{
    public class Book
    {
        public Guid Id;
        public string Name;
        public string Description;
        public Guid StatusId;
        public DateTime? LastReadTime;
        public DateTime CreatedOn;

        public Status Status;
        public List<Author> Authors;
    }
}
