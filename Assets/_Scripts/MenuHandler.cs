using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public void OnPlayButton()
    {
        GameManager.Instance.SetState(States.Game);
    }
}
