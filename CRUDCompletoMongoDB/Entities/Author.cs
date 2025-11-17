using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CRUDCompletoMongoDB.Entities
{
    internal class Author
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }

        public Author(string name, string country)
        {
            Name = name;
            Country = country;
        }

        public void CreateAuthor(string name, string country)
        {
            Console.Write("Insert the Author name: ");
            name = Console.ReadLine()!;

            Console.Write("Insert the Author country: ");
            country = Console.ReadLine()!;
        }

        public override string? ToString()
        {
            return
                $"Id: {Id}" +
                $"\nName: {Name}" +
                $"\nCountry: {Country}";
        }
    }
}
