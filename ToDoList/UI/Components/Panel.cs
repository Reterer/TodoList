using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.UI.Components
{
    class Panel : Component
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int W { get; set; }
        public int H { get; set; }

        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }

        public Panel(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;

            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;

        }

        private void SetChar(Frame frame, char ch, int x, int y)
        {
            frame[y, x].Char = ch;
            frame[y, x].ForeGroundColor = ForegroundColor;
            frame[y, x].BackGroundColor = BackgroundColor;
        }

        public override void Draw(Frame frame)
        {
            // Верхяя и нижния линии
            for(int i = 1; i < W; ++i)
            {
                SetChar(frame, '═', i + X, Y);
                SetChar(frame, '═', i + X, Y + H);
            }

            // Левая и правая линии
            for (int i = 1; i < H; ++i)
            {
                SetChar(frame, '║', X, Y + i);
                SetChar(frame, '║', X + W, Y + i);
            }

            // Углы
            SetChar(frame, '╔', X, Y);
            SetChar(frame, '╚', X, Y + H);
            SetChar(frame, '╗', X + W, Y);
            SetChar(frame, '╝', X + W, Y + H);

            // Заполняем внутренее простарнство
            for(int x = X + 1; x < W + X; ++x)
            {
                for(int y = Y + 1; y < H + Y; ++y)
                {
                    SetChar(frame, ' ', x, y);
                }
            }
        }
    }
}
