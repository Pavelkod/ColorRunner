using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public void OnPlayButton(float delay)
    {
        StartCoroutine(OnPlayDelay(delay));
    }

    IEnumerator OnPlayDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameManager.Instance.SetState(States.Game);
    }
}
