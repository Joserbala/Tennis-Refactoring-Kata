using System;

namespace Tennis
{
    public class TennisGame5 : ITennisGame
    {
        private const int MaxPoints = 4;
        private int player1Score;
        private int player2Score;
        private string player1Name;
        private string player2Name;

        public TennisGame5(string player1Name, string player2Name)
        {
            this.player1Name = player1Name;
            this.player2Name = player2Name;
        }

        public void WonPoint(string playerName)
        {
            if (playerName == player1Name)
                player1Score++;
            else if (playerName == player2Name)
                player2Score++;
            else
                throw new ArgumentException("Invalid player name.");
        }

        public string GetScore()
        {
            if (IsGameOver())
                return $"Win for {Winner()}";

            if (IsDeuce())
                return "Deuce";

            if (IsAdvantage())
                return $"Advantage {LeadingPlayer()}";

            if (IsTie())
                return $"{ScoreName(player1Score)}-All";

            return $"{ScoreName(player1Score)}-{ScoreName(player2Score)}";
        }

        private bool IsAdvantage() => !string.IsNullOrEmpty(LeadingPlayer());

        private string ScoreName(int score)
        {
            return score switch
            {
                0 => "Love",
                1 => "Fifteen",
                2 => "Thirty",
                3 => "Forty",
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        private bool IsTie() => player1Score == player2Score;

        private string LeadingPlayer()
        {
            if (player1Score >= MaxPoints && player1Score - player2Score == 1)
                return player1Name;
            else if (player2Score >= MaxPoints && player2Score - player1Score == 1)
                return player2Name;
            else
                return null;
        }

        private string Winner()
        {
            if (player1Score >= MaxPoints && player1Score - player2Score >= 2)
                return player1Name;
            else if (player2Score >= MaxPoints && player2Score - player1Score >= 2)
                return player2Name;
            else
                return null;
        }

        private bool IsDeuce()
        {
            return IsTie() && player1Score >= MaxPoints - 1;
        }

        private bool IsGameOver()
        {
            return !string.IsNullOrEmpty(Winner());
        }
    }
}