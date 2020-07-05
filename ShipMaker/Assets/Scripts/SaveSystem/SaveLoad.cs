using System.IO;
using UnityEngine;


public class SaveLoad
{
    public void SaveAs(string name, string content)
    {
        string path = Application.persistentDataPath + "/" + name + ".chancla";

        StreamWriter sw = File.CreateText(path);
        sw.Close();
        File.WriteAllText(path, content);
        Debug.Log("Saved as " + name);
    }


    public string LoadAs(string name)
    {
        string path = Application.persistentDataPath + "/" + name + ".chancla";
        string ans = File.ReadAllText(path);
        return ans;
    }
}