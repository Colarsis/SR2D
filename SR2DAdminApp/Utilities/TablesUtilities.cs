using System;
using SR2DAdminApp.Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SR2DAdminApp.Utilities
{
    static class TablesUtilities //Useful functions for operations on tables
    {

        static public string stringToDate(string date)
        {
            return "Le " + date.Split('H')[0] + " à " + date.Split('H')[1];
        }

        static public string getNextID(string table, SR2DDatabase db)
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

        static public DataRowCollection getFoodTypeFromFood(DataRow dr, SR2DDataset ds)
        {
            DataRowCollection drC = ds.Dataset.Tables["food"].Rows;

            DataTable drT = ds.Dataset.Tables["food"].Clone();

            foreach (DataRow r in drC)
            {
                if (r.ItemArray[3].ToString() == dr.ItemArray[0].ToString())
                {
                    drT.Rows.Add(r.ItemArray);
                }
            }

            return drT.Rows;
        }

        static public DataRowCollection getExcludedFromFoodType(DataRow dr, SR2DDataset ds)
        {
            DataRowCollection drC = ds.Dataset.Tables["excluded_food_types_join"].Rows;

            DataTable drT = ds.Dataset.Tables["excluded_food_types_join"].Clone();

            foreach (DataRow r in drC)
            {
                if (r.ItemArray[1].ToString() == dr.ItemArray[0].ToString())
                {
                    drT.Rows.Add(r.ItemArray);
                }
            }

            return drT.Rows;
        }

        static public DataRow getFoodFromId(string id, SR2DDataset ds)
        {
            return ds.Dataset.Tables["food"].Rows.Find(int.Parse(id));
        }

        static public DataRow getFoodTypeFromId(string id, SR2DDataset ds)
        {
            return ds.Dataset.Tables["food_types"].Rows.Find(int.Parse(id));
        }

        static public DataRow getExcludedFromId(string id, SR2DDataset ds)
        {
            return ds.Dataset.Tables["excluded_food_types_join"].Rows.Find(int.Parse(id));
        }

        static public DataRow getVarFromId(string id, SR2DDataset ds)
        {
            /*int i;

            for (i = 0; i < ds.Dataset.Tables["vars"].Rows.Count; i++)
            {
                if (ds.Dataset.Tables["vars"].Rows[i].ItemArray[0].ToString() == id)
                {
                    return ds.Dataset.Tables["vars"].Rows[i];
                }
            }

            return null;*/

            return ds.Dataset.Tables["vars"].Rows.Find(int.Parse(id));
        }

        static public DataRowCollection getRowsToArray(string table, SR2DDataset ds, out List<int> ids)
        {
            List<int> tempIds = new List<int>();

            foreach (DataRow dr in ds.Dataset.Tables[table].Rows)
            {
                tempIds.Add(int.Parse(dr.ItemArray[0].ToString()));
            }

            ids = tempIds;

            return ds.Dataset.Tables[table].Rows;
        }

        static public void removeFoodByID(string id, SR2DDataset ds)
        {
            ds.Dataset.Tables["food"].Rows.Find(int.Parse(id)).Delete();
        }

        static public void removeFoodTypeByID(string id, SR2DDataset ds)
        {
            ds.Dataset.Tables["food_types"].Rows.Find(int.Parse(id)).Delete();
        }
    }
}
