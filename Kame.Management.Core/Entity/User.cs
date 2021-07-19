using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Kame.Management.Core.Entity
{
    public class User
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }
    }
}
