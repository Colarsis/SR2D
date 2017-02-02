using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CGestServer.Network
{
    class CGestReader
    {

        TcpClient client;
        CGestClient clientGest;
        CGestListener listener;

        public Thread readerThread;

        public CGestReader(CGestClient clientGest, CGestListener listener, TcpClient client)
        {
            this.client = client;
            this.clientGest = clientGest;
            this.listener = listener;

            ThreadStart readerThreadStart = new ThreadStart(read);

            readerThread = new Thread(readerThreadStart);

            readerThread.Start();
        }

        public void update()
        {
            NetworkStream clientStream = client.GetStream();

            ASCIIEncoding encoder = new ASCIIEncoding();

            byte[] updateRequest = encoder.GetBytes("rt=update;m=new_infos;");

            clientStream.Write(updateRequest, 0, updateRequest.Length);

            listener.writeConsole("Updating " + client.Client.RemoteEndPoint);

            clientStream.Flush();

            byte[] message = new byte[1024];

            int read = clientStream.Read(message, 0, 1024);

            listener.writeConsole("Received message from " + client.Client.RemoteEndPoint);

            string[] splittedMessage = encoder.GetString(message, 0, read).Split(';');

            bool validitie = checkMessageValiditie(splittedMessage);

            if (splittedMessage[0].Split('=')[0] == "at" && validitie == true && splittedMessage[0].Split('=')[1] == "update")
            {
                clientGest.updateStatus = "updated";
            }
        }

        public void read()
        {
            while (clientGest.readerWork)
            {
                try
                {
                    if (client.GetStream().DataAvailable)
                    {
                        clientGest.resetTimer();

                        NetworkStream clientStream = client.GetStream();

                        byte[] message = new byte[1024];

                        int read = clientStream.Read(message, 0, 1024);

                        ASCIIEncoding encoder = new ASCIIEncoding();

                        listener.writeConsole("Received message from " + client.Client.RemoteEndPoint);

                        string[] splittedMessage = encoder.GetString(message, 0, read).Split(';');

                        bool validitie = checkMessageValiditie(splittedMessage);

                        if (splittedMessage[0].Split('=')[0] == "rt" && validitie == true)
                        {
                            switch (splittedMessage[0].Split('=')[1])
                            {
                                case "update":
                                    listener.writeConsole("Message from " + client.Client.RemoteEndPoint + " is an update");

                                    byte[] updateBuffer = encoder.GetBytes("at=update;m=updating;");

                                    clientStream.Write(updateBuffer, 0, updateBuffer.Length);

                                    clientGest.setLastSeen(DateTime.Now);

                                    clientGest.status = "connected";

                                    listener.mF.sendUpdateToClient();
                                    break;
                                case "userid":
                                    listener.writeConsole("Message from " + client.Client.RemoteEndPoint + " is an user request");

                                    byte[] userBuffer = encoder.GetBytes("at=userid;id=" + splittedMessage[1].Split('=')[1] + ";un=" + listener.mF.getUsername(int.Parse(splittedMessage[1].Split('=')[1])) + ";");

                                    clientStream.Write(userBuffer, 0, userBuffer.Length);

                                    clientGest.setLastSeen(DateTime.Now);

                                    clientGest.status = "connected";
                                    break;
                                case "color":
                                    listener.writeConsole("Message from " + client.Client.RemoteEndPoint + " is an color request");

                                    byte[] colorBuffer = encoder.GetBytes("at=color;id=" + splittedMessage[1].Split('=')[1] + ";color=" + listener.mF.getColor(int.Parse(splittedMessage[1].Split('=')[1])) + ";");

                                    clientStream.Write(colorBuffer, 0, colorBuffer.Length);

                                    clientGest.setLastSeen(DateTime.Now);

                                    clientGest.status = "connected";
                                    break;
                                case "ping":
                                    listener.writeConsole("Message from " + client.Client.RemoteEndPoint + " is a ping");

                                    listener.writeConsole("Responding to " + client.Client.RemoteEndPoint);

                                    clientGest.setLastSeen(DateTime.Now);

                                    clientGest.status = "connected";

                                    clientStream.Flush();
                                    break;
                                case "connect":
                                    listener.writeConsole("Message from " + client.Client.RemoteEndPoint + " is a connect request");

                                    string[] connectionInfo = getConnectionInfos(encoder.GetString(message, 0, read));

                                    if (listener.mF.checkConnectionInfoValiditie(connectionInfo[0], connectionInfo[1]))
                                    {
                                        int id = listener.mF.getUserID(connectionInfo[0], connectionInfo[1]);

                                        clientGest.connectionID = id;

                                        byte[] connectAnswer = encoder.GetBytes("at=connect;m=right_infos;" + id.ToString() + ";");

                                        clientStream.Write(connectAnswer, 0, connectAnswer.Length);

                                        clientGest.setLastSeen(DateTime.Now);

                                        clientGest.status = "connected";

                                        listener.writeConsole("Responding to " + client.Client.RemoteEndPoint);
                                    }
                                    else
                                    {
                                        byte[] connectAnswer = encoder.GetBytes("at=connect;m=wrong_infos;");

                                        clientStream.Write(connectAnswer, 0, connectAnswer.Length);

                                        clientGest.setLastSeen(DateTime.Now);

                                        clientGest.status = "not_connected";

                                        listener.writeConsole("Responding to " + client.Client.RemoteEndPoint);
                                    }

                                    listener.writeConsole("Responding to " + client.Client.RemoteEndPoint);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (clientGest.askForPresence)
                        {
                            NetworkStream clientStream = client.GetStream();

                            ASCIIEncoding encoder = new ASCIIEncoding();

                            byte[] pingRequest = encoder.GetBytes("rt=ping;m=ping;");

                            clientStream.Write(pingRequest, 0, pingRequest.Length);

                            listener.writeConsole("Pinging " + client.Client.RemoteEndPoint);

                            clientStream.Flush();

                            byte[] pingAnswer = new byte[1024];

                            int read = clientStream.Read(pingAnswer, 0, 1024);

                            string[] splittedMessage = encoder.GetString(pingAnswer).Split(';');

                            listener.writeConsole("Received from " + client.Client.RemoteEndPoint);

                            clientGest.setLastSeen(DateTime.Now);

                            clientGest.status = "connected";

                            clientGest.askForPresence = false;
                        }
                        else
                        {
                            Thread.Sleep(200);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);

                    clientGest.status = "disconnected";

                    clientGest.readerWork = false;

                    clientGest.delete();
                }
            }

            client.Close();

        }

        public string[] getConnectionInfos(string stream)
        {
            string[] splittedMessage = stream.Split(';');

            string username = splittedMessage[1].Split('=')[1].ToString();

            string password = splittedMessage[2].Split('=')[1].ToString();

            string[] returnedArray = {"", ""};

            returnedArray[0] = username;
            returnedArray[1] = password;

            return returnedArray;
        }

        public bool checkMessageValiditie(string[] message)
        {

            string[] messageType = message[0].Split('=');

            if (messageType[0] == "rt")
            {
                switch (messageType[1])
                {
                    case "connect":
                        if (message[1].Split('=')[0] == "un" && message[2].Split('=')[0] == "pw")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case "userid":
                        if (message[1].Split('=')[0] == "m")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case "color":
                        if (message[1].Split('=')[0] == "id")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case "ping":
                        return true;
                    case "update":
                        return true;

                    default:
                        listener.writeConsole("Message from " + client.Client.RemoteEndPoint + " is invalid !");
                        return false;


                }
            }
            else if (messageType[0] == "at")
            {
                return true;
            }
            else
            {
                return false;
            }


        }

    }
}
