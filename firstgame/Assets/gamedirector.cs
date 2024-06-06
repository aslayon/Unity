using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // SceneManager�� ����ϱ� ���� �߰�

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

        // hpGauge�� fillAmount�� 0�� �Ǹ� ���� �����
        if (this.hpGauge.GetComponent<Image>().fillAmount <= 0)
        {
            RestartGame();
        }
    }

    void RestartGame()
    {
        // ���� Ȱ��ȭ�� ���� �ٽ� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
