using app.Model;
using app.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.services
{
    public class TicketService
    {
        private TicketRepository ticketRepository;
        private int maxId = 0;

        private int getMaxId()
        {
            IEnumerable<Ticket> all = ticketRepository.FindAll();
            foreach (Ticket a in all)
            {
                if (a.Id > maxId)
                    maxId = a.Id;
            }
            return maxId + 1;

        }
        public TicketService(TicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        public void Save(int nrWantedSeats, string buyerName, int idShow)
        {
            ticketRepository.Save(new Ticket() { Id = getMaxId(),  NrSeatsWanted=nrWantedSeats,BuyerName=buyerName,IdShow=idShow});
            maxId += 1;
        }

        public void Delete(int Id)
        {
            ticketRepository.Delete(Id);
        }

        public void Update(int Id, int nrWantedSeats, string buyerName, int idShow)
        {
            ticketRepository.Update(Id, new Ticket() { Id = Id, NrSeatsWanted = nrWantedSeats, BuyerName = buyerName, IdShow = idShow });
        }

        public Ticket FindOne(int ID)
        {
            return ticketRepository.FindOne(ID);
        }

        public IEnumerable<Ticket> FindAll()
        {
            return ticketRepository.FindAll();
        }
    }
}
