using app.Model;
using app.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.services
{
    public class ShowService
    {
        private ShowRepository showRepository;
        private int maxId = 0;

        private int getMaxId()
        {
            IEnumerable<Show> all = showRepository.FindAll();
            foreach (Show a in all)
            {
                if (a.Id > maxId)
                    maxId = a.Id;
            }
            return maxId + 1;

        }
        public ShowService(ShowRepository showRepository)
        {
            this.showRepository = showRepository;
        }

        public void Save(string location,DateTime dataora, int nrAvailableSeats, int nrSoldSeats, string artistName)
        {
            showRepository.Save(new Show() { Id = getMaxId(), ShowDateTime=dataora, NrAvailableSeats=nrAvailableSeats,NrSoldSeats=nrSoldSeats,ArtistName=artistName});
            maxId += 1;
        }

        public void Delete(int Id)
        {
            showRepository.Delete(Id);
        }

        public void Update(int Id, string location, DateTime dataora, int nrAvailableSeats, int nrSoldSeats, string artistName)
        {
            showRepository.Update(Id, new Show() { Id = Id, Location=location, ShowDateTime = dataora, NrAvailableSeats = nrAvailableSeats, NrSoldSeats = nrSoldSeats, ArtistName = artistName });
        }

        public Show FindOne(int ID)
        {
            return showRepository.FindOne(ID);
        }

        public IEnumerable<Show> FindAll()
        {
            return showRepository.FindAll();
        }
    }
}
