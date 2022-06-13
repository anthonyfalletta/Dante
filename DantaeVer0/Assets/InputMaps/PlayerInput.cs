// GENERATED AUTOMATICALLY FROM 'Assets/InputMaps/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Combat"",
            ""id"": ""3b26aa0f-3fe0-47c6-85dc-4dad7b8f7fde"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""ed7e7196-784b-41ae-9197-d2b7f068bc09"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shooting"",
                    ""type"": ""Button"",
                    ""id"": ""92e2391d-97f7-4d5a-a8a4-b92ea846d438"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dashing"",
                    ""type"": ""Button"",
                    ""id"": ""38630bf0-fa0e-4005-bf2e-b6e3ed2b6b9a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attacking"",
                    ""type"": ""Button"",
                    ""id"": ""da6d58bf-4e63-4998-a518-25cb90c7a285"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Special"",
                    ""type"": ""Button"",
                    ""id"": ""14a24693-aeba-42e4-bee9-3efaa084590e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7111a7b0-635c-488e-88dc-07fc7993a5b6"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""539be9aa-7a9e-4293-b2fc-31926ebf7987"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shooting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6dd460ee-c4e4-406c-a0c8-3a07067564a4"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dashing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ce1015a-e5f1-496a-9003-fe8d66873014"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attacking"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34129a41-fd46-42a5-9abb-7ce7c66d7dac"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Special"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Combat
        m_Combat = asset.FindActionMap("Combat", throwIfNotFound: true);
        m_Combat_Movement = m_Combat.FindAction("Movement", throwIfNotFound: true);
        m_Combat_Shooting = m_Combat.FindAction("Shooting", throwIfNotFound: true);
        m_Combat_Dashing = m_Combat.FindAction("Dashing", throwIfNotFound: true);
        m_Combat_Attacking = m_Combat.FindAction("Attacking", throwIfNotFound: true);
        m_Combat_Special = m_Combat.FindAction("Special", throwIfNotFound: true);
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

    // Combat
    private readonly InputActionMap m_Combat;
    private ICombatActions m_CombatActionsCallbackInterface;
    private readonly InputAction m_Combat_Movement;
    private readonly InputAction m_Combat_Shooting;
    private readonly InputAction m_Combat_Dashing;
    private readonly InputAction m_Combat_Attacking;
    private readonly InputAction m_Combat_Special;
    public struct CombatActions
    {
        private @PlayerInput m_Wrapper;
        public CombatActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Combat_Movement;
        public InputAction @Shooting => m_Wrapper.m_Combat_Shooting;
        public InputAction @Dashing => m_Wrapper.m_Combat_Dashing;
        public InputAction @Attacking => m_Wrapper.m_Combat_Attacking;
        public InputAction @Special => m_Wrapper.m_Combat_Special;
        public InputActionMap Get() { return m_Wrapper.m_Combat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CombatActions set) { return set.Get(); }
        public void SetCallbacks(ICombatActions instance)
        {
            if (m_Wrapper.m_CombatActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnMovement;
                @Shooting.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnShooting;
                @Shooting.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnShooting;
                @Shooting.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnShooting;
                @Dashing.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnDashing;
                @Dashing.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnDashing;
                @Dashing.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnDashing;
                @Attacking.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnAttacking;
                @Attacking.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnAttacking;
                @Attacking.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnAttacking;
                @Special.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnSpecial;
                @Special.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnSpecial;
                @Special.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnSpecial;
            }
            m_Wrapper.m_CombatActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Shooting.started += instance.OnShooting;
                @Shooting.performed += instance.OnShooting;
                @Shooting.canceled += instance.OnShooting;
                @Dashing.started += instance.OnDashing;
                @Dashing.performed += instance.OnDashing;
                @Dashing.canceled += instance.OnDashing;
                @Attacking.started += instance.OnAttacking;
                @Attacking.performed += instance.OnAttacking;
                @Attacking.canceled += instance.OnAttacking;
                @Special.started += instance.OnSpecial;
                @Special.performed += instance.OnSpecial;
                @Special.canceled += instance.OnSpecial;
            }
        }
    }
    public CombatActions @Combat => new CombatActions(this);
    public interface ICombatActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnShooting(InputAction.CallbackContext context);
        void OnDashing(InputAction.CallbackContext context);
        void OnAttacking(InputAction.CallbackContext context);
        void OnSpecial(InputAction.CallbackContext context);
    }
}
