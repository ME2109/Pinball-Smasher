using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    public TMP_Text scoreText;

    private ScoreModel model;

    public void Bind(ScoreModel scoreModel)
    {
        if (model != null)
        {
            model.OnScoreChanged -= Refresh;
        }

        model = scoreModel;

        if (model != null)
        {
            model.OnScoreChanged += Refresh;
            Refresh();
        }
    }

    private void Refresh()
    {
        if (model == null) return;

        UpdateScore(model.Score, model.Combo);
    }

    public void UpdateScore(int score, int combo)
    {
        if (scoreText == null) return;

        if (combo > 1)
        {
            scoreText.text = "яв╗р: " + score + "  |  йнлан x" + combo;
        }
        else
        {
            scoreText.text = "яв╗р: " + score;
        }
    }

    private void OnDestroy()
    {
        if (model != null)
        {
            model.OnScoreChanged -= Refresh;
        }
    }
}