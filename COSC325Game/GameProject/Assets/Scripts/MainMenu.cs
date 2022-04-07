using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //increments game scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void RestartGame()
    {
        //takes back to main menu and destroys game manager
        SceneManager.LoadScene(0);
        Destroy(GameObject.Find("GameManager"));
        Destroy(GameObject.Find("CharacterSelection"));
    }
}
