using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playershootingmove : MonoBehaviour
{
    // 폭발 이펙트 컴포넌트를 저장하기 위한 변수
    public List<SpriteRenderer> flame;
    public WaitForSeconds ws;
    public float Movespeed; //미사일이 날라가는 속도
    public float Destroypos; // 미사일이 사라지는 지점
    //public hp AA;// 하트를 없애주기 위해서 스크립트 가져옵니다.
    public bool go; // 하트 없애주려면 go가 true여야 합니다.
    private AudioSource shotgunSfx;
    public AudioClip shotgunShot;
    public GameObject FadeImage; // Fade 동하 눌렀을 때 나와야 함.
    public GameObject text; // Text 문제 해결
    public AudioClip starSound;
    private AudioSource starAudio;
    private ShakeCamera shake;
    private ShakeCamera starShake;
    public bool soundManageVar;
    private void Awake()
    {
        //Star 의 하위 컴포넌트를  flame에 저장.
        var group = GameObject.Find("Star");
        if (group != null)
        {
            group.GetComponentsInChildren<SpriteRenderer>(flame);
            flame.RemoveAt(0); 
        }
        ws = new WaitForSeconds(0.15f);

        Screen.SetResolution(490, 910, false);

        // ShakeCamera 스크립트 저장
        shake = GameObject.Find("CameraRig").GetComponent<ShakeCamera>();
        starShake = GameObject.Find("Star").GetComponent<ShakeCamera>();
        shotgunSfx = GetComponent<AudioSource>();
    }
    void Start() {
        soundManageVar = false;
        go = false;
      //  AA = GameObject.Find("Heart").GetComponent<hp>();
        text.SetActive(false);

        for (int i = 0; i < flame.Count; i++)
        {
            Debug.Log("ffff");
            // 전부 사용 안함으로 체크.
            flame[i].enabled = false;
        }
      

    }



    public void SoundPlay()
    {
        shotgunSfx.PlayOneShot(shotgunShot);
    }

    void Update()
    {

        if (go == true) // 하트를 누르면 총알이 발사됩니다.
        {
            if (soundManageVar == false)
            {
                SoundPlay();
                soundManageVar = true;
            }
            // 카메라 흔들림 효과 호출
            StartCoroutine(shake.Shake(0.01f,0.2f));
            // 매 프레임 마다 미사일이 Movespeed만큼 up (y축 +방향) 으로 날라갑니다.
            transform.Translate(Vector2.up * Movespeed * Time.deltaTime);
            //만약에 미사일의 위치가 Destroypos를 넘어서면
            if (transform.position.y >= Destroypos)
            {
               // GetComponent<CircleCollider2D>().enabled = false;
                EndPosition();
                soundManageVar = false;
            }

           

        }

    
    
    }

    public void StarSoundPlay()
    {
        shotgunSfx.PlayOneShot(starSound);
        soundManageVar = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 별과 충돌시
        if (collision.collider.CompareTag("STAR"))
        {
            StarSoundPlay();
            // 카메라 흔들림 효과 호출
            StartCoroutine(shake.Shake(0.05f, 0.5f));
            Debug.Log("적 기체와 충돌");
            EndPosition();
              // 별과 충돌시 이펙트 전부 출력하는 코루틴 호출
            StartCoroutine(StarFlame());
            StarController.difficulty++;
            //  NewBehaviourScript.gameManager.instance.AddScore(50);
            GameManager.instance.AddScore(50);
        }
        else if (collision.collider.CompareTag("Bird")){
            EndPosition();
        }
        if (go == true)
        {
            Go();

        }
    }

    // 별과 충돌시 폭발하는 코루틴
    IEnumerator StarFlame()
    {

        
        for (int i = 0; i < flame.Count; i++)
        {
            flame[i].enabled = true;
            yield return ws;
            flame[i].enabled = false;
            Debug.Log("FLAME");
        }
     
    }
    public void Go()
    {
        go = true;
      //  AA.HpDelete();
    }

    public void EndPosition()
    {
        go = false;
        transform.position = new Vector3(0, -2.45f, 0);
    }

    public void FadeStart()
    {
        if (go == false)
        {
          FadeImage.SetActive(true);
        }
       
    }
}



