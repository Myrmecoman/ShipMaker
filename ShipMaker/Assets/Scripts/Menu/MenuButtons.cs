using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuButtons : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit(0);
    }


    public void Craft()
    {
        SceneManager.LoadScene("Craft");
    }
}