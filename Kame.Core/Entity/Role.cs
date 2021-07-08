using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Kame.Core.Data;

namespace Kame.Core.Entity
{
    [Serializable]
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool Administrator { get; set; }

        public virtual ICollection<KameUser> Users { get; set; }

        public static List<Role> GetRoles(Role roleFilter)
        {
            KameDbContext dbContext = new KameDbContext();
            dbContext.Configuration.LazyLoadingEnabled = false;

            IQueryable<Role> query = dbContext.Set<Role>();

            if (roleFilter != null && roleFilter.RoleID > 0)
            {
                query = query.Where(f => f.RoleID == roleFilter.RoleID);
            }


            query = query.OrderBy(f => f.RoleName);

            List<Role> roleList = query.ToList<Role>();
            dbContext.Dispose();
            return roleList;
        }

        public static Role GetRoleByID(int roleID)
        {
            List<Role> roleList = Role.GetRoles(new Role() { RoleID = roleID });

            if (roleList.Count == 1)
            {
                return roleList[0];
            }
            else
            {
                return null;
            }
        }
    }
}
