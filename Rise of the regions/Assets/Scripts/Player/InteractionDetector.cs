using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null;
    public GameObject interactionIcon;

    private float lastTapTime = 0f;
    private float doubleTapThreshold = 0.3f; // Tiempo máximo entre dos toques para considerarse doble tap

    void Start()
    {
        interactionIcon.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            // Solo para teclado (como la tecla E)
            interactableInRange?.Interact();
#endif
        }
    }

    // Este se llamará por el sistema de Input si defines una acción de tipo "Tap" en un Touchscreen
    public void OnTouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float currentTime = Time.time;
            if (currentTime - lastTapTime < doubleTapThreshold)
            {
                // Doble tap detectado
                interactableInRange?.Interact();
                lastTapTime = 0f; // Reset para evitar múltiples interacciones
            }
            else
            {
                lastTapTime = currentTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false);
        }
    }
}
