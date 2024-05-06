using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppMy
{
    public static class EntryExtentsion
    {
        public static void ChangesSubscribe(this Entry entry, Cell cell)
        {
            if (cell.value != null)
            {
                entry.Text = cell.value.ToString();
            }
            else
            {
                entry.Text = "?";
            }
        }
    }
}
