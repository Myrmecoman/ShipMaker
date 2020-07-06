// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Inputs/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""CraftCam"",
            ""id"": ""d96259e6-8ae6-4b89-a7c2-d0d5423b35c5"",
            ""actions"": [
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""70a875d8-100b-461d-be71-7854b7fd2686"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""aec14616-d785-4f1d-9af2-575245cbba61"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Scroll"",
                    ""type"": ""Value"",
                    ""id"": ""5ecbe196-d4c5-4dfb-81fa-503a5ea779ed"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""07984fca-45fc-4d91-a802-306ed715408c"",
                    ""expectedControlType"": ""Dpad"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ClickFrame"",
                    ""type"": ""Button"",
                    ""id"": ""21a4576f-852b-49cc-ba8b-f269639ca7aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ClickHold"",
                    ""type"": ""Button"",
                    ""id"": ""6e2851fe-0652-4e2f-bd5e-cb3c7d516e17"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Alt"",
                    ""type"": ""Button"",
                    ""id"": ""b76f43fb-c9a6-4f94-a9a9-278117f1d461"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""RightClickHold"",
                    ""type"": ""Button"",
                    ""id"": ""4cbe5ca9-d15c-4b3f-a5eb-cc07e11ae40d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a65f3558-d310-47f2-bc48-54f87d140397"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b9e5692-6e29-49d9-8611-cc2fc285828e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""ClickFrame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5cf52a3c-37fc-4136-abe5-2096fe2a60ea"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""ClickHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keys"",
                    ""id"": ""93fb6a24-badd-4bf7-a1b2-8108c11a31e2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""234f5848-67eb-4467-982e-91456f62879e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7f601bb0-e0b1-4555-920b-9d53cde439b3"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bfd97733-2bf6-46a2-91fd-cd6f93d1baf8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bb0b363e-1127-46d7-8106-28e9b6304e8d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""7ea1c5e1-8294-4736-9831-64e6b8fa55d0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a6121fed-47eb-4dcd-85a0-06ea73c3a183"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""96782dd7-4124-4bfd-b33b-48fff7f9d857"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""dccd4798-e3c7-4b68-9387-8ce6aeb07fdf"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7df4706e-d6fa-48e5-b105-f52a8d06e59e"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d61895a7-4100-443a-9ce5-540e437835a7"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a47b573-b48e-42f2-b28e-73cb37950b10"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b2ecbd40-1d4a-43a9-9a7c-fe4013aa779a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""RightClickHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e17382da-6e3d-488b-b4e2-ea12f871ef20"",
                    ""path"": ""<Keyboard>/leftAlt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Alt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ShipControl"",
            ""id"": ""70cc7ab9-dbe3-459a-b119-1d0cc326512c"",
            ""actions"": [
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""3e0a21fd-8820-4262-9d2e-c6051c59c0b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""465c79f9-8bc8-4b75-a090-b186759d8c84"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Scroll"",
                    ""type"": ""Value"",
                    ""id"": ""1006d596-3c35-4df8-9ec9-ae0b989cb21f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""236f3738-e5d8-49e5-b3b2-bb6113754d9a"",
                    ""expectedControlType"": ""Dpad"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftClickHold"",
                    ""type"": ""Button"",
                    ""id"": ""e52b7a07-0b54-4196-816e-08daaba397f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""RightClickHold"",
                    ""type"": ""Button"",
                    ""id"": ""47f9de67-6382-42fb-9a81-230913572787"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""50af9396-0e11-47b4-9695-a2fc93185ab8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""LeftClickHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69a6936f-f04e-428d-a487-b5dce3953f57"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keys"",
                    ""id"": ""fd51c6ab-ca5c-46e6-a77a-8ae4ce71b279"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e489346c-211d-4f8b-ae36-b8a65eab0e06"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7a7fd3d4-a807-4e3f-82fe-d13f71553c2d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f10d15be-8f2a-482e-9f29-af9d8354f8e0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5371a046-de05-4286-90dc-3068177ea04e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""c4bcb858-771b-443f-9a59-1ef484e65329"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""35f9eed2-cc22-4a7c-90c8-32cfe2050872"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f68a0dcb-8d82-4246-b5e9-5664e39920eb"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""20f0b921-9bb4-45a6-8b71-2467612998cf"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""08e57e13-bce3-4251-b9bd-202be1dfc903"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ca6e6d98-fbf0-449a-a6dc-9c6ecbfa1570"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c3ba6c3-0713-4c3f-8b9b-5574033f2691"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0d80f92-6f78-48ff-abdc-1a342fc0f0dd"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""RightClickHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": []
        }
    ]
}");
        // CraftCam
        m_CraftCam = asset.FindActionMap("CraftCam", throwIfNotFound: true);
        m_CraftCam_Escape = m_CraftCam.FindAction("Escape", throwIfNotFound: true);
        m_CraftCam_Look = m_CraftCam.FindAction("Look", throwIfNotFound: true);
        m_CraftCam_Scroll = m_CraftCam.FindAction("Scroll", throwIfNotFound: true);
        m_CraftCam_Movement = m_CraftCam.FindAction("Movement", throwIfNotFound: true);
        m_CraftCam_ClickFrame = m_CraftCam.FindAction("ClickFrame", throwIfNotFound: true);
        m_CraftCam_ClickHold = m_CraftCam.FindAction("ClickHold", throwIfNotFound: true);
        m_CraftCam_Alt = m_CraftCam.FindAction("Alt", throwIfNotFound: true);
        m_CraftCam_RightClickHold = m_CraftCam.FindAction("RightClickHold", throwIfNotFound: true);
        // ShipControl
        m_ShipControl = asset.FindActionMap("ShipControl", throwIfNotFound: true);
        m_ShipControl_Escape = m_ShipControl.FindAction("Escape", throwIfNotFound: true);
        m_ShipControl_Look = m_ShipControl.FindAction("Look", throwIfNotFound: true);
        m_ShipControl_Scroll = m_ShipControl.FindAction("Scroll", throwIfNotFound: true);
        m_ShipControl_Movement = m_ShipControl.FindAction("Movement", throwIfNotFound: true);
        m_ShipControl_LeftClickHold = m_ShipControl.FindAction("LeftClickHold", throwIfNotFound: true);
        m_ShipControl_RightClickHold = m_ShipControl.FindAction("RightClickHold", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // CraftCam
    private readonly InputActionMap m_CraftCam;
    private ICraftCamActions m_CraftCamActionsCallbackInterface;
    private readonly InputAction m_CraftCam_Escape;
    private readonly InputAction m_CraftCam_Look;
    private readonly InputAction m_CraftCam_Scroll;
    private readonly InputAction m_CraftCam_Movement;
    private readonly InputAction m_CraftCam_ClickFrame;
    private readonly InputAction m_CraftCam_ClickHold;
    private readonly InputAction m_CraftCam_Alt;
    private readonly InputAction m_CraftCam_RightClickHold;
    public struct CraftCamActions
    {
        private @Controls m_Wrapper;
        public CraftCamActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Escape => m_Wrapper.m_CraftCam_Escape;
        public InputAction @Look => m_Wrapper.m_CraftCam_Look;
        public InputAction @Scroll => m_Wrapper.m_CraftCam_Scroll;
        public InputAction @Movement => m_Wrapper.m_CraftCam_Movement;
        public InputAction @ClickFrame => m_Wrapper.m_CraftCam_ClickFrame;
        public InputAction @ClickHold => m_Wrapper.m_CraftCam_ClickHold;
        public InputAction @Alt => m_Wrapper.m_CraftCam_Alt;
        public InputAction @RightClickHold => m_Wrapper.m_CraftCam_RightClickHold;
        public InputActionMap Get() { return m_Wrapper.m_CraftCam; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CraftCamActions set) { return set.Get(); }
        public void SetCallbacks(ICraftCamActions instance)
        {
            if (m_Wrapper.m_CraftCamActionsCallbackInterface != null)
            {
                @Escape.started -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnEscape;
                @Look.started -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnLook;
                @Scroll.started -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnScroll;
                @Scroll.performed -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnScroll;
                @Scroll.canceled -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnScroll;
                @Movement.started -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnMovement;
                @ClickFrame.started -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnClickFrame;
                @ClickFrame.performed -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnClickFrame;
                @ClickFrame.canceled -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnClickFrame;
                @ClickHold.started -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnClickHold;
                @ClickHold.performed -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnClickHold;
                @ClickHold.canceled -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnClickHold;
                @Alt.started -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnAlt;
                @Alt.performed -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnAlt;
                @Alt.canceled -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnAlt;
                @RightClickHold.started -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnRightClickHold;
                @RightClickHold.performed -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnRightClickHold;
                @RightClickHold.canceled -= m_Wrapper.m_CraftCamActionsCallbackInterface.OnRightClickHold;
            }
            m_Wrapper.m_CraftCamActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Scroll.started += instance.OnScroll;
                @Scroll.performed += instance.OnScroll;
                @Scroll.canceled += instance.OnScroll;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @ClickFrame.started += instance.OnClickFrame;
                @ClickFrame.performed += instance.OnClickFrame;
                @ClickFrame.canceled += instance.OnClickFrame;
                @ClickHold.started += instance.OnClickHold;
                @ClickHold.performed += instance.OnClickHold;
                @ClickHold.canceled += instance.OnClickHold;
                @Alt.started += instance.OnAlt;
                @Alt.performed += instance.OnAlt;
                @Alt.canceled += instance.OnAlt;
                @RightClickHold.started += instance.OnRightClickHold;
                @RightClickHold.performed += instance.OnRightClickHold;
                @RightClickHold.canceled += instance.OnRightClickHold;
            }
        }
    }
    public CraftCamActions @CraftCam => new CraftCamActions(this);

    // ShipControl
    private readonly InputActionMap m_ShipControl;
    private IShipControlActions m_ShipControlActionsCallbackInterface;
    private readonly InputAction m_ShipControl_Escape;
    private readonly InputAction m_ShipControl_Look;
    private readonly InputAction m_ShipControl_Scroll;
    private readonly InputAction m_ShipControl_Movement;
    private readonly InputAction m_ShipControl_LeftClickHold;
    private readonly InputAction m_ShipControl_RightClickHold;
    public struct ShipControlActions
    {
        private @Controls m_Wrapper;
        public ShipControlActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Escape => m_Wrapper.m_ShipControl_Escape;
        public InputAction @Look => m_Wrapper.m_ShipControl_Look;
        public InputAction @Scroll => m_Wrapper.m_ShipControl_Scroll;
        public InputAction @Movement => m_Wrapper.m_ShipControl_Movement;
        public InputAction @LeftClickHold => m_Wrapper.m_ShipControl_LeftClickHold;
        public InputAction @RightClickHold => m_Wrapper.m_ShipControl_RightClickHold;
        public InputActionMap Get() { return m_Wrapper.m_ShipControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShipControlActions set) { return set.Get(); }
        public void SetCallbacks(IShipControlActions instance)
        {
            if (m_Wrapper.m_ShipControlActionsCallbackInterface != null)
            {
                @Escape.started -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnEscape;
                @Look.started -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnLook;
                @Scroll.started -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnScroll;
                @Scroll.performed -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnScroll;
                @Scroll.canceled -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnScroll;
                @Movement.started -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnMovement;
                @LeftClickHold.started -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnLeftClickHold;
                @LeftClickHold.performed -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnLeftClickHold;
                @LeftClickHold.canceled -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnLeftClickHold;
                @RightClickHold.started -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnRightClickHold;
                @RightClickHold.performed -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnRightClickHold;
                @RightClickHold.canceled -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnRightClickHold;
            }
            m_Wrapper.m_ShipControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Scroll.started += instance.OnScroll;
                @Scroll.performed += instance.OnScroll;
                @Scroll.canceled += instance.OnScroll;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @LeftClickHold.started += instance.OnLeftClickHold;
                @LeftClickHold.performed += instance.OnLeftClickHold;
                @LeftClickHold.canceled += instance.OnLeftClickHold;
                @RightClickHold.started += instance.OnRightClickHold;
                @RightClickHold.performed += instance.OnRightClickHold;
                @RightClickHold.canceled += instance.OnRightClickHold;
            }
        }
    }
    public ShipControlActions @ShipControl => new ShipControlActions(this);
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    public interface ICraftCamActions
    {
        void OnEscape(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnScroll(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnClickFrame(InputAction.CallbackContext context);
        void OnClickHold(InputAction.CallbackContext context);
        void OnAlt(InputAction.CallbackContext context);
        void OnRightClickHold(InputAction.CallbackContext context);
    }
    public interface IShipControlActions
    {
        void OnEscape(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnScroll(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnLeftClickHold(InputAction.CallbackContext context);
        void OnRightClickHold(InputAction.CallbackContext context);
    }
}
