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
    public class ShowRepository:ICrudRepository<int,Show>
    {

        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String connectionString = ConfigurationManager.ConnectionStrings["FestivalBD"].ToString();
        private int maxId = 0;
        private int getMaxId()
        {
            IEnumerable<Show> all = FindAll();
            foreach (Show a in all)
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
                log.InfoFormat("Delete one Show id={0}", Id);
                conn.Execute("DELETE FROM show WHERE id=@id", new { id = Id });
                log.InfoFormat("Deleted the Show id={0} succesfully", Id);
            }
        }

        public IEnumerable<Show> FindAll()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.Info(conn.State);
                log.Info("Selecting everything from show table");
                return conn.Query<Show>("SELECT * FROM show");
            }

        }

        public Show FindOne(int Id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.InfoFormat("Selecting Show with id={0}", Id);
                return conn.QueryFirstOrDefault<Show>("SELECT * FROM show where id=@id", new { id = Id });
            }
        }

        public void Save(Show elem)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.InfoFormat("Save element with id={0}", elem.Id);
                conn.Execute("INSERT INTO show(id,showDateTime,location,nrAvailableSeats,nrSoldSeats,artistName) VALUES (@Id,@showDateTime,@location,@nrAvailableSeats,@nrSoldSeats,@artistName)", new { id = getMaxId(), showDateTime = elem.ShowDateTime,
                location=elem.Location,nrAvailable=elem.NrAvailableSeats,nrSoldSeats=elem.NrSoldSeats,artistName=elem.ArtistName});
                log.InfoFormat("Saved element with id={0} succesfully", elem.Id);
                maxId += 1;
            }
        }

        public void Update(int Id, Show elem)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.InfoFormat("Update element with id={0}", elem.Id);
                conn.Execute("update show set showDateTime=@showDateTime, location=@location" +
                    ", nrAvailableSeats=@nrAvailableSeats, nrSoldSeats=@nrSoldSeats," +
                    "artistName=@artistName where id=@id", new { showDateTime = elem.ShowDateTime,
                location=elem.Location,nrAvailableSeats=elem.NrAvailableSeats,nrSoldSeats=elem.NrSoldSeats,
                        artistName = elem.ArtistName,
                        id = elem.Id,
                    });
                log.InfoFormat("Updated element with id={0} succesfully", elem.Id);
                
            }
        }
    }
}
