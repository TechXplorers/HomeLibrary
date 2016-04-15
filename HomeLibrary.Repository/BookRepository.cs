using HomeLibrary.Common.Contracts;
using HomeLibrary.Common.Dto;
using System.Linq;

namespace HomeLibrary.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly string jsonFile = "books.json";
        private readonly JsonHelper jsonHelper;

        public BookRepository(JsonHelper jsonHelper)
        {
            this.jsonHelper = jsonHelper;
        }

        public Book GetById(int id)
        {
            return 
                jsonHelper.ReadFromJson<Book>(jsonFile).FirstOrDefault(x => x.Id == id);
        }

        public Book[] GetAll()
        {
            return jsonHelper.ReadFromJson<Book>(jsonFile);
        }

        public Book Create(Book book)
        {
            var books = jsonHelper.ReadFromJson<Book>(jsonFile).ToList();
            var maxId = books.Any() ? books.Max(x => x.Id) : 0;

            book.Id = maxId + 1;
            books.Add(book);

            jsonHelper.WriteAsJson(jsonFile, books.ToArray());
            
            return book;
        }
    }
}
