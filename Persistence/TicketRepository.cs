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

namespace app.persistence { 
    public class TicketRepository:ICrudRepository<int,Ticket>
    {

        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private int maxId = 0;
        String connectionString = ConfigurationManager.ConnectionStrings["FestivalBD"].ToString();
        private int getMaxId()
        {
            IEnumerable<Ticket> all = FindAll();
            foreach (Ticket a in all)
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
                log.InfoFormat("Delete one Ticket id={0}", Id);
                conn.Execute("DELETE FROM ticket WHERE id=@id", new { id = Id });
                log.InfoFormat("Deleted the Ticket id={0} succesfully", Id);
            }
        }

        public IEnumerable<Ticket> FindAll()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.Info("Selecting everything from ticket table");
                return conn.Query<Ticket>("SELECT * FROM Ticket");
            }

        }

        public Ticket FindOne(int Id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.InfoFormat("Selecting Ticket with id={0}", Id);
                return conn.QueryFirstOrDefault<Ticket>("SELECT * FROM ticket where id=@id", new { id = Id });
            }
        }

        public void Save(Ticket elem)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.InfoFormat("Save element with id={0}", elem.Id);
                conn.Execute("INSERT INTO ticket VALUES (@id,@nrWantedSeats,@buyerName,@idShow)", new { id = getMaxId(), nrWantedSeats=elem.NrSeatsWanted, buyerName=elem.BuyerName,idShow=elem.IdShow});
                log.InfoFormat("Saved element with id={0} succesfully", elem.Id);
                maxId += 1;
            }
        }

        public void Update(int Id, Ticket elem)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                log.InfoFormat("Update element with id={0}", elem.Id);
                conn.Execute("update ticket set nrSeatsWanted=@nrSeatsWanted, buyerName=@buyerName, idShow=@idShow where id=@id",
                    new { id = Id, nrSeatsWanted=elem.NrSeatsWanted,buyerName=elem.BuyerName,idShow=elem.IdShow });
                log.InfoFormat("Updated element with id={0} succesfully", elem.Id);
            }
        }
    }
}
