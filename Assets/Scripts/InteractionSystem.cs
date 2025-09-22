using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{

    [Header("상호작용설정")]
    public float interactionRange = 2.0f;    //상호 작용 범위
    public LayerMask interactionLayerMask = 1;  //상호작용할 레이어
    public KeyCode interactionKey = KeyCode.E;  //상호작용

    [Header("UI 설정")]
    public Text interactionText;
    public GameObject interactionUI;

    private Transform playerTransform;
    private InteractableObject currentInteractiable;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = transform;
        HideInteractionUI();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInteractables();
        HandleInteractionInput();
    }

    void CheckForInteractables()
    {
        Vector3 checkPosition = playerTransform.position + playerTransform.forward * (interactionRange * 0.5f);
        Collider[]hitColliders = Physics.OverlapSphere(checkPosition, interactionRange, interactionLayerMask);

        InteractableObject closestInsteractable = null;
        float closestDistance = float.MaxValue;

        foreach (Collider collider in hitColliders)
        {
            InteractableObject interactable = collider.GetComponent<InteractableObject>();
            if (interactable != null)
            {
                float distance = Vector3.Distance(playerTransform.position, checkPosition);
                Vector3 directionToObject = (collider.transform.position - playerTransform.position).normalized;
                float angle = Vector3.Angle(playerTransform.forward, directionToObject);

                if (angle < 90f && distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInsteractable = interactable;
                }
            }
        }

        if(closestInsteractable != currentInteractiable)
        {
            if (currentInteractiable != null)
            {
                currentInteractiable.OnPlayerExit();

            }

            currentInteractiable = closestInsteractable;

            if(currentInteractiable != null)
            {
                currentInteractiable.OnPlayerEnter();
                ShowInteractionUI(currentInteractiable.GetInteractionText());
            }
            else
            {
                HideInteractionUI();
            }
        }
    }
    void HandleInteractionInput()
    {
        if(currentInteractiable != null && Input.GetKeyDown(interactionKey))
        {
            currentInteractiable.Interact();
        }
        else
        {
            HideInteractionUI();
        }
    }
    void ShowInteractionUI(string text)
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(true);
        }
        if (interactionText != null)
        {
            interactionText.text = text;

        }
    }
    void HideInteractionUI()
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(false);
        }
    }
}
