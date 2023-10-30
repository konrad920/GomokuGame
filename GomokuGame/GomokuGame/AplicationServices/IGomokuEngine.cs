using GomokuGame.DataAccess;
using GomokuGame.Models;

namespace GomokuGame.AplicationServices
{
    public interface IGomokuEngine
    {
        Player GetNewPlayer();

        void SetFieldType(Player player);

        void SetMarkerOnField(GameOptions.FieldType fieldType);

        bool CheckFinishAMatch();

        Player WhoWinsAMatch();

        bool CheckFieldIsFull();

        void StartNewGame();
    }
}
