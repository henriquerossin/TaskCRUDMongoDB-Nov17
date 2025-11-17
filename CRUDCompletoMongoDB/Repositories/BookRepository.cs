using CRUDCompletoMongoDB.Entities;
using MongoDB.Driver;

namespace CRUDCompletoMongoDB.Repositories
{
    internal class BookRepository
    {
        private readonly IMongoCollection<Book> _collectionBooks;
        private readonly AuthorRepository _authorRep;

        public BookRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("TaskCRUDMongoDB");

            _collectionBooks = database.GetCollection<Book>("Books");
            _authorRep = new AuthorRepository();
        }

        public async Task ShowAuthors()
        {
            var authors = await _authorRep.GetAuthors();
            Console.WriteLine("Authors list: ");
            foreach (var aut in authors)
            {
                Console.WriteLine($"ID: {aut.Id} | Name: {aut.Name}");
            }

            Console.WriteLine();
        }

        public async Task AddBook()
        {
            await ShowAuthors();

            Console.Write("Enter the Book's name: ");
            var name = Console.ReadLine()!;
            Console.Write("Enter the Book's Author Id: ");
            var authorId = Console.ReadLine()!;

            await _collectionBooks.InsertOneAsync(new Book(name, authorId));
        }

        public async Task AddBooks()
        {

            await ShowAuthors();

            Console.Write("How many books will be added: ");
            var num = int.Parse(Console.ReadLine()!);

            var books = new List<Book>();

            for (int i = 0; i < num; i++)
            {
                Console.Write("Enter the Book's name: ");
                var name = Console.ReadLine()!;
                Console.Write("Enter the Book's Author Id: ");
                var authorId = Console.ReadLine()!;
                Console.WriteLine();

                books.Add(new Book(name, authorId));
            }

            await _collectionBooks.InsertManyAsync(books);
        }

        public async Task<Book> GetBook()
        {
            Console.Write("Enter the Book's title: ");
            var title = Console.ReadLine()!;

            var pointer = await _collectionBooks.FindAsync(x => x.Title == title);

            var book = await pointer.FirstOrDefaultAsync();

            return book;
        }

        public async Task<List<Book>> GetBooks()
        {
            var pointer = await _collectionBooks.FindAsync(a => true);
            var books = await pointer.ToListAsync();

            return books;
        }

        public async Task UpdateBook()
        {
            Console.Write("Enter the Book's title: ");
            var title = Console.ReadLine()!;

            Console.Write("Enter the new Book's title: ");
            var newName = Console.ReadLine()!;

            await _collectionBooks.UpdateOneAsync(x => x.Title == title, Builders<Book>.Update.Set(x => x.Title, newName));
        }

        public async Task DeleteBook()
        {
            Console.Write("Enter the Book to be deleted: ");
            var title = Console.ReadLine()!;

            await _collectionBooks.DeleteOneAsync(x => x.Title == title);
        }

        public async Task BookMenu()
        {
            int num = 0;

            Console.WriteLine("Book's Menu");
            do
            {
                Console.WriteLine("1 - Add one new Book: ");
                Console.WriteLine("2 - Add many new Books: ");
                Console.WriteLine("3 - Get one Book: ");
                Console.WriteLine("4 - Get all the Books: ");
                Console.WriteLine("5 - Update one Book: ");
                Console.WriteLine("6 - Delete one Book: ");
                Console.WriteLine("7 - Quit.");
                num = int.Parse(Console.ReadLine()!);

                switch (num)
                {
                    case 1:
                        await AddBook();
                        break;
                    case 2:
                        await AddBooks();
                        break;
                    case 3:
                        await GetBook();
                        break;
                    case 4:
                        await GetBooks();
                        break;
                    case 5:
                        await UpdateBook();
                        break;
                    case 6:
                        await DeleteBook();
                        break;
                    case 7:
                        Console.WriteLine("Exiting system.");
                        break;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            } while (num != 7);
        }
    }
}
