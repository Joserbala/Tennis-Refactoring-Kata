using System;

namespace Tennis
{
    public class TennisGame5 : ITennisGame
    {
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
            return new ScoreRenderer(player1Name, player2Name).Render(player1Score, player2Score);
        }
    }

    internal class ScoreRenderer
    {
        private const int MaxPoints = 4;
        private string player1Name;
        private string player2Name;

        public ScoreRenderer(string player1Name, string player2Name)
        {
            this.player1Name = player1Name;
            this.player2Name = player2Name;
        }

        public string Render(int player1Score, int player2Score)
        {
            if (IsGameOver(player1Score, player2Score))
                return $"Win for {LeadingPlayer(player1Score, player2Score)}";

            if (IsDeuce(player1Score, player2Score))
                return "Deuce";

            if (IsAdvantage(player1Score, player2Score))
                return $"Advantage {LeadingPlayer(player1Score, player2Score)}";

            if (IsTie(player1Score, player2Score))
                return $"{ScoreName(player1Score)}-All";

            return $"{ScoreName(player1Score)}-{ScoreName(player2Score)}";
        }

        private bool IsAdvantage(int player1Score, int player2Score)
        {
            return ScoreDifference(player1Score, player2Score) == 1 && (player1Score >= MaxPoints || player2Score >= MaxPoints);
        }

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

        private bool IsTie(int player1Score, int player2Score) => player1Score == player2Score;

        private string LeadingPlayer(int player1Score, int player2Score)
        {
            return player1Score > player2Score ? player1Name : player2Name;
        }

        private int ScoreDifference(int player1Score, int player2Score) => Math.Abs(player1Score - player2Score);

        private bool IsDeuce(int player1Score, int player2Score)
        {
            return IsTie(player1Score, player2Score) && player1Score >= MaxPoints - 1;
        }

        private bool IsGameOver(int player1Score, int player2Score)
        {
            return ScoreDifference(player1Score, player2Score) >= 2 && (player1Score >= MaxPoints || player2Score >= MaxPoints);
        }
    }
}