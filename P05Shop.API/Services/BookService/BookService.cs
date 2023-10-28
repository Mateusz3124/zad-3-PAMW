using P06.Shared;
using P06.Shared.Services.BookService;
using P06.Shared.Books;
using P07Book.DataSeeder;

namespace P05Shop.API.Services.BookService
{
    public class BookService : IBookService
    {
        public async Task<ServiceResponse<string>> CreateBookAsync(Book book)
        {
            try
            {
                List<Book> books = BookSeeder.GenerateBookData();
                Book result = new Book();
                foreach (Book checkedBook in books)
                {
                    if (checkedBook.Id == book.Id)
                    {
                        return new ServiceResponse<string>()
                        {
                            Data = null,
                            Message = "Book with this id already exists",
                            Success = false,
                            CodeError = 400
                        };
                    }
                }
                var response = new ServiceResponse<string>()
                {
                    Data = "Book added",
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<string>()
                {
                    Data = null,
                    Message = "Problem with Database",
                    Success = false,
                    CodeError = 500
                };
            }
        }

        public async Task<ServiceResponse<string>> CreateMultipleBookAsync(List<Book> book)
        {
            try
            {
                List<Book> generatedBooks = BookSeeder.GenerateBookData();
                Book result = new Book();
                var hs = new HashSet<int>();
                bool areAllGivenBookUnique = book.All(x => hs.Add(x.Id));
                if (!areAllGivenBookUnique)
                {
                    return new ServiceResponse<string>()
                    {
                        Data = null,
                        Message = "Tries to add books with same id",
                        Success = false,
                        CodeError = 400
                    };
                }
                foreach (Book checkedBook in generatedBooks)
                {
                    foreach (Book instanceBook in book) 
                    {
                        if (checkedBook.Id == instanceBook.Id)
                        {
                            return new ServiceResponse<string>()
                            {
                                Data = null,
                                Message = "Book with this id already exists",
                                Success = false,
                                CodeError = 400
                            };
                        }
                    }
                }
                var response = new ServiceResponse<string>()
                {
                    Data = "Books added",
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<string>()
                {
                    Data = null,
                    Message = "Problem with Database",
                    Success = false,
                    CodeError = 500
                };
            }
        }

        public async Task<ServiceResponse<string>> DeleteBookAsync(int id)
        {
            try
            {
                List<Book> books = BookSeeder.GenerateBookData();
                Book result = new Book();
                foreach (Book checkedBook in books)
                {
                    if (checkedBook.Id == id)
                    {
                        result = checkedBook;
                    }
                }
                if (result.Author == null)
                {
                    return new ServiceResponse<string>()
                    {
                        Data = null,
                        Message = "Book with this id doesn't exist",
                        Success = false,
                        CodeError = 400
                    };
                }

                var response = new ServiceResponse<string>()
                {
                    Data = "Deleted Book",
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<string>()
                {
                    Data = null,
                    Message = "Problem with Database",
                    Success = false,
                    CodeError = 500
                };
            }
        }

        public async Task<ServiceResponse<List<Book>>> ReadBooksAsync()
        {
            try
            {
                var response = new ServiceResponse<List<Book>>()
                {
                    Data = BookSeeder.GenerateBookData(),
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<List<Book>>()
                {
                    Data = null,
                    Message = "Problem with dataseeder library",
                    Success = false,
                    CodeError = 500
                };
            }

        }

        public async Task<ServiceResponse<Book>> ReadByIdBooksAsync(int id)
        {
            try
            {
                List<Book> books = BookSeeder.GenerateBookData();
                Book result = new Book();
                foreach(Book book in books)
                {
                    if(book.Id == id)
                    {
                        result = book;
                    }
                }
                if(result.Author == null)
                {
                    return new ServiceResponse<Book>()
                    {
                        Data = null,
                        Message = "Book with this id doesn't exist",
                        Success = false,
                        CodeError = 400
                    };
                }
                var response = new ServiceResponse<Book>()
                {
                    Data = result,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<Book>()
                {
                    Data = null,
                    Message = "Problem with Database",
                    Success = false,
                    CodeError = 500
                };
            }
        }

        public async Task<ServiceResponse<string>> UpdateBookAsync(int id, Book book)
        {
            try
            {
                List<Book> books = BookSeeder.GenerateBookData();
                Book result = new Book();
                foreach (Book checkedBook in books)
                {
                    if (checkedBook.Id == id)
                    {
                        result = checkedBook;
                    }
                    if (checkedBook.Id == book.Id)
                    {
                        if (checkedBook.Id != id) 
                        {
                            return new ServiceResponse<string>()
                            {
                                Data = null,
                                Message = "Book with this id already exists",
                                Success = false,
                                CodeError = 400
                            };
                        }

                    }
                }
                if (result.Author == null)
                {
                    return new ServiceResponse<string>()
                    {
                        Data = null,
                        Message = "Book with this id doesn't exist",
                        Success = false,
                        CodeError = 400
                    };
                }

                var response = new ServiceResponse<string>()
                {
                    Data = "Updated info",
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<string>()
                {
                    Data = null,
                    Message = "Problem with Database",
                    Success = false,
                    CodeError = 500
                };
            }
        }
    }
}
