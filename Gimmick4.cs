using UnityEngine;
using System;

public class Gimmick4 : MonoBehaviour {

    public GameObject redpointballPrefab, yellowpointballPrefab;
    public GameObject[] plane;
    public Material GreenColor, WaterColor;
    public static int Result = 0;

    private int red_or_yellow; //赤か黄色か決める
    private float BallSpan = 1.0f, PlaneSpan = 1.0f; //PointBallが発生する間隔,planeが消える間隔
    private float balldelta = 0, planedelta = 0;
    private bool flag;

    // Use this for initialization
    private void Start() {
        flag = GameObject.Find("GameManager").GetComponent<GameManager>().flag;
        GameManager.time = 45.0f;
        GameManager.score = 0;
    }

    // Update is called once per frame
    private void Update() {

        flag = GameObject.Find("GameManager").GetComponent<GameManager>().flag;

        
        //ボールの生産
        if (flag) {
            balldelta += Time.deltaTime;
            if (flag && balldelta > BallSpan) {
                balldelta = 0;
                red_or_yellow = UnityEngine.Random.Range(0, 4);
                CreatePointBall();
            }


            //床の縮小をランダムに開始
            planedelta += Time.deltaTime;
            if (flag && planedelta > PlaneSpan) {
                planedelta = 0;
                int pos = UnityEngine.Random.Range(0, 9);
                plane[pos].GetComponent<MeshRenderer>().material.color = GreenColor.color;
                plane[pos].transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }

            //床の復活
            foreach (var p in plane) {
                if (p.transform.localScale == new Vector3(0, 0, 0)) {
                    p.transform.localScale = new Vector3(1, 1, 1);
                    p.GetComponent<MeshRenderer>().material.color = WaterColor.color;
                }
            }

            //床を縮小し続ける、ある程度小さくなった床を消す
            foreach (var p in plane) {
                if (p.transform.localScale.magnitude <= 1.732f) {
                    p.transform.localScale -= new Vector3(1, 1, 1) * 0.01f;
                }
                else if (p.transform.localScale.magnitude < 0.001f) {
                    p.transform.localScale = new Vector3(0, 0, 0);
                }
            }

            //得点の取得
            Result = GameManager.score;
        }
    }

    void CreatePointBall() {
        var X = UnityEngine.Random.Range(-15, 15);
        var Z = UnityEngine.Random.Range(-15, 15);
        if (red_or_yellow == 3) {
            Instantiate(redpointballPrefab, new Vector3(X, 0.5f, Z), Quaternion.identity);
        }
        else {
            Instantiate(yellowpointballPrefab, new Vector3(X, 0.5f, Z), Quaternion.identity);
        }
    }

    
}
