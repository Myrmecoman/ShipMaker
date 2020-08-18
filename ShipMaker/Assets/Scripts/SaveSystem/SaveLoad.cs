using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Security.Cryptography;


public class SaveLoad
{
    public void SaveAs(string name, string content)
    {
        string path = Application.persistentDataPath + "/" + name + ".chancla";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, content);
        stream.Close();

        EncryptFile(path, path + "i");
    }


    public string LoadAs(string name)
    {
        string path = Application.persistentDataPath + "/" + name + ".chancla";

        DecryptFile(path, path);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        string s = formatter.Deserialize(stream) as string;
        stream.Close();

        EncryptFile(path, path);

        return s;
    }


    public string LoadAsset(string name)
    {
        string path = Application.streamingAssetsPath + "/" + name + ".chancla";

        DecryptFile(path, path);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        string s = formatter.Deserialize(stream) as string;
        stream.Close();

        EncryptFile(path, path);

        return s;
    }


    private void EncryptFile(string inputFile, string outputFile)
    {
        //try
        //{
            byte[] key = new byte[] { 103, 9, 84, 234, 104, 93, 29, 2, 192, 140, 28, 182, 249, 189, 48, 59, 84, 234, 104, 93, 29, 2, 192, 140 };
            byte[] IV = new byte[] { 103, 9, 84, 234, 104, 93, 29, 2, 192, 140, 28, 93, 29, 2, 192, 140 };

            string cryptFile = outputFile;
            FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

            RijndaelManaged RMCrypto = new RijndaelManaged();

            CryptoStream cs = new CryptoStream(fsCrypt,
                RMCrypto.CreateEncryptor(key, IV),
                CryptoStreamMode.Write);
            
            FileStream fsIn = new FileStream(inputFile, FileMode.Open);

            int data;
            while ((data = fsIn.ReadByte()) != -1)
                cs.WriteByte((byte)data);


            fsIn.Close();
            cs.Close();
            fsCrypt.Close();
        //}
        //catch
        //{
        //    throw new ArgumentNullException("error");
        //}
    }


    private void DecryptFile(string inputFile, string outputFile)
    {
        byte[] key = new byte[] { 103, 9, 84, 234, 104, 93, 29, 2, 192, 140, 28, 182, 249, 189, 48, 59, 84, 234, 104, 93, 29, 2, 192, 140 };
        byte[] IV = new byte[] { 103, 9, 84, 234, 104, 93, 29, 2, 192, 140, 28, 93, 29, 2, 192, 140 };

        FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

        RijndaelManaged RMCrypto = new RijndaelManaged();

        CryptoStream cs = new CryptoStream(fsCrypt,
            RMCrypto.CreateDecryptor(key, IV),
            CryptoStreamMode.Read);

        FileStream fsOut = new FileStream(outputFile, FileMode.Create);

        int data;
        while ((data = cs.ReadByte()) != -1)
            fsOut.WriteByte((byte)data);

        fsOut.Close();
        cs.Close();
        fsCrypt.Close();
    }
}