using System;
using UnityEngine;

public class RefrectorController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //リフレクターを動かす
        transform.position += new Vector3(0, 0, -0.1f);

        //リフレクターを消す
        if (Math.Abs(transform.position.z) > 15) {
            Destroy(gameObject);
        }
    }
}
