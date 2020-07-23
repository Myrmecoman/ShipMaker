using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class UnderwaterFX : MonoBehaviour
{
    public PostProcessVolume volume;
    public PostProcessProfile normal;
    public PostProcessProfile underwater;
    public Transform Depth;


    void Update()
    {
        if (transform.position.y <= 0)
        {
            if (volume.profile != underwater)
            {
                volume.profile = underwater;
                RenderSettings.fog = true;
            }
        }
        else if (volume.profile != normal)
        {
            volume.profile = normal;
            RenderSettings.fog = false;
        }

        Depth.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}