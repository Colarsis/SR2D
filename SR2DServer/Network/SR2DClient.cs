using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;

namespace SR2DServer.Network
{
    public class SR2DClient
    {
        mainForm mF;
        SR2DListener listener;
        TcpClient client;

        public int connectionID = -1;

        SR2DReader reader;

        System.Timers.Timer pingTick;

        public volatile bool readerWork = false;
        public volatile bool askForPresence = false;
        public bool update = false;

        public string status;

        public EndPoint clientAddr;
        public string updateStatus = "updated";

        public DateTime last_seen;

        public SR2DClient(SR2DListener listener, mainForm mF, TcpClient client)
        {
            status = "unknown";

            this.mF = mF;
            this.listener = listener;
            this.client = client;

            clientAddr = this.client.Client.RemoteEndPoint;

            pingTick = new System.Timers.Timer(10000);

            pingTick.Start();

            pingTick.Elapsed += new ElapsedEventHandler(ping);

            

            createReader(this, listener, client);
        }

        public void delete()
        {
            listener.mF.clientsList.Remove(this);
        }

        public void resetTimer()
        {
            pingTick.Stop();
            pingTick.Start();
        }

        public void ping(object sender, EventArgs e)
        {
            askForPresence = true;
        }

        public void stopReader()
        {
            readerWork = false;

            reader.readerThread.Join();
        }

        public void sendUpdate()
        {
            reader.update();
        }

        public void createReader(SR2DClient clientGest, SR2DListener listener, TcpClient client)
        {
            readerWork = true;
            
            reader = new SR2DReader(clientGest, listener, client);
        }

    }
}
