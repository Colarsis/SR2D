using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tests
{
    public class Connection
    {
        TcpClient client;
        IPEndPoint serverEndPoint;

        public string ip;

        public bool working = false;
        public bool inited = false;
        public bool connected = false;
        public bool clientWork = false;
        public bool updated = false;

        public Thread workThread;


        public Connection(string ip)
        {
            this.ip = ip;
        }

        public bool start()
        {
            try
            {
                client = new TcpClient();

                //Create end point
                serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), 11347);

                //Create connection
                client.Connect(serverEndPoint);

                clientWork = true;

                workThread = new Thread(work);

                workThread.Start();

                inited = true;

                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool connect(string username, string password)
        {
            try
            {
                NetworkStream clientStream = client.GetStream();

                ASCIIEncoding encoder = new ASCIIEncoding();

                byte[] buffer = encoder.GetBytes("rt=connect;un=" + username + ";pw=" + password + ";");

                clientStream.Write(buffer, 0, buffer.Length);

                clientStream.Flush();

                connected = true;

                Console.WriteLine("New connection established");

                return true;
            }
            catch (Exception e)
            {
                DialogResult dr = MessageBox.Show("Impossible de s'authifier au serveur: " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return false;
            }
        }

        public bool sendUpdate()
        {
            try
            {
                NetworkStream clientStream = client.GetStream();

                ASCIIEncoding encoder = new ASCIIEncoding();

                byte[] buffer = encoder.GetBytes("rt=update;m=new_infos;");

                clientStream.Write(buffer, 0, buffer.Length);

                clientStream.Flush();

                connected = true;

                Console.WriteLine("New connection established");

                return true;
            }
            catch (Exception e)
            {
                DialogResult dr = MessageBox.Show("Impossible d'update : " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        public void work()
        {
            while (clientWork)
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
                        switch (messageType)
                        {
                            case "ping":
                                Console.WriteLine("Responding to ping");

                                byte[] answer = encoder.GetBytes("at=ping;m=pong;"); //#LOL

                                clientStream.Write(answer, 0, answer.Length);

                                clientStream.Flush();

                                Console.WriteLine("server alive");
                                break;
                            case "update":

                                updated = true;

                                Console.WriteLine("Server request an update");

                                byte[] answer2 = encoder.GetBytes("at=update;m=updated;");

                                clientStream.Write(answer2, 0, answer2.Length);

                                clientStream.Flush();

                                break;
                        }

                        clientStream.Flush();


                    }
                    catch (Exception e)
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
