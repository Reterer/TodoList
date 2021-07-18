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
            int y = MaxW == -1 ? Y : Y -1;
            int x = X - 1;
            for (int i = 0; i < Text.Length; ++i)
            {
                if (MaxW != -1  && i % MaxW == 0)
                {
                    y++;
                    x = X;
                }
                else
                    x++;

                frame[y, x].Char = Text[i];
            }

            for (int j = 0; j < (Text.Length + MaxW - 1) / MaxW; ++j)
                for (int i = 0; i < MaxW; ++i)
                {
                    if (MaxW == -1)
                        break;

                    frame[Y + j, i + X].ForeGroundColor = ForegroundColor;
                    frame[Y + j, i + X].BackGroundColor = BackgroundColor;
                }
        }
    }
}
