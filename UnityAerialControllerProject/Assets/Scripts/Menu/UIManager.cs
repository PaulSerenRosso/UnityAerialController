using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Image = UnityEngine.UI.Image;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject HUDPanel;
    [SerializeField] private GameObject EndPanel;

    [SerializeField] private GameObject[] Life;
    
    [SerializeField] private TMP_Text EndTitle;
    [SerializeField] private TMP_Text TimerText;
    [SerializeField] private GameObject EnemyContainer;
    [SerializeField] private GameObject enemyImagePrefab;
    
    private void Update()
    {
        DisplayTimerText(Time.timeSinceLevelLoad);
    }
    
    public void DisplayTimerText(float timer)
    {
        float min = timer / 60;
        float sec = timer % 60;
        TimerText.text = "Timer : " + (int) min + ":" + sec.ToString("00.0");
    }

    public void AddEnemyToList(Color enemyColor)
    {
        var temp = Instantiate(enemyImagePrefab, EnemyContainer.transform);
        temp.GetComponent<Image>().color = enemyColor;
    }
    
    /*public void UpdateEnemyCount(int count, int max)
    {
        EnemyCount.text = "Enemies : " + count + " / " + max;
    }*/

    public void UpdateLife(int remainingLife)
    {
        Life[remainingLife].SetActive(false);
        if(remainingLife < 1 ) End(false);
    }
    
    public void End(bool win)
    {
        HUDPanel.SetActive(false);
        EndPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        EndTitle.text = win ? "You Won !" : "You Lost...";
    }
    
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }
}
