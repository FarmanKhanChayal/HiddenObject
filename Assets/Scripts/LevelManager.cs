using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static readonly string CURRENT_LEVEL = "currentLevel";

    public static LevelManager Instance;

    public List<GameObject> LevelsList;
    public TextMeshProUGUI TimerText;
    public int maxTime = 90;
    public GameObject WrongClickedPanel;
    public Canvas canvas;

    private GameObject currentActiveLevel;

    private int currentLevel;
    private bool timeMinimize = false;

    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;

        currentLevel = PlayerPrefs.GetInt(CURRENT_LEVEL, 0);
    }

    void Start()
    {
        for (int i = 0; i < LevelsList.Count; i++)
        {
            LevelsList[i].SetActive(false);
        }

      
        
        currentActiveLevel = LevelsList[currentLevel];
        currentActiveLevel.SetActive(true);

        StartCoroutine(SetTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SetTimer()
    {
        for (int i = maxTime; i >= 0; i--)
        {

            if (timeMinimize)
            {
                i -= 10;
                timeMinimize = false;
            }

            int min = i / 60;
            int sec = i % 60;

            string minute = min > 9 ? min.ToString() : $"0{min}";
            string second = sec > 9 ? sec.ToString() :$"0{sec}.";

            yield return new WaitForSeconds(1);

            TimerText.text = $"{minute}:{second}";

            if (i == 0)
            {
                LevelUiManager.Instance.OnGameOver();
            }

           

            
        }
    }

    public void OnNextButton()
    {

        //currentActiveLevel.SetActive(false);
        //currentActiveLevel = LevelsList[currentLevel];
        //currentActiveLevel.SetActive(true);
        
        //StopAllCoroutines();
        //StartCoroutine(SetTimer());

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;


    }

    public void IncreaseLevel()
    {
        currentLevel = (currentLevel + 1) % 4;
        PlayerPrefs.SetInt(CURRENT_LEVEL, currentLevel);
        PlayerPrefs.Save();

    }

    public void OnRestartClick()
    {
        //LevelUiManager.Instance.GameOverPanel.SetActive(false);
        //StopAllCoroutines();
        //StartCoroutine(SetTimer());
        //HiddenObjectManager.Instance.ActivateObjecs();
        //HiddenObjectManager.Instance.ActivateHiddenObjectIcons();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }

    public void OnBackGroundClick( )
    {
        timeMinimize = true;


        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, Input.mousePosition,null,out anchoredPosition);
        Debug.Log(anchoredPosition);
        GameObject obj = Instantiate(WrongClickedPanel, canvas.transform);
        RectTransform objectRectTransform = obj.GetComponent<RectTransform>();
        objectRectTransform.anchoredPosition = anchoredPosition;
        StartCoroutine(WrongclickedObjectAnimation(obj));







    }

    IEnumerator WrongclickedObjectAnimation(GameObject obj)
    {
        Vector3 pos = obj.transform.position;
        pos.y += 15;
        obj.transform.DOMove(pos,0.5f);
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0, 1);
        yield return new WaitForSeconds(0.5f);
        obj.SetActive(false);


    }


}
