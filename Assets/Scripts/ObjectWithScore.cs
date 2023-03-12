using ObjectScripts;
using UnityEngine;

public class ObjectWithScore : MonoBehaviour {
	[SerializeField] private int _scoreCount = 100;
	[SerializeField] private bool _withMultiplication = true;
	[SerializeField] private float _offset = 1;
	[SerializeField] private ScoreTextTransform _scorePrefab;

	private int _scoreMultiplication = 1;

	public int GetScoreCount() {
		var count = _withMultiplication ? _scoreCount * _scoreMultiplication : _scoreCount;
		CreateScoreText(count);
		_scoreMultiplication++;
		return count;
	}

	private void CreateScoreText(int count) {
		var positionWithOffset = new Vector3(transform.position.x, transform.position.y, transform.position.z - _offset);
		var text = Instantiate(_scorePrefab, positionWithOffset, Quaternion.identity);
		text.SetText(count);
	}
}