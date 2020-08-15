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


    public void LoadNameForAIs()
    {
        GameObject obj = Instantiate(dontdestroy);
        string s = GetComponentInChildren<Text>().text;
        SaveLoad saveNload = new SaveLoad();
        obj.GetComponent<DontDestroyLoadName>().NameShip = saveNload.LoadAs(s);
        transform.parent.parent.parent.parent.GetChild(1).gameObject.SetActive(true);
        transform.parent.parent.parent.gameObject.SetActive(false);
    }


    public void LoadShipSolo()
    {
        GameObject obj = Instantiate(dontdestroy);
        string s = GetComponentInChildren<Text>().text;
        SaveLoad saveNload = new SaveLoad();
        obj.GetComponent<DontDestroyLoad>().fileValue = saveNload.LoadAs(s);
        SceneManager.LoadScene("AIsScene");
    }
}