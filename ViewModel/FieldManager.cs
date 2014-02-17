﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Mechanics;
using Mechanics.Cell;
using Mechanics.Geometry;
using ViewModel.Annotations;

namespace ViewModel
{
    public sealed class FieldManager : INotifyPropertyChanged
    {
        private Mechanics.FieldManager.IFieldManager _fieldManager;


        public FieldManager()
        {
            Reset = new RelayCommand(ResetInner);
            ResetInner();
        }

        private void ResetInner()
        {
            _fieldManager = Factory.Instance.CreateEmptyFieldManager();
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
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ValueChoosen(int value, Point p)
        {
            var newField = _fieldManager.SetCell(p, (NumericValue)value);
            CurrentField.SetField(newField);
        }

        public ICommand Reset { get; set; }
    }
}
