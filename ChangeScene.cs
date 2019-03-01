using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour {

    public GameObject[] PointText = new GameObject[5];
    public GameObject SumText;
    public AudioClip DecideStageSound, RankingSound;
    public int[] Result;
    public int Sum;

    private AudioSource audioSource;

    // Use this for initialization
    void Start() {
        for (int i = 0; i < 5; i++) {
            Result[i] = 0;
            PointText[i].GetComponent<Text>().text = Result[i].ToString() + "点";
        }
        audioSource = this.gameObject.GetComponent<AudioSource>();
        Sum = 0;
    }

    // Update is called once per frame
    void Update() {
        Result[0] = Gimmick1.Result;
        Result[1] = Gimmick2.Result;
        Result[2] = Gimmick3.Result;
        Result[3] = Gimmick4.Result;
        Result[4] = Gimmick5.Result;

        //ステージごとの最高得点を記録
        for (int i = 0; i < 5; i++) {
            Result[i] = Math.Max(Result[i], PlayerPrefs.GetInt("MAX" + i.ToString(), 0));
            PlayerPrefs.SetInt("MAX" + i.ToString(), Result[i]);
            PointText[i].GetComponent<Text>().text = Result[i].ToString();
        }

        //合計得点を記録
        Sum = Result[0] + Result[1] + Result[2] + Result[3] + Result[4];
        SumText.GetComponent<Text>().text = Sum.ToString();
    }

    //Stage1~5のボタン処理
    public void SelectStage1() {
        audioSource.PlayOneShot(DecideStageSound);
        StartCoroutine(DelayMethod(2.0f, () =>
        {
            SceneManager.LoadScene("Stage1");
        }));
    }

    public void SelectStage2() {
        audioSource.PlayOneShot(DecideStageSound);
        StartCoroutine(DelayMethod(2.0f, () =>
        {
            SceneManager.LoadScene("Stage2");
        }));
    }

    public void SelectStage3() {
        audioSource.PlayOneShot(DecideStageSound);
        StartCoroutine(DelayMethod(2.0f, () =>
        {
            SceneManager.LoadScene("Stage3");
        }));
    }

    public void SelectStage4() {
        audioSource.PlayOneShot(DecideStageSound);
        StartCoroutine(DelayMethod(2.0f, () =>
        {
            SceneManager.LoadScene("Stage4");
        }));
    }

    public void SelectStage5() {
        audioSource.PlayOneShot(DecideStageSound);
        StartCoroutine(DelayMethod(2.0f, () =>
        {
            SceneManager.LoadScene("Stage5");
        }));
    }

    //ランキングを呼び出す
    public void Callranking() {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(Sum);
        audioSource.PlayOneShot(RankingSound);
    }

    //遅延処理
    private IEnumerator DelayMethod(float waitTime, Action action) {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}
