using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Security.Cryptography;

namespace Kame.Management.Core.Entity
{
    public class User
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public int Profile { get; set; }

        public static string CreateSha512Password(string password)
        {
            SHA512 sha1 = SHA512.Create();
            return Convert.ToBase64String(sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
        }
    }

    public class UserProfile
    { 
        public const int User = 0;
        public const int Administrator = 1;
    }
}
