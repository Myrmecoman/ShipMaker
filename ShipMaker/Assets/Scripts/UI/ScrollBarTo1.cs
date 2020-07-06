using UnityEngine;
using UnityEngine.UI;


public class ScrollBarTo1 : MonoBehaviour
{
    void Start()
    {
        GetComponent<Scrollbar>().value = 1;
    }
}