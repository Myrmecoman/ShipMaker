using UnityEngine;


public class InputShip : MonoBehaviour
{
    public ShipController ship;

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
        controls.ShipControl.Movement.performed += _ => ship.WantMove = _.ReadValue<Vector2>();
        controls.ShipControl.Movement.canceled += _ => ship.WantMove = Vector2.zero;
        // move but single frame
        controls.ShipControl.Movement.performed += _ => ship.MoveInput(_.ReadValue<Vector2>());
        // look around
        controls.ShipControl.Look.performed += _ => ship.WantMouse = _.ReadValue<Vector2>();
        controls.ShipControl.Look.canceled += _ => ship.WantMouse = Vector2.zero;
        // left click hold
        controls.ShipControl.LeftClickHold.performed += _ => ship.LeftClickHold = true;
        controls.ShipControl.LeftClickHold.canceled += _ => ship.LeftClickHold = false;
        // right click hold
        controls.ShipControl.RightClickHold.performed += _ => ship.RightClickHold = true;
        controls.ShipControl.RightClickHold.canceled += _ => ship.RightClickHold = false;
        // scroll
        controls.ShipControl.Scroll.performed += _ => ship.WantScroll = _.ReadValue<Vector2>();
        controls.ShipControl.Scroll.canceled += _ => ship.WantScroll = Vector2.zero;
        // escape key
        controls.ShipControl.Escape.performed += _ => ship.PressEscape();
    }
}