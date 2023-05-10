using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using static System.Formats.Asn1.AsnWriter;

namespace ProjectMindWar.graphics
{
    internal class Board : RenderWindow
    {
        string figuresPath = AppDomain.CurrentDomain.BaseDirectory + @"..\\..\\..\\\graphics\img";

        public Board(uint width, uint height, string title) : base(new VideoMode(width, height), title)
        {
            // VSync, to limit FPS and Closed window event sub
            SetVerticalSyncEnabled(true);
            Closed += (_, __) => Close();
        }

        public void Run()
        {
            // Defining colors for board and square look
            Color dark = new Color(43, 43, 43);
            Color bright = new Color(200, 200, 200);

            RectangleShape square = new RectangleShape(new Vector2f(100, 100));
            bool isDragging = false;
            Vector2i dragOffset = new Vector2i();

            Texture pawnTexture = new Texture(figuresPath + @"\pawn.png");

            Sprite pawn = new Sprite(pawnTexture);
            pawn.Scale = new Vector2f(50f / pawn.Texture.Size.X, 50f / pawn.Texture.Size.Y);

            while (IsOpen)
            {
                // Handling events
                DispatchEvents();

                // Drawing board
                Clear(Color.White);
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        square.Position = new Vector2f(100 * i, 100 * j);
                        square.FillColor = ((i + j) % 2 == 0) ? dark : bright;
                        Draw(square);


                        pawn.Position = new Vector2f(25 + (100 * i), 125);
                        Draw(pawn);
                        pawn.Position = new Vector2f(25 + (100 * i), 625);
                        Draw(pawn);
                    }
                }              
                // Moving window
                if (isDragging)
                {
                    Position = new Vector2i(Convert.ToInt32(Mouse.GetPosition().X), Convert.ToInt32(Mouse.GetPosition().Y)) - dragOffset;
                }       
                
                Display();
            }
        }
    }
}
