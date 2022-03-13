using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum GateOperation
{
    plus,
    minus,
    random
}

public class GameManager : Singleton<GameManager>
{

    #region Public Fields
    public Vector3 offSetY;
    public Transform parent, stackPosition;
    public Vector3 rotateFinish;
    [HideInInspector] public List<GameObject> moneys;
    #endregion

    #region Private Fields
    private int _moneyCounter; // total money size
    private int _amountOfMoney; // total money amount
    private int _multiplier;

    public int MoneyCounter { get => _moneyCounter; set => _moneyCounter = value; }
    public int AmountOfMoney { get => _amountOfMoney; set => _amountOfMoney = value; }
    public int Multiplier { get => _multiplier; set => _multiplier = value; }
    #endregion



    private void Start()
    {
        _moneyCounter = 0;
        _amountOfMoney = 0;
    }

    #region Money Functions

    /// <summary>
    /// we adding new amount if we collise in game money
    /// </summary>
    /// <param name="amount"> adding value </param>
    public void AddMoney(int amount)
    {
        _amountOfMoney += amount;
        EventManager.Instance.MoneyEvent();
    }

    /// <summary>
    /// subtract the value from the total money 
    /// </summary>
    /// <param name="amount"> minus value </param>
    public void MinusMoney(int amount)
    {

        _amountOfMoney -= amount;
        if (_amountOfMoney < 0)
        {
            EventManager.Instance.GameOverEvent();
        }
        EventManager.Instance.MoneyEvent();
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

    public void CollisionMinusMoneyFunction()
    {
        MinusMoney(10);
        _moneyCounter--;
        moneys[moneys.Count - 1].transform.SetParent(null);
        moneys[moneys.Count - 1].SetActive(false);
        moneys.Remove(moneys[moneys.Count - 1]);
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
    public GateOperation RandomGateOperation()
    {
        int randomOperation = Random.Range(0, 10);
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
    /// this func calling in gate money func for plus
    /// </summary>
    /// <param name="amount"> amount = gate's amount</param>
    void InstantiateNewMoney(int amount)
    {
        for (int i = 0; i < CalculateMoney(amount); i++)
        {
            CollisionMoneyFunction(ObjectPoolManager.Instance.GetMoney().transform);
            
        }
    }

    /// <summary>
    /// this func calling in gate func for destroy money
    /// </summary>
    /// <param name="amount"></param>
    void DestroyMoney(int amount)
    {
        for (int i = 0; i < CalculateMoney(amount); i++)
        {
            CollisionMinusMoneyFunction();
        }
    }

    /// <summary>
    /// calling collision gates
    /// </summary>
    /// <param name="gateOperation"> gate's value like plus minus random</param>
    /// <param name="amount"> gate amount value </param>
    public void GateMoneyFunction(GateOperation gateOperation, int amount)
    {
        switch (gateOperation)
        {
            case GateOperation.plus:
                InstantiateNewMoney(amount);
                break;
            case GateOperation.minus:
                DestroyMoney(amount);
                break;
            case GateOperation.random:
                if (RandomGateOperation() == GateOperation.plus)
                {
                    InstantiateNewMoney(amount);
                }
                else
                {
                    DestroyMoney(amount);
                }
                break;
            default:
                break;
        }
    }

    #endregion

    #region Finish Functions
    public void Finish()
    {
        StartCoroutine(NullParent());
    }
    IEnumerator NullParent()
    {
        MovementController.Instance.ForwardSpeed = 0;
        MovementController.Instance.transform.DOMoveX(0, 1);
        yield return new WaitForSeconds(1);
        PlayerAnimationController.Instance.FinishAnimation();
        parent.DORotate(rotateFinish,1);
        yield return new WaitForSeconds(1.5f);
        
    }
    public void Bonus(){
        StateManager.Instance.State = State.EndGame;
        Debug.Log(Multiplier);
    }
    #endregion
}
