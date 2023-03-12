using UnityEngine;

public class ObjectWithScore : MonoBehaviour
{
    [SerializeField] private int _scoreCount = 100;
    [SerializeField] private bool _withMultiplication = true;

    private int _scoreMultiplication = 1;


    public int GetScoreCount() => _withMultiplication ? _scoreCount * _scoreMultiplication : _scoreCount;
}