using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("next level");
    } 
    public void Restart()
    {
        SceneManager.LoadScene("màn 1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
