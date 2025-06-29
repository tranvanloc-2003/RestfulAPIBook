﻿namespace RestfulAPIBook.Models.Domain
{
    public class Categories
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlHandle { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
