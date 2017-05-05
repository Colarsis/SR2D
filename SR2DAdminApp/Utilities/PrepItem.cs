using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGest.Utilities
{
    public class PrepItem
    {
        public int id;
        private string name;

        public string Name { get { return name;}  }
        public string student;
        public string type;
        public int quantity;
        public string state;
        public string supplement;

        public PrepItem(int id, string name, string type, int quantity)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.quantity = quantity;
        }

    }
}
