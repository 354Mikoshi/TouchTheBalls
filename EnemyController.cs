using UnityEngine;

public class EnemyController : MonoBehaviour {

    private GameObject target, gameManager;

    private const int DAMAGE = -300;
    private float FlipForce = 200.0f;
    private int LeftOrRight;
    private bool flag;

    private void Start () {
        target = GameObject.Find("ThirdPersonController");
        gameManager = GameObject.Find("GameManager");
        flag = gameManager.GetComponent<GameManager>().flag;
    }
	
    private void Update () {

        flag = gameManager.GetComponent<GameManager>().flag;

        if (flag) {
            if (LeftOrRight == 1) transform.position += new Vector3(0, 0, 0.1f);
            else transform.position -= new Vector3(0, 0, 0.1f);

            if (transform.position.z > 14) LeftOrRight = 0;
            else if (transform.position.z < -14) LeftOrRight = 1;
        }
    }

    public void TouchEnemy() {
        var direction = Vector3.Normalize(target.transform.position - transform.position) * 100.0f;
        direction.y = 2.0f;
        target.gameObject.GetComponent<Rigidbody>().AddForce(FlipForce * direction);
    }
}
