﻿using System;
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
       
        public event PropertyChangedEventHandler PropertyChanged;

        public Field()
        {
            Cells = new Cell[Cell.Width * Cell.Width];

            for (int x = 0; x < Cell.Width; x++)
            {
                for (int y = 0; y < Cell.Width; y++)
                {
                    Cells[x + y *Cell.Width] = new Cell();
                }
            }
        }

        public Cell[] Cells
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
