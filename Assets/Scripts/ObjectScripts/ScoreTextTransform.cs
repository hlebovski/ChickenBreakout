using TMPro;
using UnityEngine;

namespace ObjectScripts
{
    public class ScoreTextTransform : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private TMP_Text _text;

        private void Awake()
        {
            Destroy(gameObject, 1.5f);
        }

        private void Update()
        {
            var y = transform.position.y + (_speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

        public void SetText(int score)
        {
            _text.text = score.ToString();
        }
    }
}