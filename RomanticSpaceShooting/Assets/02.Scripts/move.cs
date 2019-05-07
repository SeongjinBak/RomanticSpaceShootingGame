using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {


    float Speed = 8;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))  
        {
            transform.Translate(Vector2.left * Speed * Time.deltaTime);
        }
    }
}
