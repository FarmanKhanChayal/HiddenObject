using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUiManager : MonoBehaviour
{
    public static LevelUiManager Instance;

   public GameObject LevelCompletePanel;
    public GameObject GameOverPanel;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLevelComplete()
    {
        LevelCompletePanel.SetActive(true);
        LevelManager.Instance.IncreaseLevel();
        Time.timeScale = 0f;
    }

    public void OnGameOver()
    {
        GameOverPanel.SetActive(true);
    }
}
