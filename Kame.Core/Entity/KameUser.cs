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
    public class KameUser
    {
        public int UserID {get; set;}
        public string Name { get; set; }
        public string Login { get; set; }

        private string password;
        public string Password 
        {
            get { return this.password; }
            set
            {
                this.password = value;
            }
        }
        public string Email { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public KameUser EfetuarLogin()
        {
            KameUser usuarioLogado = null;
            KameDbContext dbContext = new KameDbContext();

            List<KameUser> listaUsuarios = dbContext.Set<KameUser>().Include("Roles").Where(u => u.Login == this.Login && u.Password == this.Password).ToList<KameUser>();

            if (listaUsuarios.Count == 1)
            {
                usuarioLogado = listaUsuarios[0];
            }


            return usuarioLogado;
        }

        public static List<KameUser> GetUsers(KameUser usuarioFiltro)
        {
            KameDbContext dbContext = new KameDbContext();
            
            IQueryable<KameUser> query = dbContext.Set<KameUser>().Include("Roles");

            if (usuarioFiltro != null && !string.IsNullOrEmpty(usuarioFiltro.Name))
            {
                query = query.Where(u => u.Name.Contains(usuarioFiltro.Name));
            }

            if (usuarioFiltro != null && usuarioFiltro.UserID > 0)
            {
                query = query.Where(u => u.UserID == usuarioFiltro.UserID);
            }

            query = query.OrderBy(u => u.Name);

            List<KameUser> listaUsuarios = query.ToList<KameUser>();
            dbContext.Dispose();
            return listaUsuarios;
        }

        public static KameUser GetUserById(int userID)
        {
            List<KameUser> listaUsuarios = KameUser.GetUsers(new KameUser() { UserID = userID });

            if (listaUsuarios.Count == 1)
            {
                return listaUsuarios[0];
            }
            else 
            {
                return null;
            }
        }

        public void Save()
        {
            KameDbContext dbContext = new KameDbContext();

            if (this.UserID > 0)
            {
                
                KameDbContext dbContextAux = new KameDbContext();                
                var usuarioAux = (from u in dbContextAux.Set<KameUser>().Include("Roles")
                                  where u.UserID == this.UserID
                                 select u).FirstOrDefault<KameUser>();

                if (usuarioAux.Roles.Count > 0)
                {
                    usuarioAux.Roles.ToList<Role>().ForEach(f => usuarioAux.Roles.Remove(f));
                    dbContextAux.SaveChanges();
                }
                dbContextAux.Dispose();

                dbContext.Set<KameUser>().Add(this);
                dbContext.Entry(this).State = System.Data.EntityState.Modified;
            }
            else
            {
                dbContext.Set<KameUser>().Add(this);
            }

            foreach (Role funcao in this.Roles)
            {
                dbContext.Set<Role>().Add(funcao);
                dbContext.Entry(funcao).State = System.Data.EntityState.Unchanged;
            }
            
            dbContext.SaveChanges();
            dbContext.Dispose();
        }
    }
}
