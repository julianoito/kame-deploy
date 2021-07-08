using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;

using Kame.Core.Data.Mapping;
using Kame.Core.Entity;

namespace Kame.Core.Data
{
    class KameDbContext : DbContext
    {
        public KameDbContext()
            :base("KameDbContext")
        {
            Database.SetInitializer<KameDbContext>(null);
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new KameUserMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new DeployProjectMap());
            modelBuilder.Configurations.Add(new StepParameterMap());
            modelBuilder.Configurations.Add(new ProjectParameterMap());
            modelBuilder.Configurations.Add(new StepMap());
        }

        public void Save(BaseEntity entity)
        {
            DbSet dbSet = this.Set(entity.GetType());
            dbSet.Add(entity);

            this.Entry(entity).State = entity.EntityState;

            if (entity.EntityState == System.Data.EntityState.Added)
            {
                entity.EntityState = System.Data.EntityState.Modified;
            }
        }
    }
}
