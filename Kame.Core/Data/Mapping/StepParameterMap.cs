using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

using Kame.Core.Entity;

namespace Kame.Core.Data.Mapping
{
    public class StepParameterMap : EntityTypeConfiguration<StepParameter>
    {
        public StepParameterMap()
        {
            this.ToTable("StepParameter");
            this.HasKey(x => new { x.StepID, x.ParameterKey });
        }
    }
}
