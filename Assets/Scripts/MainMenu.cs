using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /*public void Start()
    {
        
        StartCoroutine(Countdown(3));
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {

            // display something...
            yield return new WaitForSeconds(1);
            count--;
        }

        // count down is finished...
        //PlayGame();
    }
    */
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        //Debug.Log("QUIT!");
        Application.Quit();
    }
}
