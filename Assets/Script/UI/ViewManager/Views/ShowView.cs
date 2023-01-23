using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowView : MonoBehaviour 
{
    public void ShowLast()
    {
        ViewManager.ShowLast();
    }

    public void ShowPauseView()
    {
        ViewManager.Show<PauseView>();
    }

    public void ShowGameView()
    {
        ViewManager.Show<GameView>();
    }

    public void ShowBeginGameView()
    {
        ViewManager.Show<BeginGameView>();
    }

    public void ShowEndGameView()
    {
        ViewManager.Show<EndGameView>();
    }

    public void ShowStatView()
    {
        Debug.Log("ShowStatView");
        ViewManager.Show<InGameStatView>();
    }

    public void ShowMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}