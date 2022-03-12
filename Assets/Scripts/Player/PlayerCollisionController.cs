using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Money"))
        {
            other.tag = "Untagged";
            GameManager.Instance.CollisionMoneyFunction(other.transform);
        }
    }
}
