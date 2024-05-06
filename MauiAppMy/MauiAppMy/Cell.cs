using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppMy
{
    public class Cell
    {
        public int value;
        public event EventHandler ValueChanged;
        public void Change()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
