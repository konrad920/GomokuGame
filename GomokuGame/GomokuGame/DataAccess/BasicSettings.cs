﻿namespace GomokuGame.DataAccess
{
    public class BasicSettings
    {
        public enum FieldType { fTCross = 1, fTCircle = 10}

        public FieldType[,] Field = new FieldType[3, 3];
    }
}