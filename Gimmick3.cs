using System;
using UnityEngine;

public class Gimmick3 : MonoBehaviour {

    public GameObject redpointballPrefab, yellowpointballPrefab;
    public static int Result = 0;

    private int red_or_yellow; //赤か黄色か決める
    private float SPAN = 1.0f, delta = 0; //PointBallが発生する間隔
    private bool flag;

    // Use this for initialization
    private void Start () {
        flag = gameObject.GetComponent<GameManager>().flag;
        GameManager.time = 45.0f;
        GameManager.score = 0;
    }

    // Update is called once per frame
    private void Update () {

        flag = gameObject.GetComponent<GameManager>().flag;

        if (flag) {
            delta += Time.deltaTime;
            if (flag && delta > SPAN) {
                delta = 0;
                red_or_yellow = UnityEngine.Random.Range(0, 4);
                CreatePointBall();
            }
        }

        //得点の取得
        if (flag) Result = GameManager.score;
    }

    private void CreatePointBall() {
        int angleY = UnityEngine.Random.Range(-180, 181);
        if (red_or_yellow == 3) {
            GameObject redpointball = Instantiate(redpointballPrefab,
                new Vector3(0, 0.5f, 0), Quaternion.identity) as GameObject;
            var direction = Quaternion.Euler(new Vector3(0, angleY, 0)) * new Vector3(1, 0, 0);

            redpointball.GetComponent<Rigidbody>().AddForce(direction * 150);
        }
        else {
            GameObject redpointball = Instantiate(yellowpointballPrefab,
               new Vector3(0, 0.5f, 0), Quaternion.identity) as GameObject;
            var direction = Quaternion.Euler(new Vector3(0, angleY, 0)) * new Vector3(1, 0, 0);

            redpointball.GetComponent<Rigidbody>().AddForce(direction * 150);
        }
    }
}
