using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

namespace ProjectMindWar.src
{   
    internal class Board : RenderWindow
    {
        // Path to folder with pngs
        static string figuresPath = AppDomain.CurrentDomain.BaseDirectory + @"..\\..\\..\\\graphics\img";

        // Creating sprite of Figures
        Sprite CreateChessPieceTexture(string filePath, Vector2f position)
        {
            filePath = figuresPath + filePath;
            Texture texture = new Texture(filePath);
            Sprite sprite = new Sprite(texture);
            sprite.Scale = new Vector2f(75f / sprite.Texture.Size.X, 75f / sprite.Texture.Size.Y);
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

            // DEFINING FIGURES
            // Pawns
            ChessPiece[] pawnsB = new ChessPiece[8];
            ChessPiece[] pawnsW = new ChessPiece[8];

            // NOW 13, BEFORE 25 ! ! !

            for (int i = 0; i < 8; i++)
            {
                pawnsB[i] = new ChessPiece(CreateChessPieceTexture(@"\pawnB.png", new Vector2f(13 + 100 * i, 613)), "black");
                pawnsW[i] = new ChessPiece(CreateChessPieceTexture(@"\pawnW.png", new Vector2f(13 + 100 * i, 113)), "white");
            }

            // Defining Sprites with their png's and position
            // Kings
            //Sprite kingB = CreateChessPieceTexture(@"\kingB.png", new Vector2f(425, 725));
            ChessPiece kingB = new ChessPiece(CreateChessPieceTexture(@"\kingB.png", new Vector2f(413, 713)), "black");
            ChessPiece kingW = new ChessPiece(CreateChessPieceTexture(@"\kingW.png", new Vector2f(413, 13)), "white");
            // Queens
            ChessPiece queenB = new ChessPiece(CreateChessPieceTexture(@"\queenB.png", new Vector2f(313, 713)), "black");
            ChessPiece queenW = new ChessPiece(CreateChessPieceTexture(@"\queenW.png", new Vector2f(313, 13)), "white");
            // Rooks
            ChessPiece rookB1 = new ChessPiece(CreateChessPieceTexture(@"\rookB.png", new Vector2f(13, 713)), "black");
            ChessPiece rookB2 = new ChessPiece(CreateChessPieceTexture(@"\rookB.png", new Vector2f(713, 713)), "black");
            ChessPiece rookW1 = new ChessPiece(CreateChessPieceTexture(@"\rookW.png", new Vector2f(13, 13)), "white");
            ChessPiece rookW2 = new ChessPiece(CreateChessPieceTexture(@"\rookW.png", new Vector2f(713, 13)), "white");
            // Bishops
            ChessPiece bishopB1 = new ChessPiece(CreateChessPieceTexture(@"\bishopB.png", new Vector2f(213, 713)), "black");
            ChessPiece bishopB2 = new ChessPiece(CreateChessPieceTexture(@"\bishopB.png", new Vector2f(513, 713)), "black");
            ChessPiece bishopW1 = new ChessPiece(CreateChessPieceTexture(@"\bishopW.png", new Vector2f(213, 13)), "white");
            ChessPiece bishopW2 = new ChessPiece(CreateChessPieceTexture(@"\bishopW.png", new Vector2f(513, 13)), "white");
            //Knights
            ChessPiece knightB1 = new ChessPiece(CreateChessPieceTexture(@"\knightB.png", new Vector2f(113, 713)), "black");
            ChessPiece knightB2 = new ChessPiece(CreateChessPieceTexture(@"\knightB.png", new Vector2f(613, 713)), "black");
            ChessPiece knightW1 = new ChessPiece(CreateChessPieceTexture(@"\knightW.png", new Vector2f(113, 13)), "white");
            ChessPiece knightW2 = new ChessPiece(CreateChessPieceTexture(@"\knightW.png", new Vector2f(613, 13)), "white");

            // Creating dictionary with figures
            Dictionary<string, ChessPiece> pieces = new Dictionary<string, ChessPiece>
            {
                {"PAWN BLACK(1)", pawnsB[0]},
                {"PAWN BLACK(2)", pawnsB[1]},
                {"PAWN BLACK(3)", pawnsB[2]},
                {"PAWN BLACK(4)", pawnsB[3]},
                {"PAWN BLACK(5)", pawnsB[4]},
                {"PAWN BLACK(6)", pawnsB[5]},
                {"PAWN BLACK(7)", pawnsB[6]},
                {"PAWN BLACK(8)", pawnsB[7]},
                {"PAWN WHITE(1)", pawnsW[0]},
                {"PAWN WHITE(2)", pawnsW[1]},
                {"PAWN WHITE(3)", pawnsW[2]},
                {"PAWN WHITE(4)", pawnsW[3]},
                {"PAWN WHITE(5)", pawnsW[4]},
                {"PAWN WHITE(6)", pawnsW[5]},
                {"PAWN WHITE(7)", pawnsW[6]},
                {"PAWN WHITE(8)", pawnsW[7]},
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

            ChessPiece ?selectedFigure = null;
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
                        square.FillColor = (i + j) % 2 == 0 ? dark : bright;
                        Draw(square);
                    }
                }

                // Moving figure to selected position by mouse location
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    Vector2i mousePosition = Mouse.GetPosition(this);
                    Vector2f relativeMousePosition = new Vector2f(mousePosition.X + 565, mousePosition.Y + 140) - new Vector2f(Position.X, Position.Y);
                    int column = (int)Math.Floor(relativeMousePosition.X / 100);
                    int row = (int)Math.Floor(relativeMousePosition.Y / 100);
                    Thread.Sleep(200);

                    if (column > 7.25 || row > 7.25 || column < 0 || row < 0)
                    {
                        Vector2f newPosition = new Vector2f(column * 100 + 13, row * 100 + 13);
                        Console.WriteLine($"To far: ({newPosition.X}, {newPosition.Y})");
                    }
                    if (isPawnSelected)
                    {
                        Vector2f newPosition = new Vector2f(column * 100 + 13, row * 100 + 13);
                        // Check, if there is a piece on target position
                        bool isDestinationOccupied = false;
                        // Adding variable to beaten piece
                        KeyValuePair<string, ChessPiece> beatenPiece = new KeyValuePair<string, ChessPiece>(); 

                        foreach (var piece in pieces)
                        {
                            if (piece.Value != selectedFigure && piece.Value.GetGlobalBounds().Contains(newPosition.X, newPosition.Y))
                            {
                                isDestinationOccupied = true;
                                if (selectedFigure.PieceColor != piece.Value.PieceColor && newPosition != selectedFigure.Position)
                                {
                                    beatenPiece = piece;
                                    selectedFigure.Position = newPosition;
                                    Console.WriteLine("Beating: " + piece.Key);
                                    beatenPiece.Value.Position = new Vector2f(-50, -50);
                                    pieces.Remove(beatenPiece.Key);
                                }
                                else
                                {
                                    Console.WriteLine("You cannot beat your figure");
                                }

                                break;
                            }
                        }

                        if (!isDestinationOccupied && newPosition != selectedFigure.Position)
                        {
                            // Moving selected figure to target position
                            selectedFigure.Position = newPosition;
                            Console.WriteLine($"Moved selected figure to position: ({newPosition.X}, {newPosition.Y})");
                        }

                        Console.WriteLine();
                        isPawnSelected = false;
                    }
                    else
                    {
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
