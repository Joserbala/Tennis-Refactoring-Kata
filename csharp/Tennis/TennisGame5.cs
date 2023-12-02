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
            int p1 = player1Score;
            int p2 = player2Score;

            while (p1 > MaxPoints || p2 > MaxPoints)
            {
                p1--;
                p2--;
            }

            if (HasWon(player1Score, player2Score))
                return $"Win for {player1Name}";
            else if(HasWon(player2Score, player1Score))
                return $"Win for {player2Name}";

            if (IsDeuce())
                return "Deuce";

            return (p1, p2) switch
            {
                (0, 0) => "Love-All",
                (0, 1) => "Love-Fifteen",
                (0, 2) => "Love-Thirty",
                (0, 3) => "Love-Forty",
                (1, 0) => "Fifteen-Love",
                (1, 1) => "Fifteen-All",
                (1, 2) => "Fifteen-Thirty",
                (1, 3) => "Fifteen-Forty",
                (2, 0) => "Thirty-Love",
                (2, 1) => "Thirty-Fifteen",
                (2, 2) => "Thirty-All",
                (2, 3) => "Thirty-Forty",
                (3, 0) => "Forty-Love",
                (3, 1) => "Forty-Fifteen",
                (3, 2) => "Forty-Thirty",
                (3, 4) => $"Advantage {player2Name}",
                (4, 3) => $"Advantage {player1Name}",
                _ => throw new ArgumentException("Invalid score.")
            };
        }

        private bool IsDeuce()
        {
            return player1Score >= 3 && player1Score == player2Score;
        }

        private bool HasWon(int p1, int p2)
        {
            return p1 >= MaxPoints && p1 - p2 >= 2;
        }
    }
}