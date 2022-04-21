using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject newGamePanel;
    [SerializeField] GameObject gamePausePanel;

    private void Start() {
        GameController.registerUIManager(this);
    }

    public void ShowGameover() {
        gameOverPanel.SetActive(true);
    }
    public void HideGameover() {
        gameOverPanel.SetActive(false);
    }
    public void ShowNewGamePanel() {
        newGamePanel.SetActive(true);
    }
    public void HideNewGamePanel() {
        newGamePanel.SetActive(false);
    }
    public void ShowGamePausedPanel() {
        gamePausePanel.SetActive(true);
    }
    public void HideGamePausedPanel() {
        gamePausePanel.SetActive(false);
    }
}
