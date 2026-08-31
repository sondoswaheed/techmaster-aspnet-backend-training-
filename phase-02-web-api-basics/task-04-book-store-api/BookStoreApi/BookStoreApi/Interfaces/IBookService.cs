using BookStoreApi.DTOs;

namespace BookStoreApi.Interfaces
{
    public interface IBookService
    {
        BookResponse GetById(int id);
        BookResponse Create(CreateBookRequest request);
        BookResponse Update(int id,UpdateBookRequest request);
        bool Delete(int id);
        List<BookResponse> GetAll(BookSearchRequest request);
        BookReportResponse GetReports();

    }
}
