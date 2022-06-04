using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreAnimation : MonoBehaviour
{
    [SerializeField] ScoresContainer _soScoreContainer;
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] AnimationCurve _curve;
    [SerializeField] float _animationSpeed = 1f;

    private float _targetPoints = 0;
    private void OnEnable()
    {
        Debug.Log("scoresOnEnable");
        _targetPoints = _soScoreContainer.Score;
        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        float currentPoints = 0;
        float current = 0, target = 1;

        do
        {
        Debug.Log("Scores animations loop");

            current = Mathf.MoveTowards(current, target, Time.deltaTime * _animationSpeed);
            currentPoints = Mathf.Lerp(currentPoints, _targetPoints, _curve.Evaluate(current));
            _scoreText.SetText(currentPoints.ToString("F0"));
            yield return null;
        } while (currentPoints < _targetPoints);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
