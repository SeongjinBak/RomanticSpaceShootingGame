using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {

    public GameObject[] Scene;
    public GameObject GameCanvas;
    public GameObject OpenCanvas;
    public GameObject FadeImage;
    public bool flag;
    // Use this for initialization
    void Start()
    {
        flag = false;
        Scene[0].SetActive(true);
        for (int i = 1; i < 4; i++)
        {
            Scene[i].SetActive(false);
        }
        GameCanvas.SetActive(false);

       // StartCoroutine(OpeningOn());
    }

    // 자동으로 오프닝 넘어가도록 구현
    IEnumerator OpeningOn()
    {
            for (int i = 0; i < 3; i++)
            {
                Scene[i].SetActive(false);
                Scene[i + 1].SetActive(true);
                yield return new WaitForSeconds(2.5f);
            }
            Scene[3].SetActive(false);
            GameCanvas.SetActive(true);
            OpenCanvas.SetActive(false);
            FadeImage.SetActive(false);
        
    }
    private void Update()
    {
        if (!flag)
        {
            if (Input.GetMouseButtonDown(0))
            {
                flag = true;
                StartCoroutine(OpeningOn());
            }
        }
    }
    // Update is called once per frame
    /* void Update()
     {
         if (i==2)
         {
             Scene[1].SetActive(false);
             Scene[2].SetActive(true);
         }
         if (i == 3)
         {
             Scene[2].SetActive(false);
             Scene[3].SetActive(true);
         }

         if (Input.GetMouseButtonDown(0)) i = i + 1;
         if (i == 4)
         {
             GameCanvas.SetActive(true);
             OpenCanvas.SetActive(false);
             FadeImage.SetActive(false);
         }

     }

     public void Opening()
     {
             Scene[0].SetActive(false);
             Scene[1].SetActive(true);
     }
     */
}
