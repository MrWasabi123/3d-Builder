// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputActionsSpawn.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @SpawnControlls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @SpawnControlls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActionsSpawn"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""908bef1a-452f-42e9-9e05-153b1a39ee5b"",
            ""actions"": [
                {
                    ""name"": ""Spawn"",
                    ""type"": ""Button"",
                    ""id"": ""a7637424-d8cb-42b9-8e39-637264052144"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drop"",
                    ""type"": ""Button"",
                    ""id"": ""192990b0-a708-485c-b619-7d65c3a3f142"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StartGame"",
                    ""type"": ""Button"",
                    ""id"": ""0ef3e53a-cbf6-43a3-8d32-5bf1bba0c9fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""EndGame"",
                    ""type"": ""Button"",
                    ""id"": ""1a623274-7e03-4699-bb86-60699377c1cb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GoStartScreen"",
                    ""type"": ""Button"",
                    ""id"": ""8fc235d8-36b0-4e8e-a164-6614101b2214"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StartStory"",
                    ""type"": ""Button"",
                    ""id"": ""3eb0dd4f-a604-48bc-8c37-fcc5472c1d0d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveForward"",
                    ""type"": ""Button"",
                    ""id"": ""ff1245c1-6c72-4909-8f95-82006ba2c997"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Redo"",
                    ""type"": ""Button"",
                    ""id"": ""1caf52c6-6ca4-48ff-a97e-7757ccd02de0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Settings"",
                    ""type"": ""Button"",
                    ""id"": ""6684ecf2-bcda-4279-9adf-1412845476fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GoControlsScreen"",
                    ""type"": ""Button"",
                    ""id"": ""b0c54daa-e778-41bb-b3b3-c8c8b83984d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1d6b5bff-38db-4009-b79c-d633d8ac6a29"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Spawn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71106a3f-c2e4-4611-9fb2-53eb6d43a1cf"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df9832e6-db2f-47b9-ab13-467031fed398"",
                    ""path"": ""<Keyboard>/n"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StartGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ba8304d-a5bd-4003-b3ae-5aa4cc7ed608"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EndGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e63340d6-a5c2-43ff-8531-74d64c791936"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GoStartScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3227ee54-e59d-46d0-85f9-85488d7c12d9"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StartStory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""25c93563-f20f-4186-b33b-d1c4622ad187"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d98da1b-5258-49d9-9b7f-dd917eb8cc6b"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Redo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""01440527-946d-4dce-83c4-7ef45a943510"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Settings"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59229e91-dd08-41b9-be94-3ad903200de6"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GoControlsScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Spawn = m_Player.FindAction("Spawn", throwIfNotFound: true);
        m_Player_Drop = m_Player.FindAction("Drop", throwIfNotFound: true);
        m_Player_StartGame = m_Player.FindAction("StartGame", throwIfNotFound: true);
        m_Player_EndGame = m_Player.FindAction("EndGame", throwIfNotFound: true);
        m_Player_GoStartScreen = m_Player.FindAction("GoStartScreen", throwIfNotFound: true);
        m_Player_StartStory = m_Player.FindAction("StartStory", throwIfNotFound: true);
        m_Player_MoveForward = m_Player.FindAction("MoveForward", throwIfNotFound: true);
        m_Player_Redo = m_Player.FindAction("Redo", throwIfNotFound: true);
        m_Player_Settings = m_Player.FindAction("Settings", throwIfNotFound: true);
        m_Player_GoControlsScreen = m_Player.FindAction("GoControlsScreen", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Spawn;
    private readonly InputAction m_Player_Drop;
    private readonly InputAction m_Player_StartGame;
    private readonly InputAction m_Player_EndGame;
    private readonly InputAction m_Player_GoStartScreen;
    private readonly InputAction m_Player_StartStory;
    private readonly InputAction m_Player_MoveForward;
    private readonly InputAction m_Player_Redo;
    private readonly InputAction m_Player_Settings;
    private readonly InputAction m_Player_GoControlsScreen;
    public struct PlayerActions
    {
        private @SpawnControlls m_Wrapper;
        public PlayerActions(@SpawnControlls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Spawn => m_Wrapper.m_Player_Spawn;
        public InputAction @Drop => m_Wrapper.m_Player_Drop;
        public InputAction @StartGame => m_Wrapper.m_Player_StartGame;
        public InputAction @EndGame => m_Wrapper.m_Player_EndGame;
        public InputAction @GoStartScreen => m_Wrapper.m_Player_GoStartScreen;
        public InputAction @StartStory => m_Wrapper.m_Player_StartStory;
        public InputAction @MoveForward => m_Wrapper.m_Player_MoveForward;
        public InputAction @Redo => m_Wrapper.m_Player_Redo;
        public InputAction @Settings => m_Wrapper.m_Player_Settings;
        public InputAction @GoControlsScreen => m_Wrapper.m_Player_GoControlsScreen;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Spawn.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpawn;
                @Spawn.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpawn;
                @Spawn.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpawn;
                @Drop.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                @Drop.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                @Drop.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                @StartGame.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStartGame;
                @StartGame.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStartGame;
                @StartGame.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStartGame;
                @EndGame.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEndGame;
                @EndGame.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEndGame;
                @EndGame.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEndGame;
                @GoStartScreen.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGoStartScreen;
                @GoStartScreen.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGoStartScreen;
                @GoStartScreen.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGoStartScreen;
                @StartStory.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStartStory;
                @StartStory.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStartStory;
                @StartStory.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStartStory;
                @MoveForward.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveForward;
                @MoveForward.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveForward;
                @MoveForward.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveForward;
                @Redo.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRedo;
                @Redo.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRedo;
                @Redo.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRedo;
                @Settings.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSettings;
                @Settings.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSettings;
                @Settings.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSettings;
                @GoControlsScreen.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGoControlsScreen;
                @GoControlsScreen.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGoControlsScreen;
                @GoControlsScreen.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGoControlsScreen;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Spawn.started += instance.OnSpawn;
                @Spawn.performed += instance.OnSpawn;
                @Spawn.canceled += instance.OnSpawn;
                @Drop.started += instance.OnDrop;
                @Drop.performed += instance.OnDrop;
                @Drop.canceled += instance.OnDrop;
                @StartGame.started += instance.OnStartGame;
                @StartGame.performed += instance.OnStartGame;
                @StartGame.canceled += instance.OnStartGame;
                @EndGame.started += instance.OnEndGame;
                @EndGame.performed += instance.OnEndGame;
                @EndGame.canceled += instance.OnEndGame;
                @GoStartScreen.started += instance.OnGoStartScreen;
                @GoStartScreen.performed += instance.OnGoStartScreen;
                @GoStartScreen.canceled += instance.OnGoStartScreen;
                @StartStory.started += instance.OnStartStory;
                @StartStory.performed += instance.OnStartStory;
                @StartStory.canceled += instance.OnStartStory;
                @MoveForward.started += instance.OnMoveForward;
                @MoveForward.performed += instance.OnMoveForward;
                @MoveForward.canceled += instance.OnMoveForward;
                @Redo.started += instance.OnRedo;
                @Redo.performed += instance.OnRedo;
                @Redo.canceled += instance.OnRedo;
                @Settings.started += instance.OnSettings;
                @Settings.performed += instance.OnSettings;
                @Settings.canceled += instance.OnSettings;
                @GoControlsScreen.started += instance.OnGoControlsScreen;
                @GoControlsScreen.performed += instance.OnGoControlsScreen;
                @GoControlsScreen.canceled += instance.OnGoControlsScreen;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnSpawn(InputAction.CallbackContext context);
        void OnDrop(InputAction.CallbackContext context);
        void OnStartGame(InputAction.CallbackContext context);
        void OnEndGame(InputAction.CallbackContext context);
        void OnGoStartScreen(InputAction.CallbackContext context);
        void OnStartStory(InputAction.CallbackContext context);
        void OnMoveForward(InputAction.CallbackContext context);
        void OnRedo(InputAction.CallbackContext context);
        void OnSettings(InputAction.CallbackContext context);
        void OnGoControlsScreen(InputAction.CallbackContext context);
    }
}
