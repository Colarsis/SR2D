using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGest.Utilities
{
    public class Food
    {
        public int id;
        private string name;

        public string Name { get { return name;}  }
        public string type;
        public int quantity;

        public Food(int id, string name, string type, int quantity)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.quantity = quantity;
        }
    }
}
