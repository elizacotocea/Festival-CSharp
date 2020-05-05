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
namespace Networking.network
    {
        public class ServicesRpcProxy : IServices
        {
            private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            private String host;
            private int port;
            private bool isInitialized = false;

            private IAppObserver client;

            private IFormatter formatter;
            private NetworkStream stream;
            private TcpClient connection;
            private EventWaitHandle _waitHandle;

            private Queue<Response> qresponses;
            private volatile bool finished;
            public ServicesRpcProxy(String host, int port)
            {
                this.host = host;
                this.port = port;
                qresponses = new Queue<Response>();
                logger.Debug("Client Side Proxy Init");
            }

            public void login(Employee user, IAppObserver client)
            {
                if (!isInitialized)
                {
                    initializeConnection();
                    this.isInitialized = true;
                }
                UserDTO udto = DTOUtils.getDTO(user);
                Request req = new Request.Builder().Type(RequestType.LOGIN).Data(udto).Build();
                sendRequest(req);
                logger.Debug("PROXY CLIENT: login SENT REQUEST");
                Response response = readResponse();
                logger.Debug("PROXY CLIENT: login RECEIVED RESPONSE @");
                if (response.Type() == ResponseType.OK)
                {
                    this.client = client;
                    logger.Debug("PROXY CLIENT: SUCCESSFUL login");
                    return;
                }
                if (response.Type() == ResponseType.ERROR)
                {
                    String err = response.Data().ToString();
                    closeConnection();
                    logger.Debug("PROXY CLIENT: FAILED login");
                    throw new ServicesException(err);
                }
            }


            public void logout(Employee user, IAppObserver client)
            {
                UserDTO udto = DTOUtils.getDTO(user);
                Request req = new Request.Builder().Type(RequestType.LOGOUT).Data(udto).Build();
                sendRequest(req);
                logger.Debug("PROXY CLIENT: logout SENT REQUEST");
                Response response = readResponse();
                logger.Debug("PROXY CLIENT: logout RECEIVED RESPONSE");
                closeConnection();
                this.isInitialized = false;
                logger.Debug("PROXY CLIENT: SUCCESSFUL logout");
                if (response.Type() == ResponseType.ERROR)
                {
                    String err = response.Data().ToString();
                    logger.Debug("PROXY CLIENT: FAILED logout");
                    throw new ServicesException(err);

                }
            }

            public Show[] findAllShowsForArtist(Artist artist)
            {
                ArtistDTO adto = DTOUtils.getDTO(artist);
                Request req = new Request.Builder().Type(RequestType.GET_SHOWS_W_ARTIST).Data(adto).Build();
                sendRequest(req);
                logger.Debug("PROXY CLIENT: findAllShowWithTickets SENT REQUES");
                Response response = readResponse();
                logger.Debug("PROXY CLIENT: findAllShowWithTickets RECEIVED RESPONSE");
                if (response.Type() == ResponseType.ERROR)
                {
                    String err = response.Data().ToString();
                    logger.Debug("PROXY CLIENT: FAILED");
                    throw new ServicesException(err);
                }
                ShowDTO[] showDTOS = (ShowDTO[])response.Data();
                Show[] meciuri = DTOUtils.getFromDTO(showDTOS);
                logger.Debug("PROXY CLIENT: SUCCESSFUL");
                return meciuri;
            }


            public Show[] findAllShows()
            {
                if (!isInitialized)
                {
                    initializeConnection();
                    this.isInitialized = true;
                }
                Request req = new Request.Builder().Type(RequestType.GET_SHOWS).Build();
                sendRequest(req);
                logger.Debug("PROXY CLIENT: findAllShow SENT REQUEST");
                Response response = readResponse();
                logger.Debug("PROXY CLIENT: findAllShow RECEIVED RESPONSE");
                if (response.Type() == ResponseType.ERROR)
                {
                    String err = response.Data().ToString();
                    logger.Debug("PROXY CLIENT: FAILED findAllSho");
                    throw new ServicesException(err);
                }
                ShowDTO[] showsDTOS = (ShowDTO[])response.Data();
                Show[] shows = DTOUtils.getFromDTO(showsDTOS);
                logger.Debug("PROXY CLIENT: SUCCESSFUL findAllShow");
                return shows;
            }


            public Show ticketsSold(Show show, Ticket t)
            {
                ShowDTO showDTO = DTOUtils.getDTO(show);
                TicketDTO ticketDTO = DTOUtils.getDTO(t);
                Object[] sendData = new Object[2];
                sendData[0] = showDTO;
                sendData[1] = ticketDTO;
                Request req = new Request.Builder().Type(RequestType.TICKETS_SOLD).Data(sendData).Build();
                sendRequest(req);
                logger.Debug("PROXY CLIENT: ticketsSold SENT REQUEST WARNING: THIS FUNCTION HAS NO RESPONSE!!!");
                Response response = readResponse();
                if (response.Type() == ResponseType.UPDATED_SHOWS)
                {
                    logger.Debug("PROXY CLIENT: SUCCESSFUL update");
                    return null;
                }
                if (response.Type() == ResponseType.ERROR)
                {
                    String err = response.Data().ToString();
                    throw new ServicesException(err);
                }
                logger.Debug("PROXY CLIENT: SUCCESSFUL ticketsSold WARNING: THIS FUNCTION HAS NO RESPONSE!!!");
                return null;
            }


            private void closeConnection()
            {
                finished = true;
                try
                {
                    stream.Close();
                    //output.close();
                    connection.Close();
                    _waitHandle.Close();
                    client = null;
                    logger.Debug("PROXY CLIENT: SUCCESSFUL closeConnection");
                }
                catch (IOException e)
                {
                    logger.Debug("PROXY CLIENT: FAILED closeConnection");
                    Console.WriteLine(e.StackTrace);
                }

            }


            private void sendRequest(Request request)
            {

                if (!isInitialized)
                {
                    initializeConnection();
                    this.isInitialized = true;
                }

                logger.DebugFormat("NETWORKING FROM CLIENT PROXY TO SERVER: INITIALIZING sendReques", request);
                try
                {
                    logger.Debug("output.writeObject(request);");
                    formatter.Serialize(stream, request);
                    logger.DebugFormat("--- scriere: {}", request);
                    stream.Flush();
                    logger.DebugFormat("NETWORKING FROM CLIENT PROXY TO SERVER: SUCESSFUL sendRequest", request);
                }
                catch (IOException e)
                {
                    logger.Debug("NETWORKING FROM CLIENT PROXY TO SERVER: FAILED sendRequest");
                    throw new ServicesException("Error sending object " + e);
                }
            }

            private Response readResponse()
            {
                Response response = null;
                logger.Debug("NETWORKING FROM CLIENT PROXY TO SERVER: INITIALIZING readResponse");
                try
                {
                    logger.Debug(" response=qresponses.take();");
                    _waitHandle.WaitOne();
                    lock (qresponses)
                    {
                        //Monitor.Wait(responses); 
                        response = qresponses.Dequeue();
                    }
                    logger.DebugFormat("--- citire: {}", response);
                    logger.DebugFormat("NETWORKING FROM CLIENT PROXY TO SERVER: SUCESSFUL qresponses.take readResponse {} @" + DateTime.Now, response);
                }
                catch (ThreadInterruptedException e)
                {
                    logger.Debug("NETWORKING FROM CLIENT PROXY TO SERVER: FAILED readResponse @" + DateTime.Now);
                    Console.WriteLine(e.StackTrace);
                }
                return response;
            }


            private void initializeConnection()
            {
                if (!this.isInitialized)
                {
                    try
                    {
                        logger.Debug("PROXY CLIENT: INITIALIZING initializeConnection @" + DateTime.Now);
                        connection = new TcpClient(host, port);
                        stream = connection.GetStream();
                        formatter = new BinaryFormatter();
                        finished = false;
                        _waitHandle = new AutoResetEvent(false);
                        startReader();
                        logger.Debug("PROXY CLIENT: SUCCESSFUL initializeConnection @" + DateTime.Now);
                    }
                    catch (IOException e)
                    {
                        logger.Debug("PROXY CLIENT: FAILED initializeConnection @" + DateTime.Now);
                        Console.WriteLine(e.StackTrace);
                    }
                }
            }
            private void startReader()
            {
                Thread tw = new Thread(run);
                tw.Start();
            }


            private void handleUpdate(Response response)
            {

                if (response.Type() == ResponseType.UPDATED_SHOWS)
                { // this is the handler for the TICKETS_SOLD request

                    Show show = DTOUtils.getFromDTO((ShowDTO)response.Data());
                    try
                    {
                        
                        client.notifyTicketSold(show);
                    }
                    catch (ServicesException e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                }
            }


            private bool isUpdate(Response response)
            {
                return response.Type() == ResponseType.UPDATED_SHOWS;
            }


            public virtual void run()
            {
                while (!finished)
                {
                    try
                    {
                        object response = formatter.Deserialize(stream);

                        logger.DebugFormat("PROXY CLIENT: READING DATA FROM THE SERVER", response);
                        Console.WriteLine("response received " + response);
                        if (isUpdate((Response)response))
                        {
                            logger.DebugFormat("PROXY CLIENT: Debug isUpdate((Response)response) == true", response);
                            handleUpdate((Response)response);
                        }
                        else
                        {                                                                          
                            lock (qresponses)
                            {
                                logger.DebugFormat("PROXY CLIENT: Debug qresponses.put((Response)response)", response);

                                qresponses.Enqueue((Response)response);

                            }
                            _waitHandle.Set();
                        }
                    }
                    catch (IOException e)
                    {
                        logger.Debug("PROXY CLIENT: FAILED run (because other end of socket disconnected from server) : IOException {}", e);
                        Console.WriteLine("Reading error " + e);
                    }
                }
            }

            
        }
    

    }
}
