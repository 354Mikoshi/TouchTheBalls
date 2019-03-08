using UnityEngine;

public class Gimmick5 : MonoBehaviour {

    public GameObject RefrectorPrefab, redpointballPrefab, yellowpointballPrefab;
    public static int Result = 0;

    private GameObject RotatingCube;
    private int red_or_yellow; //赤か黄色か決める
    private float BallSpan = 1.0f, RefrectorSpan = 3.0f; //PointBallが発生する間隔,planeが消える間
    private float balldelta = 0, refrectordelta = 1.0f;
    private bool flag;

    // Use this for initialization
    private void Start() {
        RotatingCube = GameObject.Find("RotatingCube");
        flag = GameObject.Find("GameManager").GetComponent<GameManager>().flag;
        GameManager.time = 45.0f;
        GameManager.score = 0;
    }

    // Update is called once per frame
    private void Update() {

        flag = gameObject.GetComponent<GameManager>().flag;

        if (flag) {
            //ボールの生産
            balldelta += Time.deltaTime;
            if (flag && balldelta > BallSpan) {
                balldelta = 0;
                red_or_yellow = Random.Range(0, 4);
                CreatePointBall();
            }

            //リフレクターの生産
            refrectordelta += Time.deltaTime;
            if (flag && refrectordelta > RefrectorSpan) {
                refrectordelta = 0;
                CreateRefrector();
            }

            //キューブを回転させる
            RotatingCube.transform.Rotate(new Vector3(0, 0.8f, 0));

            //得点の取得
            Result = GameManager.score;
        }
    }

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

    private void CreateRefrector() {
        Vector3[] AppearPos = new Vector3[] {
                new Vector3(-11.25f, 2.0f, 15.0f),
                new Vector3(-3.75f, 2.0f, 15.0f),
                new Vector3(3.75f, 2.0f, 15.0f),
                new Vector3(11.25f, 2.0f, 15.0f),
            };
        int tmp = Random.Range(0, 4);
        GameObject Refrector = Instantiate(RefrectorPrefab, AppearPos[tmp], Quaternion.Euler(0, 0, 0));
    }
}
