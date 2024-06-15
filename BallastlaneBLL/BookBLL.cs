using BallastlaneDAL;
using BallastlaneDAL.Interfaces;
using BallastlaneDAL.Models;
using Microsoft.Extensions.Configuration;

namespace BallastlaneBLL
{
    public class BookBLL
    {
        private readonly ICrud<Book> BookDal;

        public BookBLL(IConfiguration configuration)
        {
            this.BookDal = new BookDAL(configuration);
        }

        public void Create(string name, string author, int year)
        {
            var Book = new Book()
            {
                Name = name,
                Author = author,
                Year = year,
            };

            BookDal.Create(Book);
        }

        public IEnumerable<Book> Read()
        {
            return BookDal.Read(u => true);
        }

        public void Update(string bookId, string name, string author, int year)
        {
            var Book = new Book()
            {
                BookId = bookId,
                Name = name,
                Author = author,
                Year = year
            };

            BookDal.Update(Book);
        }

        public void Delete(string bookId)
        {
            var Book = new Book()
            {
                BookId = bookId
            };

            BookDal.Delete(Book);
        }
    }
}
