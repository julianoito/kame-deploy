using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

using Kame.Core.Entity;

namespace Kame.Core.Data.Mapping
{
    public class StepMap : EntityTypeConfiguration<Step>
    {
        public StepMap()
        {
            this.ToTable("DeployProjectStep");
            this.HasKey(x => x.StepID);

            this.HasRequired(step => step.Project).WithMany(dp => dp.Steps).HasForeignKey(step => step.ParentProjectID).WillCascadeOnDelete(true);
            this.HasOptional(step => step.ParentStep).WithMany(dp => dp.ChildSteps).HasForeignKey(step => step.ParentStepID).WillCascadeOnDelete(true);
        }
    }
}
