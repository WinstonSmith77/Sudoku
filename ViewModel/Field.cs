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
    public class Field : INotifyPropertyChanged
    {
        private int _width = 9;
        public event PropertyChangedEventHandler PropertyChanged;



        public Field()
        {
            Cells = new object[_width * _width];
        }

        public object[] Cells
        {
            get; 
            private set;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
