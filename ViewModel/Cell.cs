using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Annotations;

namespace ViewModel
{
    public class Cell : INotifyPropertyChanged
    {
        public const int Start = 1;
        public const int Width = 9;

        public Cell()
        {
            Values = Enumerable.Range(Start, Width).ToArray();
            Result = "?";
        }

        public int[] Values
        {
            get;
            set;
        }

        public string Result { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
