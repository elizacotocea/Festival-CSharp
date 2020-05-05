using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using persistence;
using app.Model;
using System.Data.SQLite;

namespace app.persistence
{
    public class BuyerRepository
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private int maxId = 0;
        String connectionString = ConfigurationManager.ConnectionStrings["FestivalBD"].ToString();
        private int getMaxId()
        {
            IEnumerable<Buyer> all = FindAll();
            foreach (Buyer a in all)
            {
                if (a.Id > maxId)
                    maxId = a.Id;
            }
            return maxId + 1;
        }

        public void Delete(int Id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.InfoFormat("Delete one Buyer id={0}", Id);
                conn.Execute("DELETE FROM buyer WHERE id=@id", new { id = Id });
                log.InfoFormat("Deleted the Buyer id={0} succesfully", Id);
            }
        }

        public Buyer FindByName(string name)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.InfoFormat("Selecting Buyer with name={0}", name);
                return conn.QueryFirstOrDefault<Buyer>("SELECT * FROM buyer where name=@name", new { name=name});
            }
        }
        public IEnumerable<Buyer> FindAll()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.Info("Selecting everything from buyer table");
                return conn.Query<Buyer>("SELECT * FROM Buyer");
            }

        }

        public Buyer FindOne(int Id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.InfoFormat("Selecting Buyer with id={0}", Id);
                return conn.QueryFirstOrDefault<Buyer>("SELECT * FROM buyer where id=@id", new { id = Id });
            }
        }

        public void Save(Buyer elem)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.InfoFormat("Save element with id={0}",elem.Id);
                conn.Execute("INSERT INTO buyer(id,name) VALUES (@Id,@Name)", new { id = getMaxId(), name = elem.Name });
                log.InfoFormat("Saved element with id={0} succesfully", elem.Id);
            }
        }

        public void Update(int Id, Buyer elem)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.InfoFormat("Update element with id={0}", elem.Id);
                conn.Execute("update buyer set name=@name where id=@id", new { id = Id, name = elem.Name });
                log.InfoFormat("Updated element with id={0} succesfully", elem.Id);
            }
        }

      
    }
}
