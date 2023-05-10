using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using static System.Formats.Asn1.AsnWriter;

namespace ProjectMindWar.graphics
{
    internal class Board : RenderWindow
    {
        // Path to folder with pngs
        string figuresPath = AppDomain.CurrentDomain.BaseDirectory + @"..\\..\\..\\\graphics\img";

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
            bool isDragging = false;
            Vector2i dragOffset = new Vector2i();


            // DEFINING FIGURES
            // Pawn
            Texture pawnWTexture = new Texture(figuresPath + @"\pawnW.png");
            Sprite pawnW = new Sprite(pawnWTexture);
            pawnW.Scale = new Vector2f(50f / pawnW.Texture.Size.X, 50f / pawnW.Texture.Size.Y);
            pawnW.Texture.Smooth = true;

            Texture pawnBTexture = new Texture(figuresPath + @"\pawnB.png");
            Sprite pawnB = new Sprite(pawnBTexture);
            pawnB.Scale = new Vector2f(50f / pawnB.Texture.Size.X, 50f / pawnB.Texture.Size.Y);
            pawnB.Texture.Smooth = true;



            while (IsOpen)
            {
                // Handling events
                DispatchEvents();

                // Drawing board
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        // Squares location and color
                        square.Position = new Vector2f(100 * i, 100 * j);
                        square.FillColor = ((i + j) % 2 == 0) ? dark : bright;
                        Draw(square);

                        // Pawns location and color
                        pawnB.Position = new Vector2f(25 + (100 * i), 125);
                        Draw(pawnB);
                        pawnW.Position = new Vector2f(25 + (100 * i), 625);
                        Draw(pawnW);
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
