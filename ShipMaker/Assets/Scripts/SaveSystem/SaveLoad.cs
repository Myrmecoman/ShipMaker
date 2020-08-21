using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Security.Cryptography;
using System;
using System.Text;


public class SaveLoad
{
    public void SaveAs(string name, string content)
    {
        string path = Application.persistentDataPath + "/" + name + ".chancla";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        string s = EncryptString(content, "65FVg93fdh879fggv87fgVuyi878qiaBfFTYU");
        formatter.Serialize(stream, s);
        stream.Close();
    }


    public string LoadAs(string name)
    {
        string path = Application.persistentDataPath + "/" + name + ".chancla";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        string str = formatter.Deserialize(stream) as string;
        string s = DecryptString(str, "65FVg93fdh879fggv87fgVuyi878qiaBfFTYU");
        stream.Close();
        return s;
    }


    public string LoadAsset(string name)
    {
        string path = Application.streamingAssetsPath + "/" + name + ".chancla";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        string str = formatter.Deserialize(stream) as string;
        string s = DecryptString(str, "65FVg93fdh879fggv87fgVuyi878qiaBfFTYU");
        stream.Close();
        return s;
    }


    public void SavePlayer(string content)
    {
        string path = Application.persistentDataPath + "/player.p";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        string s = EncryptString(content, "65FTiafggv8793VYU78qgf987uyi8FfdhBfgV");
        formatter.Serialize(stream, s);
        stream.Close();
    }


    public string LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.p";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        string str = formatter.Deserialize(stream) as string;
        string s = DecryptString(str, "65FTiafggv8793VYU78qgf987uyi8FfdhBfgV");
        stream.Close();
        return s;
    }


    private static string EncryptString(string plainText, string passPhrase)
    {
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        string initVector = "heaIl9gRupgF6lr8";
        int keysize = 256;

        byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
        byte[] keyBytes = password.GetBytes(keysize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();
        byte[] cipherTextBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        return Convert.ToBase64String(cipherTextBytes);
    }


    private static string DecryptString(string cipherText, string passPhrase)
    {
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        string initVector = "heaIl9gRupgF6lr8";
        int keysize = 256;

        byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
        byte[] keyBytes = password.GetBytes(keysize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];
        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
    }
}