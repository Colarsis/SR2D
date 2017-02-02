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

        CGestListener listener;

        public CGestReader(CGestListener listener, TcpClient client)
        {
            this.client = client;

            this.listener = listener;

            read();
        }

        public void read()
        {
            try
            {
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
                        case "ping":
                            listener.writeConsole("Message from " + client.Client.RemoteEndPoint + " is a ping");

                            byte[] pingBuffer = encoder.GetBytes("at=ping;m=pong");

                            clientStream.Write(pingBuffer, 0, pingBuffer.Length);

                            listener.writeConsole("Responding to " + client.Client.RemoteEndPoint);

                            clientStream.Flush();
                            break;
                        case "connect":
                            listener.writeConsole("Message from " + client.Client.RemoteEndPoint + " is a connect request");

                            listener.writeConsole("Responding to " + client.Client.RemoteEndPoint);
                            break;
                    }
                }

                client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                client.Close();
            }
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

                    case "ping":
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
