using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

using Kame.Core.Entity;

namespace Kame.Core.Data.Mapping
{
    public class ProjectParameterMap : EntityTypeConfiguration<ProjectParameter>
    {
        public ProjectParameterMap()
        {
            this.ToTable("ProjectParameter");
            this.HasKey(x => x.ParameterID );

            //this.HasRequired(x => x.Project).WithMany().HasForeignKey<DeployProject>(m => m.Project);// .Map(x => x.MapKey("ProjectID"));
            this.HasRequired(par => par.Project).WithMany(dp => dp.Parameters).HasForeignKey(par => par.ProjectID).WillCascadeOnDelete(true);
        }
    }
}
