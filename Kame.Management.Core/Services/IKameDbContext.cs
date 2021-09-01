using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kame.Core.Entity.Log;
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

        public abstract bool CheckDeployExecutionLogTable(bool createTable);

        public abstract void SaveUser(User user);
        public abstract void DeleteUser(User user);

        public abstract void SaveDeployProject(DeployConfig deployConfig);
        public abstract void DeleteDeployConfig(DeployConfig deployConfig);

        public abstract DeployConfig FindDeployConfigByName(string name);

        public abstract DeployConfig FindDeployConfigById(string id);

        public abstract void SaveDeployExecution(DeployLogXML deployLog);

    }
}
