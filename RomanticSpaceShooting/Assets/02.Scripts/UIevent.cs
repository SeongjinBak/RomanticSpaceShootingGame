using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIevent : MonoBehaviour
{
    private bool pauseOn = false;
    private GameObject normalPanel;
    private GameObject pausePanel;
    public GameObject image;
    //여기다가 이미지 끌어다 놔주세요
    //시작합니당 이제꺼야해
    public GameObject developerInfo;
   void update()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
            image.SetActive(!image.activeSelf);
        if (Input.GetKey(KeyCode.Space))
            image.SetActive(!image.activeSelf);
    }

    void Awake()
    {
        normalPanel = GameObject.Find("Canvas").transform.GetChild(5).gameObject;
        pausePanel = GameObject.Find("Canvas").transform.GetChild(6).gameObject;
        developerInfo.SetActive(false);
    }

    public void ActivePauseBt()
    {
        if (!pauseOn)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            normalPanel.SetActive(false);
        }
        else
        {//일시정지 중이면 해제
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
            normalPanel.SetActive(true);
        }
        pauseOn = !pauseOn;
    }

    public void RetryBt()
    {
        Debug.Log("게임 재시작.");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Scene1");
        
    }

    public void ShowDevelopers()
    {
        developerInfo.SetActive(!developerInfo.activeSelf);
    }
    public void QuitShowDevelopers()
    {
        developerInfo.SetActive(!developerInfo.activeSelf);
    }

    public void CharacterBt()
    {
        image.SetActive(!image.activeSelf);
    }

    public void CaharacterBtCancle()
    {
        image.SetActive(!image.activeSelf);
    }
    //눌렀던버튼 다시 눌러서 끄면 되는데 어디있죠? 그거 몰라앗차
    //그 버튼 만 다시 잘 잡으면 될 거 같습니다 그게 저 이미지 안족에잇을껄
    //쉽지않네요 ㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋ
    //그러면 x버튼이라도 만드셔야 할 거가틋빈다. 아니면은
    // 안드로이드에서 하시는건가요이거./? 아마그럴거야
    //ㅇ업데이트에서 if(Input.GetTouch(0).phase == TouchPhase.Ended)
    //눌렀다 뗄 때인데 이거 쓰시면 될 거같아요 근데 묵ㄴ제는 화면 전체라
    //포지션 설정해주시려면 해주시고, 화면 전체 어디든 눌렀다뗄떼 호출돼요 포지션 설정 ㄴㄴ그냥전체 해도될듯
    //ㅡ럼저 한여름밤의꿈하러갈게요
    //이거그럼 스크립트 여기다가 해도되는거? 네네 구현은 ㄱ Quit.Apllication 처럼 여기서는 안되고 그 안드로이드내엣ㅓ만볼수잇는거지
    //그렇죠 터치를 컴퓨터로는 못보니까오. 아니면 ㅇ키 하나 눌럿을대 똑같이 작동되게 하면 확인하기 쉬울거에요아오키

    public void QuitBt()
    {
        Application.Quit();
    }
}
