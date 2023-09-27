using ProjectMindWar.src;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

namespace ProjectMindWar.graphics
{
    
    internal class Board : RenderWindow
    {
        // Path to folder with pngs
        static string figuresPath = AppDomain.CurrentDomain.BaseDirectory + @"..\\..\\..\\\graphics\img";

        // 
        Sprite CreateChessPieceTexture(string filePath, Vector2f position)
        {
            Texture texture = new Texture(filePath);
            Sprite sprite = new Sprite(texture);
            sprite.Scale = new Vector2f(50f / sprite.Texture.Size.X, 50f / sprite.Texture.Size.Y);
            sprite.Texture.Smooth = true;
            sprite.Position = position;
            return sprite;
        }

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
            Sprite[] pawnsB = new Sprite[8];
            Sprite[] pawnsW = new Sprite[8];

            for (int i = 0; i < 8; i++)
            {
                pawnsB[i] = CreateChessPieceTexture(figuresPath + @"\pawnB.png", new Vector2f(25 + (100 * i), 625));
                pawnsW[i] = CreateChessPieceTexture(figuresPath + @"\pawnW.png", new Vector2f(25 + (100 * i), 125));
            }

            // Kings
            Sprite kingB = CreateChessPieceTexture(figuresPath + @"\kingB.png", new Vector2f(425, 725));
            Sprite kingW = CreateChessPieceTexture(figuresPath + @"\kingW.png", new Vector2f(425, 25));

            // Queens
            Sprite queenB = CreateChessPieceTexture(figuresPath + @"\queenB.png", new Vector2f(325, 725));
            Sprite queenW = CreateChessPieceTexture(figuresPath + @"\queenW.png", new Vector2f(325, 25));

            // Rooks
            Sprite rookB1 = CreateChessPieceTexture(figuresPath + @"\rookB.png", new Vector2f(25, 725));
            Sprite rookB2 = CreateChessPieceTexture(figuresPath + @"\rookB.png", new Vector2f(725, 725));
            Sprite rookW1 = CreateChessPieceTexture(figuresPath + @"\rookW.png", new Vector2f(25, 25));
            Sprite rookW2 = CreateChessPieceTexture(figuresPath + @"\rookW.png", new Vector2f(725, 25));

            // Bishops
            Sprite bishopB1 = CreateChessPieceTexture(figuresPath + @"\bishopB.png", new Vector2f(225, 725));
            Sprite bishopB2 = CreateChessPieceTexture(figuresPath + @"\bishopB.png", new Vector2f(525, 725));
            Sprite bishopW1 = CreateChessPieceTexture(figuresPath + @"\bishopW.png", new Vector2f(225, 25));
            Sprite bishopW2 = CreateChessPieceTexture(figuresPath + @"\bishopW.png", new Vector2f(525, 25));

            //Knights
            Sprite knightB1 = CreateChessPieceTexture(figuresPath + @"\knightB.png", new Vector2f(125, 725));
            Sprite knightB2 = CreateChessPieceTexture(figuresPath + @"\knightB.png", new Vector2f(625, 725));
            Sprite knightW1 = CreateChessPieceTexture(figuresPath + @"\knightW.png", new Vector2f(125, 25));
            Sprite knightW2 = CreateChessPieceTexture(figuresPath + @"\knightW.png", new Vector2f(625, 25));

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

                // Moving figure to selected position by mouse location
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
                        }

                        Dictionary<string, Sprite> pieces = new Dictionary<string, Sprite>
                        {   
                            {"KING BLACK", kingB},
                            {"KING WHITE", kingW},
                            {"QUEEN BLACK", queenB},
                            {"QUEEN WHITE", queenW},
                            {"ROOK BLACK (left)", rookB1},
                            {"ROOK BLACK (right)", rookB2},
                            {"ROOK WHITE (left)", rookW1},
                            {"ROOK WHITE (right)", rookW2},
                            {"BISHOP BLACK (left)", bishopB1},
                            {"BISHOP BLACK (right)", bishopB2},
                            {"BISHOP WHITE (left)", bishopW1},
                            {"BISHOP WHITE (right)", bishopW2},
                            {"KNIGHT BLACK (left)", knightB1},
                            {"KNIGHT BLACK (right)", knightB2},
                            {"KNIGHT WHITE (left)", knightW1},
                            {"KNIGHT WHITE (right)", knightW2},
                        };

                        foreach (var piece in pieces)
                        {
                            if (piece.Value.GetGlobalBounds().Contains(relativeMousePosition.X, relativeMousePosition.Y))
                            {
                                selectedFigure = piece.Value;
                                isPawnSelected = true;
                                Console.WriteLine("Selected " + piece.Key + " at position: " + selectedFigure.Position.X + ", " + selectedFigure.Position.Y);
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
