using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{
    #region Panels
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject inGamePanel;
    public GameObject endGamePanel;
    public GameObject gameOverPanel;
    #endregion
    #region Private Fields
    private GameObject _currentPanel;
    #endregion
    #region In Game Fields
    [Header("InGame")]
    public Text inGameMoneyText;
    #endregion
    private void Start()
    {
        _currentPanel = mainMenuPanel;

    }


    #region Button Functions
    public void StartButton()
    {
        PanelChange(inGamePanel);
        EventManager.Instance.InGameEvent();
    }
    #endregion

    #region Panel Change Function
    /// <summary>
    /// We can close current panel after then we'll open new panel
    /// </summary>
    /// <param name="openPanel"> Panel To Open </param>
    void PanelChange(GameObject openPanel)
    {
        _currentPanel.SetActive(false);
        openPanel.SetActive(true);
        _currentPanel = openPanel;
    }
    #endregion

    #region InGame Functions
    public void InGameTextUpdate(){
        inGameMoneyText.text = GameManager.Instance.AmountOfMoney.ToString();
    }

    #region GameOver
    public void GameOver(){
        PanelChange(gameOverPanel);
    }
    
    #endregion
    #endregion
}
