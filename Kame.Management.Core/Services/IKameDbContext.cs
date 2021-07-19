using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Kame.Management.Core.Entity;

namespace Kame.Management.Core.Services
{
    public abstract class IKameDbContext
    {
        public abstract User GetUser(string name, string password);
        public abstract List<User> GetUsers();
        public abstract List<DeployConfig> GetDeployConfigs();

        public abstract bool CheckUserTable(bool createTable);
        public abstract bool CheckDeployConfigTable(bool createTable);
    }
}
