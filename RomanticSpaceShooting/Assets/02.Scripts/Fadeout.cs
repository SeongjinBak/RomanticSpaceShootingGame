using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fadeout : MonoBehaviour {
    SpriteRenderer rend;
    public float i;
    float firstColor;
    public AudioSource audioSource;
    public GameObject player;// fade 나올 때 총알 나타냄.
    // Use this for initialization

    void Start () {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("bull");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void DonghaSay()
    {
        audioSource.Stop();
        StartCoroutine(FadeOut());
 //       already.Already = true;
    }

    IEnumerator FadeOut()
    {
        player.SetActive(true);
        Debug.Log("fadein");
        for (float i = 1f; i >= 0; i -= 0.1f)
        {
            rend = this.gameObject.GetComponent<SpriteRenderer>();
            Color c = rend.color;
            c.a = i;
            rend.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
