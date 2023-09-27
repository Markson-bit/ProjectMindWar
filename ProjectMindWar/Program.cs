using ProjectMindWar.graphics;
using System;
using SFML.Graphics;
using SFML.Window;

namespace ProjectMindWar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creating object of class Board and running window
            //Menu menu = new Menu();
            Board window = new Board(800, 800, "Mind War");
            Image icon = new Image(AppDomain.CurrentDomain.BaseDirectory + @"..\\..\\..\\\graphics\img\icon.png");
            window.SetIcon(icon.Size.X, icon.Size.Y, icon.Pixels);
            //menu.Run();
            window.Run();
        }
    }
}