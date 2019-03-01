using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject textScoreNumber, timeText, timeupText, Countdowntext;
    public AudioClip TimeUpSound, ThreeTwoOneSound, GoSound;

    public bool flag; //音を鳴らすか,ボールを生産するか,敵が動くか
    public static int score = 0; //現在の点数
    public static float time = 45.0f; //制限時間

    private int displayscore = 0; //表示用スコア
    private AudioSource audioSource;

    void Start() {
        flag = false;
        StartCoroutine(CountdownCoroutine());

        RefreshScoreText();
        audioSource = gameObject.GetComponent<AudioSource>();

        Invoke("MoveToTitle", 53.0f); //ゲーム終了5秒後にタイトル画面に戻る

        //Go表示の瞬間にflagをtrueにする
        StartCoroutine(DelayMethod(3.0f, () =>
        {
            flag = true;
        }));
        
    }

    void Update() {

        //表示させるスコアが10刻みで変動していくようにする
        if (score > displayscore) {
            displayscore += 10;
            if (displayscore > score) displayscore = score;
        }
        if (score < displayscore) {
            displayscore -= 10;
            if (displayscore < score) displayscore = score;
        }
        RefreshScoreText();

        //得点が負にならないようにする
        if (score < 0) score = 0;

        //カウントダウン処理
        if (flag) {
            time -= Time.deltaTime;
            if (time < 0) {
                time = 0;
                if (flag) TimeUp();
                flag = false;
            }
        }

        //スコアテキストの更新
        timeText.GetComponent<Text>().text = "Remaining Time: " +  time.ToString("F1");

        //tキーを押したらタイトル画面に戻る
        if (Input.GetKeyDown("t")) {
            MoveToTitle();
        }
    }

    //スコアテキストの更新
    private void RefreshScoreText() {
        textScoreNumber.GetComponent<Text>().text = "Score: " + displayscore.ToString();
    }

    //スコア加算
    public void AddScore(int val) {
        if(flag) score += val;
    }

    //時間切れ処理
    void TimeUp() {
        timeupText.SetActive(true);
        audioSource.PlayOneShot(TimeUpSound);
    }

    //タイトル画面に戻る
    void MoveToTitle() {
        SceneManager.LoadScene("StartScene");
    }

    //ゲームスタート時のカウントダウン処理
    private IEnumerator CountdownCoroutine() {
        Countdowntext.gameObject.SetActive(true);

        Countdowntext.GetComponent<Text>().text = "3";
        yield return new WaitForSeconds(1.0f);

        Countdowntext.GetComponent<Text>().text = "2";
        audioSource.PlayOneShot(ThreeTwoOneSound);
        yield return new WaitForSeconds(1.0f);

        Countdowntext.GetComponent<Text>().text = "1";
        audioSource.PlayOneShot(ThreeTwoOneSound);
        yield return new WaitForSeconds(1.0f);

        Countdowntext.GetComponent<Text>().text = "GO!";
        audioSource.PlayOneShot(GoSound);
        yield return new WaitForSeconds(1.0f);

        Countdowntext.GetComponent<Text>().text = "";
        Countdowntext.GetComponent<Text>().gameObject.SetActive(false);

    }

    //遅延処理
    private IEnumerator DelayMethod(float waitTime, Action action) {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}
