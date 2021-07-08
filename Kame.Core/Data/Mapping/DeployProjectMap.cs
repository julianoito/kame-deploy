using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

using Kame.Core.Entity;

namespace Kame.Core.Data.Mapping
{
    public class DeployProjectMap : EntityTypeConfiguration<DeployProject>
    {
        public DeployProjectMap()
        {
            this.ToTable("DeployProject");
            this.HasKey(x => x.ProjectID);

            /*this.HasOptional(proj => proj.User)
                .WithRequired()
                .Map(user => user.MapKey("UserId"));*/

            //this.HasOptional(proj => proj.User).WithMany(user => user.Projects).HasForeignKey(proj => proj.UserID);

//            this.HasMany(x => x.Parameters).WithRequired().Map(m => { m.MapKey("ProjectId"); });
                
                //WithMany(p => p.Users).Map(m => { m.MapLeftKey("UserID"); m.MapRightKey("RoleID"); m.ToTable("KameUserRole"); });
        }
    }
}
