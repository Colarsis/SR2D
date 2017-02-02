using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CGest.Network
{
    public class CGestClient
    {
        TcpClient client = null;
        IPEndPoint serverEndPoint;
        CGestNetworkClientControl controler;

        public Queue<string> enteringQueue = new Queue<string>();

        public CGestClient(CGestNetworkClientControl controler)
        {
            this.controler = controler;

            ThreadStart initThreadStart = new ThreadStart(init);

            Thread initThread = new Thread(initThreadStart);

            initThread.Start();
        }

        public void init()
        {
            //try
            //{
                
            controler.serverStatus = "searching";

                client = new TcpClient();

                Console.WriteLine(IPAddress.Parse(controler.ip).ToString());
                Console.WriteLine(controler.port);

                serverEndPoint = new IPEndPoint(IPAddress.Parse(controler.ip), controler.port);

                client.Connect(serverEndPoint);

                controler.serverStatus = "connected";

                controler.inited = true;

                work();
            //}
            //catch
            //{
            //    controler.serverStatus = "disconnected";
            //
            //    controler.inited = true;
            //}
        }

        public bool ping()
        {
            try
            {
                if (controler.serverStatus != "disconnected")
                {
                    Console.WriteLine("Pinging server");

                    NetworkStream clientStream = client.GetStream();

                    ASCIIEncoding encoder = new ASCIIEncoding();

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

        public void userID(string id)
        {
            try
            {
                if (controler.serverStatus != "disconnected")
                {
                    Console.WriteLine("Request username for userID " + id);

                    NetworkStream clientStream = client.GetStream();

                    ASCIIEncoding encoder = new ASCIIEncoding();

                    byte[] buffer = encoder.GetBytes("rt=userid;m=" + id + ";");

                    clientStream.Write(buffer, 0, buffer.Length);

                    clientStream.Flush();

                }
                else
                {
                    enteringQueue.Enqueue("userid;error;");
                }
            }
            catch
            {
                enteringQueue.Enqueue("userid;error;");
            }
        }

        public void color(string id)
        {
            try
            {
                if (controler.serverStatus != "disconnected")
                {
                    Console.WriteLine("Request color for userID " + id);

                    NetworkStream clientStream = client.GetStream();

                    ASCIIEncoding encoder = new ASCIIEncoding();

                    byte[] buffer = encoder.GetBytes("rt=color;id=" + id + ";");

                    clientStream.Write(buffer, 0, buffer.Length);

                    clientStream.Flush();
                }
                else
                {
                    enteringQueue.Enqueue("color;error;");
                }
            }
            catch
            {
                enteringQueue.Enqueue("color;error;");
            }
        }

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

        public void updateCGest()
        {
            controler.updateDatabase();
        }

        public void work()
        {
             while (controler.clientWork)
            {
                if (client.GetStream().DataAvailable)
                {
                    try
                    {
                        NetworkStream clientStream = client.GetStream();

                        ASCIIEncoding encoder = new ASCIIEncoding();

                        byte[] buffer = new byte[1024];

                        int read = clientStream.Read(buffer, 0, 1024);

                        string message = encoder.GetString(buffer);

                        string messageType = message.Split(';')[0].Split('=')[1];

                        string messageDest = message.Split(';')[0].Split('=')[0];

                        Console.WriteLine("Message received");

                        Console.WriteLine(messageDest);
                        Console.WriteLine(messageType);

                            switch(messageType)
                            {
                                case "ping":
                                    Console.WriteLine("Responding to ping");

                                    byte[] answer = encoder.GetBytes("at=ping;m=pong;");

                                    clientStream.Write(answer, 0, answer.Length);

                                    clientStream.Flush();

                                    controler.resetTimer();

                                    Console.WriteLine("server alive");
                                    break;
                                case "update":

                                    ThreadStart updateThreadStart = new ThreadStart(updateCGest);

                                    Thread updateThread = new Thread(updateThreadStart);

                                    updateThread.Start();

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
                                case "userid":
                                    Console.WriteLine("Server respond to userID");

                                    if (message.Split(';')[2].Split('=')[1] != "refused")
                                    {
                                        enteringQueue.Enqueue("userid;" + message.Split(';')[1].Split('=')[1] + ";" + message.Split(';')[2].Split('=')[1].TrimEnd(' ') + ";");
                                    }
                                    else
                                    {
                                        enteringQueue.Enqueue("userid;" + message.Split(';')[1].Split('=')[1].TrimEnd(' ') + ";refused;");
                                    }

                                    controler.resetTimer();
                                    break;
                                case "color":
                                    Console.WriteLine("Server respond to color");

                                    enteringQueue.Enqueue("color;" + message.Split(';')[1].Split('=')[1] + ";" + message.Split(';')[2].Split('=')[1] + ";");

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
