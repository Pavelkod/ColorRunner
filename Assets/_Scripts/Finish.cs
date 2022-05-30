using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerPower>(out PlayerPower pp))
        {
            GameManager.Instance.SetState(States.Win);
        }
    }
}
