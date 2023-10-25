using GomokuGame.DataAccess;
using GomokuGame.Models;
using GomokuGame.UI;

namespace GomokuGame.AplicationServices
{
    public class GomokuEngine : IGomokuEngine
    {
        private bool cross = false;
        private bool circle = false;
        private List<Player> playerList = new List<Player>();
        private readonly BasicSettings basicSettings;

        public GomokuEngine(BasicSettings basicSettings)
        {
            this.basicSettings = basicSettings;
        }

        public Player GetNewPlayer()
        {
            Console.Write("Podaj imie gracza: ");
            var nameOfPlayer = Console.ReadLine();
            var player = new Player(){ Name = nameOfPlayer, Score = 0};
            SetFieldType(player);
            playerList.Add(player);
            return player;
        }

        public void SetFieldType(Player player)
        {
            while (true)
            {
                Console.Write("Podaj jaki typ znacznika chcesz X/O: ");
                var fieldType = Console.ReadLine();
                if (fieldType == "X" || fieldType == "x")
                {
                    if (cross == false)
                    {
                        player.FieldType = BasicSettings.FieldType.fTCross;
                        cross = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("This marker is used!!!");
                    }
                }
                else if (fieldType == "O" || fieldType == "o")
                {
                    if (circle == false)
                    {
                        player.FieldType = BasicSettings.FieldType.fTCircle;
                        circle = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("This marker is used!!!");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong char, please try again!!!");
                    continue;
                }
            }
        }
        public void SetMarkerOnField(BasicSettings.FieldType fieldType)
        {
            Console.Write("Podaj współrzędną X dla swojego znacznika: ");
            var xPosition = int.Parse(Console.ReadLine());
            Console.Write("Podaj współrzędną Y dla swojego znacznika: ");
            var yPosition = int.Parse(Console.ReadLine());
            basicSettings.Field[xPosition, yPosition] = fieldType;
        }
    }
}
