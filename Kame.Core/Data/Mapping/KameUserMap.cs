using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

using Kame.Core.Entity;

namespace Kame.Core.Data.Mapping
{
    public class KameUserMap : EntityTypeConfiguration<KameUser>
    {
        public KameUserMap()
        {
            this.ToTable("KameUser");
            this.HasKey(x => x.UserID);

            this.HasMany(u => u.Roles).WithMany(f => f.Users).Map(m => { m.MapLeftKey("UserID"); m.MapRightKey("RoleID"); m.ToTable("KameUserRole"); });
        }
    }
}
