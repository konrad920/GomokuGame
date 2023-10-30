using GomokuGame.DataAccess;

namespace GomokuGame.AplicationServices
{
    public class GameOptions
    {
        private readonly IGomokuEngine gomokuEngine;
        public static int rangeOfField = 3;
        public FieldType[,] Field = new FieldType[rangeOfField, rangeOfField];
        public enum FieldType { X = 10, O = 1, _ = 0 }
        public GameOptions()
        {
        }

        public void InitialGame()
        {
            for (int i = 0; i < rangeOfField; i++)
            {
                for(int j = 0; j < rangeOfField; j++)
                {
                    this.Field[i, j] = FieldType._;
                }
            }
        }
    }
}
