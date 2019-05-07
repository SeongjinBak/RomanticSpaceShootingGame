using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour {
    // 운석이 떨어지는 경로의 목적지
    public Transform goalTr;
    // 운석의 트랜스폼
    private Transform meteoTr;
    // 카메라 흔들기 위한 스크립트
    private ShakeCamera CameraShake;
    // 낙하 속도
    public float fallSpeed;
    // 운석과 땅 충돌 후 엔딩화면 출력을 위한 변수
    public bool collide;
    public bool endingOn;
    private SpriteRenderer meteoSr;
    public Sprite []meteoSp;
    public AudioClip meteoSound;
    public AudioSource meteoAudio;
    public bool isSoundPlay;
    // Use this for initialization
    void Start () {
        // 낙하속도, 목적지, 트랜스폼, 카메라 초기화
        fallSpeed = 2.0f;
        goalTr = GameObject.Find("MeteoGoal").GetComponent<Transform>();
        meteoTr = GameObject.Find("Meteo").GetComponent<Transform>();
        CameraShake = GameObject.Find("CameraRig").GetComponent<ShakeCamera>();
        collide = false;
        endingOn = false;
        isSoundPlay = false;
        meteoAudio = GetComponent<AudioSource>();
        meteoSr = GetComponent<SpriteRenderer>();
        StartCoroutine(MeteoAnim());
    }
    public IEnumerator MeteoAnim()
    {
        int len = meteoSp.Length;
        int tmp = 0;
        while (!endingOn)
        {
            meteoSr.sprite = meteoSp[(tmp++) % 3];
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void SoundPlay()
    {
        meteoAudio.PlayOneShot(meteoSound);
    }
    public IEnumerator MeteoStrike()
    {
        while (meteoTr.position != goalTr.position)
        {
            // 운석 이동
            meteoTr.position = Vector2.MoveTowards(meteoTr.position, goalTr.position, fallSpeed * Time.deltaTime);
            
            //meteoSr.spr[(tmp++) % 3];
         yield return new WaitForSeconds(1.0f);
        }
        if(isSoundPlay == false)
        {
            SoundPlay();
        }
        // 화면 흔들기    
        StartCoroutine(CameraShake.Shake(4.0f, 2.0f));
        yield return new WaitForSeconds(4.0f);
        Debug.Log("meteo striked");
        if (isSoundPlay == false)
        {
            SoundPlay();
            isSoundPlay = true;
        }
        collide = true;
        endingOn = true;
    }

}
