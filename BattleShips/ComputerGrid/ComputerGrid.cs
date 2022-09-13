namespace BattleShips
{
    public class ComputerGrid : IComputerGrid
    {
        private readonly IField[,] Fields;

        public ComputerGrid(IField[,] fields)
        {
            Fields = fields;
        }

        public IField GetField(Coordinates coordinates)
        {
            return Fields[coordinates.Row, coordinates.Column];
        }
    }
}