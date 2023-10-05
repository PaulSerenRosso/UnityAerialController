using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject[] tutorialPanels;

    private int panelIndex;
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void StartGame(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Tutorial(bool quit)
    {
        if (quit)
        {
            tutorialPanels[panelIndex].SetActive(false);
            mainPanel.SetActive(true);
            panelIndex = 0;
        }
        else
        {
            mainPanel.SetActive(false);
            tutorialPanels[panelIndex].SetActive(true);
        }
    }

    public void ChangePanel(bool previous)
    {
        tutorialPanels[panelIndex].SetActive(false);
        panelIndex += previous ? -1 : 1;
        tutorialPanels[panelIndex].SetActive(true);
    }
    
    
    public void Quit()
    {
        if (Application.isEditor) UnityEditor.EditorApplication.isPlaying = false;
        else Application.Quit();
    }
}
