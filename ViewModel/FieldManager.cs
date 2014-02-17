using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Mechanics.Cell;
using ViewModel.Annotations;

namespace ViewModel
{
    public class FieldManager : INotifyPropertyChanged
    {
        private readonly Mechanics.FieldManager.IFieldManager _fieldManager;


        public FieldManager()
        {
            _fieldManager = Mechanics.Factory.Instance.CreateEmptyFieldManager();
            CurrentField = new Field(_fieldManager.CurrentField, this);
        }


        private Field _currentField;
        public Field CurrentField
        {
            get { return _currentField; }
            set
            {
                if (Equals(value, _currentField)) return;
                _currentField = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ValueChoosen(int value, int x, int y)
        {
            var newField =_fieldManager.SetCell(x, y, (NumericValue) value);
            CurrentField.SetField(newField);
        }
    }
}
