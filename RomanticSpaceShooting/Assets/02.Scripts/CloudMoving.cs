using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMoving : MonoBehaviour {

    private SpriteRenderer cloudRenderer;
    public List<Sprite> cloudList;
    private WaitForSeconds ws = new WaitForSeconds(0.3f);
    private Transform cloudTransform;
    private Vector3 originPos;
    // Use this for initialization
    void Start () {
        cloudRenderer = GetComponent<SpriteRenderer>();
        cloudTransform = GetComponent<Transform>();
        StartCoroutine(CloudMove());
        originPos = cloudTransform.position;
    }
    void Update()
    {
        cloudTransform.Translate(Vector2.right*Time.deltaTime);
        if(cloudTransform.position.x >= 7.6f)
        {
            cloudTransform.position = originPos;
        }
    }
    IEnumerator CloudMove()
    {
        int i = 0;
      //  bool updown = false;
        while (true)
        {
            cloudRenderer.sprite = cloudList[i++];
            if (i >= 3) i = 0;
            yield return ws;
          //  if(updown) cloudTransform.Translate(Vector2.up * Time.deltaTime);
           // else cloudTransform.Translate(Vector2.down * Time.deltaTime);
          //  updown = !updown;
        }
    }
}
