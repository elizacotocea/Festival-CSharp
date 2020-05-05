using Dapper;
using app.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using persistence;
using System.Data.SQLite;

namespace app.persistence
{

    public class ArtistRepository: ICrudRepository<int,Artist>
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private int maxId = 0;
        String connectionString = ConfigurationManager.ConnectionStrings["FestivalBD"].ToString();
        private int getMaxId()
        {
            IEnumerable<Artist> all = FindAll();
            foreach (Artist a in all)
            {
                if (a.Id > maxId)
                    maxId = a.Id;
            }
            return maxId + 1;
        }


        public Artist FindByName(string name)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.InfoFormat("Selecting Artist with name={0}", name);
                return conn.QueryFirstOrDefault<Artist>("SELECT * FROM artist where name=@name", new { name = name });
            }
        }
        public void Delete(int Id)
        {
            using (var conn = new SQLiteConnection(connectionString)) {
                log.InfoFormat("Delete one Artist id={0}", Id);
                conn.Execute("DELETE FROM artist WHERE id=@id", new { id = Id });
                log.InfoFormat("Deleted the Artist id={0} succesfully", Id);
            }
        }

        public IEnumerable<Artist> FindAll()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.Info("Selecting everything from artist table");
                return conn.Query<Artist>("SELECT * FROM artist");
            }
           
        }

        public Artist FindOne(int Id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.InfoFormat("Selecting artist with id={0}",Id);
                return conn.QueryFirstOrDefault<Artist>("SELECT * FROM artist where id=@id",new { id=Id});
            }
        }

        public void Save(Artist elem)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.Info("Selecting everything from artist table");
                conn.Execute("INSERT INTO artist(id,name) VALUES (@Id,@Name)",new { id=getMaxId(),name=elem.Name});
             
            }
        }

        public void Update(int Id,Artist elem)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.Info("Selecting everything from artist table");
                conn.Execute("update artist set name=@name where id=@id", new { id = Id, name =elem.Name });
            }
        }
    }
}
