using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene("Game");
    }

    public void Clickbut(Button but)
    {
        but.gameObject.SetActive(false);
    }
}
