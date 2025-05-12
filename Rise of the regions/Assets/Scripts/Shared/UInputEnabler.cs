using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputEnabler : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;

    private void Awake()
    {
        // Asegura que el mapa de acción de UI esté habilitado
        var uiActionMap = inputActions.FindActionMap("UI", true);
        if (!uiActionMap.enabled)
        {
            uiActionMap.Enable();
        }
    }
}
