using System;
using SFML.Graphics;
using SFML.Window;
using ProjectMindWar.src;
using SFML.System;

namespace ProjectMindWar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            uint windowWidth = 800;
            uint windowHeight = 800;
            // Creating object of class Board and running window
            //Menu menu = new Menu();
            Board window = new Board(windowWidth, windowHeight, "Mind War");
            Image icon = new Image(AppDomain.CurrentDomain.BaseDirectory + @"..\\..\\..\\\graphics\img\icon.png");
            window.SetIcon(icon.Size.X, icon.Size.Y, icon.Pixels);
            //menu.Run();
            window.Run();
        }
    }
}