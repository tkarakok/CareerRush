using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Money"))
        {
            other.tag = "Untagged";
            Debug.Log(GameManager.Instance.MoneyCounter);
            GameManager.Instance.CollisionMoneyFunction(other.transform);
        }
        else if (other.CompareTag("Plus"))
        {
            other.tag = "Untagged";
            
            GateController gateController = other.GetComponent<GateController>();
            GameManager.Instance.GateMoneyFunction(gateController.gateOperation,gateController.amount);
        }
    }
}
