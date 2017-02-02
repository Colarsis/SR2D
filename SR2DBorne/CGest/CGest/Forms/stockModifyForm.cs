using CGest.Database;
using CGest.Utilities;
using CGest.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CGest.Forms
{
    public partial class stockModifyForm : Form
    {

        string stockID;

        List<int> materiauxIdsList = new List<int>();
        List<int> chantierIdsList = new List<int>();

        stockForm sF;
        CGestDatabase db;
        CGestNetworkClientControl networkControl;

        public stockModifyForm(string stock_id, CGestClassBound cB, stockForm sF)
        {
            InitializeComponent();

            this.stockID = stock_id;
            this.db = cB.db;
            this.networkControl = cB.networkControler;

            this.sF = sF;

            saveButton.DialogResult = DialogResult.OK;

            initData();
        }

        public void initData()
        {
            DataRowCollection matRowCollection = TablesUtilities.getRowsToArray("materiaux", db.Ds, out materiauxIdsList);
            DataRowCollection chantierRowCollection = TablesUtilities.getRowsToArray("chantier", db.Ds, out chantierIdsList);

            DataRow stockRow = TablesUtilities.getStockFromId(stockID, db.Ds);

            foreach (DataRow dr in matRowCollection)
            {
                string item = dr.ItemArray[1].ToString().TrimEnd();

                matComboBox.Items.Add(item);

                if (dr.ItemArray[0].ToString() == stockRow.ItemArray[2].ToString())
                {
                    matComboBox.SelectedIndex = matComboBox.Items.Count - 1;
                }
            }

            foreach (DataRow dr in chantierRowCollection)
            {
                string item = dr.ItemArray[2].ToString().TrimEnd();

                chantierComboBox.Items.Add(item);

                if (dr.ItemArray[0].ToString() == stockRow.ItemArray[1].ToString())
                {
                    chantierComboBox.SelectedIndex = chantierComboBox.Items.Count - 1;
                }
            }

            quantityNum.Value = int.Parse(stockRow.ItemArray[3].ToString());

            unitLabel.Text = "en " + TablesUtilities.getUnitFromId(TablesUtilities.getMatFromId(stockRow.ItemArray[2].ToString(), db.Ds).ItemArray[2].ToString(), db.Ds).ItemArray[1].ToString();
        }

        private void stockModifyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            sF.Enabled = true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void matComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow stockRow = TablesUtilities.getStockFromId(stockID, db.Ds);

            string matId = materiauxIdsList[matComboBox.SelectedIndex].ToString();

            unitLabel.Text = "en " + TablesUtilities.getUnitFromId(TablesUtilities.getMatFromId(stockRow.ItemArray[2].ToString(), db.Ds).ItemArray[2].ToString(), db.Ds).ItemArray[1].ToString();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string[] tables = { "stock" };

            DataRow stockRow = TablesUtilities.getStockFromId(stockID, db.Ds);

            object[] obj = stockRow.ItemArray;

            DataRowCollection stockRowCollection = db.Ds.Dataset.Tables["stock"].Rows;

            if (matComboBox.SelectedItem == "")
            {
                DialogResult errorMsg = MessageBox.Show("L'élément sélectionné est incorrecte. Veuillez remplir le champs correctement", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (chantierComboBox.SelectedItem == "")
            {
                DialogResult errorMsg = MessageBox.Show("Le chantier sélectionné est incorrecte. Veuillez remplir le champs correctement", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            obj[1] = chantierIdsList[chantierComboBox.SelectedIndex];
            obj[2] = materiauxIdsList[matComboBox.SelectedIndex];
            obj[3] = quantityNum.Value.ToString();

            db.Ds.Dataset.Tables["stock"].Rows.Find(stockID).ItemArray = obj;

            db.Ds.updateDatabase(tables);

            this.Close();
        }

    }
}
