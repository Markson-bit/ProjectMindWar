using ProjectMindWar.graphics;

namespace ProjectMindWar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creating object of class Board and running window
            Board window = new Board(800, 800, "Mind War");
            window.Run();
        }
    }
}