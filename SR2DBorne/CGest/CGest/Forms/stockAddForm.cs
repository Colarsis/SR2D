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
    public partial class stockAddForm : Form
    {
        CGestDatabase db;
        CGestNetworkClientControl networkControler;

        List<int> materiauxIdsList = new List<int>();
        List<int> chantierIdsList = new List<int>();

        stockForm sF;

        public stockAddForm(CGestClassBound cB, stockForm sF)
        {
            InitializeComponent();

            this.db = cB.db;
            this.networkControler = cB.networkControler;

            this.sF = sF;

            initData();
        }

        public void initData()
        {
            DataRowCollection matRowCollection = TablesUtilities.getRowsToArray("materiaux", db.Ds, out materiauxIdsList);
            DataRowCollection chantierRowCollection = TablesUtilities.getRowsToArray("chantier", db.Ds, out chantierIdsList);

            foreach (DataRow dr in matRowCollection)
            {
                string item = dr.ItemArray[1].ToString().TrimEnd();

                matComboBox.Items.Add(item);
            }

            foreach (DataRow dr in chantierRowCollection)
            {
                string item = dr.ItemArray[2].ToString().TrimEnd();

                chantierComboBox.Items.Add(item);
            }

            quantityNum.Value = 0;

            unitLabel.Text = "en N/A";

        }

        private void matComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string matId = materiauxIdsList[matComboBox.SelectedIndex].ToString();

            DataRow matRow = TablesUtilities.getMatFromId(matId, db.Ds);

            DataRow unitRow = TablesUtilities.getUnitFromId(matRow.ItemArray[2].ToString(), db.Ds);

            unitLabel.Text = "en " + unitRow.ItemArray[1].ToString();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string[] table = { "stock" };

            string matID = materiauxIdsList[matComboBox.SelectedIndex].ToString();
            string chantierID = chantierIdsList[chantierComboBox.SelectedIndex].ToString();
            string quantity = quantityNum.Value.ToString();

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

            foreach (DataRow dr in stockRowCollection)
            {
                if (dr.ItemArray[1].ToString() == chantierID && dr.ItemArray[2].ToString() == matID)
                {
                    DialogResult errorMsg = MessageBox.Show("Une entrée de stock similaire a été trouvée dans la base donnée. Veuillez utiliser la fonction \"Modifier\" pour modifier l'entrée de stock existante.", "Entrée de stock existante", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }

            string id = TablesUtilities.getNextID("stock", db);

            DataRow newRow = db.Ds.Dataset.Tables["stock"].NewRow();

            newRow["id_stock"] = id;
            newRow["id_chantier"] = chantierID;
            newRow["id_mat"] = matID;
            newRow["quantity"] = quantity;

            db.Ds.Dataset.Tables["stock"].Rows.Add(newRow);

            db.Ds.updateDatabase(table);

            this.Close();
        }

        private void stockAddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            sF.Enabled = true;
        }
    }
}
