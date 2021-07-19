using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kame.Management.Core.Entity;
using MongoDB.Bson;

namespace Kame.Management.Core.Services
{
    public class MongoDbContext : IKameDbContext
    {
        public const string DefaultDatabaseName = "KameDeploy";

        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static bool IsSSL { get; set; }

        private IMongoDatabase _database { get; set; }

        public MongoDbContext()
        {
            ConnectToDatabase();
        }

        private void ConnectToDatabase()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                if (IsSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                var mongoClient = new MongoClient(settings);
                _database = mongoClient.GetDatabase(DatabaseName);

                _database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível se conectar com o servidor.", ex);
            }
        }

        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("User");
            }
        }

        public IMongoCollection<DeployConfig> DeployConfigs
        {
            get
            {
                return _database.GetCollection<DeployConfig>("DeployConfig");
            }
        }


        

        public override User GetUser(string name, string password)
        {
            User user = Users.Find(u => u.Name == name && u.Password == password).FirstOrDefault();
            return user;
        }

        public override List<DeployConfig> GetDeployConfigs()
        {
            return this.DeployConfigs.Find<DeployConfig>(u => true).ToList();
        }

        public override List<User> GetUsers()
        {
            return this.Users.Find<User>(u => true).ToList();
        }

        public override bool CheckUserTable(bool createTable)
        {
            try
            {
                bool aux = this.Users == null;
            }
            catch
            {
                if (createTable)
                {
                    _database.CreateCollection("User");
                    
                    return true;
                }
                return false;
            }

            return true;
        }

        public override bool CheckDeployConfigTable(bool createTable)
        {
            try
            {
                bool aux = this.DeployConfigs == null;
            }
            catch
            {
                if (createTable)
                {
                    _database.CreateCollection("DeployConfig");
                    return true;
                }
                return false;
            }

            return true;
        }
        

    }
}
