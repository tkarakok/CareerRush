using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCollisionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Money"))
        {
            other.tag = "Untagged";
            GameManager.Instance.CollisionMoneyFunction(other.transform);
        }
        else if (other.CompareTag("ObstacleMoney"))
        {
            other.tag = "Untagged";
            other.gameObject.SetActive(false);
            GameManager.Instance.CollisionMinusMoneyFunction();
        }
        else if (other.CompareTag("Gate"))
        {
            other.tag = "Untagged";
            GateController gateController = other.GetComponent<GateController>();
            GameManager.Instance.GateMoneyFunction(gateController.gateOperation,gateController.amount);
        }
        else if (other.CompareTag("Finish"))
        {
            other.tag = "Untagged";
            EventManager.Instance.EndGameEvent();
            
        }
        
        
    }
}
