using UnityEngine;
using UnityEngine.SceneManagement;


public class TestUI : MonoBehaviour
{
    public GameObject dontdestroy;
    [HideInInspector] public string load;


    public void BackMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Menus");
    }


    public void BackCraft()
    {
        GameObject obj = Instantiate(dontdestroy);
        SaveLoad saveNload = new SaveLoad();
        obj.GetComponent<DontDestroyLoad>().fileValue = saveNload.LoadAs(load);
        SceneManager.LoadScene("Craft");
    }
}