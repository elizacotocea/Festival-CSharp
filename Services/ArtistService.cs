using app.Model;
using app.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.services
{
    public class ArtistService
    {
        private ArtistRepository artistRepository;
        private int maxId = 0;

        private int getMaxId()
        {
            IEnumerable<Artist> all = artistRepository.FindAll();
            foreach (Artist a in all)
            {
                if (a.Id > maxId)
                    maxId = a.Id;
            }
            return maxId + 1;
            
        }
        public ArtistService(ArtistRepository artistRepository)
        {
            this.artistRepository = artistRepository;
        }

        public void Save(string name)
        {
            artistRepository.Save(new Artist() { Id = getMaxId(), Name = name });
            maxId += 1;
        }

        public void Delete(int Id)
        {
            artistRepository.Delete(Id);
        }

        public void Update(int Id, string name)
        {
            artistRepository.Update(Id, new Artist() { Id = Id, Name = name });
        }

        public Artist FindOne(int ID)
        {
            return artistRepository.FindOne(ID);
        }

        public IEnumerable<Artist> FindAll()
        {
            return artistRepository.FindAll();
        }
    }
}
