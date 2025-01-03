using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class option : MonoBehaviour
{
    public GameObject pausepanel;
    public GameObject homePanel;
    public GameObject exitPanel;
    void Start()
    {
        pausepanel.SetActive(false);
        homePanel.SetActive(false);
        exitPanel.SetActive(false);
    }
    public void onpauseclick()
    {
        pausepanel.SetActive(true);
        homePanel.SetActive(false);
        exitPanel.SetActive(false);
        ResetPauseMenu();
    }
    public void onhomeclick()
    {
        pausepanel.SetActive(true);
        homePanel.SetActive(true);
        exitPanel.SetActive(false);
    }
    public void onexitclick()
    {
        pausepanel.SetActive(true);
        homePanel.SetActive(false);
        exitPanel.SetActive(true);
    }
    public void ClosePanels1()
    {
        pausepanel.SetActive(false);
        homePanel.SetActive(false);
        exitPanel.SetActive(false);
    }
    public void ClosePanels2()
    {
        pausepanel.SetActive(true);
        homePanel.SetActive(false);
        exitPanel.SetActive(false);
        ResetPauseMenu();
    }
    private void ResetPauseMenu()
    {
        Button[] button=pausepanel.GetComponentsInChildren<Button>();
    }
    public void home()
    {
        SceneManager.LoadScene("intro");
    }
    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
