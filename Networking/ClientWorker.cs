using app.Model;
using app.services;
using services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace networking
{
    public class ClientWorker : IAppObserver //, Runnable
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IServices server;

        private TcpClient connection;
        private NetworkStream stream;
        private IFormatter formatter;

        private volatile bool connected;
        public ClientWorker(IServices server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                connected = true;
                logger.Debug("PROXY SERVER: SUCCESSFUL ClientRpcWorke");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
                logger.Debug("PROXY SERVER: FAILED ClientRpcWorker");
            }
        }


        public virtual void run()
        { // aici intra din proxy, de la sendRequest()
            while (connected)
            {
                try
                {
                    logger.Debug(" Object request=input.readObject();");
                    object request = formatter.Deserialize(stream);

                    logger.DebugFormat("--- citire: {}", request);
                    logger.Debug("PROXY SERVER: run RECEIVED REQUEST");
                    object response = handleRequest((Request)request);
                    if (response != null)
                    {
                        sendResponse((Response)response);
                        logger.Debug("PROXY SERVER: run SENT RESPONSE");
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                try
                {
                    Thread.Sleep(250);
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            try
            {
                logger.Debug("PROXY SERVER: run : BEGIN SHUTDOWN");
                stream.Close();
                connection.Close();
                logger.Debug("PROXY SERVER: run : COMPLETED SHUTDOWN");
            }
            catch (IOException e)
            {
                logger.Debug("PROXY SERVER: run : IOException @" + DateTime.Now);
                Console.WriteLine("Error " + e);
            }
        }


        public void notifyTicketSold(Show show)
        {
            ShowDTO  showdto = DTOUtils.getDTO(show);
            Response resp = new Response.Builder().Type(ResponseType.UPDATED_SHOWS).Data(showdto).Build();
            logger.Debug("PROXY SERVER: ticketsSold BUILT RESPONSE @" + DateTime.Now);
            Console.WriteLine("Tickets sold for match " + show);
            try
            {
                sendResponse(resp);
                logger.DebugFormat("PROXY SERVER: ticketsSold SENT RESPONSE @" + DateTime.Now, resp);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private static Response okResponse = new Response.Builder().Type(ResponseType.OK).Build();


        private Response handleRequest(Request request)
        {
            Response response = null;
            if (request.Type() == RequestType.LOGIN)
            {
                logger.Debug("PROXY SERVER: handleRequest RECEIVED REQUEST type==RequestType.LOGIN");
                Console.WriteLine("Login request ..." + request.Type());
                UserDTO udto = (UserDTO)request.Data();
                Employee user = DTOUtils.getFromDTO(udto);
                try
                {
                    server.login(user, this);
                    logger.Debug("PROXY SERVER: handleRequest SENT COMMAND TO SERVER server.login");
                    return okResponse;
                }
                catch (ServicesException e)
                {
                    connected = false;
                    logger.Debug("PROXY SERVER: FAILED handleRequest type==RequestType.LOGIN");
                    return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
                }
            }
            if (request.Type() == RequestType.LOGOUT)
            {
                logger.Debug("PROXY SERVER: handleRequest RECEIVED REQUEST type==RequestType.LOGOUT");
                Console.WriteLine("Logout request");
                UserDTO udto = (UserDTO)request.Data();
                Employee user = DTOUtils.getFromDTO(udto);
                try
                {
                    server.logout(user, this);
                    connected = false;
                    logger.Debug("PROXY SERVER: handleRequest SENT COMMAND TO SERVER server.logou");
                    return okResponse;

                }
                catch (ServicesException e)
                {
                    logger.Debug("PROXY SERVER: FAILED handleRequest type==RequestType.LOGOUT");
                    return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
                }
            }

            if (request.Type() == RequestType.GET_SHOWS)
            {
                logger.Debug("PROXY SERVER: handleRequest RECEIVED REQUEST type==RequestType.GET_MATCHES @" + DateTime.Now);
                Console.WriteLine("Get Matches request");
                try
                {
                    Show[] shows = server.findAllShows();
                    ShowDTO[] showdtos = DTOUtils.getDTO(shows);
                    logger.Debug("PROXY SERVER: handleRequest SENT COMMAND TO SERVER server.findAllMeci @" + DateTime.Now);
                    return new Response.Builder().Type(ResponseType.GET_SHOWS).Data(showdtos).Build(); 

                }
                catch (ServicesException e)
                {
                    logger.Debug("PROXY SERVER: FAILED handleRequest type==RequestType.GET_MATCHES @" + DateTime.Now);
                    return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
                }
            }


            if (request.Type() == RequestType.TICKETS_SOLD)
            {
                logger.Debug("PROXY SERVER: handleRequest RECEIVED REQUEST type==RequestType.TICKETS_SOLD @" + DateTime.Now);
                Console.WriteLine("TICKETS_SOLD update meci request");
                Object[] data = (Object[])request.Data();
                ShowDTO showDTO = (ShowDTO)data[0];
                TicketDTO tcket = (TicketDTO)data[1];
                Show s = DTOUtils.getFromDTO(showDTO);
                Ticket ticket = DTOUtils.getFromDTO(tcket);
                try
                {
                    Show shU=server.ticketsSold(s, ticket); // this is the response of the TICKETS_SOLD request
                    logger.Debug("PROXY SERVER: handleRequest SENT COMMAND TO SERVER server.ticketsSold @" + DateTime.Now);
                    ShowDTO sDTO = DTOUtils.getDTO(shU);
                    //return new Response.Builder().Type(ResponseType.UPDATED_SHOWS).Data(sDTO).Build();
                    return okResponse; // de aici vine double update-ul bun (aici trimiteai bine)
                    //aici trimiti doar o confirmare, nu un update. AICI era. pentru ca el astepta in continuare ok-ul, dar notificarea ta era prinsa in update . asa zic

                }
                catch (ServicesException e)
                {
                    logger.Debug("PROXY SERVER: FAILED handleRequest type==RequestType.TICKETS_SOLD @" + DateTime.Now);
                    return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
                }
            }

            logger.DebugFormat("PROXY SERVER: RETURN RESPONSE handleRequest @" + DateTime.Now + " WARNING: should never reach this point: request type not found !!!", response);
            return response;
        }


        private void sendResponse(Response response)
        {
            Console.WriteLine("sending response " + response);
            logger.Debug("E S T E   P O S I B I L   S A     F I U   B L O C A T     D E     S C R I E R E     !!! - output.writeObject(response);");
            formatter.Serialize(stream, response);
            logger.DebugFormat("--- scriere: {}", response);
            stream.Flush();
            logger.DebugFormat("PROXY SERVER: SUCCESSFUL sendResponse @" + DateTime.Now, response);
        }

    }

}
