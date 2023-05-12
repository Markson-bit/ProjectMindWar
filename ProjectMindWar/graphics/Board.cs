using ProjectMindWar.src;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Windows.Input;
using static System.Formats.Asn1.AsnWriter;

namespace ProjectMindWar.graphics
{
    internal class Board : RenderWindow
    {
        // Path to folder with pngs
        static string figuresPath = AppDomain.CurrentDomain.BaseDirectory + @"..\\..\\..\\\graphics\img";

        public Board(uint width, uint height, string title) : base(new VideoMode(width, height), title)
        {
            // VSync, to limit FPS
            SetVerticalSyncEnabled(true);
            // Closed window event sub
            Closed += (_, __) => Close();
        }

        public void Run()
        {
            // Defining colors for squares
            Color dark = new Color(100, 100, 100);
            Color bright = new Color(200, 200, 200);

            // Square as element of board
            RectangleShape square = new RectangleShape(new Vector2f(100, 100));
            RectangleShape square2 = new RectangleShape(new Vector2f(25, 100));

            // DEFINING FIGURES
            // Pawn
            Texture pawnBTexture = new Texture(figuresPath + @"\pawnB.png");
            Sprite[] pawnsB = new Sprite[8];
            for (int i = 0; i < pawnsB.Length; i++)
            {
                pawnsB[i] = new Sprite(pawnBTexture);
                pawnsB[i].Scale = new Vector2f(50f / pawnsB[i].Texture.Size.X, 50f / pawnsB[i].Texture.Size.Y);
                pawnsB[i].Texture.Smooth = true;
            }

            Texture pawnWTexture = new Texture(figuresPath + @"\pawnW.png");
            Sprite[] pawnsW = new Sprite[8];
            for (int i = 0; i < pawnsB.Length; i++)
            {
                pawnsW[i] = new Sprite(pawnWTexture);
                pawnsW[i].Scale = new Vector2f(50f / pawnsW[i].Texture.Size.X, 50f / pawnsW[i].Texture.Size.Y);
                pawnsW[i].Texture.Smooth = true;
            }

            for (int i = 0; i < 8; i++)
            {
                pawnsB[i].Position = new Vector2f(25 + (100 * i), 625);
                pawnsW[i].Position = new Vector2f(25 + (100 * i), 125);
            }

            while (IsOpen)
            {
                DispatchEvents();
                Clear();

                // Drawing board (squares)
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        square.Position = new Vector2f(100 * i, 100 * j);
                        square.FillColor = ((i + j) % 2 == 0) ? dark : bright;
                        Draw(square);
                    }
                }

                // Moving pawn to selected position by mouse location
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    Vector2i mousePosition = Mouse.GetPosition(this);
                    Vector2f relativeMousePosition = new Vector2f(mousePosition.X + 540, mousePosition.Y + 160) - new Vector2f(Position.X, Position.Y);
                    int column = (int)Math.Floor(relativeMousePosition.X / 100);
                    int row = (int)Math.Floor(relativeMousePosition.Y / 100);

                    Vector2f newPosition = new Vector2f(column * 100 + 25, row * 100 + 25);
                    pawnsW[0].Position = newPosition;
                    Console.WriteLine($"New position: ({newPosition.X}, {newPosition.Y})");
                    Thread.Sleep(100);
                }

                // Drawing figures
                for (int i = 0; i < 8; i++)
                {
                    Draw(pawnsB[i]);
                    Draw(pawnsW[i]);
                }

                Display();
            }


        }
    }
}
