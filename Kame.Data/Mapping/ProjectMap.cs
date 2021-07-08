using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

using Kame.Core.Entity;

namespace Kame.Data.Mapping
{
    public class ProjectMap : EntityTypeConfiguration<DeployProject>
    {
        public ProjectMap()
        {
            this.ToTable("TB_PROJECT");
        }
    }
}
