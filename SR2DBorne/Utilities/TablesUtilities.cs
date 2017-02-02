using System;
using CGest.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CGest.Utilities
{
    static class TablesUtilities
    {

        static public string arrayToAddress(string[] array)
        {
            if (array.Length == 3)
            {
                return array[0] + " " + array[1] + " " + array[2];
            }
            else
            {
                return null;
            }
        }

        static public string stringToDate(string date)
        {
            return "Le " + date.Split('H')[0] + " à " + date.Split('H')[1];
        }

        //TODO
        //static public string dateStringToArray(string date)
        //{
        //    return "Le " + date.Split('H')[0] + " à " + date.Split('H')[1];
        //}

        static public string getNextID(string table, CGestDatabase db)
        {
            DataRowCollection drC = db.Ds.Dataset.Tables[table].Rows;

            int maxID = 0;

            foreach (DataRow dr in drC)
            {
                if (int.Parse(dr.ItemArray[0].ToString()) > maxID)
                {
                    maxID = int.Parse(dr.ItemArray[0].ToString());
                }
            }

            maxID++;

            return maxID.ToString();
        }

        static public DataRowCollection getStockFromChantier(DataRow dr, CGestDataset ds)
        {
            DataRowCollection drC = ds.Dataset.Tables["stock"].Rows;

            DataTable drT = ds.Dataset.Tables["stock"].Clone();

            foreach (DataRow r in drC)
            {
                if (r.ItemArray[1].ToString() == dr.ItemArray[0].ToString())
                {
                    drT.Rows.Add(r.ItemArray);
                }
            }

            return drT.Rows;
        }

        static public DataRowCollection getStockFromMateriaux(string id, CGestDataset ds)
        {
            DataRowCollection drC = ds.Dataset.Tables["stock"].Rows;

            DataTable drT = ds.Dataset.Tables["stock"].Clone();

            foreach (DataRow r in drC)
            {
                if (r.ItemArray[2].ToString() == id)
                {
                    drT.Rows.Add(r.ItemArray);
                }
            }

            return drT.Rows;
        }

        static public DataRowCollection getChantierFromClient(string id, CGestDataset ds)
        {
            DataRowCollection drC = ds.Dataset.Tables["chantier"].Rows;

            DataTable drT = ds.Dataset.Tables["chantier"].Clone();

            foreach (DataRow r in drC)
            {
                if (r.ItemArray[1].ToString() == id)
                {
                    drT.Rows.Add(r.ItemArray);
                }
            }

            return drT.Rows;
        }

        static public DataRowCollection getMeetingsFromClient(string id, CGestDataset ds)
        {
            DataRowCollection drC = ds.Dataset.Tables["meetings"].Rows;

            DataTable drT = ds.Dataset.Tables["meetings"].Clone();

            foreach (DataRow r in drC)
            {
                if (r.ItemArray[1].ToString() == id)
                {
                    drT.Rows.Add(r.ItemArray);
                }
            }

            return drT.Rows;
        }

        static public DataRow getUnitFromId(string id, CGestDataset ds)
        {
            int i;

            for (i = 0; i < ds.Dataset.Tables["unit"].Rows.Count; i++)
            {

                DataRow unitRow = ds.Dataset.Tables["unit"].Rows[i];

                if (unitRow.ItemArray[0].ToString() == id)
                {
                    return unitRow;
                }
            }
            return null;
        }

        static public DataRow getMatFromId(string id, CGestDataset ds)
        {
            int i;

            for (i = 0; i < ds.Dataset.Tables["materiaux"].Rows.Count; i++)
            {

                DataRow r = ds.Dataset.Tables["materiaux"].Rows[i];

                if (r.ItemArray[0].ToString() == id)
                {
                    return r;
                }
            }

            return null;
        }

        static public DataRow getStockFromId(string id, CGestDataset ds)
        {
            int i;

            for (i = 0; i < ds.Dataset.Tables["stock"].Rows.Count; i++)
            {
                if (ds.Dataset.Tables["stock"].Rows[i].ItemArray[0].ToString() == id)
                {
                    return ds.Dataset.Tables["stock"].Rows[i];
                }
            }

            return null;
        }

        static public DataRow getChantierFromId(string id, CGestDataset ds)
        {
            int i;

            for (i = 0; i < ds.Dataset.Tables["chantier"].Rows.Count; i++)
            {
                if (ds.Dataset.Tables["chantier"].Rows[i].ItemArray[0].ToString() == id)
                {
                    return ds.Dataset.Tables["chantier"].Rows[i];
                }
            }

            return null;
        }

        static public DataRow getEventFromId(string id, CGestDataset ds)
        {
            int i;

            for (i = 0; i < ds.Dataset.Tables["event"].Rows.Count; i++)
            {
                if (ds.Dataset.Tables["event"].Rows[i].ItemArray[0].ToString() == id)
                {
                    return ds.Dataset.Tables["event"].Rows[i];
                }
            }

            return null;
        }

        static public string getClientNameFromID(string id, CGestDataset ds)
        {
            int i;

            for (i = 0; i < ds.Dataset.Tables["client"].Rows.Count; i++)
            {

                DataRow clientRow = ds.Dataset.Tables["client"].Rows[i];

                if (clientRow.ItemArray[0].ToString() == id)
                {
                    return clientRow.ItemArray[1].ToString();
                }
            }
            return null;
        }

        static public DataRowCollection getRowsToArray(string table, CGestDataset ds, out List<int> ids)
        {
            List<int> tempIds = new List<int>();

            foreach (DataRow dr in ds.Dataset.Tables[table].Rows)
            {
                tempIds.Add(int.Parse(dr.ItemArray[0].ToString()));
            }

            ids = tempIds;

            return ds.Dataset.Tables[table].Rows;
        }

        static public void removeStockByID(string id, CGestDataset ds)
        {
            ds.Dataset.Tables["stock"].Rows.Find(int.Parse(id)).Delete();
        }

        static public void removeChantierByID(string id, CGestDataset ds)
        {
            ds.Dataset.Tables["chantier"].Rows.Find(int.Parse(id)).Delete();
        }

        static public void removeClientByID(string id, CGestDataset ds)
        {
            ds.Dataset.Tables["client"].Rows.Find(int.Parse(id)).Delete();
        }

        static public void removeMateriauxByID(string id, CGestDataset ds)
        {
            ds.Dataset.Tables["materiaux"].Rows.Find(int.Parse(id)).Delete();
        }

        static public void removeEventByID(string id, CGestDataset ds)
        {
            ds.Dataset.Tables["event"].Rows.Find(int.Parse(id)).Delete();
        }

        static public void removeMeetingsByID(string id, CGestDataset ds)
        {
            ds.Dataset.Tables["meetings"].Rows.Find(int.Parse(id)).Delete();
        }
    }
}
