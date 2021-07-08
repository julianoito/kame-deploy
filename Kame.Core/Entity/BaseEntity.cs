using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Kame.Core.Entity
{
    [Serializable]
    public class BaseEntity
    {
        public System.Data.EntityState EntityState { get; set; }

        protected BaseEntity()
        {
            this.EntityState = System.Data.EntityState.Modified;
        }
    }
}
