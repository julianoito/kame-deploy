using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kame.Core.Entity
{
    public interface IProjectExecutionLog
    {
        void SetMessage(string message, string messageDetail);
		void SetMessageFixedLine(string message, string messagePrefix, string messageDetail);
    }
}
