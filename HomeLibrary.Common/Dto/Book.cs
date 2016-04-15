using System.Collections.Generic;

namespace HomeLibrary.Common.Dto
{
    public class Book
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public List<string> Authors { get; set; }
        public Genre Genre { get; set; }
        public string Title { get; set; }
    }
}
