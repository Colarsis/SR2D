using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CGestServer.Network
{
    class CGestListener
    {
        public delegate void writeConsoleCallback(string text);

        Thread listenerThread;

        public mainForm mF;

        TcpListener listener = null;

        public CGestListener(mainForm mF)
        {
            this.mF = mF;

            listenerThread = new Thread(new ThreadStart(init));

            listenerThread.Start();
        }

        public void addClient(string name, EndPoint ipAddr)
        {
            if (!mF.clientsDictionary.ContainsKey(ipAddr))
            {
                mF.clientsDictionary.Add(ipAddr, name);
                writeConsole("Nouvel utilisateur enregistré");
            }
            else
            {
                writeConsole("l'utilisateur a déjà été enregistré");
            }
        }

        public void stopListener()
        {
            mF.textBox1.AppendText("\r\n [" + DateTime.Now.ToString("dd/MM/yyyy h:mm TT") + "] " + "Arret du serveur ...");

            listenerThread.Join();

            mF.textBox1.AppendText("\r\n [" + DateTime.Now.ToString("dd/MM/yyyy h:mm TT") + "] " + "Serveur arrêté");
        }

        public void writeConsole(string text)
        {
            if (mF.textBox1.InvokeRequired)
            {
                writeConsoleCallback c = new writeConsoleCallback(writeConsole);
                mF.Invoke(c, new object[] { text });
            }
            else
            {
                mF.textBox1.AppendText("\r\n [" + DateTime.Now.ToString("dd/MM/yyyy h:mm TT") + "] " + text);
            }
        }

        public void createReader(CGestListener listener, TcpClient client)
        {
            CGestReader reader = new CGestReader(listener, client);
        }

        public void init()
        {
            try
            {
                int port = 11347;

                IPAddress ipAdrr = IPAddress.Parse("127.0.0.1");

                listener = new TcpListener(ipAdrr, port);

                listener.Start();

                writeConsole("Le serveur a été démarré à l'adresse " + ipAdrr.ToString() + ".");
                writeConsole("Le serveur est en écoute sur le port " + port.ToString());

                while (mF.listenerWork)
                {

                    if (listener.Pending())
                    {
                        TcpClient receivedClient = listener.AcceptTcpClient();

                        ThreadStart readerThreadStart = delegate() { createReader(this, receivedClient); };

                        Thread readerThread = new Thread(readerThreadStart);

                        readerThread.Start();
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                   
                }

            }
            catch(Exception e)
            {
                writeConsole("Erreur : " + e.Message);

                
            }
            finally
            {
                listener.Server.Close();
            }
                
        }

    }
}
