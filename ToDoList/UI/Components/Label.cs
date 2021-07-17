using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.UI.Components
{
    class Label : Component
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int MaxW { get; set; }
        public string Text { get; set; }

        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }

        public Label(string text, int x, int y)
        {
            this.Text = text;
            this.X = x;
            this.Y = y;
            this.MaxW = -1;
            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;
        }

        public Label(string text, int x, int y, int maxW)
            : this(text, x, y)
        {
            this.MaxW = maxW;
        }

        public override void Draw(Frame frame)
        {
            for (int i = 0; i < Text.Length; ++i)
            {
                if (i + X >= frame.W)
                    break;
                if (MaxW != -1 && i + X >= MaxW)
                    break;

                frame[Y, i + X].Char = Text[i];
                frame[Y, i + X].ForeGroundColor = ForegroundColor;
                frame[Y, i + X].BackGroundColor = BackgroundColor;
            }

            for (int i = Text.Length; i < MaxW; ++i)
            {
                if (MaxW == -1)
                    break;
                if (i + X >= frame.W)
                    break;

                frame[Y, i + X].Char = ' ';
                frame[Y, i + X].ForeGroundColor = ForegroundColor;
                frame[Y, i + X].BackGroundColor = BackgroundColor;
            }
        }
    }
}
