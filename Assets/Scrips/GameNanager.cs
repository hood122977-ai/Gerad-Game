using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameNanager : MonoBehaviour
{
    Image titlelmage;

    public GameObject mainlmage;
    public Sprite gameOverSpr;
    public Sprite gameClearSpr;
    public GameObject panel;
    public GameObject restarButton;
    public GameObject nextButton;
    public GameObject over;
    public GameObject clear;

    public GameObject timeBar;
    public GameObject timeText;
    TimeController timeCnt;

    public GameObject scoreText;
    public static int totalScore;
    public int stageScore = 0;

    
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Invoke("Inactivelmage", 1.0f);

        panel.SetActive(false);

        timeCnt = GetComponent<TimeController>();
        if (timeCnt != null)
        {
            if (timeCnt.gameTime == 0.0f)
            {
                timeBar.SetActive(false);
            }
        }

        UpdateScore();

        over.SetActive(false);
        clear.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gameState == "gameclear")
        {
            mainlmage.SetActive(true);
            panel.SetActive(true);
            over.SetActive(false) ;
            clear.SetActive(true);
            Button bt = restarButton.GetComponent<Button>();
            bt.interactable = false;
            mainlmage.GetComponent<Image>().sprite = gameClearSpr;
            PlayerController.gameState = "gameend";

            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true;

                int time = (int)timeCnt.displayTime;
                totalScore += time * 10;
            }
            totalScore += stageScore;
            stageScore = 0;
            UpdateScore();
        }
        else if (PlayerController.gameState == "gameover")
        {
            mainlmage.SetActive(true);
            panel.SetActive(true);
            over.SetActive(true);
            clear.SetActive(false);

            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainlmage.GetComponent<Image>().sprite = gameOverSpr;
            PlayerController.gameState = "gameend";

            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true;
            }
        }
        else if (PlayerController.gameState == "playing")
        {
            //mainlmage.active = false;
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            PlayerController playerCnt = player.GetComponent<PlayerController>();
            if (timeCnt != null)
            {
                if (timeCnt.gameTime > 0.0f)
                {
                    int time = (int)timeCnt.displayTime;

                    timeText.GetComponent<Text>().text = time.ToString();

                    if (time == 0)
                    {
                        playerCnt.GameOver();
                    }
                }
                if (playerCnt.score != 0)
                {
                    stageScore += playerCnt.score;
                    playerCnt.score = 0;
                    UpdateScore();
                }
            }
            
        }
    }

    void Inactivelmage()
    {
        mainlmage.SetActive(false);
    }

    void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.GetComponent<Text>().text = score.ToString();
    }
}
