using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monstergenerator : MonoBehaviour
{

    public GameObject monsterprefab;
    float span = 1.0f;
    float delta = 0;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject go = Instantiate(monsterprefab);
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight, 0));

            float randomX = Random.Range(-screenBounds.x, screenBounds.x);// 화면 내부 랜덤 위치 설정.
            float randomY = Random.Range(-screenBounds.y, screenBounds.y);// 화면 내부 랜덤 위치 설정.
            Vector3 randomPlace;
            randomPlace.x = randomX;
            randomPlace.y = randomY;
            randomPlace.z = 0;
            float distanceToPlayer = Vector3.Distance(transform.position, randomPlace);
            if (distanceToPlayer < 5f)
                go.transform.position = new Vector3(randomX, randomY, 0);

        }
    }
}
