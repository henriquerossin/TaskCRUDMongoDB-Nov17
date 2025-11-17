using CRUDCompletoMongoDB.Entities;
using MongoDB.Driver;

namespace CRUDCompletoMongoDB.Repositories
{
    internal class AuthorRepository
    {
        private readonly IMongoCollection<Author> _collectionAuthors;

        public AuthorRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("TaskCRUDMongoDB");

            _collectionAuthors = database.GetCollection<Author>("Authors");
        }

        public async Task AddAuthor()
        {
            Console.Write("Insert the Author's name: ");
            var name = Console.ReadLine()!;
            Console.Write("Insert the Author's Country");
            var country = Console.ReadLine()!;

            await _collectionAuthors.InsertOneAsync(new Author(name,country));
        }

        public async Task AddAuthors()
        {
            Console.Write("How many authors will be added: ");
            var num = int.Parse(Console.ReadLine()!);

            var authors = new List<Author>();

            for (int i = 0; i < num; i++)
            {
                Console.Write("Insert the Author's name: ");
                var name = Console.ReadLine()!;
                Console.WriteLine("Insert the Author's Country");
                var country = Console.ReadLine()!;
                Console.WriteLine();

                authors.Add(new Author(name, country));
            }

            await _collectionAuthors.InsertManyAsync(authors);
        }

        public async Task<Author> GetAuthor()
        {
            Console.Write("Insert the Author's name: ");
            var name = Console.ReadLine()!;

            var pointer = await _collectionAuthors.FindAsync(x => x.Name == name);

            var author = await pointer.FirstOrDefaultAsync();

            return author;
        }

        public async Task<List<Author>> GetAuthors()
        {
            var pointer = await _collectionAuthors.FindAsync(a => true);
            var authors = await pointer.ToListAsync();

            return authors;
        }

        public async Task UpdateAuthor()
        {
            Console.Write("Insert the Author's name: ");
            var name = Console.ReadLine()!;

            Console.Write("Insert the new Author's country: ");
            var countryName = Console.ReadLine()!;

            await _collectionAuthors.UpdateOneAsync(x => x.Name == name, Builders<Author>.Update.Set(x => x.Country, countryName));
        }

        public async Task DeleteAuthor()
        {
            Console.Write("Insert the Author that will be deleted: ");
            var name = Console.ReadLine()!;

            await _collectionAuthors.DeleteOneAsync(x => x.Name == name);
        }

        public async Task AuthorMenu()
        {
            int num = 0;

            Console.WriteLine("Author's Menu");
            do
            {
                Console.WriteLine("1 - Add one new Author: ");
                Console.WriteLine("2 - Add many new Authors: ");
                Console.WriteLine("3 - Get one Author: ");
                Console.WriteLine("4 - Get all the Authors: ");
                Console.WriteLine("5 - Update one Author: ");
                Console.WriteLine("6 - Delete one Author: ");
                Console.WriteLine("7 - Leave.");
                num = int.Parse(Console.ReadLine()!);

                switch (num)
                {
                    case 1:
                        await AddAuthor();
                        break;
                    case 2:
                        await AddAuthors();
                        break;
                    case 3:
                        await GetAuthor();
                        break;
                    case 4:
                        await GetAuthors();
                        break;
                    case 5:
                        await UpdateAuthor();
                        break;
                    case 6:
                        await DeleteAuthor();
                        break;
                    case 7:
                        Console.WriteLine("Closing system.");
                        break;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            } while (num != 7);
        }
    }
}
