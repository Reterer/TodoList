using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.UI
{
    class Frame
    {
        private int w;
        private int h;

        public int W => w;
        public int H => h;

        private ConsoleCharacter[,] data;

        public Frame(int h, int w)
        {
            this.w = w;
            this.h = h;

            data = new ConsoleCharacter[h, w];
            for(int i = 0; i < h; ++i)
            {
                for(int j = 0; j < w; ++j)
                {
                    data[i, j] = new ConsoleCharacter('.');
                }
            }
        }

        public ConsoleCharacter this[int y, int x]
        {
            get => data[y, x];
            set => data[y, x] = value;
        }

    }
    class ConsoleCharacter
    {
        public char Char;
        public ConsoleColor ForeGroundColor;
        public ConsoleColor BackGroundColor;

        public ConsoleCharacter(char ch, ConsoleColor fg, ConsoleColor bg)
        {
            this.Char = ch;
            this.ForeGroundColor = fg;
            this.BackGroundColor = bg;
        }

        public ConsoleCharacter(char ch)
            : this(ch, ConsoleColor.White, ConsoleColor.Black)
        { }

        public static bool operator ==(ConsoleCharacter a, ConsoleCharacter b)
        {
            return a.Char == b.Char && 
                   a.ForeGroundColor == b.ForeGroundColor &&
                   a.BackGroundColor == b.BackGroundColor;
        }
        public static bool operator !=(ConsoleCharacter a, ConsoleCharacter b)
        {
            return a.Char != b.Char ||
                   a.ForeGroundColor != b.ForeGroundColor ||
                   a.BackGroundColor != b.BackGroundColor;
        }
    }
}
