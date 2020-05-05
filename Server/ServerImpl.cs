using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.services;

namespace server
{
    using app.Model;
    using app.persistence;
    using server;
    using services;
    using System.Threading;

    public class ServerImpl :  IServices
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private EmployeeRepository userRepository;
        private ShowRepository showRepository;
        private ArtistRepository artistRepository;
        private BuyerRepository buyerRepository;
        private TicketRepository ticketRepository;
        private readonly IDictionary<String, IAppObserver> loggedClients;

        public ServerImpl(EmployeeRepository uRepo, ShowRepository sRepo, ArtistRepository aRepo,BuyerRepository bRepo,TicketRepository tRepo)
        {
            userRepository = uRepo;
            showRepository = sRepo;
            artistRepository = aRepo;
            buyerRepository = bRepo;
            ticketRepository = tRepo;
            loggedClients = new Dictionary<String, IAppObserver>();
        }


        private void notifyTicketsBought(Show show)
        {
            IEnumerable<Employee> users = userRepository.FindAll();
            logger.Debug("Server: notifyTicketsBought Observer");

            foreach (Employee us in users)
            {
                if (loggedClients.ContainsKey(us.Username))
                {
                    IAppObserver chatClient = loggedClients[us.Username];
                    Task.Run(() => chatClient.notifyTicketSold(show));
                }
            }
        }


        public void login(Employee user, IAppObserver client)
        {
            logger.Debug("Server: login");
            Employee userR = userRepository.FindByUserAndPasswd(user.Username, user.Password);
            if (userR != null)
            {
                if (loggedClients.ContainsKey(userR.Username))
                    throw new ServicesException("User already logged in.");
                loggedClients[userR.Username] = client;
            }
            else
                throw new ServicesException("Authentication failed.");
        }



        public void logout(Employee user, IAppObserver client)
        {
            logger.Debug("Server: logout");
            IAppObserver localClient = loggedClients[user.Username];
            if (localClient == null)
                throw new ServicesException("User " + user.Id + " is not logged in.");
            loggedClients.Remove(user.Username);
        }


        public Show[] findAllShows()
        {
            logger.Debug("Server: findAllShows()");
            IEnumerable<Show> fromDBresult = showRepository.FindAll();
            Show[] shows = fromDBresult.ToArray();
            return shows;
        }


        public Show ticketsSold(Show show,Ticket t)
        {
            logger.Debug("Server: ticketsSold");
            int nrlocuri = t.NrSeatsWanted;
            Show sh = showRepository.FindOne(show.Id);
            Show shUpd=new Show();
            if (sh.NrAvailableSeats >= nrlocuri)
            {
                shUpd = new Show()
                {
                    Id = sh.Id,
                    ShowDateTime = sh.ShowDateTime,
                    Location = sh.Location,
                    NrAvailableSeats = sh.NrAvailableSeats - nrlocuri,
                    NrSoldSeats = sh.NrSoldSeats + nrlocuri,
                    ArtistName = sh.ArtistName
                };
                showRepository.Update(show.Id,shUpd);
                ticketRepository.Save(t);
            }
            Task.Run(() =>
            {
                try
                {
                    Thread.Sleep(1000);
                    logger.Debug("Server: it's sold!");
                    //notifyTicketsBought(show);
                    notifyTicketsBought(shUpd);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            });
            return shUpd;
        }
    }
}

