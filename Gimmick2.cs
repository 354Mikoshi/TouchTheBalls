using System;
using UnityEngine;

public class Gimmick2 : MonoBehaviour {

    public GameObject redpointballPrefab, yellowpointballPrefab;
    public GameObject[] FloorPrefab;
    public static int Result = 0;

    private int red_or_yellow; //赤か黄色か決める
    private float SPAN = 1.0f, delta = 0; //PointBallが発生する間隔
    private bool flag;

    // Use this for initialization
    void Start () {

        flag = gameObject.GetComponent<GameManager>().flag;
        GameManager.time = 45.0f;
        GameManager.score = 0;

        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                float tmp = UnityEngine.Random.Range(0.0f, 10.0f);
                Instantiate(FloorPrefab[3 * i + j], new Vector3(-10 + 10 * i, tmp, -10 + 10 * j), Quaternion.identity);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

        flag = gameObject.GetComponent<GameManager>().flag;

        if (flag) {
            delta += Time.deltaTime;
            if (flag && delta > SPAN) {
                this.delta = 0;
                red_or_yellow = UnityEngine.Random.Range(0, 4);
                CreatePointBall();
            }
        }

        //得点の取得
        if (flag) Result = GameManager.score;
    }

    //ボールの生成
    void CreatePointBall() {
        var X = UnityEngine.Random.Range(-15.0f, 15.0f);
        var Z = UnityEngine.Random.Range(-15.0f, 15.0f);
        if (red_or_yellow == 3) {
            GameObject redpointball = Instantiate(redpointballPrefab,
                new Vector3(X, 13.0f, Z), Quaternion.identity) as GameObject;
        }
        else {
            GameObject yellowpointball = Instantiate(yellowpointballPrefab,
                new Vector3(X, 13.0f, Z), Quaternion.identity) as GameObject;
        }
    }
}
