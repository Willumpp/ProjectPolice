using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public bool resetScores = false;
    public int totalScore = 0;
    public Text score1;
    public Text score2;
    public Text score3;
    public Text score4;
    public Text score5;
    public Text bestTimeScore;
    public Text highScore;
    public Text scoreText;
    private float time;
    List<float> scores;

    void OrganiseScores(string hours, string minutes, string seconds)
    {

        //Update best time
        if (time > PlayerPrefs.GetFloat("BestTimeNum", 0f))
        {
            PlayerPrefs.SetFloat("BestTimeNum", time);
            PlayerPrefs.SetString("BestTimeString", $"{hours}:{minutes}:{seconds}");
        }

        //Update highscore
        if (totalScore > PlayerPrefs.GetFloat("HighscoreNum", 0f))
        {
            PlayerPrefs.SetFloat("HighscoreNum", totalScore);
            PlayerPrefs.SetString("HighscoreString", $"{totalScore}");
        }

        PlayerPrefs.SetString("Score5", PlayerPrefs.GetString("Score4", "0"));
        PlayerPrefs.SetString("Score4", PlayerPrefs.GetString("Score3", "0"));
        PlayerPrefs.SetString("Score3", PlayerPrefs.GetString("Score2", "0"));
        PlayerPrefs.SetString("Score2", PlayerPrefs.GetString("Score1", "0"));
        PlayerPrefs.SetString("Score1", totalScore.ToString());
        //PlayerPrefs.SetString("HighscoreString", $"{totalScore}");
    }

    public void UpdateScores()
    {
        //Get the finishing time
        time = Timer.endTime;

        string hours = ((int)time / 3600).ToString(); //Convert seconds to hours
        string minutes = ((int)time / 60).ToString(); //Convert seconds to minutes
        string seconds = (time % 60).ToString("f2"); //Get milliseconds

        //Shange Score Values
        OrganiseScores(hours, minutes, seconds);

        //Update other scores
        bestTimeScore.text = "Best Time :-   " + PlayerPrefs.GetString("BestTimeString",$"{hours}:{minutes}:{seconds}"); //Update best time text
        highScore.text = "Highscore :-   " + PlayerPrefs.GetString("HighscoreString", $"{totalScore}"); //Update highscore text
        score5.text = "Score 5:-   " + PlayerPrefs.GetString("Score5","0");
        score4.text = "Score 4:-   " + PlayerPrefs.GetString("Score4", "0");
        score3.text = "Score 3:-   " + PlayerPrefs.GetString("Score3", "0");
        score2.text = "Score 2:-   " + PlayerPrefs.GetString("Score2", "0");
        score1.text = "Score 1:-   " + PlayerPrefs.GetString("Score1", "0");
        //score1.text = "Score 1:-   " + $"{hours}:{minutes}:{seconds}";

        //Increase overall score
        PlayerPrefs.SetInt("OverallScore", PlayerPrefs.GetInt("OverallScore", 0) + totalScore);
    }

    public void ChangeScore(int num) //Increases from time, PlatformCreator, EnemyCarHealth
    {
        totalScore += num;
        scoreText.text = totalScore.ToString();
    }

    void Start()
    {
        bestTimeScore.text = PlayerPrefs.GetString("BestTimeString", "0:0:0"); //Update best time text
        highScore.text = PlayerPrefs.GetString("HighscoreString", "0"); //Update highscore text
    }

    void Update()
    {
        if (resetScores == true)
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
