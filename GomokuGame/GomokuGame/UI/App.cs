using GomokuGame.AplicationServices;
using GomokuGame.DataAccess;
using GomokuGame.Models;

namespace GomokuGame.UI
{
    public class App : IApp
    {
        private readonly IGomokuEngine gomokuEngine;

        public App(IGomokuEngine gomokuEngine)
        {
            this.gomokuEngine = gomokuEngine;
        }
        public void Run()
        {
            Console.WriteLine("Hello");
            var firstPlayer = gomokuEngine.GetNewPlayer();
            var secondPlayer = gomokuEngine.GetNewPlayer();

            Console.WriteLine($"Imie pierwszego gracza to: {firstPlayer.Name}, jego id to: {firstPlayer.Score}, a jego znacznik to: {firstPlayer.FieldType}");
            Console.WriteLine($"Imie drugiego gracza to: {secondPlayer.Name}, jego id to: {secondPlayer.Score}, a jego znacznik to: {secondPlayer.FieldType}");

            if (firstPlayer.IsPlaying == true)
            {
                gomokuEngine.SetMarkerOnField(firstPlayer.FieldType);
            }
            Console.WriteLine($"Teraz ruch gracza ID: {firstPlayer.Id}");
            

        }
    }
}
