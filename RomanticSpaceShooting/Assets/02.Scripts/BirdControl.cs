using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl : MonoBehaviour {
    private SpriteRenderer birdSr;
    public List<Sprite> birdSpriteList;
    private Transform birdTr;
    public float speedOffset;
    private WaitForSeconds ws;
    private WaitForSeconds ws2;
    private Vector3 originPos;
    private Quaternion originRot;
    private bool moveOn;
    private static bool isDie;
    private Collider2D birdColl;
    private SpriteRenderer birdFlame;
    private AudioSource birdAudio;
    public AudioClip birdClip;
    // Use this for initialization
    void Start () {
        // 새의 transform 저장
        birdTr = GetComponent <Transform>();
        // 새의 sprite renderer 저장
        birdSr = GetComponent <SpriteRenderer>();
        // 새의 속도 offset
        speedOffset = 5.0f;
        ws = new WaitForSeconds(0.2f);
        ws2 = new WaitForSeconds(3.0f);
        originPos = birdTr.position;
        originRot = birdTr.rotation;
        // moveOn 변수로, Update 함수에서 코루틴 호출할때 중복해서 호출되는거 막아줌.
        moveOn = false;
        birdColl = GetComponent<Collider2D>();
        isDie = false;
        birdFlame = GameObject.Find("BirdFlame").GetComponentInChildren<SpriteRenderer>();
        birdFlame.enabled = false;
        birdSr.enabled = false;
        birdAudio = GetComponent<AudioSource>();
    }
	
    public void BirdSound()
    {
        birdAudio.PlayOneShot(birdClip);
    }
	// Update is called once per frame
	void Update () {
        // 난이도가 4단계 이상일 경우
        if(StarController.difficulty >= 4)
        {
            if (!isDie)
            {
                birdTr.Translate(Vector2.left * speedOffset * Time.deltaTime);
            }
            if (!moveOn)
            {
                // 1번만 사용하는 moveOn변수
                moveOn = true;
                StartCoroutine(BirdMove());
                
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            // Update함수 멈춤
            isDie = true;

            birdColl.enabled = false;
            // 이펙트 보이게 함
            birdFlame.enabled = true;
            // 죽은 새 그림
            birdSr.sprite = birdSpriteList[2];
            StartCoroutine(BirdDie());

    
        }
    }
    
     
    IEnumerator BirdDie()
    {
        
        // 폭발이펙트 다시 안보이게함
        while (birdTr.position.y >= -4.5f)
        {
            birdSr.enabled = true;
            birdTr.rotation = originRot;
            birdTr.Translate(Vector2.down * Time.deltaTime * speedOffset);
            yield return new WaitForSeconds(0.05f);
            
        }
        birdSr.enabled = false;
        birdFlame.enabled = false;
        birdTr.position = originPos;
        yield return ws2;
        isDie = false;
        moveOn = false;
        // 다시 원래 새로 돌아옴
        birdSr.sprite = birdSpriteList[0];
        birdColl.enabled = true;
    }
    
    IEnumerator BirdMove()
    {
        int i = 0;
        // 안죽었을경우
        while (!isDie && StarController.difficulty <6)
        {
            BirdSound();
            birdTr.position = originPos;
            birdTr.rotation = originRot;
            while (birdTr.position.x >= -7.6f && !isDie)
            {
                birdSr.enabled = true;
                Debug.Log("ZZAEK");
                birdSr.sprite = birdSpriteList[(i++)%2];
                yield return ws;
            }
            birdSr.enabled = false;
            yield return ws2;
            BirdSound();
        }
    }

}
