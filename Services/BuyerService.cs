using app.Model;
using app.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.services
{
    public class BuyerService
    {
        private BuyerRepository buyerRepository;
        private int maxId = 0;

        private int getMaxId()
        {
            IEnumerable<Buyer> all = buyerRepository.FindAll();
            foreach (Buyer a in all)
            {
                if (a.Id > maxId)
                    maxId = a.Id;
            }
            return maxId + 1;

        }
        public BuyerService(BuyerRepository buyerRepository)
        {
            this.buyerRepository = buyerRepository;
        }

        public Buyer FindByName(string name)
        {
            IEnumerable<Buyer> all = FindAll();
            foreach (Buyer b in all)
            {
                if (b.Name == name)
                {
                    return b;
                }
            }
            return null;
        }
        public void Save(string name)
        {
            if (FindByName(name) == null)
            {
                buyerRepository.Save(new Buyer() { Id = getMaxId(), Name = name });
                maxId += 1;
            }
        }

        public void Delete(int Id)
        {
            buyerRepository.Delete(Id);
        }

        public void Update(int Id, string name)
        {
            buyerRepository.Update(Id, new Buyer() { Id = Id, Name = name });
        }

        public Buyer FindOne(int ID)
        {
            return buyerRepository.FindOne(ID);
        }

        public IEnumerable<Buyer> FindAll()
        {
            return buyerRepository.FindAll();
        }

   
    }
}
