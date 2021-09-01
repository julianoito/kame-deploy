using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kame.Management.Core.Entity;
using MongoDB.Bson;
using Kame.Core.Entity.Log;
using MongoDB.Bson.Serialization.Attributes;

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

        public IMongoCollection<MongoDeployLog> DeployExecutionLog
        {
            get
            {
                return _database.GetCollection<MongoDeployLog>("DeployExecutionLog");
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

        public override void SaveUser(User user)
        {
            if (string.IsNullOrEmpty(user.Id))
            {
                //user.Id = Guid.NewGuid().ToString();
                this.Users.InsertOne(user);
            }
            else
            {
                var filter = Builders<User>.Filter.Eq("_id", ObjectId.Parse(user.Id));
                this.Users.ReplaceOne(filter, user);
            }
        }

        public override void DeleteUser(User user)
        {
            this.Users.DeleteOne(Builders<User>.Filter.Eq("_id", ObjectId.Parse(user.Id)));
        }

        public override void SaveDeployProject(DeployConfig deployConfig)
        {
            if (string.IsNullOrEmpty(deployConfig.Id))
            {
                //user.Id = Guid.NewGuid().ToString();
                this.DeployConfigs.InsertOne(deployConfig);
            }
            else
            {
                var filter = Builders<DeployConfig>.Filter.Eq("_id", ObjectId.Parse(deployConfig.Id));
                this.DeployConfigs.ReplaceOne(filter, deployConfig);
            }
        }

        public override void DeleteDeployConfig(DeployConfig deployConfig)
        {
            this.DeployConfigs.DeleteOne(Builders<DeployConfig>.Filter.Eq("_id", ObjectId.Parse(deployConfig.Id)));
        }

        public override DeployConfig FindDeployConfigByName(string name)
        {
            var filter = Builders<DeployConfig>.Filter.Eq("Name", name);
            return  this.DeployConfigs.Find(filter).FirstOrDefault();
        }

        public override DeployConfig FindDeployConfigById(string id)
        {
            var filter = Builders<DeployConfig>.Filter.Eq("_id", ObjectId.Parse(id));
            return  this.DeployConfigs.Find(filter).FirstOrDefault();

            
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

        public override bool CheckDeployExecutionLogTable(bool createTable)
        {
            try
            {
                bool aux = this.DeployExecutionLog == null;
            }
            catch
            {
                if (createTable)
                {
                    _database.CreateCollection("DeployExecutionLog");
                    return true;
                }
                return false;
            }

            return true;
        }

        


        public override void SaveDeployExecution(DeployLogXML deployLog)
        {
            this.DeployExecutionLog.InsertOne(new MongoDeployLog() { DeployLog = deployLog });
        }


        public class MongoDeployLog
        {
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }
            public DeployLogXML DeployLog { get; set; }
        }
    }
}
