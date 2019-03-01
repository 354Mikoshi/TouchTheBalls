using UnityEngine;

public class FloorController : MonoBehaviour {

    private bool flag;
    private int UpOrDown;

	// Use this for initialization
	void Start () {
        flag = GameObject.Find("GameManager").GetComponent<GameManager>().flag;
        UpOrDown = Random.Range(0, 2);
	}
	
	// Update is called once per frame
	void Update () {

        flag = GameObject.Find("GameManager").GetComponent<GameManager>().flag;

        if (flag) {
            if (UpOrDown == 1) transform.position += new Vector3(0, 0.03f, 0);
            else transform.position -= new Vector3(0, 0.03f, 0);

            if (transform.position.y > 10) UpOrDown = 0;
            else if (transform.position.y < 0) UpOrDown = 1;
        }
    }
}
