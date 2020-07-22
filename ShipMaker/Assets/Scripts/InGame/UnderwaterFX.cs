using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class UnderwaterFX : MonoBehaviour
{
    public PostProcessVolume volume;
    public PostProcessProfile normal;
    public PostProcessProfile underwater;


    void Update()
    {
        if (transform.position.y < 0)
        {
            if (volume.profile != underwater)
                volume.profile = underwater;
        }
        else if (volume.profile != normal)
        {
            volume.profile = normal;
        }
    }
}