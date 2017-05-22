using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script;
using System.Web.Script.Serialization;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace Tests
{

    public partial class Form1 : Form
    {

        Main mF;

        public Form1(Main mF)
        {
            InitializeComponent();

            this.mF = mF;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {        runSyncO();
            }
            else if (radioButton3.Checked)
            {
                runAsyncM();
            }
        }

        public void runSyncO()
        {
            try
            {
                Stopwatch sw = new Stopwatch();

                label8.Text = "Etat: Initialisation";
                label7.Text = "dt:";

                init();

                WebRequest request = WebRequest.Create("http://" + textBox1.Text + ":8080/final");
                WebRequest request2 = WebRequest.Create("http://" + textBox1.Text + ":8080/final");

                request.Method = "POST";
                request2.Method = "POST";

                object[] postData = new object[] { "1", new object[] { new object[] { "11" } } };
                object[] postData2 = new object[] { "2", new object[] { new object[] { "11" } } };

                request.ContentType = "application/json";
                request2.ContentType = "application/json";

                label8.Text = "Etat: En cours";
                
                StreamWriter dataStream = new StreamWriter(request.GetRequestStream());
                StreamWriter dataStream2 = new StreamWriter(request2.GetRequestStream());

                sw.Start();
                dataStream.Write(new JavaScriptSerializer().Serialize(postData));
                dataStream2.Write(new JavaScriptSerializer().Serialize(postData2));
                sw.Stop();

                dataStream.Close();
                dataStream2.Close();

                WebResponse response = request.GetResponse();
                WebResponse response2 = request2.GetResponse();

                Stream dataStreamR = response.GetResponseStream();
                Stream dataStreamR2 = response2.GetResponseStream();

                StreamReader reader = new StreamReader(dataStreamR);
                StreamReader reader2 = new StreamReader(dataStreamR2);

                string responseFromServer = reader.ReadToEnd();
                string responseFromServer2 = reader2.ReadToEnd();

                label1.Text = "Résultat requête 1: " + responseFromServer;
                label2.Text = "Résultat requête 2: " + responseFromServer2;

                label7.Text = "dt: " + sw.Elapsed.TotalMilliseconds + " ms";

                reader.Close();
                dataStream.Close();
                response.Close();
                reader2.Close();
                dataStream2.Close();
                response2.Close();

                label8.Text = "Etat: Terminé";
            }
            catch(Exception e)
            {
                DialogResult dr = MessageBox.Show("Erreur" + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                label8.Text = "Etat: Erreur";
            }    
            finally
            {
                clear();
            }
        }

        public void asyncWorker(StreamWriter sw, object[] data)
        {
            sw.Write(new JavaScriptSerializer().Serialize(data));
        }

        public void runAsyncM()
        {
            try
            {
                Stopwatch sw = new Stopwatch();

                label8.Text = "Etat: Initialisation";
                label7.Text = "dt:";

                init();

                WebRequest request = WebRequest.Create("http://" + textBox1.Text + ":8080/final");
                WebRequest request2 = WebRequest.Create("http://" + textBox1.Text + ":8080/final");

                request.Method = "POST";
                request2.Method = "POST";

                object[] postData = new object[] { "1", new object[] { new object[] { "11" } } };
                object[] postData2 = new object[] { "2", new object[] { new object[] { "11" } } };

                request.ContentType = "application/json";
                request2.ContentType = "application/json";

                label8.Text = "Etat: En cours";               
                
                StreamWriter dataStream = new StreamWriter(request.GetRequestStream());
                StreamWriter dataStream2 = new StreamWriter(request2.GetRequestStream());

                sw.Start();
                Task task1 = Task.Factory.StartNew(() => asyncWorker(dataStream, postData));
                Task task2 = Task.Factory.StartNew(() => asyncWorker(dataStream2, postData2));
                Task.WaitAll(task1, task2);
                sw.Stop();        

                dataStream.Close();
                dataStream2.Close();

                WebResponse response = request.GetResponse();
                WebResponse response2 = request2.GetResponse();

                Stream dataStreamR = response.GetResponseStream();
                Stream dataStreamR2 = response2.GetResponseStream();

                StreamReader reader = new StreamReader(dataStreamR);
                StreamReader reader2 = new StreamReader(dataStreamR2);

                string responseFromServer = reader.ReadToEnd();

                string responseFromServer2 = reader2.ReadToEnd();

                label1.Text = "Résultat requête 1: " + responseFromServer;
                label2.Text = "Résultat requête 2: " + responseFromServer2;

                label7.Text = "dt: " + sw.Elapsed.TotalMilliseconds + " ms";

                reader.Close();
                dataStream.Close();
                response.Close();
                reader2.Close();
                dataStream2.Close();
                response2.Close();

                label8.Text = "Etat: Terminé";
            }
            catch (Exception e)
            {
                DialogResult dr = MessageBox.Show("Erreur" + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                label8.Text = "Etat: Erreur";
            }
            finally
            {
                clear();
            }
        }

        public void clear()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection("server=" + textBox2.Text + ";port=3306;uid=" + textBox4.Text + ";pwd=" + textBox3.Text + ";database=dev;");

                connection.Open();

                MySqlCommand cmd = new MySqlCommand("TRUNCATE TABLE booking;" +
                                                    "DELETE FROM badges WHERE name='test1';" +
                                                    "DELETE FROM badges WHERE name='test2';", connection);

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch(Exception e){}
            
        }

        public void init()
        {
            MySqlConnection connection = new MySqlConnection("server=" + textBox2.Text + ";port=3306;uid=" + textBox4.Text + ";pwd=" + textBox3.Text + ";database=dev;");

            connection.Open();

            MySqlCommand cmd = new MySqlCommand("INSERT INTO badges(first_name, name, code_id, class_code, class_fullname, passed, retired) values('test1', 'test1', 1, 'test1', 'test1', 0, 0);" +
                                                "INSERT INTO badges(first_name, name, code_id, class_code, class_fullname, passed, retired) values('test2', 'test2', 2, 'test2', 'test2', 0, 0);" +
                                                "UPDATE vars SET k='2' WHERE id=1;" +
                                                "UPDATE food SET quantity=1 WHERE id=11;" +
                                                "TRUNCATE TABLE booking", connection);

            cmd.ExecuteNonQuery();

            connection.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            mF.button1.Enabled = true;
        }

    }   
}
