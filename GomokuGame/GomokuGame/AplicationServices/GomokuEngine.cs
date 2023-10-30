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
        private static int crossWin = rangeOfField * (int)GameOptions.FieldType.X;
        private static int circleWin = rangeOfField * (int)GameOptions.FieldType.O;
        private int finalSum = 0;
        private GameOptions.FieldType[,] Field = new GameOptions.FieldType[rangeOfField, rangeOfField];


        public void StartNewGame()
        {
            var newGame = new GameOptions();
            newGame.InitialGame();
            newGame.Field = this.Field;
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
                        player.FieldType = GameOptions.FieldType.X;
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
                        player.FieldType = GameOptions.FieldType.O;
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
        public void SetMarkerOnField(GameOptions.FieldType fieldType)
        {
            while(true)
            {
                Console.Write("Podaj współrzędną X dla swojego znacznika: ");
                var xPositionFromUser = Console.ReadLine();
                if (int.TryParse(xPositionFromUser, out var xPosition) == false)
                {
                    Console.WriteLine("To nie liczba, spróbuj ponownie");
                    continue;
                }
                else if (xPosition <= 0 || xPosition > rangeOfField)
                {
                    Console.WriteLine("Współrzędna spoza zakresu");
                    continue;
                }
                Console.Write("Podaj współrzędną Y dla swojego znacznika: ");
                var yPositionFromUser = Console.ReadLine();
                if (int.TryParse(yPositionFromUser, out var yPosition) == false)
                {
                    Console.WriteLine("To nie liczba, spróbuj ponownie");
                    continue;
                }
                else if (yPosition <= 0 || yPosition > rangeOfField)
                {
                    Console.WriteLine("Współrzędna spoza zakresu");
                    continue;
                }

                var xPositionOnTheField = xPosition - 1;
                var yPositionOnTheField = yPosition - 1;

                var isEmpty = CheckFieldIsEmpty(xPositionOnTheField, yPositionOnTheField);
                if (isEmpty == true)
                {
                    this.Field[xPositionOnTheField, yPositionOnTheField] = fieldType;
                    PresentScore();
                    break;
                }
                else
                {
                    Console.WriteLine("To pole jest zajete");
                    continue;
                }
            }
        }

        private void PresentScore()
        {
            for (var i = 0; i < 3; i++)
            {
                Console.Write($"|{this.Field[i, 0]}|");
            }
            Console.WriteLine();
            Console.WriteLine("---------");
            for (var i = 0; i < 3; i++)
            {
                Console.Write($"|{this.Field[i, 1]}|");
            }
            Console.WriteLine();
            Console.WriteLine("---------");
            for (var i = 0; i < 3; i++)
            {
                Console.Write($"|{this.Field[i, 2]}|");
            }
            Console.WriteLine();
        }

        private bool CheckFieldIsEmpty(int x, int y)
        {
            if (this.Field[x, y] == GameOptions.FieldType.O || this.Field[x, y] == GameOptions.FieldType.X)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckFieldIsFull()
        {
            var numberOfUsedFields = 0;
            foreach (var field in this.Field)
            {
                if (field == GameOptions.FieldType.X || field == GameOptions.FieldType.O)
                {
                    numberOfUsedFields++;
                }
            }
            if (this.Field.Length == numberOfUsedFields)
            {
                return true;
            }
            else 
            { 
                return false; 
            }
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
                return playerList.FirstOrDefault(x => x.FieldType == GameOptions.FieldType.X);
            }
            else if(this.finalSum == circleWin)
            {
                return playerList.FirstOrDefault(x => x.FieldType == GameOptions.FieldType.O);
            }
            else
            {
                return null;
            }
        }
    }
}
