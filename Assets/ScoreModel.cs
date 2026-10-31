using System;

public class ScoreModel
{
    public int Score { get; private set; }
    public int Combo { get; private set; }

    public event Action OnScoreChanged;

    public void AddPoints(int combo)
    {
        Combo = combo;
        Score += combo;

        OnScoreChanged?.Invoke();
    }

    public void ResetCombo()
    {
        Combo = 0;

        OnScoreChanged?.Invoke();
    }
}