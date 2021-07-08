using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kame.Desktop.Entity
{
    public class ExecutionMode
    {
        public int ExecutionCode { get; set; }
        public string ExecutionDescription { get; set; }

        private static ExecutionMode normal = null;
        public static ExecutionMode Normal
        {
            get 
            {
                if (ExecutionMode.normal == null)
                {
                    ExecutionMode.normal = new ExecutionMode();
                    ExecutionMode.normal.ExecutionCode = 1;
                    ExecutionMode.normal.ExecutionDescription = "Executar em modo normal";
                }
                return ExecutionMode.normal;
            }
        }

        private static ExecutionMode restore = null;
        public static ExecutionMode Restore
        {
            get
            {
                if (ExecutionMode.restore == null)
                {
                    ExecutionMode.restore = new ExecutionMode();
                    ExecutionMode.restore.ExecutionCode = 2;
                    ExecutionMode.restore.ExecutionDescription = "Executar em modo Restore";
                }
                return ExecutionMode.restore;
            }
        }
    }
}
