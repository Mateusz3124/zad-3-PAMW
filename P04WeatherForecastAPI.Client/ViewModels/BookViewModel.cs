using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services.WeatherServices;
using P06.Shared.Services.BookService;
using P06.Shared.Books;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public partial class BookViewModel : ObservableObject
    {
        private readonly IBookService _bookService;

        public ObservableCollection<Book> visibleBooks { get; set; }
        private int page;
        private int current;
        private List<List<Book>> books;

        [ObservableProperty]
        private string shownPage;

        public BookViewModel(IBookService bookService)
        {
            _bookService = bookService;
            visibleBooks = new ObservableCollection<Book>();
            books = new List<List<Book>>();
            page = 0;
            current = 0;
            shownPage = "0";
        }

        public async void GetProducts()
        {
            visibleBooks.Clear();
            books.Clear();
            page = 0;
            ShownPage = "0";
            current = 0;
            var BookResult = await _bookService.ReadBooksAsync();
            int count = 0;
            books.Add(new List<Book>());
            if (BookResult.Success)
            {
                foreach (var p in BookResult.Data)
                {
                    count++;
                    if (count == 6)
                    {
                        count = 1;
                        page++;
                        books.Add(new List<Book>());
                    }
                    books[page].Add(p);
                }
            }
            page++;
            foreach (Book b in books[0])
            {
                visibleBooks.Add(b);
            }
        }

        [RelayCommand]
        public void reloadRight()
        {
            visibleBooks.Clear();
            current++;
            if (current > page -1)
            {
                current = 0;
            }
            foreach (Book b in books[Math.Abs(current%page)])
            {
                visibleBooks.Add(b);
            }
            ShownPage = current.ToString();
        }
        [RelayCommand]
        public void reloadLeft()
        {
            visibleBooks.Clear();
            current--;
            if(current < 0)
            {
                current = page - 1;
            }
            foreach (Book b in books[Math.Abs(current % page)])
            {
                visibleBooks.Add(b);
            }
            ShownPage = current.ToString();
        }

    }
}
