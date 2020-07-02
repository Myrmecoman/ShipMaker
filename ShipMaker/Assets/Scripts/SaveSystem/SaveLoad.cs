using System.IO;
using UnityEngine;


public class SaveLoad
{
    public void SaveAs(string name, string content)
    {
        string path = Application.persistentDataPath + "/" + name;

        File.Create(path);
        TextWriter tw = new StreamWriter(path);
        tw.WriteLine(content);
        tw.Close();
    }


    public string LoadAs(string name)
    {
        string path = Application.persistentDataPath + "/" + name;
        return File.ReadAllText(path);
    }
}