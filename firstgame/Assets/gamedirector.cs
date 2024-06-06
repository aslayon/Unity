using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // SceneManager를 사용하기 위해 추가

public class gamedirector : MonoBehaviour
{
    GameObject hpGauge;

    void Start()
    {
        this.hpGauge = GameObject.Find("hpguage");
    }

    public void DecreaseHp()
    {
        this.hpGauge.GetComponent<Image>().fillAmount -= 0.1f;

        // hpGauge의 fillAmount가 0이 되면 게임 재시작
        if (this.hpGauge.GetComponent<Image>().fillAmount <= 0)
        {
            RestartGame();
        }
    }

    void RestartGame()
    {
        // 현재 활성화된 씬을 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
