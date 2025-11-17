using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CRUDCompletoMongoDB.Entities
{
    internal class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? AuthorId { get; set; }

        public Book(string title, string authorId)
        {
            Title = title;
            AuthorId = authorId;
        }

        public override string? ToString()
        {
            return
                $"Id: {Id}" +
                $"\nTitle: {Title}" +
                $"\nAuthor Id: {AuthorId}";
        }
    }
}
