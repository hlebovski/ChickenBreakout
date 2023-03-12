using TMPro;
using UnityEngine;

public class ScoreService : MonoBehaviour
{
    [SerializeField] private TMP_Text _textScore;
    private int _score;

    public void AddScore(int score)
    {
        _score += score;
        ChangeScoreText();
    }

    public void ClearScore()
    {
        _score = 0;
        ChangeScoreText();
    }

    private void ChangeScoreText()
    {
        _textScore.text = _score.ToString();
    }
}
