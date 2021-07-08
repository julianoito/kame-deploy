using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Kame.Core.Entity
{
    [Serializable]
    public abstract class Parameter : BaseEntity
    {
        public string ParameterID { get; set; }
        public string ParameterKey { get; set; }
        public string ParameterValue { get; set; }
        public string ParameterDescription { get; set; }

        protected Parameter()
        {
            this.EntityState = System.Data.EntityState.Modified;
        }

        public bool GetBoolValue()
        { 
            bool value;
            bool.TryParse(this.ParameterValue, out value);

            return value;
        }

        public int GetIntValue()
        {
            int value;
            int.TryParse(this.ParameterValue, out value);

            return value;
        }

        public string GetStringValue()
        {
            return this.ParameterValue;
        }
    }
}
