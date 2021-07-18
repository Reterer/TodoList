using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.UI.Components
{
    class InputLabel : Component
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int W { get; set; }

        public StringBuilder Text { get; set; }

        private int currPos;
        public int CurrPos 
        {
            get
                => currPos;
            set
            {
                currPos = value;
                if (value < 0)
                    currPos = 0;
                if (value > Text.Length)
                    currPos = Text.Length;
            }
        }
        public bool focused;


        public ConsoleColor foregroundColor { get; set; }
        public ConsoleColor backgroundColor { get; set; }
        public ConsoleColor HighlightBackgorundColor { get; set; }
        public ConsoleColor CurrPosColor { get; set; }

        public InputLabel(string text, int x, int y, int w)
        {
            this.Text = new StringBuilder(text);
            this.X = x;
            this.Y = y;
            this.W = w;

            currPos = text.Length;
            focused = false;

            foregroundColor = ConsoleColor.White;
            backgroundColor = Menu.Theme.DefaultColorBg;
            HighlightBackgorundColor = Menu.Theme.HighlightColorBg;
            CurrPosColor = Menu.Theme.CurrCharColorBg;

        }

        public void Update(ConsoleKeyInfo keyInfo)
        {
            ConsoleKey key = keyInfo.Key;
            if (key == ConsoleKey.LeftArrow)
            {
                CurrPos--;
            }
            else if (key == ConsoleKey.RightArrow)
            {
                CurrPos++;
            }
            else if (key == ConsoleKey.Backspace)
            {
                CurrPos--;
                if(currPos >= 0 && currPos < Text.Length)
                    Text.Remove(CurrPos, 1);
            }
            else
            {
                if (currPos == Text.Length)
                    Text.Append(keyInfo.KeyChar);
                else
                    Text[currPos] = keyInfo.KeyChar;
                CurrPos++;
            }
        }

        public override void Draw(Frame frame)
        {
            int y = Y - 1;
            int x = X - 1;
            for (int i = 0; i < Text.Length; ++i)
            {

                if (i % W == 0)
                {
                    y++;
                    x = X;
                }
                else
                    x++;

                frame[y, x].Char = Text[i];
                if (focused)
                {
                    if (i == currPos)
                        frame[y, x].BackGroundColor = CurrPosColor;
                    else
                        frame[y, x].BackGroundColor = HighlightBackgorundColor;
                }
                else
                    frame[y, x].BackGroundColor = backgroundColor;
            }

            if (focused && currPos == Text.Length)
                frame[Y + Text.Length / W, X + Text.Length % W].BackGroundColor = CurrPosColor;
            else
                frame[Y + Text.Length / W, X + Text.Length % W].BackGroundColor = backgroundColor;
        }
    }
}
