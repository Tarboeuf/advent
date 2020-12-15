namespace Advent2
{
    public class Rule
    {
        public Rule(int minOccurence, int maxOccurence, char letter, string password)
        {
            MinOccurence = minOccurence;
            MaxOccurence = maxOccurence;
            Letter = letter;
            Password = password;
        }

        public int MinOccurence{get;set;}
        public int MaxOccurence{get;set;}
        public char Letter{get;set;}
        public string Password{get;set;}
    }
}