using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public int SCORE_INIT = 500;
    private static int click = 0;
    private static int drag = 0;
    private static int score;

    public bool isEnd = false;
    public bool isGameOver = false;

    public Text clickCount;
    public Text dragCount;
    public Text scoreText;
    public Text highscoreText;
    public RawImage[] stars;

    public Texture star_empty;
    public Texture star_full;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Highscore") != 0)
        {
            highscoreText.text = "High Score  : " + PlayerPrefs.GetInt("Highscore");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnd && !isGameOver)
        {
            clickCount.text = "Click Count : " + click;
            dragCount.text = "Drag Count  : " + drag;
            score = SCORE_INIT - (click * 10 + drag);
            scoreText.text = "Score       : " + score;

            if (score <= 0)
            {
                isGameOver = true;
            }
        }
    }

    public static void increaseClick()
    {
        click += 1;
    }

    public static void increaseDrag()
    {
        drag += 1;
    }

    public void Ending()
    {
        int highscore;
        highscore = PlayerPrefs.GetInt("Highscore");
        Debug.Log(highscore);
        isEnd = true;

        stars[0].texture = score > 0 ? star_full : star_empty;
        stars[1].texture = score > 100 ? star_full : star_empty;
        stars[2].texture = score > 200 ? star_full : star_empty;

        if (highscore == 0 || score > highscore)
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }

}
