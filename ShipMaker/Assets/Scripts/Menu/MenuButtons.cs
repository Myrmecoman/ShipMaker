﻿using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuButtons : MonoBehaviour
{
    public GameObject parentShips;
    public GameObject shipButton;


    public void Quit()
    {
        Application.Quit(0);
    }


    public void Craft()
    {
        SceneManager.LoadScene("Craft");
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
}