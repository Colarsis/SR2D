using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SR2DServer.Network
{
    public class SR2DListener
    {
        //Delegate (???) de la console
        public delegate void writeConsoleCallback(string text);

        Thread listenerThread;

        public mainForm mF;

        TcpListener listener = null;

        //********** CONSTRUCTEUR **********//

        public SR2DListener(mainForm mF)
        {
            this.mF = mF;

            listenerThread = new Thread(new ThreadStart(init));

            listenerThread.Start();
        }

        //********** END CONSTRUCTEUR **********//

        //TEMP

        //TEMP

        //Arret du listener
        public void stopListener()
        {
            mF.textBox1.AppendText("\r\n [" + DateTime.Now.ToString("dd/MM/yyyy h:mm TT") + "] " + "Arret du serveur ...");

            listenerThread.Join();

            mF.textBox1.AppendText("\r\n [" + DateTime.Now.ToString("dd/MM/yyyy h:mm TT") + "] " + "Serveur arrêté");
        }

        //Writer de la console locale
        public void writeConsole(string text)
        {
            if (mF.textBox1.InvokeRequired)
            {
                writeConsoleCallback c = new writeConsoleCallback(writeConsole);
                mF.Invoke(c, new object[] { text });
            }
            else
            {
                mF.textBox1.AppendText("\r\n [" + DateTime.Now.ToString("dd/MM/yyyy h:mm tt") + "] " + text);
            }
        }

        //Creation d'un reader
        public void createReader(SR2DListener listener, TcpClient client)
        {
            SR2DClient reader = new SR2DClient(listener, mF, client);

            mF.clientsList.Add(reader);
        }

        public static IPAddress GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        //Initialiastion du listener
        public void init()
        {
            try
            {
                //Port
                int port = 11347;

                IPAddress[] bug  = new IPAddress[5];

                //Adresse IP
                //IPAddress ipAdrr = IPAddress.Parse(Dns.GetHostAddresses(Dns.GetHostName())[2].ToString());
                IPAddress ipAdrr = GetLocalIPAddress();

                listener = new TcpListener(ipAdrr, port);

                //Demarrage du listener
                listener.Start();

                writeConsole("Le serveur a été démarré à l'adresse " + ipAdrr.ToString() + ".");
                writeConsole("Le serveur est en écoute sur le port " + port.ToString());

                //Boucle de travail
                while (mF.listenerWork)
                {
                    //Attente de connection
                    if (listener.Pending())
                    {
                        //Accepete les connection entrantes
                        TcpClient receivedClient = listener.AcceptTcpClient();

                        //Creation d'un reader
                        createReader(this, receivedClient);
                    }
                    else
                    {
                        //Attente
                        Thread.Sleep(100);
                    }
                   
                }

            }
            catch(Exception e)
            {
                Console.WriteLine("Erreur : " + e.StackTrace);
            }
            finally
            {
                //Toujours fermer le listener
                listener.Server.Close();
            }
                
        }

    }
}
