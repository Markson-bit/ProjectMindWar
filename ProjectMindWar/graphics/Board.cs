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
            // Pawns
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

            // KING - B
            Texture kingBTexture = new Texture(figuresPath + @"\kingB.png");
            Sprite kingB = new Sprite(kingBTexture);
            kingB.Scale = new Vector2f(50f / kingB.Texture.Size.X, 50f / kingB.Texture.Size.Y);
            kingB.Texture.Smooth = true;   
            kingB.Position = new Vector2f(425, 725);

            // KING - W
            Texture kingWTexture = new Texture(figuresPath + @"\kingW.png");
            Sprite kingW = new Sprite(kingWTexture);
            kingW.Scale = new Vector2f(50f / kingW.Texture.Size.X, 50f / kingW.Texture.Size.Y);
            kingW.Texture.Smooth = true;
            kingW.Position = new Vector2f(425, 25);

            // QUEEN - B
            Texture queenBTexture = new Texture(figuresPath + @"\queenB.png");
            Sprite queenB = new Sprite(queenBTexture);
            queenB.Scale = new Vector2f(50f / queenB.Texture.Size.X, 50f / queenB.Texture.Size.Y);
            queenB.Texture.Smooth = true;
            queenB.Position = new Vector2f(325, 725);

            // QUEEN - W
            Texture queenWTexture = new Texture(figuresPath + @"\queenW.png");
            Sprite queenW = new Sprite(queenWTexture);
            queenW.Scale = new Vector2f(50f / queenW.Texture.Size.X, 50f / queenW.Texture.Size.Y);
            queenW.Texture.Smooth = true;
            queenW.Position = new Vector2f(325, 25);

            // ROOKS - B
            Texture rookBTexture = new Texture(figuresPath + @"\rookB.png");
            Sprite rookB1 = new Sprite(rookBTexture);
            Sprite rookB2 = new Sprite(rookBTexture);
            rookB1.Scale = new Vector2f(50f / rookB1.Texture.Size.X, 50f / rookB1.Texture.Size.Y);
            rookB1.Texture.Smooth = true;
            rookB1.Position = new Vector2f(25, 725);
            rookB2.Scale = new Vector2f(50f / rookB2.Texture.Size.X, 50f / rookB2.Texture.Size.Y);
            rookB2.Texture.Smooth = true;
            rookB2.Position = new Vector2f(725, 725);

            // ROOKS - W
            Texture rookWTexture = new Texture(figuresPath + @"\rookW.png");
            Sprite rookW1 = new Sprite(rookWTexture);
            Sprite rookW2 = new Sprite(rookWTexture);
            rookW1.Scale = new Vector2f(50f / rookW1.Texture.Size.X, 50f / rookW1.Texture.Size.Y);
            rookW1.Texture.Smooth = true;
            rookW1.Position = new Vector2f(25, 25);
            rookW2.Scale = new Vector2f(50f / rookW2.Texture.Size.X, 50f / rookW2.Texture.Size.Y);
            rookW2.Texture.Smooth = true;   
            rookW2.Position = new Vector2f(725, 25);

            // BISHOPS - B
            Texture bishopBTexture = new Texture(figuresPath + @"\bishopB.png");
            Sprite bishopB1 = new Sprite(bishopBTexture);
            Sprite bishopB2 = new Sprite(bishopBTexture);
            bishopB1.Scale = new Vector2f(50f / bishopB1.Texture.Size.X, 50f / bishopB1.Texture.Size.Y);
            bishopB1.Texture.Smooth = true;
            bishopB1.Position = new Vector2f(225, 725);
            bishopB2.Scale = new Vector2f(50f / bishopB2.Texture.Size.X, 50f / bishopB2.Texture.Size.Y);
            bishopB2.Texture.Smooth = true;
            bishopB2.Position = new Vector2f(525, 725);

            // BISHOPS - W
            Texture bishopWTexture = new Texture(figuresPath + @"\bishopW.png");
            Sprite bishopW1 = new Sprite(bishopWTexture);
            Sprite bishopW2 = new Sprite(bishopWTexture);
            bishopW1.Scale = new Vector2f(50f / bishopW1.Texture.Size.X, 50f / bishopW1.Texture.Size.Y);
            bishopW1.Texture.Smooth = true;
            bishopW1.Position = new Vector2f(225, 25);
            bishopW2.Scale = new Vector2f(50f / bishopW2.Texture.Size.X, 50f / bishopW2.Texture.Size.Y);
            bishopW2.Texture.Smooth = true;
            bishopW2.Position = new Vector2f(525, 25);

            // KNIGHTS - B
            Texture knightBTexture = new Texture(figuresPath + @"\knightB.png");
            Sprite knightB1 = new Sprite(knightBTexture);
            Sprite knightB2 = new Sprite(knightBTexture);
            knightB1.Scale = new Vector2f(50f / knightB1.Texture.Size.X, 50f / knightB1.Texture.Size.Y);
            knightB1.Texture.Smooth = true;
            knightB1.Position = new Vector2f(125, 725);
            knightB2.Scale = new Vector2f(50f / knightB2.Texture.Size.X, 50f / knightB2.Texture.Size.Y);
            knightB2.Texture.Smooth = true;
            knightB2.Position = new Vector2f(625, 725);

            // KNIGHTS - W
            Texture knightWTexture = new Texture(figuresPath + @"\knightW.png");
            Sprite knightW1 = new Sprite(knightWTexture);
            Sprite knightW2 = new Sprite(knightWTexture);
            knightW1.Scale = new Vector2f(50f / knightW1.Texture.Size.X, 50f / knightW1.Texture.Size.Y);
            knightW1.Texture.Smooth = true;
            knightW1.Position = new Vector2f(125, 25);
            knightW2.Scale = new Vector2f(50f / knightW2.Texture.Size.X, 50f / knightW2.Texture.Size.Y);
            knightW2.Texture.Smooth = true;
            knightW2.Position = new Vector2f(625, 25);


            Sprite selectedFigure = null;
            bool isPawnSelected = false;

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
                    Thread.Sleep(100);

                    if ((column > 7.25 || row > 7.25) || (column < 0 || row < 0))
                    {
                        Vector2f newPosition = new Vector2f(column * 100 + 25, row * 100 + 25);
                        Console.WriteLine($"To far: ({newPosition.X}, {newPosition.Y})");
                    }
                    else if (isPawnSelected)
                    {
                        Vector2f newPosition = new Vector2f(column * 100 + 25, row * 100 + 25);
                        selectedFigure.Position = newPosition;
                        Console.WriteLine($"Moved selected figure to position: ({newPosition.X}, {newPosition.Y})");
                        Console.WriteLine();
                        isPawnSelected = false;
                    }
                    else
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            if (pawnsB[i].GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = pawnsB[i];
                                isPawnSelected = true;
                                Console.WriteLine("Selected PAWN BLACK at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
                                break;
                            }
                            if (pawnsW[i].GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = pawnsW[i];
                                isPawnSelected = true;
                                Console.WriteLine("Selected PAWN WHITE at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
                                break;
                            }
                            if (kingB.GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = kingB;
                                isPawnSelected = true;
                                Console.WriteLine("Selected KING BLACK at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
                                break;
                            }
                            if (kingW.GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = kingW;
                                isPawnSelected = true;
                                Console.WriteLine("Selected KING WHITE at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
                                break;
                            }
                            if (queenB.GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = queenB;
                                isPawnSelected = true;
                                Console.WriteLine("Selected QUEEN BLACK at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
                                break;
                            }
                            if (queenW.GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = queenW;
                                isPawnSelected = true;
                                Console.WriteLine("Selected QUEEN WHITE at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
                                break;
                            }
                            if (rookB1.GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = rookB1;
                                isPawnSelected = true;
                                Console.WriteLine("Selected ROOK BLACK (left) at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
                                break;
                            }
                            if (rookB2.GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = rookB2;
                                isPawnSelected = true;
                                Console.WriteLine("Selected ROOK BLACK (right) at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
                                break;
                            }
                            if (rookW1.GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = rookW1;
                                isPawnSelected = true;
                                Console.WriteLine("Selected ROOK WHITE (left) at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
                                break;
                            }
                            if (rookW2.GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = rookW2;
                                isPawnSelected = true;
                                Console.WriteLine("Selected ROOK WHITE (right) at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
                                break;
                            }
                            if (bishopB1.GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = bishopB1;
                                isPawnSelected = true;
                                Console.WriteLine("Selected BISHOP BLACK (left) at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
                                break;
                            }
                            if (bishopB2.GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = bishopB1;
                                isPawnSelected = true;
                                Console.WriteLine("Selected BISHOP BLACK (right) at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
                                break;
                            }
                            if (bishopW1.GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = bishopW1;
                                isPawnSelected = true;
                                Console.WriteLine("Selected BISHOP WHITE (left) at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
                                break;
                            }
                            if (bishopW2.GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = bishopW2;
                                isPawnSelected = true;
                                Console.WriteLine("Selected BISHOP WHITE (right) at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
                                break;
                            }
                            if (knightB1.GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = knightB1;
                                isPawnSelected = true;
                                Console.WriteLine("Selected KNIGHT BLACK (left) at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
                                break;
                            }
                            if (knightB2.GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = knightB2;
                                isPawnSelected = true;
                                Console.WriteLine("Selected KNIGHT BLACK (right) at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
                                break;
                            }
                            if (knightW1.GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = knightW1;
                                isPawnSelected = true;
                                Console.WriteLine("Selected KNIGHT WHITE (left) at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
                                break;
                            }
                            if (knightW2.GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = knightW2;
                                isPawnSelected = true;
                                Console.WriteLine("Selected KNIGHT WHITE (right) at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
                                break;
                            }
                        }
                    }
                }

                // Drawing figures
                for (int i = 0; i < 8; i++)
                {
                    Draw(pawnsB[i]);
                    Draw(pawnsW[i]);
                    Draw(kingB);
                    Draw(kingW);
                    Draw(queenB);
                    Draw(queenW);
                    Draw(rookB1);
                    Draw(rookB2);
                    Draw(rookW1);
                    Draw(rookW2);
                    Draw(bishopB1);
                    Draw(bishopB2);
                    Draw(bishopW1);
                    Draw(bishopW2);
                    Draw(knightB1);
                    Draw(knightB2);
                    Draw(knightW1);
                    Draw(knightW2);
                }

                Display();
            }



        }
    }
}
