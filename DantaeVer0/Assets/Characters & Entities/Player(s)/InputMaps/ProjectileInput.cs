// GENERATED AUTOMATICALLY FROM 'Assets/InputMaps/ProjectileInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ProjectileInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ProjectileInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ProjectileInput"",
    ""maps"": [
        {
            ""name"": ""Combat (Projectile)"",
            ""id"": ""79b513c8-fce3-4f21-8d2d-63f98d118049"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""6f070c37-5aa2-4ba9-a682-7ab6f76d77d6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shooting"",
                    ""type"": ""Button"",
                    ""id"": ""ee13e7b5-59a9-477f-aea4-2a8f8cf79e90"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Special"",
                    ""type"": ""Button"",
                    ""id"": ""d934bb8b-e9d5-4479-bdbd-1cb0f065455f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""86b44cc7-7868-49eb-820b-c53021f18e68"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5aa96a1f-630f-4194-9474-261b961a0f1f"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shooting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88ce2a1f-9534-4bb7-8e3a-98f36c84e60d"",
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
        // Combat (Projectile)
        m_CombatProjectile = asset.FindActionMap("Combat (Projectile)", throwIfNotFound: true);
        m_CombatProjectile_Movement = m_CombatProjectile.FindAction("Movement", throwIfNotFound: true);
        m_CombatProjectile_Shooting = m_CombatProjectile.FindAction("Shooting", throwIfNotFound: true);
        m_CombatProjectile_Special = m_CombatProjectile.FindAction("Special", throwIfNotFound: true);
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

    // Combat (Projectile)
    private readonly InputActionMap m_CombatProjectile;
    private ICombatProjectileActions m_CombatProjectileActionsCallbackInterface;
    private readonly InputAction m_CombatProjectile_Movement;
    private readonly InputAction m_CombatProjectile_Shooting;
    private readonly InputAction m_CombatProjectile_Special;
    public struct CombatProjectileActions
    {
        private @ProjectileInput m_Wrapper;
        public CombatProjectileActions(@ProjectileInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_CombatProjectile_Movement;
        public InputAction @Shooting => m_Wrapper.m_CombatProjectile_Shooting;
        public InputAction @Special => m_Wrapper.m_CombatProjectile_Special;
        public InputActionMap Get() { return m_Wrapper.m_CombatProjectile; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CombatProjectileActions set) { return set.Get(); }
        public void SetCallbacks(ICombatProjectileActions instance)
        {
            if (m_Wrapper.m_CombatProjectileActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CombatProjectileActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CombatProjectileActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CombatProjectileActionsCallbackInterface.OnMovement;
                @Shooting.started -= m_Wrapper.m_CombatProjectileActionsCallbackInterface.OnShooting;
                @Shooting.performed -= m_Wrapper.m_CombatProjectileActionsCallbackInterface.OnShooting;
                @Shooting.canceled -= m_Wrapper.m_CombatProjectileActionsCallbackInterface.OnShooting;
                @Special.started -= m_Wrapper.m_CombatProjectileActionsCallbackInterface.OnSpecial;
                @Special.performed -= m_Wrapper.m_CombatProjectileActionsCallbackInterface.OnSpecial;
                @Special.canceled -= m_Wrapper.m_CombatProjectileActionsCallbackInterface.OnSpecial;
            }
            m_Wrapper.m_CombatProjectileActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Shooting.started += instance.OnShooting;
                @Shooting.performed += instance.OnShooting;
                @Shooting.canceled += instance.OnShooting;
                @Special.started += instance.OnSpecial;
                @Special.performed += instance.OnSpecial;
                @Special.canceled += instance.OnSpecial;
            }
        }
    }
    public CombatProjectileActions @CombatProjectile => new CombatProjectileActions(this);
    public interface ICombatProjectileActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnShooting(InputAction.CallbackContext context);
        void OnSpecial(InputAction.CallbackContext context);
    }
}
