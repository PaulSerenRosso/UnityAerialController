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
    public int maxEnemyCount;

    public GameObject player;
    
    [SerializeField] private GameObject HUDPanel;
    [SerializeField] private GameObject EndPanel;

    [SerializeField] private GameObject[] Life;
    
    [SerializeField] private TMP_Text EndTitle;
    [SerializeField] private TMP_Text TimerText;
    [SerializeField] private TMP_Text EnemyCountText;
    [SerializeField] private GameObject EnemyContainer;
    [SerializeField] private GameObject enemyImagePrefab;

    private int enemyCount;
    
    void Update()
    {
        DisplayTimerText(Time.timeSinceLevelLoad);
    }
    
    void DisplayTimerText(float timer)
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

    public void UpdateEnemyCount()
    {
        enemyCount--;
        UpdateEnemyCount(enemyCount);
        if(enemyCount<1) End(true);
    }
    
    void UpdateEnemyCount(int count)
    {
        EnemyCountText.text = "Enemies : " + count + " / " + enemyCount;
    }

    public void UpdateHealth(int remainingLife)
    {
        Life[remainingLife].SetActive(false);
        if(remainingLife < 1 ) End(false);
    }
    
    void End(bool win)
    {
        player.GetComponent<AirplaneController>().LockControls();
        
        HUDPanel.SetActive(false);
        EndPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        EndTitle.text = win ? "You Won !" : "You Lost...";
        EnemyCountText.text =
            win
                ? "Enemies : " + (maxEnemyCount+1) + " / " + (maxEnemyCount+1)
                : "Enemies : " + (maxEnemyCount - enemyCount) + " / " + (maxEnemyCount+1);
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
