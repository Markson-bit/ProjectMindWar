using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMindWar.src
{
    public class ChessPiece : Sprite
    {
        public string PieceColor { get; set; }

        public ChessPiece(Sprite sprite, string color) : base(sprite)
        {
            PieceColor = color;
        }
    }
}
