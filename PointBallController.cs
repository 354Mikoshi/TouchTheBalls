using UnityEngine;

public class PointBallController : MonoBehaviour {

    //オブジェクト参照
    private GameObject gameManager; //ゲームマネージャー
    private AudioSource audiosource;

    private const int RED_POINTBALL = 500;
    private const int YELLOW_POINTBALL = 100;

    private void Start () {
        gameManager = GameObject.Find("GameManager");
        audiosource = this.gameObject.GetComponent<AudioSource>();
    }
	
    private void Update () {
        //ステージ上から落ちたボールを消す
        if (transform.position.y < - 3) {
            Destroy(gameObject);
        }
    }

    //RedPointBall入手処理
    public void GetRedPointBall() {
        gameManager.GetComponent<GameManager>().AddScore(RED_POINTBALL);
        if(gameManager.GetComponent<GameManager>().flag) Destroy(this.gameObject);
    }

    //YellowPointBall入手処理
    public void GetYellowPointBall() {
        gameManager.GetComponent<GameManager>().AddScore(YELLOW_POINTBALL);
        if(gameManager.GetComponent<GameManager>().flag) Destroy(this.gameObject);
    }
}
