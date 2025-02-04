namespace Tennis;

public class TennisGame6 : ITennisGame
{
    private int player1Score;
    private int player2Score;
    private readonly string player1Name;
    private readonly string player2Name;

    public TennisGame6(string player1Name, string player2Name)
    {
        this.player1Name = player1Name;
        this.player2Name = player2Name;
    }

    public void WonPoint(string playerName)
    {
        if (playerName == player1Name)
            player1Score++;
        else
            player2Score++;
    }

    public string GetScore()
    {
        string result;

        if (player1Score == player2Score)
            result = ComputeTieScore();
        else if (player1Score >= 4 || player2Score >= 4)
            result = ComputeEndGameScore();
        else
            result = ComputeRegularScore();

        return result;
    }

    private string ComputeRegularScore()
    {
        var score1 = GetScoreName(player1Score);
        var score2 = GetScoreName(player2Score);

        return $"{score1}-{score2}";
    }

    private static string GetScoreName(int score) => score switch
    {
        0 => "Love",
        1 => "Fifteen",
        2 => "Thirty",
        _ => "Forty"
    };

    private string ComputeEndGameScore()
    {
        var difference = player1Score - player2Score;

        var leadingPlayer = difference > 0 ? player1Name : player2Name;

        if (difference is 1 or -1)
            return $"Advantage {leadingPlayer}";
        else
            return $"Win for {leadingPlayer}";
    }

    private string ComputeTieScore() => player1Score switch
    {
        0 => "Love-All",
        1 => "Fifteen-All",
        2 => "Thirty-All",
        _ => "Deuce",
    };
}