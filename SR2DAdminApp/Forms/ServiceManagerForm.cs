using BrightIdeasSoftware;
using CGest.Utilities;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using SR2DAdminApp.Database;
using SR2DAdminApp.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SR2DAdminApp.Forms
{
    public partial class ServiceManagerForm : Form
    {

        MainForm mF;

        SR2DDatabase db;
        SR2DClassBound cB;

        public ChartValues<MeasureModel> reserv { get; set; }
        public ChartValues<MeasureModel> retired { get; set; }

        public ServiceStates currentStatus;

        public Timer timer;

        public delegate void thisCallback();

        public ServiceManagerForm(MainForm mF, SR2DClassBound cB)
        {
            InitializeComponent();

            this.cB = cB;
            this.db = cB.db;

            this.mF = mF;

            db.Ds.DatasetUpdatedEvent += new SR2DDataset.EventHandler(update);

            #region INIT_CHARTS
            var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.DateTime.Ticks)   //use DateTime.Ticks as X
                .Y(model => model.Value);           //use the value property as Y

            //lets save the mapper globally.
            Charting.For<MeasureModel>(mapper);

            //the ChartValues property will store our values array
            reserv = new ChartValues<MeasureModel>();
            retired = new ChartValues<MeasureModel>();

            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = reserv,
                    PointGeometry = null,
                    LineSmoothness = 0.5
                }
            };

            cartesianChart1.DataTooltip = null;



            cartesianChart1.AxisX.Add(new Axis
            {
                DisableAnimations = false,
                LabelFormatter = value => new System.DateTime((long)value).ToString("HH:mm:ss"),
                Separator = new Separator
                {
                    Step = TimeSpan.FromSeconds(60).Ticks
                }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Separator = new Separator
                {
                    Step = 1,
                }
            });

            cartesianChart1.AxisY[0].MinValue = 0;

            SetAxisLimits(System.DateTime.Now, cartesianChart1);

            cartesianChart2.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = retired,
                    PointGeometry = null,
                    LineSmoothness = 0.5
                }
            };

            cartesianChart2.DataTooltip = null;

            

            cartesianChart2.AxisX.Add(new Axis
            {
                DisableAnimations = false,
                LabelFormatter = value => new System.DateTime((long)value).ToString("HH:mm:ss"),
                Separator = new Separator
                {
                    Step = TimeSpan.FromSeconds(60).Ticks
                }
            });

            cartesianChart2.AxisY.Add(new Axis
            {
                Separator = new Separator
                {
                    Step = 1,
                }
            });

            cartesianChart2.AxisY[0].MinValue = 0;

            SetAxisLimits(System.DateTime.Now, cartesianChart2);

            guiUpdate();

            timer = new Timer
            {
                Interval = 5000
            };
            timer.Tick += timerOnTick;

            timer.Start();

            #endregion

            #region INIT_LIST

            objectListView1.CellEditActivation = ObjectListView.CellEditActivateMode.DoubleClick;
            objectListView1.ShowGroups = true;
            objectListView1.Sort(2);

            populateDispList();

            #endregion

        }

        public void setServiceMode()
        {
            int state = (int)(TablesUtilities.getVarFromId("1", db.Ds).ItemArray[2]);

            switch(state)
            {
                case 0:
                    updateButton(ServiceStates.Stopped);
                    tabControl1.Enabled = false;
                    objectListView1.Visible = false;
                    cartesianChart1.Visible = false;
                    cartesianChart2.Visible = false;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    break;
                case 1:
                    updateButton(ServiceStates.Preparation);
                    tabControl1.Enabled = true;
                    cartesianChart1.Visible = false;
                    cartesianChart2.Visible = false;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = false;
                    break;
                case 2:
                    updateButton(ServiceStates.Booking);
                    tabControl1.Enabled = true;
                    objectListView1.Visible = false;
                    cartesianChart1.Visible = true;
                    cartesianChart2.Visible = true;
                    label3.Visible = false;
                    label4.Visible = true;
                    label5.Visible = true;
                    break;
                case 3:
                    updateButton(ServiceStates.Concoction);
                    tabControl1.Enabled = true;
                    objectListView1.Visible = false;
                    cartesianChart1.Visible = true;
                    cartesianChart2.Visible = true;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = true;
                    break;
                case 4:
                    updateButton(ServiceStates.Distribution);
                    tabControl1.Enabled = true;
                    objectListView1.Visible = false;
                    cartesianChart1.Visible = true;
                    cartesianChart2.Visible = true;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = true;
                    break;
                case 5:
                    updateButton(ServiceStates.Ended);
                    tabControl1.Enabled = true;
                    objectListView1.Visible = false;
                    cartesianChart1.Visible = true;
                    cartesianChart2.Visible = true;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = true;
                    break;
            }            
        }

        public void setServiceMode(int state)
        {
            switch (state)
            {
                case 0:
                    currentStatus = ServiceStates.Stopped;
                    updateButton(ServiceStates.Stopped);
                    tabControl1.Enabled = false;
                    objectListView1.Visible = false;
                    cartesianChart1.Visible = false;
                    cartesianChart2.Visible = false;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    break;
                case 1:
                    currentStatus = ServiceStates.Preparation;
                    updateButton(ServiceStates.Preparation);
                    tabControl1.Enabled = true;
                    cartesianChart1.Visible = false;
                    cartesianChart2.Visible = false;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = false;
                    break;
                case 2:
                    currentStatus = ServiceStates.Booking;
                    updateButton(ServiceStates.Booking);
                    tabControl1.Enabled = true;
                    objectListView1.Visible = false;
                    cartesianChart1.Visible = true;
                    cartesianChart2.Visible = true;
                    label3.Visible = false;
                    label4.Visible = true;
                    label5.Visible = true;
                    break;
                case 3:
                    currentStatus = ServiceStates.Concoction;
                    updateButton(ServiceStates.Concoction);
                    tabControl1.Enabled = true;
                    objectListView1.Visible = false;
                    cartesianChart1.Visible = true;
                    cartesianChart2.Visible = true;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = true;
                    break;
                case 4:
                    currentStatus = ServiceStates.Distribution;
                    updateButton(ServiceStates.Distribution);
                    tabControl1.Enabled = true;
                    objectListView1.Visible = false;
                    cartesianChart1.Visible = true;
                    cartesianChart2.Visible = true;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = true;
                    break;
                case 5:
                    currentStatus = ServiceStates.Ended;
                    updateButton(ServiceStates.Ended);
                    tabControl1.Enabled = true;
                    objectListView1.Visible = false;
                    cartesianChart1.Visible = true;
                    cartesianChart2.Visible = true;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = true;
                    break;
            }
        }

        public void update()
        {
            if (this.InvokeRequired)
            {
                thisCallback c = new thisCallback(guiUpdate);
                Invoke(c);
                thisCallback c2 = new thisCallback(populateDispList);
                Invoke(c2);
            }
            else
            {
                guiUpdate();
                populateDispList();
            }
        }

        public void updateButton(ServiceStates state)
        {
            switch(state)
            {
                case ServiceStates.Stopped:
                    actionButton.Text = "COMMENCER UN NOUVEAU SERVICE";
                    actionButton.BackColor = Color.FromArgb(0, 192, 0);
                    break;
                case ServiceStates.Preparation:
                    actionButton.Text = "OUVRIR LES RESERVATIONS";
                    actionButton.BackColor = Color.Yellow;
                    break;
                case ServiceStates.Booking:
                    actionButton.Text = "FERMER LES RESERVATIONS";
                    actionButton.BackColor = Color.FromArgb(255, 128, 0);
                    break;
                case ServiceStates.Concoction:
                    actionButton.Text = "COMMENCER LA DISTRIBUTION";
                    actionButton.BackColor = Color.DeepSkyBlue;
                    break;
                case ServiceStates.Distribution:
                    actionButton.Text = "TERMINER LA DISTRIBUTION";
                    actionButton.BackColor = Color.Red;
                    break;
                case ServiceStates.Ended:
                    actionButton.Text = "TERMINER LE SERVICE";
                    actionButton.BackColor = Color.Red;
                    break;
            }
        }

        #region LIST

        public void populateDispList()
        {
            objectListView1.Items.Clear();

            List<Food> f = new List<Food>();

            foreach(DataRow food in db.Ds.Dataset.Tables["food"].Rows)
            {
                f.Add(new Food((int)food.ItemArray[0], (string)food.ItemArray[1], (string)(TablesUtilities.getFoodTypeFromId(food.ItemArray[3].ToString(), db.Ds).ItemArray[1]), (int)food.ItemArray[2]));
            }

            objectListView1.AddObjects(f);

            objectListView1.Sort(2);

        }

        #endregion

        #region CHARTS
        private void SetAxisLimits(System.DateTime now, LiveCharts.WinForms.CartesianChart cc)
        {
            cc.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromSeconds(0).Ticks;
            cc.AxisX[0].MinValue = now.Ticks - TimeSpan.FromMinutes(10).Ticks;
        }  

        public void guiUpdate()
        {
            var now = System.DateTime.Now;

            int ret = 0;
            int res = 0;

            foreach(DataRow dr in db.Ds.Dataset.Tables["badges"].Rows)
            {
                if(dr.ItemArray[6].ToString() == "True")
                {
                    foreach(DataRow drB in db.Ds.Dataset.Tables["booking"].Rows)
                    {
                        if(drB.ItemArray[1].ToString() == dr.ItemArray[0].ToString())
                        {
                            res++;
                            break;
                        }
                    }

                    if (dr.ItemArray[7].ToString() == "True")
                    {
                        ret++;
                    }
                }
            }

            retired.Add(new MeasureModel
            {
                DateTime = now,
                Value = ret
            });

            reserv.Add(new MeasureModel
            {
                DateTime = now,
                Value = res
            });

            SetAxisLimits(now, cartesianChart1);
            SetAxisLimits(now, cartesianChart2);

        }

        private void timerOnTick(object sender, EventArgs eventArgs)
        {
            guiUpdate();
        }

        #endregion

        private void ServiceManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Ds.DatasetUpdatedEvent -= new SR2DDataset.EventHandler(update);

            mF.setButtonEnabling(mF.dayGestButton, true);
        }

        private void actionButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("");
        }

    }
}
