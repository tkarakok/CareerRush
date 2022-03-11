using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject inGamePanel;
    public GameObject endGamePanel;
    public GameObject gameOverPanel;

    #region Private Fields
    private GameObject _currentPanel;
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
}
