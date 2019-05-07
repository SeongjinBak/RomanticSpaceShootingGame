using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour {
    // 변수들 테스트시 확인하기 쉽게 public으로 선언해 둔것 있음.

    // 총알의 게임 오브젝트 정보 저장
    public GameObject bullet;
    // 머쓱 표현할 스프라이트 이미지 리스트 생성
    public List<Sprite> meosseuk;
    // 현재 별의 스프라이트 렌더러 컴포넌트를 저장할 변수
    private SpriteRenderer starSr;
    // 별의 Rigidbody 저장할 변수 선언
    private Rigidbody2D starRb;
    // 별의 Transform을 저장할 변수 선언
    private Transform starTr;
    // 별의 움직임 난이도(레벨)을 저장할 변수 선언, 총알과 충돌시 difficulty ++; 하는 코드 필요.
    public static int difficulty;
    // 맨 끝으로 갔을때 방향 전환을 위한 충돌체크 변수
    private bool switching;
    // 별의 이동속도를 조절할 변수 선언
    public float moveSpeed;
    // 랜덤 포인트를 위한 리스트 선언
    public List<Transform> spawnPos;
    // 랜덤 리스트의 인덱스를 저장할 변수 선언
    public int randIdx;
    // 운석 낙하 코루틴을 호출하기 위한 스크립트 불러옴.
    private Meteo meteo;
    Dongha dong;
    public GameObject Happy; // 해피엔딩
    public GameObject[] Collapse; //땅에 행성이 부딪히는 엔딩
    public bool flag = false;
    public bool bgmControl = false;
    public AudioSource audioSource;
    public AudioClip bgm; // 레벨 3때 나오도록
    public AudioSource FadeAudioSource;
    public AudioClip basicBgm; // 1 ~ 2 까지 나오도록
    hp heart;
    Dongha dongha;
    // 테스팅 변수
    public int testdifficulty;

    // Use this for initialization
    void Start () {
        // 테스팅 변수
        testdifficulty = 1;


        starTr = GetComponent<Transform>();
        starRb = GetComponent<Rigidbody2D>();
        starSr = GetComponent<SpriteRenderer>();
        // 총알 찾아서 게임오브젝트에 저장.
        GameObject.Find("bull").GetComponent<GameObject>();
        // SpawnLocation 아래 SpawnPos들을 리스트에 저장.
        var group = GameObject.Find("SpawnLocation");
        if(group != null)
        {
            group.GetComponentsInChildren<Transform>(spawnPos);
            spawnPos.RemoveAt(0);
        }
        // SpawnPos의 인덱스를 랜덤으로 생성.
        randIdx = Random.Range(0, spawnPos.Count);
        // 난이도 1로 시작.
        difficulty = 1;
        // 초기 속도 10으로 시작.
        moveSpeed = 10.0f;
        // switching 변수 false로 시작, 충돌시 true.
        switching = false;

       dong = GameObject.Find("Fade").GetComponent<Dongha>();
        Happy.SetActive(false); Collapse[0].SetActive(false);
        Collapse[1].SetActive(false);

        // 운석낙하 스크립트 얻음
        meteo = GameObject.Find("Meteo").GetComponent<Meteo>();

        audioSource = GetComponent<AudioSource>();
        FadeAudioSource = GameObject.Find("Fade").GetComponent<AudioSource>();
        heart = GameObject.Find("Fade").GetComponent<hp>();
        dongha = GameObject.Find("Fade").GetComponent<Dongha>();
    }
	
	// Update is called once per frame
	void Update () {
        // 테스팅 변수
        //difficulty = testdifficulty;


        // 난이도에 따라 별의 움직임이 달라짐.
        StarMove(difficulty);
        // 현재 별의 위치보다 총알의 위치(y값)이 더 높으면 머쓱 이미지 출력
        if (starTr.position.y <= bullet.transform.position.y)
        {

            StartCoroutine(StarMeosseuk());
        }

        if (difficulty >= 6)// 난이도 6일 경우 즉 끝날 경우.
        {
            audioSource.Stop();
            if (dong.current < 5)
                Happy.SetActive(true);  // 5번 이하이면 해피엔딩.
            else if (dong.current < 10)
            {
                if (!meteo.collide)
                    StartCoroutine(collideEnding());

            }
            else
            {
                Happy.SetActive(true);  // 그 외 전부 해피엔딩.
            }

        }
        else if (difficulty <= 2) sound1();
        else if (difficulty >= 3)
        {
            if (bgmControl == false)
            {
                audioSource.Stop();
                bgmControl = true;
            }
            sound();  // 레벨 3부터 소리나게 만들었습니다.
        }
    }

    public void sound1()
    {
        if (!audioSource.isPlaying)
        { audioSource.clip = basicBgm; audioSource.Play(); }
        if (FadeAudioSource.isPlaying)
            audioSource.Stop();
        if (heart.current == 0 || dongha.current > 13) audioSource.Stop();
    }
    public void sound()
    {
        if (!audioSource.isPlaying)
        { audioSource.clip = bgm; audioSource.Play(); }
        if (FadeAudioSource.isPlaying)
            audioSource.Stop();
        if( heart.current == 0 || dongha.current > 13) audioSource.Stop();
    }

    IEnumerator collideEnding()
    {
       
            if(flag == false)
            {
                Collapse[0].SetActive(true);
                yield return new WaitForSeconds(2.5f);
                Collapse[0].SetActive(false);
                flag = true;
            }
            //yield return new WaitForSeconds(2.5f);
            // Collapse[1].SetActive(false);
            StartCoroutine(meteo.MeteoStrike());
        
             yield return new WaitForSeconds(3.0f);
            if(meteo.endingOn)
             Collapse[1].SetActive(true);
            
        
            
    }
    // 양 옆에있는 기둥에 별이 닿았을 경우
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("COLUMN"))
        {
            switching = !switching;
        }
    }

    // 레벨별 별의 움직임을 구현하는 함수.
    public void StarMove(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                if (!switching)
                {
                    starRb.AddForce(Vector2.left * moveSpeed );
                }
                else
                {
                    starRb.AddForce(Vector2.right * moveSpeed );
                }
                break;
            case 2:
                moveSpeed = 30.0f;
                if (!switching)
                {
                    starRb.AddForce(Vector2.left * moveSpeed );
                }
                else
                {
                    starRb.AddForce(Vector2.right * moveSpeed );
                }
                break;
            case 3: 
            case 4:
                moveSpeed = 20.0f;
                // 레벨 3의 경우, 패턴없이 랜덤으로 움직인다.
                // 더 복잡하게 하고싶다면, Hierachy View의 SpawnLocation에 Sprite 추가하면 됨.
                // 목적지까지 가지 못했다면 목적지까지 가는 분기
                if (starTr.position != spawnPos[randIdx].position)
                {
                    starTr.position = Vector2.MoveTowards(starTr.position, spawnPos[randIdx].position, moveSpeed * Time.deltaTime);
                }
                // 목적지에 도착 했다면, 다음 랜덤인덱스를 받는다.
                else
                {
                    randIdx = Random.Range(0, spawnPos.Count);
                }
                break;
            case 5:
                // 속도 랜덤
                if (starTr.position != spawnPos[randIdx].position)
                {
                    starTr.position = Vector2.MoveTowards(starTr.position, spawnPos[randIdx].position, moveSpeed * Time.deltaTime);
                }
                // 목적지에 도착 했다면, 다음 랜덤인덱스를 받는다.
                else
                {
                    moveSpeed = Random.Range(3.0f, 25.0f);
                    randIdx = Random.Range(0, spawnPos.Count);
                }
                break;
        }
    }

    // 머쓱;; 출력 코루틴
    IEnumerator StarMeosseuk()
    {
        for(int i = 0; i < meosseuk.Count; i++)
        {
            starSr.sprite = meosseuk[i];
            yield return new WaitForSeconds(0.2f);
        }
    }
}