using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

using Kame.Core.Entity;

namespace Kame.Core.Data.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            this.ToTable("Role");
            this.HasKey(x => x.RoleID);

            /*this.HasMany(f => f.Usuarios)
                .WithOptional(
            .WithRequired(u => u.ListaFuncoes)
            .WillCascadeOnDelete(true);*/

            //this.HasMany(f => f.Usuarios).WithMany().Map(m => { m.MapLeftKey("CodigoFuncao"); m.MapRightKey("CodigoUsuario"); m.ToTable("TB_USUARIO_FUNCAO"); });
        }
    }
}
