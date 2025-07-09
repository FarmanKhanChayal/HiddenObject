using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HiddenObjectManager : MonoBehaviour
{
    public static HiddenObjectManager Instance;

    public List<GameObject> hiddenObjectIconList;
    public List<GameObject> activeHiddenObjectList;

    private List<GameObject> gotHiddenObjectIconList;
    private bool canClick = true;

    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //gotHiddenObjectIconList = new List<GameObject>();
        //foreach (GameObject item in hiddenObjectIconList)
        //{
        //    gotHiddenObjectIconList.Add(item);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnObjectClick(GameObject obj)
    {
        if (canClick)
        {
            StartCoroutine(ObjectMatchAnimation(obj));
        }
      
    }

    private void ScaleObject(GameObject Object)
    {
        DOTween.Sequence()
            .Append(Object.transform.DOScale(1.25f, 0.25f))
            .Append(Object.transform.DOScale(1, 0.25f))
            .Append(Object.transform.DOScale(1.25f, 0.25f))
            .Append(Object.transform.DOScale(1, 0.25f));
            
    }

    IEnumerator ObjectMatchAnimation(GameObject obj)
    {           
       canClick = false;

        for (int i = 0; i < hiddenObjectIconList.Count; i++)
        {
            if (hiddenObjectIconList[i].gameObject.name == obj.gameObject.name)
            {
                ScaleObject(hiddenObjectIconList[i]);
                ScaleObject(obj);

                yield return new WaitForSeconds(1f);

                hiddenObjectIconList[i].SetActive(false);
                obj.SetActive(false);
                

                hiddenObjectIconList.RemoveAt(i);
                break;
            }
        }

        if (hiddenObjectIconList.Count == 0)
        {
            LevelUiManager.Instance.OnLevelComplete();
        }

        canClick = true;

    }

    //public void ActivateObjecs()
    //{
    //    for (int i = 0; i < activeHiddenObjectList.Count;i++)
    //    {
    //        activeHiddenObjectList[i].SetActive(true);
    //    }
    //}

    //public void ActivateHiddenObjectIcons()
    //{
    //    for(int i = 0; i < gotHiddenObjectIconList.Count; i++)
    //    {
    //        hiddenObjectIconList.Clear();

    //        hiddenObjectIconList.Add(gotHiddenObjectIconList[i]);
    //        hiddenObjectIconList[i].SetActive(true);
    //    }
    //}

}
