using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameController GameManager;

    public void ResumeGame() => GameManager.ResumeGame();
    public void QuitGame() => Application.Quit();
    public void MainMenuGame() => GameManager.PlayAgain();
    public void StartGame() => GameManager.StartGame();
    public void PlayAgain() => GameManager.PlayAgain();

    public void PauseMenu() => GameManager.PauseGame();

    public void ApplyChange() => Debug.Log("Applied");

    public void SettingMenu() => GameManager.SettingMenu();

}
