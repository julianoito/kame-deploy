using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kame.Core.Entity
{
	public abstract class ISQL2005Step : IStepProcessor
	{
		public static string ChangeConnectionStringServer(string connectionString, string newServer)
		{
			string newConnectiosString = string.Empty;
			string[] connectionParameters = connectionString.Split(';');

			for (int i = 0; i < connectionParameters.Length; i++)
			{
				if (connectionParameters[i].Replace(" ", "").StartsWith("Server="))
				{
					newConnectiosString += "Server=" + newServer + ";";
				}
				else
				{
					newConnectiosString += connectionParameters[i] + ";";
				}
			}

			return newConnectiosString;
		}
	}
}
