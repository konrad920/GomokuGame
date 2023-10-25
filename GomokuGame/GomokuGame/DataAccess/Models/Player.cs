using GomokuGame.DataAccess;

namespace GomokuGame.Models
{
    public class Player : PlayerBase
    {
        public string Name { get; set; }

        public int Score { get; set; }

        public BasicSettings.FieldType FieldType { get; set; }

        public bool IsPlaying { get; set; }
    }
}
