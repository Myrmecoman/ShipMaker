using UnityEngine;
using UnityEngine.SceneManagement;


public class ClickShipButton : MonoBehaviour
{
    public GameObject dontdestroy;
    [HideInInspector] public string n;


    public void LoadSelected()
    {
        GameObject obj = Instantiate(dontdestroy);
        SaveLoad saveNload = new SaveLoad();
        obj.GetComponent<DontDestroyLoad>().fileValue = saveNload.LoadAs(n);
        SceneManager.LoadScene("Craft");
    }


    public void LoadNameForAIs()
    {
        GameObject obj = Instantiate(dontdestroy);
        SaveLoad SaveNLoad = new SaveLoad();
        obj.GetComponent<DontDestroyLoadName>().NameShip = SaveNLoad.LoadAsset(n);
        string idAI = n.Substring(0, 3);
        FindObjectOfType<PlayerInfos>().lastAiID = int.Parse(idAI);
        transform.parent.parent.parent.parent.GetChild(1).gameObject.SetActive(true);
        transform.parent.parent.parent.gameObject.SetActive(false);
    }


    public void LoadShipSolo()
    {
        GameObject obj = Instantiate(dontdestroy);
        SaveLoad saveNload = new SaveLoad();
        obj.GetComponent<DontDestroyLoad>().fileValue = saveNload.LoadAs(n);
        SceneManager.LoadScene("AIsScene");
    }
}