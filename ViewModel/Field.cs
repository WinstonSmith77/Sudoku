﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Annotations;

namespace ViewModel
{
    public sealed class Field : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;


        public Field(Mechanics.Field.IField field, FieldManager parent)
        {
            _parent = parent;
            _field = field;
            Cells = new ObservableCollection<Cell>(new Cell[Cell.Width * Cell.Width]);

            for (int x = 0; x < Cell.Width; x++)
            {
                for (int y = 0; y < Cell.Width; y++)
                {
                    Cells[x + y * Cell.Width] = new Cell(_field[x, y], this, x, y);
                }
            }
        }

        public void SetField(Mechanics.Field.IField field)
        {
            _field = field;

            for (int x = 0; x < Cell.Width; x++)
            {
                for (int y = 0; y < Cell.Width; y++)
                {
                    var index = x + y * Cell.Width;
                    if (!Cells[index].InnerCell.Equals(_field[x, y]))
                    {
                        Cells[index] = new Cell(_field[x, y], this, x, y);
                    }
                }
            }
        }

        private readonly FieldManager _parent;
        private Mechanics.Field.IField _field;

        public ObservableCollection<Cell> Cells
        {
            get;
            private set;
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }



        internal void ValueChoosen(int value, int x, int y)
        {
            _parent.ValueChoosen(value, x, y);
        }


    }
}
