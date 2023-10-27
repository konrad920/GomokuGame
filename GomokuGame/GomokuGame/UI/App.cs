using GomokuGame.AplicationServices;
using GomokuGame.DataAccess;
using GomokuGame.Models;

namespace GomokuGame.UI
{
    public class App : IApp
    {
        private readonly IGomokuEngine gomokuEngine;

        private bool finishedMatch = false;

        public App(IGomokuEngine gomokuEngine)
        {
            this.gomokuEngine = gomokuEngine;
        }
        public void Run()
        {
            Console.WriteLine("Hello");
            var firstPlayer = gomokuEngine.GetNewPlayer();
            var secondPlayer = gomokuEngine.GetNewPlayer();

            Console.WriteLine($"Imie pierwszego gracza to: {firstPlayer.Name}, jego id to: {firstPlayer.Id}, a jego znacznik to: {firstPlayer.FieldType}");
            Console.WriteLine($"Imie drugiego gracza to: {secondPlayer.Name}, jego id to: {secondPlayer.Id}, a jego znacznik to: {secondPlayer.FieldType}");

            do
            {
                if (firstPlayer.IsPlaying == true)
                {
                    Console.WriteLine($"Teraz ruch gracza ID: {firstPlayer.Id}");
                    gomokuEngine.SetMarkerOnField(firstPlayer.FieldType);
                    firstPlayer.IsPlaying = false;
                    secondPlayer.IsPlaying = true;
                }
                else
                {
                    Console.WriteLine($"Teraz ruch gracza ID: {secondPlayer.Id}");
                    gomokuEngine.SetMarkerOnField(secondPlayer.FieldType);
                    firstPlayer.IsPlaying = true;
                    secondPlayer.IsPlaying = false;
                }

                this.finishedMatch = gomokuEngine.CheckFinishAMatch();
            } while (finishedMatch != true);

            var winner = gomokuEngine.WhoWinsAMatch();
            Console.WriteLine($"Zwyciężył {winner.Name}");
        }
    }
}
