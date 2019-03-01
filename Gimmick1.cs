using UnityEngine;

public class Gimmick1 : MonoBehaviour {

    public GameObject redpointballPrefab, yellowpointballPrefab;
    public GameObject[] enemyPrefab;
    public static int Result = 0;

    private int red_or_yellow; //赤か黄色か決める
    private float SPAN = 1.0f, delta = 0; //PointBallが発生する間隔
    private bool flag; //ゲームが続行中かどうか

    // Use this for initialization
    private void Start () {

        flag = gameObject.GetComponent<GameManager>().flag;
        GameManager.time = 45.0f;
        GameManager.score = 0;

        for (int i = 0; i < 5; i++) {
            float tmp = Random.Range(-15.0f, 15.0f);
            Instantiate(enemyPrefab[i], new Vector3(-11.25f + 6 * i, 1.0f, tmp), Quaternion.Euler(0, 0, 0));
        }
    }
	
	// Update is called once per frame
	private void Update () {

        flag = gameObject.GetComponent<GameManager>().flag;

        if (flag) {
            //1秒ごとにボールを作る
            delta += Time.deltaTime;
            if (flag && delta > SPAN) {
                delta = 0;
                red_or_yellow = Random.Range(0, 4);
                CreatePointBall();
            }

            //得点の取得
            if (flag) Result = GameManager.score;
        }
    }

    //ボールの生産
    private void CreatePointBall() {
        var X = Random.Range(-15.0f, 15.0f);
        var Z = Random.Range(-15.0f, 15.0f);
        if (red_or_yellow == 3) {
            GameObject redpointball = Instantiate(redpointballPrefab,
                new Vector3(X, 0.5f, Z), Quaternion.identity) as GameObject;
        }
        else {
            GameObject yellowpointball = Instantiate(yellowpointballPrefab,
                new Vector3(X, 0.5f, Z), Quaternion.identity) as GameObject;
        }
    }
}
