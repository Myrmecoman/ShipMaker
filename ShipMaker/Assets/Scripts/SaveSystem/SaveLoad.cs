using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class SaveLoad
{
    public void SaveAs(string name, string content)
    {
        string path = Application.persistentDataPath + "/" + name + ".chancla";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, content);
        stream.Close();
    }


    public string LoadAs(string name)
    {
        string path = Application.persistentDataPath + "/" + name + ".chancla";
        string s = "";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        s = formatter.Deserialize(stream) as string;
        stream.Close();
        return s;
    }


    public string LoadAsset(string name)
    {
        string path = Application.streamingAssetsPath + "/" + name + ".txt";
        string s = "";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        s = formatter.Deserialize(stream) as string;
        stream.Close();
        return s;
    }
}