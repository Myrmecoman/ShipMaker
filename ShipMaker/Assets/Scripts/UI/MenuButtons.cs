using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuButtons : MonoBehaviour
{
    public GameObject parentShips;
    public GameObject parentAIs;
    public GameObject shipButton;
    public GameObject dontdestroyNewCraftName;
    public InputField shipName;


    public void Quit()
    {
        Application.Quit(0);
    }


    public void Craft()
    {
        if (shipName.text.Length > 0)
        {
            GameObject obj = Instantiate(dontdestroyNewCraftName);
            obj.GetComponent<DontDestroyLoadName>().Name = shipName.text;
            SceneManager.LoadScene("Craft");
        }
    }


    public void DeleteAllShips()
    {
        foreach (string sFile in Directory.GetFiles(Application.persistentDataPath, "*.chancla"))
            File.Delete(sFile);

        UpdateListShips();
    }


    void Start()
    {
        UpdateListShips();
        UpdateListAIs();
    }


    private void UpdateListShips()
    {
        foreach (Transform child in parentShips.transform)
            Destroy(child.gameObject);

        string[] fileArray = Directory.GetFiles(Application.persistentDataPath, "*.chancla");
        foreach (string s in fileArray)
        {
            GameObject obj = Instantiate(shipButton, parentShips.transform);
            string s2 = s.Replace(Application.persistentDataPath + "\\", "");
            s2 = s2.Remove(s2.Length - 8);
            obj.GetComponentInChildren<Text>().text = s2;
        }
    }


    private void UpdateListAIs()
    {
        foreach (Transform child in parentAIs.transform)
            Destroy(child.gameObject);

        GameObject[] AllAIs = Resources.LoadAll("AIs") as GameObject[];
        if (AllAIs != null)
        {
            foreach (GameObject ahi in AllAIs)
            {
                GameObject obj = Instantiate(shipButton, parentAIs.transform);
                obj.GetComponentInChildren<Text>().text = ahi.name;
            }
        }
    }
}