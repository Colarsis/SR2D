using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGest.Utilities
{
    public class Booking
    {

        public int id;
        private string badgeID;
        private string foodID;

        public string Badge { get { return badgeID;}  }

        public Booking(int id, string badgeID, string foodID)
        {
            this.id = id;
            this.badgeID = badgeID;
            this.foodID = foodID;
        }
    }
}
