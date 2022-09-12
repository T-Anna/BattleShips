namespace BattleShips
{
    public class PlayerInputTranslator : IPlayerInputTranslator
    {
        public Coordinates TranslateCoordinates(string input)
        {
            int AsciiLetterOffset = 65;
            int AsciiNumberOffset = 48;
            return new Coordinates(input[1] - AsciiNumberOffset, input[0] - AsciiLetterOffset);
        }
    }
}