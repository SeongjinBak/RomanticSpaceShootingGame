using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bull : MonoBehaviour {
    public float speed = 1f;
    public float end; // 총알이 이리로 가면
    public float start; // 이 곳에서 새로 만들어짐.
    public bool go; // 버튼 누르면 true로 변하면서 총알이 가게 됨.

    public hp AA;
	// Use this for initialization
	void Start () {
        go = false;
        AA = GameObject.Find("Heart").GetComponent<hp>();
    }
	
	// Update is called once per frame
	void Update () {

        if (go == true)
        {
            transform.Translate(0, 3 * speed * Time.deltaTime, 0);

            if (transform.position.y >= end) EndPosition();
        }
    }

    public void Go()
    {
        go = true;
        AA.HpDelete();
    }

    public void EndPosition()
    {
        go = false;
        transform.position = new Vector3(0,-3,0);
    }
}
