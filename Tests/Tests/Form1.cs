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

namespace Tests
{n

    public partial class Form1 : Form
    {

        public TimeSpan dt;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            init();

            if (radioButton1.Checked)
            {
                runSyncO();
            }
            else if (radioButton2.Checked)
            {
                runSyncM();
            }
            else if (radioButton3.Checked)
            {
                runAsyncM();
            }
        }

        public void runSyncO()
        {
            DateTime start;

            WebRequest request = WebRequest.Create("http://"+textBox1.Text+":8080/final");
            WebRequest request2 = WebRequest.Create("http://" + textBox1.Text + ":8080/final");

            request.Method = "POST";
            request2.Method = "POST";

            string postData = "";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            string postData2 = "";
            byte[] byteArray2 = Encoding.UTF8.GetBytes(postData2);

            request.ContentType = "application/x-www-form-urlencoded";
            request2.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = byteArray.Length;
            request2.ContentLength = byteArray2.Length;

            Stream dataStream = request.GetRequestStream();
            start = DateTime.Now;
            Stream dataStream2 = request2.GetRequestStream();
            dt = new TimeSpan((long)((DateTime.Now - start).TotalMilliseconds)*10);

            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream2.Write(byteArray2, 0, byteArray2.Length);

            dataStream.Close();
            dataStream2.Close();

            WebResponse response = request.GetResponse();
            WebResponse response2 = request2.GetResponse();

            dataStream = response.GetResponseStream();
            dataStream2 = response2.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);
            StreamReader reader2 = new StreamReader(dataStream2);

            string responseFromServer = reader.ReadToEnd();
            string responseFromServer2 = reader2.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();
            reader2.Close();
            dataStream2.Close();
            response2.Close();

            clear();
        }

        public void runSyncM()
        {


            clear();
        }

        public void runAsyncM()
        {


            clear();
        }

        public void clear()
        {

        }

        public void init()
        {
        
        }

    }   
}
