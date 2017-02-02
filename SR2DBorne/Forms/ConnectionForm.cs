using CGest.Utilities;
using CGest.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CGest.Forms
{
    public partial class ConnectionForm : Form
    {
        initForm form;

        //Statut de la verif
        public bool returnStatut = false;
        private bool codeChange = false;

        //********** CONSTRUCTEUR **********//

        public ConnectionForm(initForm init)
        {
            InitializeComponent();
            
            this.form = init;

            updateConnectionList();
        }

        //********** END CONSTRUCTEUR **********//

        //Mise a hour de la liste des connections
        public void updateConnectionList()
        {
            XmlDocument xmlDoc;
            XmlNode root;
            XmlNodeList connectionList;

            connectionsList.Items.Clear();

            xmlDoc = new XmlDocument();

            xmlDoc.Load(CGestURLs.connectionXmlFile.getUrlFromRoot());
            root = xmlDoc.DocumentElement;
            connectionList = root.ChildNodes;
           
            for (int i = 0; i < root.ChildNodes.Count; i++)
            {
                if (root.ChildNodes[i].Attributes[0].Value != "default")
                {
                    connectionsList.Items.Add(root.ChildNodes[i].Attributes[0].Value);
                }
            }

            loadConnectionButton.Enabled = false;
            defaultConnectionCheckBox.Enabled = false;
            delConnectionButton.Enabled = false;
            codeChange = true;
            defaultConnectionCheckBox.CheckState = CheckState.Unchecked;
            codeChange = false;
        }

        //Mise a jour du bouton de connection par defaut
        public void setDefault(bool statut)
        {
            if (!codeChange)
            {
                if (connectionsList.SelectedItem != null && connectionsList.SelectedItem.ToString() != "")
                {
                    XmlDocument xmlDoc = new XmlDocument();

                    xmlDoc.Load(CGestURLs.connectionXmlFile.getUrlFromRoot());

                    if (xmlDoc.GetElementsByTagName(connectionsList.SelectedItem.ToString()).Count != 0)
                    {
                        switch (statut)
                        {
                            case true:
                                XmlNodeList l = xmlDoc.DocumentElement.ChildNodes;

                                foreach (XmlNode n in l)
                                {
                                    if (n.Attributes[1].Value == "Yes")
                                    {
                                        DialogResult dr = MessageBox.Show("Une connection par défaut existe déjà. Voulez vous la remplacer ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                        if (dr == DialogResult.Yes)
                                        {
                                            n.Attributes[1].Value = "No";

                                            xmlDoc.Save(CGestURLs.connectionXmlFile.getUrlFromRoot());
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                }
                                xmlDoc.GetElementsByTagName(connectionsList.SelectedItem.ToString())[0].Attributes[1].Value = "Yes";
                                break;
                            case false:
                                xmlDoc.GetElementsByTagName(connectionsList.SelectedItem.ToString())[0].Attributes[1].Value = "No";
                                break;
                        }

                        xmlDoc.Save(CGestURLs.connectionXmlFile.getUrlFromRoot());
                    }
                }
            }
        }

        public bool getDefault(string name)
        {
            if (connectionsList.SelectedItem != null && connectionsList.SelectedItem.ToString() != "")
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(CGestURLs.connectionXmlFile.getUrlFromRoot());

                if (xmlDoc.GetElementsByTagName(name).Count != 0)
                {
                    if (xmlDoc.GetElementsByTagName(name).Item(0).Attributes[1].Value.ToString() == "Yes")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public string[] checkDefault()
        {
            string[] ret = {"", ""};

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(CGestURLs.connectionXmlFile.getUrlFromRoot());

            XmlNodeList l = xmlDoc.DocumentElement.ChildNodes;

            foreach (XmlNode n in l)
            {
                if (n.Attributes[1].Value.ToString() == "Yes")
                {
                    ret[0] = "true";
                    ret[1] = n.Attributes[0].Value.ToString();

                    return ret;
                }
            }

            ret[0] = "false";

            return ret;
        }

        public bool getDouble(string name)
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(CGestURLs.connectionXmlFile.getUrlFromRoot());

            XmlNodeList connectionList = xmlDoc.DocumentElement.ChildNodes;

            foreach (XmlNode n in connectionList)
            {
                if (n.Attributes[0].Value.ToString() == name)
                {
                    return true;
                }
            }

            return false;
        }

        //Verification de la validité des infos
        public bool checkInfoValiditie()
        {
            if (pathTextBox.Text != "" && userTextBox.Text != "" && passwordTextBox.Text != "" && portNumUpDown.Value > 1000 && sourceTextBox.Text != "")
            {
                return true;
            }
            else
            {
                if (pathTextBox.Text == "") { errorProvider1.SetError(pathTextBox, "Valeur invalide !"); }
                if (userTextBox.Text == "") { errorProvider2.SetError(userTextBox, "Valeur invalide !"); }
                if (passwordTextBox.Text == "") { errorProvider3.SetError(passwordTextBox, "Valeur invalide !"); }
                if (sourceTextBox.Text == "") { errorProvider4.SetError(sourceTextBox, "Valeur invalide !"); }
                if (portNumUpDown.Value <= 1000) { errorProvider5.SetError(portNumUpDown, "Valeur invalide !"); }

                return false;
            }
        }

        //Chargement de la connection
        public void loadConnection()
        {
            if (connectionsList.SelectedItem != null && connectionsList.SelectedItem.ToString() != "")
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(CGestURLs.connectionXmlFile.getUrlFromRoot());

                XmlNodeList correspConnectionList = xmlDoc.GetElementsByTagName(connectionsList.SelectedItem.ToString());

                if (correspConnectionList.Count != 0)
                {
                    if (correspConnectionList[0].Attributes[1].Value == "Yes") { defaultConnectionCheckBox.Checked = true; } else { defaultConnectionCheckBox.Checked = false; }
                    pathTextBox.Text = correspConnectionList[0].ChildNodes[0].Attributes[0].Value;
                    userTextBox.Text = correspConnectionList[0].ChildNodes[1].Attributes[0].Value;
                    passwordTextBox.Text = correspConnectionList[0].ChildNodes[2].Attributes[0].Value;
                    portNumUpDown.Value = decimal.Parse(correspConnectionList[0].ChildNodes[3].Attributes[0].Value);
                    sourceTextBox.Text = correspConnectionList[0].ChildNodes[4].Attributes[0].Value;
                }
                else
                {
                    DialogResult errorBox = MessageBox.Show("Impossible de charger les données de connection sélectionnée !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        public void saveConnection()
        {
            connectionNamePopup coPop = new connectionNamePopup();

            DialogResult name = coPop.ShowDialog();

            if (name == DialogResult.OK)
            {
                string connectionName = coPop.textBox1.Text;

                if (!getDouble(connectionName))
                {
                    XmlDocument xmlDoc = new XmlDocument();

                    xmlDoc.Load(CGestURLs.connectionXmlFile.getUrlFromRoot());

                    XmlNode root = xmlDoc.DocumentElement;
                    XmlElement connectionMain = xmlDoc.DocumentElement;
                    XmlElement defaultCo = xmlDoc.CreateElement(connectionName);
                    XmlElement path = xmlDoc.CreateElement("DatabaseName");
                    XmlElement username = xmlDoc.CreateElement("Username");
                    XmlElement password = xmlDoc.CreateElement("Password");
                    XmlElement port = xmlDoc.CreateElement("Port");
                    XmlElement source = xmlDoc.CreateElement("Source");
                    defaultCo.SetAttribute("Name", connectionName);
                    defaultCo.SetAttribute("DefaultCo", "No");
                    path.SetAttribute("DatabaseName", pathTextBox.Text);
                    username.SetAttribute("Username", userTextBox.Text);
                    password.SetAttribute("Password", passwordTextBox.Text);
                    port.SetAttribute("Port", portNumUpDown.Value.ToString());
                    source.SetAttribute("Source", sourceTextBox.Text);
                    defaultCo.AppendChild(path);
                    defaultCo.AppendChild(username);
                    defaultCo.AppendChild(password);
                    defaultCo.AppendChild(port);
                    defaultCo.AppendChild(source);
                    root.AppendChild(defaultCo);

                    xmlDoc.Save(CGestURLs.connectionXmlFile.getUrlFromRoot());

                    updateConnectionList();
                }
                else
                {
                    XmlDocument xmlDoc = new XmlDocument();

                    xmlDoc.Load(CGestURLs.connectionXmlFile.getUrlFromRoot());

                    if (xmlDoc.GetElementsByTagName(connectionName) != null)
                    {
                        xmlDoc.GetElementsByTagName(connectionName)[0].ParentNode.RemoveChild(xmlDoc.GetElementsByTagName(connectionName)[0]);

                        XmlNode root = xmlDoc.DocumentElement;
                        XmlElement connectionMain = xmlDoc.DocumentElement;
                        XmlElement defaultCo = xmlDoc.CreateElement(connectionName);
                        XmlElement path = xmlDoc.CreateElement("DatabaseName");
                        XmlElement username = xmlDoc.CreateElement("Username");
                        XmlElement password = xmlDoc.CreateElement("Password");
                        XmlElement port = xmlDoc.CreateElement("Port");
                        XmlElement source = xmlDoc.CreateElement("Source");
                        defaultCo.SetAttribute("Name", connectionName);
                        defaultCo.SetAttribute("DefaultCo", "No");
                        path.SetAttribute("DatabaseName", pathTextBox.Text);
                        username.SetAttribute("Username", userTextBox.Text);
                        password.SetAttribute("Password", passwordTextBox.Text);
                        port.SetAttribute("Port", portNumUpDown.Value.ToString());
                        source.SetAttribute("Source", sourceTextBox.Text);
                        defaultCo.AppendChild(path);
                        defaultCo.AppendChild(username);
                        defaultCo.AppendChild(password);
                        defaultCo.AppendChild(port);
                        defaultCo.AppendChild(source);
                        root.AppendChild(defaultCo);

                        xmlDoc.Save(CGestURLs.connectionXmlFile.getUrlFromRoot());

                        updateConnectionList();
                    }
                }
            }
        }

        //Bouton de connection
        private void connectButton_Click(object sender, EventArgs e)
        {
            connectButton.DialogResult = DialogResult.OK;

            //Vérifie les infos
            if (checkInfoValiditie())
            {
                CGestDatabase db = new CGestDatabase();

                //Connecte
                CGestMessages m = db.connect(userTextBox.Text, passwordTextBox.Text, "postgres", sourceTextBox.Text, portNumUpDown.Value.ToString());

                if (m == CGestMessages.SUC_100)
                {
                        form.updateStatutOnWorker("Mise a jour de la base de donnée", 35);

                        if (db.updateDatabase(pathTextBox.Text))
                        {
                            form.setDatabase(db);

                            this.returnStatut = true;
                            this.Close(); 
                        }
                        else
                        {
                            this.returnStatut = false;
                            this.Close();
                        }
                }
                else
                {
                    DialogResult failBox = MessageBox.Show("Une erreur est survenue lors de la connection à la base de donnée ! Veuillez vérifier la validité de vos informations de connection.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void connectionsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (connectionsList.SelectedItem != null && connectionsList.SelectedItem.ToString() != "")
            {
                if (getDefault(connectionsList.SelectedItem.ToString()))
                {
                    codeChange = true;
                    defaultConnectionCheckBox.CheckState = CheckState.Checked;
                    codeChange = false;
                }
                else
                {
                    codeChange = true;
                    defaultConnectionCheckBox.CheckState = CheckState.Unchecked;
                    codeChange = false;
                }

                loadConnectionButton.Enabled = true;
                defaultConnectionCheckBox.Enabled = true;
                delConnectionButton.Enabled = true;
            }
            else
            {
                loadConnectionButton.Enabled = false;
                defaultConnectionCheckBox.Enabled = false;
                delConnectionButton.Enabled = false;
                codeChange = true;
                defaultConnectionCheckBox.CheckState = CheckState.Unchecked;
                codeChange = false;
            }
        }

        private void addConnectionButton_Click(object sender, EventArgs e)
        {
            if (connectionsList.SelectedItem != null && connectionsList.SelectedItem.ToString() != "")
            {
                if (checkInfoValiditie())
                {
                    DialogResult ask = MessageBox.Show("Voulez-vous créer une nouvelle connection ?", "Nouvelle connection ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (ask == DialogResult.Yes)
                    {
                        saveConnection();
                    }
                }
                else
                {
                    MessageBox.Show("Veuiller compléter le formulaire correctement", "Erreur");
                }

                    
            }
            else
            {
                if (checkInfoValiditie())
                {
                    saveConnection();
                }
                else
                {
                    MessageBox.Show("Veuiller compléter le formulaire correctement", "Erreur");
                }
            }
        }

        private void loadConnectionButton_Click(object sender, EventArgs e)
        {
            loadConnection();
        }

        private void defaultConnectionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (defaultConnectionCheckBox.CheckState == CheckState.Checked)
            {
                setDefault(true);
            }
            else
            {
                setDefault(false);
            }
        }

        private void delConnectionButton_Click(object sender, EventArgs e)
        {
            if (connectionsList.SelectedItem != null && connectionsList.SelectedItem.ToString() != "")
            {
                DialogResult dr = MessageBox.Show("Etes vous sûre de vouloir supprimer cette conection ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    XmlDocument xmlDoc = new XmlDocument();

                    xmlDoc.Load(CGestURLs.connectionXmlFile.getUrlFromRoot());

                    if (xmlDoc.GetElementsByTagName(connectionsList.SelectedItem.ToString()) != null)
                    {
                        xmlDoc.GetElementsByTagName(connectionsList.SelectedItem.ToString())[0].ParentNode.RemoveChild(xmlDoc.GetElementsByTagName(connectionsList.SelectedItem.ToString())[0]);

                        xmlDoc.Save(CGestURLs.connectionXmlFile.getUrlFromRoot());

                        updateConnectionList();
                    }     
                }
            }     
        }

    }
}
