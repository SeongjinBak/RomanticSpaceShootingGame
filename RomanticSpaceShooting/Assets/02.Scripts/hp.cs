using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hp : MonoBehaviour {

    public GameObject hp1;
    public GameObject hp2;
    public GameObject hp3;
    public GameObject hp4;
    public GameObject hp5;

    public int current; // 순서 정해줘서 case문으로 없애고 만들어내겠음.
   
    public GameObject bullchange;
    public GameObject Bad;//배드엔딩

    void Start () {
        current = 5;// 첫번째
        bullchange = GameObject.Find("bull");
    }
	

	void Update () {
       
    }

    public void HpPlus()
    {
        if (bullchange.transform.position.y == -2.45f) //총알이 움직일땐 못움직이게 하라
        {
            switch (current)
            {
                case 1:
                    hp2.SetActive(true);
                    current = 2;
                    break;
                case 2:
                    hp3.SetActive(true);
                    current = 3;
                    break;
                case 3:
                    hp4.SetActive(true);
                    current = 4;
                    break;
                case 4:
                    hp5.SetActive(true);
                    current = 5;
                    break;
                case 5:
                    break;
            }
        }
    }

    public void HpDelete()
    {
        if (bullchange.transform.position.y == -2.45f)
            switch (current)
        {
            case 1:
                hp1.SetActive(false);
                current = 0;
                    Bad.SetActive(true); // 별 다 떨어지면 배드 엔딩
                break;
            case 2:
                hp2.SetActive(false);
                current = 1;
                break;
            case 3:
                hp3.SetActive(false);
                current = 2;
                break;
            case 4:
                hp4.SetActive(false);
                current = 3;
                break;
            case 5:
                hp5.SetActive(false); 
                current = 4;
                break;
        }
    }
}
