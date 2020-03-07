// GENERATED AUTOMATICALLY FROM 'Assets/Input/Player/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player Controls"",
            ""id"": ""7dfd7934-fb2a-463f-aa2c-a3008250c368"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d99a15ad-8767-4245-a030-9b01b2d8bcdc"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""fe3d150c-a6d4-4355-8c73-f48a91864876"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActivateGhostModule"",
                    ""type"": ""Button"",
                    ""id"": ""95b397bc-1380-41b7-b537-3fb62f6c1c97"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""0d022a73-40d9-4715-823a-0962b30d0b3e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveDigital"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c39e84a9-d0f8-4e04-bbce-e2decddeaa93"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reset"",
                    ""type"": ""Button"",
                    ""id"": ""e61e1ee3-f29a-448b-a9fc-0de32348f218"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Joystick"",
                    ""id"": ""67d5ec45-7100-4823-8e40-6337f9df697d"",
                    ""path"": ""2DVector(normalize=false)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4d9afb7e-82d0-43d5-9301-3819a19fd58f"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4ae7fd6f-24a9-4ebf-85e4-a8443b87db11"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9392c280-45e7-42ee-a6bc-90796d7450ae"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2179ab79-e78d-4705-838d-544174f54657"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9a2c4117-24e6-4fae-945d-6c6835ee2e31"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""092de4e9-c917-48c9-b8a9-c751e822b613"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d45dbe13-f82f-40d3-a583-e196620cada9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3587a3a-090c-4c95-bdfc-3d37fbfbee31"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a78fc191-072b-49c6-9a79-c43396825769"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""DPad"",
                    ""id"": ""d6acf4c0-987a-4870-a860-e3de7e2cfe99"",
                    ""path"": ""2DVector(normalize=false)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDigital"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8d033dbd-7333-47a0-b3ff-186f1851471c"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDigital"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9f36d4f1-a283-42fb-951b-ab92fd74d5e9"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDigital"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""546f5580-d6a0-47e0-afdd-903e5aedc962"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDigital"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""962ee488-1159-4740-8c0f-a9511680be79"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDigital"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""0842c592-778e-4ae5-aac8-579e683ae27d"",
                    ""path"": ""2DVector(normalize=false)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDigital"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""67b14c08-5d88-4eca-9e54-07dcc8ef4ef3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDigital"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1c1d69f9-59de-43a6-8ad1-a244348e7b96"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDigital"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0666127f-c7af-4298-b58c-bd6851bc6282"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDigital"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e1320cf1-df27-44f4-8e2e-15ff4d759e40"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDigital"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""91d0175e-1306-43e8-ac95-a59d16c518dd"",
                    ""path"": ""2DVector(normalize=false)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDigital"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""df83af5f-aa84-4dc9-8225-46ea853d998e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDigital"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c4c9e7f8-1e62-4001-bf26-5dcdee9cb230"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDigital"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""49c37fcc-970a-401b-95b4-a1c5b1fcbb88"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDigital"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""80f48230-b770-47bc-8467-6c43924270f2"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDigital"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7d07ff7c-b4d1-48a8-82c4-71c554747ada"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActivateGhostModule"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02c39a00-265d-4469-b130-3e1b128a38cf"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActivateGhostModule"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""45c08e95-06a8-4436-aaa6-2cb52222b64b"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reset"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player Controls
        m_PlayerControls = asset.FindActionMap("Player Controls", throwIfNotFound: true);
        m_PlayerControls_Move = m_PlayerControls.FindAction("Move", throwIfNotFound: true);
        m_PlayerControls_Shoot = m_PlayerControls.FindAction("Shoot", throwIfNotFound: true);
        m_PlayerControls_ActivateGhostModule = m_PlayerControls.FindAction("ActivateGhostModule", throwIfNotFound: true);
        m_PlayerControls_Pause = m_PlayerControls.FindAction("Pause", throwIfNotFound: true);
        m_PlayerControls_MoveDigital = m_PlayerControls.FindAction("MoveDigital", throwIfNotFound: true);
        m_PlayerControls_Reset = m_PlayerControls.FindAction("Reset", throwIfNotFound: true);
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

    // Player Controls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_Move;
    private readonly InputAction m_PlayerControls_Shoot;
    private readonly InputAction m_PlayerControls_ActivateGhostModule;
    private readonly InputAction m_PlayerControls_Pause;
    private readonly InputAction m_PlayerControls_MoveDigital;
    private readonly InputAction m_PlayerControls_Reset;
    public struct PlayerControlsActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerControlsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerControls_Move;
        public InputAction @Shoot => m_Wrapper.m_PlayerControls_Shoot;
        public InputAction @ActivateGhostModule => m_Wrapper.m_PlayerControls_ActivateGhostModule;
        public InputAction @Pause => m_Wrapper.m_PlayerControls_Pause;
        public InputAction @MoveDigital => m_Wrapper.m_PlayerControls_MoveDigital;
        public InputAction @Reset => m_Wrapper.m_PlayerControls_Reset;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Shoot.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnShoot;
                @ActivateGhostModule.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnActivateGhostModule;
                @ActivateGhostModule.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnActivateGhostModule;
                @ActivateGhostModule.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnActivateGhostModule;
                @Pause.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPause;
                @MoveDigital.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveDigital;
                @MoveDigital.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveDigital;
                @MoveDigital.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveDigital;
                @Reset.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnReset;
                @Reset.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnReset;
                @Reset.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnReset;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @ActivateGhostModule.started += instance.OnActivateGhostModule;
                @ActivateGhostModule.performed += instance.OnActivateGhostModule;
                @ActivateGhostModule.canceled += instance.OnActivateGhostModule;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @MoveDigital.started += instance.OnMoveDigital;
                @MoveDigital.performed += instance.OnMoveDigital;
                @MoveDigital.canceled += instance.OnMoveDigital;
                @Reset.started += instance.OnReset;
                @Reset.performed += instance.OnReset;
                @Reset.canceled += instance.OnReset;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);
    public interface IPlayerControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnActivateGhostModule(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnMoveDigital(InputAction.CallbackContext context);
        void OnReset(InputAction.CallbackContext context);
    }
}
