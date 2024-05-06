using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppMy
{
    public class MatrixViewModel : INotifyPropertyChanged
    {
        private int[,] matrix;

        public int[,] Matrix
        {
            get { return matrix; }
            set
            {
                matrix = value;
                OnPropertyChanged(nameof(Matrix));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MatrixViewModel(int i)
        {
            matrix = new int[i, i];
        }
    }
}
