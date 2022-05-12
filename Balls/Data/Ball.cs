﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data
{
    public class Ball
    {
        private int x, y, r, weight;
        public int X 
        { 
            get => x;
            set
            {
                x = value;
            }
        }

        public int Y
        {
            get => y;
            set
            {
                y = value;
            }
        }

        public int R
        {
            get => r;
            set
            {
                r = value;
            }
        }

        public Ball(int x, int y, int r)
        {
            this.x = x;
            this.y = y;
            this.r = r;
            
        }

        public int Weight
        {
            get => weight;
            set
            {
                weight = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}