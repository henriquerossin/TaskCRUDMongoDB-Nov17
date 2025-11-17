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

            var collectionAuthors = database.GetCollection<Author>("Authors");
        }

        public void AddAuthor()
        {
            Console.Write("Insert the Author's name: ");
            var name = Console.ReadLine()!;
            Console.Write("Insert the Author's Country");
            var country = Console.ReadLine()!;

            _collectionAuthors.InsertOne(new Author(name,country));
        }

        public void AddAuthors()
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

            _collectionAuthors.InsertMany(authors);
        }

        public Author GetAuthor()
        {
            Console.Write("Insert the Author's name: ");
            var name = Console.ReadLine()!;

            return _collectionAuthors.Find(x => x.Name == name).FirstOrDefault();
        }

        public List<Author> GetAuthors()
        {
            return _collectionAuthors.Find(a => true).ToList();
        }

        public void UpdateAuthor()
        {
            Console.Write("Insert the Author's name: ");
            var name = Console.ReadLine()!;

            Console.Write("Insert the new Author's country: ");
            var countryName = Console.ReadLine()!;

            _collectionAuthors.UpdateOne(x => x.Name == name, Builders<Author>.Update.Set(x => x.Country, countryName));
        }

        public void DeleteAuthor()
        {
            Console.Write("Insert the Author that will be deleted: ");
            var name = Console.ReadLine()!;

            _collectionAuthors.DeleteOne(x => x.Name == name);
        }

        public void AuthorMenu()
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

                switch (num)
                {
                    case 1:
                        AddAuthor();
                        break;
                    case 2:
                        AddAuthors();
                        break;
                    case 3:
                        GetAuthor();
                        break;
                    case 4:
                        GetAuthors();
                        break;
                    case 5:
                        UpdateAuthor();
                        break;
                    case 6:
                        DeleteAuthor();
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
