using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float timeStarted;
    public bool isPlay;
    public static GameManager instance = null;

    public PlayerScript playerScript;
    public EnemyManager enemyManager;
    public DoorScript door;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SetupManager();
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void SetupManager()
    {
        IsPlaySetup();
        timeStarted = Time.time;

        return;
    }

    void IsPlaySetup()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        door = GameObject.Find("Door").GetComponent<DoorScript>();
    }

    public void FindPlayer()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    public void WinGame()
    {
        StartCoroutine("WinEnum");
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Win");
    }

    IEnumerator WinEnum()
    {
        yield return new WaitForSeconds(2f);
        EndGame();
    }

    public void GameOver()
    {
        EndGame();
    }
}
