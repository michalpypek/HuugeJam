using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    public int score;
    public Text scoreText;

    void Start()
    {
        Cursor.visible = true;
    }

	void Update()
    {
        scoreText.text = Time.time.ToString();
	}

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToGame()
    {
        SceneManager.LoadScene("Game");
    }
}
