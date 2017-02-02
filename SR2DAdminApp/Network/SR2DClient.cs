using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SR2DAdminApp.Network
{
    public class SR2DClient
    {
        /*Vars*/
        TcpClient client = null;
        IPEndPoint serverEndPoint;
        SR2DNetworkClientControl controler;

        //Queue for incoming messages
        public Queue<string> enteringQueue = new Queue<string>();

        public SR2DClient(SR2DNetworkClientControl controler)
        {
            this.controler = controler;

            //Start client in a separated thread
            ThreadStart initThreadStart = new ThreadStart(init);

            Thread initThread = new Thread(initThreadStart);

            initThread.Start();
        }

        public void init()
        {
            try
            {
                
                controler.serverStatus = "searching";

                client = new TcpClient();

                Console.WriteLine(IPAddress.Parse(controler.ip).ToString());
                Console.WriteLine(controler.port);

                //Create end point
                serverEndPoint = new IPEndPoint(IPAddress.Parse(controler.ip), controler.port);

                //Create connection
                client.Connect(serverEndPoint);

                controler.serverStatus = "connected";

                controler.inited = true;

                //Run work thread
                work();
            }
            catch
            {
                controler.serverStatus = "disconnected";
            
                controler.inited = false;
            }
        }

        //Ping function
        public bool ping()
        {
            try
            {
                if (controler.serverStatus != "disconnected")
                {
                    Console.WriteLine("Pinging server");

                    NetworkStream clientStream = client.GetStream();

                    ASCIIEncoding encoder = new ASCIIEncoding();

                    //Ping message
                    byte[] buffer = encoder.GetBytes("rt=ping;m=ping;");

                    clientStream.Write(buffer, 0, buffer.Length);

                    clientStream.Flush();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        //Send server synchro request
        public void update()
        {
            try
            {
                if (controler.serverStatus != "disconnected")
                {
                    Console.WriteLine("Updating database for server");

                    NetworkStream clientStream = client.GetStream();

                    ASCIIEncoding encoder = new ASCIIEncoding();

                    byte[] buffer = encoder.GetBytes("rt=update;m=new_infos;");

                    clientStream.Write(buffer, 0, buffer.Length);

                    clientStream.Flush();

                    return;
                }
                else
                {
                    return;
                }
            }
            catch
            {
                return;
            }
        }

        //Send server a connection request
        public bool connect(string username, string password)
        {
            try
            {
                Console.WriteLine("Connect to server");

                NetworkStream clientStream = client.GetStream();

                ASCIIEncoding encoder = new ASCIIEncoding();

                byte[] buffer = encoder.GetBytes("rt=connect;un=" + username + ";pw=" + password + ";");

                clientStream.Write(buffer, 0, buffer.Length);

                clientStream.Flush();

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //Update the local database
        public void updateSR2D()
        {
            controler.updateDatabase();
        }

        //Main function
        public void work()
        {
             while (controler.clientWork)
            {
                 //If data has been sent by the server
                if (client.GetStream().DataAvailable)
                {
                    try
                    {
                        NetworkStream clientStream = client.GetStream(); //Get data

                        ASCIIEncoding encoder = new ASCIIEncoding();

                        byte[] buffer = new byte[1024];

                        int read = clientStream.Read(buffer, 0, 1024);

                        //Get message parts
                        string message = encoder.GetString(buffer);

                        string messageType = message.Split(';')[0].Split('=')[1];

                        string messageDest = message.Split(';')[0].Split('=')[0];

                        Console.WriteLine("Message received");

                        Console.WriteLine(messageDest);
                        Console.WriteLine(messageType);

                        //Compare message type
                        switch(messageType)
                        {
                            case "ping":
                                Console.WriteLine("Responding to ping");

                                byte[] answer = encoder.GetBytes("at=ping;m=pong;"); //#LOL

                                clientStream.Write(answer, 0, answer.Length);

                                clientStream.Flush();

                                controler.resetTimer(); //Reset ping timer

                                Console.WriteLine("server alive");
                                break;
                            case "update":

                                ThreadStart updateThreadStart = new ThreadStart(updateSR2D);

                                Thread updateThread = new Thread(updateThreadStart);

                                updateThread.Start(); //Run a local update of the database

                                Console.WriteLine("Server request an update");

                                byte[] answer2 = encoder.GetBytes("at=update;m=updated;");

                                clientStream.Write(answer2, 0, answer2.Length);

                                clientStream.Flush();

                                controler.resetTimer();
                                break;
                            case "connect":
                                Console.WriteLine("Server respond to connect");

                                if (message.Split(';')[1].Split('=')[1] == "right_infos")
                                {
                                    enteringQueue.Enqueue("connect;validate;" + message.Split(';')[2]);
                                }
                                else
                                {
                                    enteringQueue.Enqueue("connect;refused");
                                }

                                controler.resetTimer();
                                break;
                            }

                        clientStream.Flush();

                        
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.StackTrace);
                    }
                }
                else
                {
                    Thread.Sleep(200);
                }
                
            }
        }
    }
}
