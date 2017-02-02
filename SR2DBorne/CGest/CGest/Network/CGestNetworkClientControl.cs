using CGest.Database;
using CGest.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace CGest.Network
{
    public class CGestNetworkClientControl
    {
        public Queue<string> requestQueue = new Queue<string>();

        public string serverStatus = "disconnected";
        public bool connectedStatus = false;
        public bool clientWork = false;
        public string workType = "";
        public string workReturn = "";
        public bool inited = false;

        public string username = "";
        public int id = -1;

        public bool queueProcessing = false;

        public CGestClient client;
        CGestDatabase db;

        System.Timers.Timer serverValiditieTimer;

        public int port;

        public string ip;

        public CGestNetworkClientControl(int port, string ip, CGestDatabase db)
        {
            this.ip = ip;
            this.port = port;
            this.db = db;

            serverValiditieTimer = new System.Timers.Timer(15000);

            serverValiditieTimer.Elapsed += new ElapsedEventHandler(setDisconnected);

            serverValiditieTimer.Start();

            startClient();

            db.Ds.DatabaseUpdatedEvent += new CGestDataset.EventHandler(requestUpdate);
        }

        public void updateDatabase()
        {
            string[] tables = { "materiaux", "stock", "unit", "chantier", "fournisseur", "representant", "matAndFourn", "client", "event" };

            db.Ds.updateDataset(tables);
        }

        private void processQueue()
        {
            queueProcessing = true;

            while (requestQueue.Count > 0)
            {
                string[] requestType = requestQueue.Dequeue().Split(';');

                switch (requestType[0])
                {
                    case "update":
                        internalRequestUpdate();
                        break;
                    case "userid":
                        internalUserID(requestType[1]);
                        break;
                    case "color":
                        internalColor(requestType[1]);
                        break;
                    case "ping":
                        break;
                }
            }

            queueProcessing = false;
        }

        private void internalRequestUpdate()
        {
            client.update();
        }

        public void requestUpdate()
        {
            requestQueue.Enqueue("update");

            if (!queueProcessing)
            {
                processQueue();
            }
        }

        public void requestUserID(string id)
        {
            requestQueue.Enqueue("userid;" + id + ";");

            if (!queueProcessing)
            {
                processQueue();
            }
        }

        public void requestColor(string id)
        {
            requestQueue.Enqueue("color;" + id + ";");

            if (!queueProcessing)
            {
                processQueue();
            }
        }

        public void startClient()
        {
            clientWork = true;

            client = new CGestClient(this);
        }

        public bool connect(string username, string password)
        {
            this.username = username;

            return client.connect(username, password);
        }

        private void internalUserID(string id)
        {
            client.userID(id);
        }

        public void internalColor(string id)
        {
            client.color(id);
        }

        public void setDisconnected(object sender, EventArgs e)
        {
            serverStatus = "disconnected";
        }

        public void resetTimer()
        {
            serverValiditieTimer.Stop();
            serverValiditieTimer.Start();
        }
        
    }
}
