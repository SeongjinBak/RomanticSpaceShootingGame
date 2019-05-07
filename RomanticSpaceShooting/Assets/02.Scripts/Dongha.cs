using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dongha : MonoBehaviour {
    SpriteRenderer rend;
    public float i;

    public int TextSize;
    public GameObject[] Textt;
    public AudioSource audioSource;
    public AudioClip[] bgm;
    public int current = 0; // 여러개의 bgm을 처리하기 위해서 현재 비지엠 위치
    public GameObject player; // fade 들어갔을 때 총알이 발사되지 않게 하기 위해서

    String[] Poem;
    public GameObject Bad; // 배드엔딩
    int dd;// 켜져있는 텍스트

    public bool Already; // 이미 텍스트가 있으면 코루틴을 돌리지 말자.
    Fadeout fadeout;
    public Text poem;
    int number;
    hp hp1;
    // Use this for initialization
    void Start () {
        rend = this.gameObject.GetComponent<SpriteRenderer>();
        Color c = rend.color;
        c.a = 0;

        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("bull");
        Bad.SetActive(false);
        Already = true;
        fadeout = GameObject.Find("Fade").GetComponent<Fadeout>();
        number = 0;
        hp1 = GetComponent<hp>();
    }
	
	// Update is called once per frame
	void Update () {
        if (current == TextSize)
            Bad.SetActive(true);
        if ((audioSource.isPlaying==false) && (Already == false))
        { fadeout.DonghaSay(); RemoveText(); Already = true; }

        poem.text = "시 들은\n 횟수: " + number+"/13";
    }

    public void DonghaSay()
    {
        if (Already == true)
        {
            StartCoroutine(FadeIn());
            Already = false;
            number += 1;          
        }
    }

    public void RemoveText()
    {
        Textt[dd].SetActive(false);
    }

    IEnumerator FadeIn()
    {
        current += 1;
        Debug.Log("fadein");
        Textt[current].SetActive(true);
        dd = current;

        player.SetActive(false);
        audioSource.clip = bgm[current];
        audioSource.Play();
        float i;
        for (i = 0f; i <= 1; i += 0.1f)
        {
            rend = this.gameObject.GetComponent<SpriteRenderer>();
            Color c = rend.color;
            c.a = i;
            rend.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        
        hp1.HpPlus();
       
    }
}
