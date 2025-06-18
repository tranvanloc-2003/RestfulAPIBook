namespace RestfulAPIBook.Models.Dto.Book
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string FeaturedImage { get; set; }
        public string Author { get; set; }
        public DateTime PublicshedDate { get; set; }
        public string UrlHandle { get; set; }
        public int Price { get; set; }
        public bool IsVisible { get; set; }
    }
}
