using SR2DAdminApp.Database;
using SR2DAdminApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SR2DAdminApp.Network
{
    public class SR2DNetworkClientControl
    {
        public Queue<string> requestQueue = new Queue<string>(); //Queue for the waiting messages to be sent

        //Vars
        public string serverStatus = "disconnected";
        public bool connectedStatus = false;
        public bool clientWork = false;
        public string workType = "";
        public string workReturn = "";
        public bool inited = false;

        public string username {get; private set;}
        public int id = -1;

        public bool queueProcessing = false;

        public SR2DClient client;
        SR2DDatabase db;

        System.Timers.Timer serverValiditieTimer;

        public int port;

        public string ip;

        public SR2DNetworkClientControl(int port, string ip, SR2DDatabase db)
        {
            this.ip = ip;
            this.port = port;
            this.db = db;

            serverValiditieTimer = new System.Timers.Timer(15000);

            serverValiditieTimer.Elapsed += new ElapsedEventHandler(setDisconnected);

            serverValiditieTimer.Start();

            startClient();

            db.Ds.DatabaseUpdatedEvent += new SR2DDataset.EventHandler(requestUpdate); //Register to database update from user
        }

        //Update dataset function
        public void updateDatabase()
        {
            string[] tables = { "badges", "food", "foodTypes", "booking", "vars", "excludedFoodTypes" };

            db.Ds.updateDataset(tables); //Trigger dataset updating
        }

        //Sorting queue processing (avoid collision between messages)
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
                    case "ping":
                        break;
                }
            }

            queueProcessing = false;
        }

        //Send an update request
        private void internalRequestUpdate()
        {
            client.update();
        }

        //Add an update request to the queue
        public void requestUpdate()
        {
            requestQueue.Enqueue("update");

            if (!queueProcessing)
            {
                processQueue();
            }
        }

        //Start the client
        public void startClient()
        {
            clientWork = true;

            client = new SR2DClient(this);
        }

        //Send a connect message to the server
        public bool connect(string username, string password)
        {
            if(inited)
            {
                this.username = username;

                return client.connect(username, password);
            }
            else
            {
                return false;
            }       
        }

        public void setDisconnected(object sender, EventArgs e)
        {
            serverStatus = "disconnected";
        }

        //Reset the Still Alive timer
        public void resetTimer()
        {
            serverValiditieTimer.Stop();
            serverValiditieTimer.Start();
        }
        
    }
}
