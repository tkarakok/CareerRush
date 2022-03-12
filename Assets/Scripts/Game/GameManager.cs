using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

 public enum GateOperation {
        plus,
        minus,
        random
    }

public class GameManager : Singleton<GameManager>
{

    #region Public Fields
    public Vector3 offSetY;
    public Transform parent, stackPosition;
    [HideInInspector] public List<GameObject> moneys;
    #endregion

    #region Private Fields
    private int moneyCounter; // total money size
    private int amountOfMoney; // total money amount

    #endregion

    #region EnCapsulation
    public int AmountOfMoney { get => amountOfMoney; set => amountOfMoney = value; }
    public int MoneyCounter { get => moneyCounter; set => moneyCounter = value; }
    #endregion

    private void Start()
    {
        moneyCounter = 0;
        amountOfMoney = 0;
    }

    #region Money Functions

    /// <summary>
    /// we adding new amount if we collise in game money
    /// </summary>
    /// <param name="amount"> adding value </param>
    public void AddMoney(int amount)
    {
        amountOfMoney += amount;
    }

    /// <summary>
    /// subtract the value from the total money 
    /// </summary>
    /// <param name="amount"> minus value </param>
    public void MinusMoney(int amount)
    {
        amountOfMoney -= amount;
    }

    /// <summary>
    /// We calculated new stack position for new money 
    /// </summary>
    /// <returns></returns>
    public Vector3 NewStackPosition()
    {
        Vector3 newPosition = MoneyCounter * GameManager.Instance.offSetY;
        return newPosition;
    }

    /// <summary>
    /// we do the actions that will take place in collision control
    /// </summary>
    /// <param name="money">hit object</param>
    public void CollisionMoneyFunction(Transform money)
    {
        money.transform.SetParent(parent);
        AddMoney(10);
        MoneyCounter++;
        money.transform.position = stackPosition.position + NewStackPosition();
        moneys.Add(money.gameObject);
    }

    /// <summary>
    /// move to new horizontal movement with delay
    /// </summary>
    /// <param name="player">Player transform</param>
    public void MoveToHorizontalWithDelay(Transform player)
    {
        for (int i = 0; i < moneys.Count; i++)
        {
            moneys[i].transform.DOMoveX(player.position.x, (i * .02f));
        }
    }
    #endregion

    #region Gate Functions

    /// <summary>
    /// we calculating total money size for ex: amount :100 size = 100 / 10 = 10
    /// </summary>
    /// <param name="amount">we get gate's value</param>
    /// <returns></returns>
    public int CalculateMoney(int amount)
    {
        int number = amount / 10;
        return number;
    }

    /// <summary>
    /// if random value <=5 minus else plus 
    /// </summary>
    /// <returns> gate operation for random gate collision</returns>
    public GateOperation RandomGateOperation(){
        int randomOperation = Random.Range(0,10);
        if (randomOperation <= 5)
        {
            return GateOperation.minus;
        }
        else
        {
            return GateOperation.minus;
        }
        
    }

/// <summary>
/// calling collision gates
/// </summary>
/// <param name="gateOperation"> gate's value like plus minus random</param>
/// <param name="amount"> gate amount value </param>
    public void GateMoneyFunction(GateOperation gateOperation, int amount){
        switch (gateOperation)
        {
            case GateOperation.plus:
                AddMoney(amount);
                break;
            case GateOperation.minus:
                MinusMoney(amount);
                break;
            case GateOperation.random:
                if (RandomGateOperation() == GateOperation.plus)
                {
                    AddMoney(amount);
                }else
                {
                    MinusMoney(amount);
                }
                break;
            default:
                break;
        }
    }

    #endregion
}
