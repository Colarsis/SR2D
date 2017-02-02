using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ColarsisUserControls;
using CGest.Database;
using CGest.Utilities;
using CGest.Network;

namespace CGest.Forms
{
    public partial class eventCalendarForm : Form
    {

        MainForm mF;
        MeetingsForm meF;

        CGestClassBound cB;
        CGestDatabase db;
        CGestNetworkClientControl networkControl;

        public delegate void fillCalendarCallback();
        int focusedEventId;
        string typeFilter = "";

        public eventCalendarForm(MainForm mF, CGestClassBound cB)
        {
            InitializeComponent();

            this.mF = mF;

            this.cB = cB;
            this.db = cB.db;
            this.networkControl = cB.networkControler;

            updateInterval();

            fillCalendar();

            db.Ds.DatasetUpdatedEvent += new CGestDataset.EventHandler(update);
            calendar1.EventClickEvent += new Calendar.EventHandler(openInfo);
        }

        public eventCalendarForm(MeetingsForm meF, CGestClassBound cB, int focusedEventID)
        {
            InitializeComponent();

            this.meF = meF;

            this.focusedEventId = focusedEventID;

            this.cB = cB;
            this.db = cB.db;
            this.networkControl = cB.networkControler;

            //typeFilter = "meetings";

            updateInterval();

            fillCalendar();

            calendar1.setFocusedEvent(focusedEventId);

            addEventButton.Enabled = false;

            db.Ds.DatasetUpdatedEvent += new CGestDataset.EventHandler(update);
        }

        public void update()
        {
            if (calendar1.InvokeRequired)
            {
                fillCalendarCallback c = new fillCalendarCallback(fillCalendar);
                calendar1.Invoke(c, new object[] {});
                Console.WriteLine("Invoke finished");
            }
            else
            {
                calendar1.clearEvents();

                foreach (DataRow dr in db.Ds.Dataset.Tables["event"].Rows)
                {
                    string color = getColor(dr.ItemArray[7].ToString());

                    Console.WriteLine(color);

                    if (dr.ItemArray[6] == typeFilter)
                    {
                        calendar1.addEvent(new Event(int.Parse(dr.ItemArray[0].ToString()), dr.ItemArray[1].ToString(), dr.ItemArray[2].ToString(), processTime(dr.ItemArray[3].ToString()), processTime(dr.ItemArray[4].ToString()), Color.FromName(color)));
                    }
                    else
                    {
                        if (dr.ItemArray[6] == "")
                        {
                            calendar1.addEvent(new Event(int.Parse(dr.ItemArray[0].ToString()), dr.ItemArray[1].ToString(), dr.ItemArray[2].ToString(), processTime(dr.ItemArray[3].ToString()), processTime(dr.ItemArray[4].ToString()), Color.FromName(color)));
                        }
                    }
                }

                calendar1.refresh();
            }
        }

        public void fillCalendar()
        {

            calendar1.clearEvents();

            foreach (DataRow dr in db.Ds.Dataset.Tables["event"].Rows)
            {
                string color = getColor(dr.ItemArray[7].ToString());

                Console.WriteLine(color);

                calendar1.addEvent(new Event(int.Parse(dr.ItemArray[0].ToString()), dr.ItemArray[1].ToString(), dr.ItemArray[2].ToString(), processTime(dr.ItemArray[3].ToString()), processTime(dr.ItemArray[4].ToString()), Color.FromName(color)));
            }

            calendar1.refresh();
        }

        public string getColor(string id)
        {
            networkControl.requestColor(id);

            while (networkControl.client.enteringQueue.Count == 0)
            {
            }

            while (networkControl.client.enteringQueue.Peek().Split(';')[0] != "color")
            {
            }

            string dequeue = networkControl.client.enteringQueue.Dequeue();

            if (dequeue.Split(';')[1] == id && dequeue.Split(';')[1] != "error")
            {
                return dequeue.Split(';')[2];
            }
            else
            {
                DialogResult dr = MessageBox.Show("Une erreur est survenue lors du chargement des l'évenement", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();

                return "red";
            }
        }

        public void openInfo(int id)
        {
            eventForm eF = new eventForm(this, id, cB);

            eF.Show();
        }

        public DataRow getRowFromID(int id)
        {
            foreach (DataRow dr in db.Ds.Dataset.Tables["event"].Rows)
            {
                if (dr.ItemArray[0].ToString() == id.ToString())
                {
                    return dr;
                }
            }

            return null;
        }

        public DateTime processTime(string datetime)
        {
            string[] split = datetime.Split('H');

            string[] splitDate = split[0].Split('/');
            string[] splitTime = split[1].Split(':');

            return new DateTime(int.Parse(splitDate[2]), int.Parse(splitDate[1]), int.Parse(splitDate[0]), int.Parse(splitTime[0]), int.Parse(splitTime[1]), 0);
        }

        public void updateInterval()
        {
            DateTime monday = calendar1.CalendarStart;
            DateTime sunday = calendar1.CalendarStart.AddDays(6);

            string mondayDate = monday.Day + "/" + monday.Month + "/" + monday.Year;
            string sundayDate = sunday.Day + "/" + sunday.Month + "/" + sunday.Year;

            intervalLabel.Text = "Du " + mondayDate + " au " + sundayDate;
        }

        private void eventCalendarForm_Resize(object sender, EventArgs e)
        {
            calendar1.Width = 857 + (this.Width - 1083);
            calendar1.Height = 477 + (this.Height - 539);

            groupBox1.Height = 477 + (this.Height - 539);
        }

        private void eventCalendarForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mF.setButtonEnabling(mF.calendarButton, true);
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            calendar1.removeWeek(1);

            updateInterval();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            calendar1.addWeek(1);

            updateInterval();
        }

        private void addEventButton_Click(object sender, EventArgs e)
        {
            eventAddForm addForm = new eventAddForm(cB, this);

            addForm.Show();

            addEventButton.Enabled = false;
        }
    }
}
