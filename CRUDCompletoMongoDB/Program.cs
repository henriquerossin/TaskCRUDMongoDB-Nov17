using CRUDCompletoMongoDB.Entities;
using MongoDB.Driver;

namespace CRUDCompletoMongoDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("TaskCRUDMongoDB");

            var collectionAuthors = database.GetCollection<Author>("Authors");

            var collectionBooks = database.GetCollection<Book>("Books");

            // CRUD -> CREATE:

            // InsertOne
            var author = new Author("Stephen King", "United States");

            collectionAuthors.InsertOne(author);

            // InsertMany
            var authors = new List<Author>
            {
                new("Maria", "senha1"),
                new("João", "senha2")
            };

            collectionAuthors.InsertMany(authors);
            
            // CRUD -> READ 
            
            // Read One
            var usuario = collectionAuthors.Find(x => x.Name == "Stephen King").FirstOrDefault();

            // Read Many
            var authorsList = collectionAuthors.Find(x => true).ToList();

            foreach (var aut in authorsList)
            {
                Console.WriteLine(aut);
                Console.WriteLine("---");
            }

            //// CRUD - UPDATE (Usando Replace)

            //// -- Busca de objetos por parâmetro, FirstOrDefault pega a primeira ocorrencia.
            //var usuario = collection.Find(x => x.Id == "69173991c372800405907965").FirstOrDefault();

            //// -- Troca informações do usuário na aplicação, precisa empurrar as mudanças para o banco
            //usuario.Password = "456@mudar";

            //// -- Empurra as mudanças feitas na aplicacao para o banco com REPLACE, o que não foi mudado, volta para o usuário normalmente.
            //collection.ReplaceOne(x => x.Id == usuario.Id, usuario); 
            
            // CRUD - UPDATE (Usando Update)

            collectionAuthors.UpdateOne(x => x.Id == "69173991c372800405907965",Builders<Author>.Update.Set(x => x.Country, "Nárnia"));

            // CRUD - DELETE
            
            collectionAuthors.DeleteOne(x => x.Id == "69173991c372800405907965");
        }
    }
}
