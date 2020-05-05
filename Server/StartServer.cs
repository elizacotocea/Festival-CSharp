using System;
using System.Net.Sockets;
using System.Threading;
using app.persistence;
using app.services;
using networking;

namespace server
{
    class StartServer
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("hi");

            ArtistRepository artistRepository=new ArtistRepository();
            EmployeeRepository employeeRepository= new EmployeeRepository();
            BuyerRepository buyerRepository =new BuyerRepository();
            ShowRepository showRepository = new ShowRepository();
            TicketRepository ticketRepository =new TicketRepository();
            IServices serviceImpl = new ServerImpl(employeeRepository, showRepository, artistRepository, buyerRepository, ticketRepository);
            AbstractServer server = new SerialServer("127.0.0.1", 55555, serviceImpl);
            server.Start();
            Console.WriteLine("Server started ...");
            //Console.WriteLine("Press <enter> to exit...");
            Console.ReadLine();

        }
    }

    public class SerialServer : ConcurrentServer
    {
        private IServices server;
        private ClientWorker worker;
        public SerialServer(string host, int port, IServices server) : base(host, port)
        {
            this.server = server;
            Console.WriteLine("SerialChatServer...");
        }
        protected override Thread createWorker(TcpClient client)
        {
            worker = new ClientWorker(server, client);
            return new Thread(new ThreadStart(worker.run));
        }
    }

}
