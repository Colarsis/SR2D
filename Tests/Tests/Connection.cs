using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
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

    }
}
