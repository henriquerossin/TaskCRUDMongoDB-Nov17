using CRUDCompletoMongoDB.Repositories;

namespace CRUDCompletoMongoDB
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            BookRepository book = new BookRepository();
            AuthorRepository author = new AuthorRepository();

            int num = 0;

            do
            {
                Console.WriteLine("---Books and Authors---");
                Console.WriteLine("1 - Author's Menu");
                Console.WriteLine("2 - Book's Menu");
                Console.WriteLine("3 - Exit");
                num = int.Parse(Console.ReadLine()!);

                switch (num)
                {
                    case 1:
                        await author.AuthorMenu();
                        break;
                    case 2:
                        await book.BookMenu();
                        break;
                    case 3:
                        Console.WriteLine("Closing System. Thank you!");
                        break;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
            while (num != 3);
        }
    }
}
