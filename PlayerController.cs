using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public AudioClip GetRedSound, GetYellowSound, TouchEnemySound, FallSound;

    private GameObject gameManager;
    private AudioSource audioSource;

    private float FlipForce = 50.0f; //敵に弾かれる力の大きさ
    private int FallPoint = -500; //場外に落ちたときにひかれる点数
    private bool flag;

	void Start () {
        gameManager = GameObject.Find("GameManager");
        audioSource = this.gameObject.GetComponent<AudioSource>();
        flag = gameManager.GetComponent<GameManager>().flag;
	}
	
	void Update () {

        flag = gameManager.GetComponent<GameManager>().flag;

        if (transform.position.y <= - 3 ||
            Math.Abs(transform.position.x) > 50 || Math.Abs(transform.position.z) > 50) {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = new Vector3(0, 10.0f, 0);
            gameManager.GetComponent<GameManager>().AddScore(FallPoint);
            if(gameManager.GetComponent<GameManager>().flag) audioSource.PlayOneShot(FallSound);
        }
	}

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "RedPointBall") {
            col.gameObject.GetComponent<PointBallController>().GetRedPointBall();
            if (gameManager.GetComponent<GameManager>().flag) audioSource.PlayOneShot(GetRedSound);
        }
        else if (col.gameObject.tag == "YellowPointBall") {
            col.gameObject.GetComponent<PointBallController>().GetYellowPointBall();
            if (gameManager.GetComponent<GameManager>().flag) audioSource.PlayOneShot(GetYellowSound);
        }
        else if (col.gameObject.tag == "Enemy") {
            if (flag) {
                audioSource.PlayOneShot(TouchEnemySound);
                col.gameObject.GetComponent<EnemyController>().TouchEnemy();
            }
        }
    }
}
