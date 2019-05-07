using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour {

    public GameObject PlayerMissile;
    public Transform MissileLocation;
    public float FireDelay;
    private bool FireState;

	void Start () {
        FireState = true;
	}
	
	
	void Update () {
        PlayerFire();		
	}

    private void PlayerFire()
    {
        if (FireState)
        {
            if ( Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(FireCycleControl());

                Instantiate(PlayerMissile, MissileLocation.position, MissileLocation.rotation);
            }
        }
    }
    IEnumerator FireCycleControl()
    {
        FireState = false;

        yield return new WaitForSeconds(FireDelay);
        FireState = true;
    }
}


