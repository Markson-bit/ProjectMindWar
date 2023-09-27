using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

// CURRENTLY UNUSED

namespace ProjectMindWar.graphics
{
    internal class Menu
    {
        public void Run()
        {
            RenderWindow window = new RenderWindow(new VideoMode(800, 800), "SFML Kwadrat");

            RectangleShape square = new RectangleShape(new Vector2f(800, 800));
            square.FillColor = Color.Red; // Kolor wypełnienia kwadratu

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear();
                window.Draw(square);
                window.Display();
            }
        }
    }
}
