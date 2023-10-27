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
        private static int rangeOfField = 3;
        private static int crossWin = rangeOfField * (int)BasicSettings.FieldType.fTCross;
        private static int circleWin = rangeOfField * (int)BasicSettings.FieldType.fTCircle;
        private BasicSettings.FieldType[,] Field = new BasicSettings.FieldType[rangeOfField, rangeOfField];
        private int finalSum = 0;
        public GomokuEngine()
        {
        }

        public Player GetNewPlayer()
        {
            Console.Write("Podaj imie gracza: ");
            var nameOfPlayer = Console.ReadLine();
            var player = new Player(){ Name = nameOfPlayer, Score = 0, IsPlaying = true};
            SetFieldType(player);
            player.Id = playerList.Count + 1;
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
            this.Field[xPosition, yPosition] = fieldType;
        }

        public bool CheckFinishAMatch()
        {
            int sumOfScoreFromFirstColumn = 0;
            int sumOfScoreFromSecondColumn = 0;
            int sumOfScoreFromThrithColumn = 0;
            int sumOfScoreFromFirstRow = 0;
            int sumOfScoreFromSecondRow = 0;
            int sumOfScoreFromThrithRow = 0;
            int sumOfScoreFromFirstCross = 0;
            int sumOfScoreFromSecondCross = 0;
            for (int i = 0;  i < rangeOfField; i++)
            {
                sumOfScoreFromFirstColumn += (int)Field[i, 0];
                sumOfScoreFromSecondColumn += (int)Field[i, 1];
                sumOfScoreFromThrithColumn += (int)Field[i, 2];
                sumOfScoreFromFirstRow += (int)Field[0, i];
                sumOfScoreFromSecondRow += (int)Field[1, i];
                sumOfScoreFromThrithRow += (int)Field[2, i];
                sumOfScoreFromFirstCross += (int)Field[i, i];
                sumOfScoreFromSecondCross += (int)Field[(rangeOfField-1)-i, i];
            }

            if (sumOfScoreFromFirstColumn == crossWin || sumOfScoreFromSecondColumn == crossWin || sumOfScoreFromThrithColumn == crossWin || sumOfScoreFromFirstRow == crossWin || sumOfScoreFromSecondRow == crossWin || sumOfScoreFromThrithRow == crossWin || sumOfScoreFromFirstCross == crossWin || sumOfScoreFromSecondCross == crossWin)
            {
                this.finalSum = crossWin;
                return true;
            }
            else if (sumOfScoreFromFirstColumn == circleWin || sumOfScoreFromSecondColumn == circleWin || sumOfScoreFromThrithColumn == circleWin || sumOfScoreFromFirstRow == circleWin || sumOfScoreFromSecondRow == circleWin || sumOfScoreFromThrithRow == circleWin || sumOfScoreFromFirstCross == circleWin || sumOfScoreFromSecondCross == circleWin)
            {
                this.finalSum = circleWin;
                return true;
            }
            else
            {
                return false;
            }
        }

        public Player WhoWinsAMatch()
        {
            if (this.finalSum == crossWin)
            {
                return playerList.FirstOrDefault(x => x.FieldType == BasicSettings.FieldType.fTCross);
            }
            else if(this.finalSum == circleWin)
            {
                return playerList.FirstOrDefault(x => x.FieldType == BasicSettings.FieldType.fTCircle);
            }
            else
            {
                return null;
            }
        }
    }
}
