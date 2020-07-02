using UnityEngine;


public class InputCraft : MonoBehaviour
{
    public CraftCam cam;

    private Controls controls;


    #region Enable/Disable

    private void OnEnable()
    {
        controls.Enable();
    }
    void OnDisable()
    {
        controls.Disable();
    }

    #endregion


    void Awake()
    {
        controls = new Controls();

        // move
        controls.CraftCam.Movement.performed += _ => cam.WantMove = _.ReadValue<Vector2>();
        controls.CraftCam.Movement.canceled += _ => cam.WantMove = Vector2.zero;
        // look around
        controls.CraftCam.Look.performed += _ => cam.WantMouse = _.ReadValue<Vector2>();
        controls.CraftCam.Look.canceled += _ => cam.WantMouse = Vector2.zero;
        // click on a single frame
        controls.CraftCam.ClickFrame.performed += _ => cam.PressClick();
        // right click hold
        controls.CraftCam.RightClickHold.performed += _ => cam.RightClickHold = true;
        controls.CraftCam.RightClickHold.canceled += _ => cam.RightClickHold = false;
        // scroll
        controls.CraftCam.Scroll.performed += _ => cam.WantScroll = _.ReadValue<Vector2>();
        controls.CraftCam.Scroll.canceled += _ => cam.WantScroll = Vector2.zero;
    }
}