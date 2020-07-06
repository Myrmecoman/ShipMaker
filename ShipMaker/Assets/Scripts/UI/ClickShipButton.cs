using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ClickShipButton : MonoBehaviour
{
    public GameObject dontdestroy;


    public void LoadSelected()
    {
        GameObject obj = Instantiate(dontdestroy);
        string s = GetComponentInChildren<Text>().text;
        SaveLoad saveNload = new SaveLoad();
        obj.GetComponent<DontDestroyLoad>().fileValue = saveNload.LoadAs(s);
        SceneManager.LoadScene("Craft");
    }
}