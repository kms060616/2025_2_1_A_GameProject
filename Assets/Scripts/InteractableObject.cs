using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("��ȣ�ۿ� ����")]
    public string objectName = "������ ";
    public string interactionTxet = "[E] ��ȣ�ۿ�";
    public InteractionType interactionType = InteractionType.Item;

    [Header("���̶���Ʈ ����")]
    public Color highlightColor = Color.yellow;
    public float highlightlntensity = 1.5f;

    public Renderer objectRenderer;
    private Color originalColor;
    private bool isHighlighted = false;

    public enum InteractionType
    {
        Item,          
        Machine,
        Builing,   //�ǹ�
        NPC,       //NPC
        Collectible,//����ǰ
    }
    // Start is called before the first frame update
   

    // Update is called once per frame
    protected virtual void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null )
        {
            originalColor = objectRenderer.material.color;
        }
        gameObject.layer = 8;
       
    }

    public virtual void OnPlayerEnter()
    {
        Debug.Log($"[{objectName}] ������");
        HighlightObject();
    }
    public virtual void OnPlayerExit()
    {
        Debug.Log($"[{objectName}]�������� ���");
        RemoveHighlight();
    }


    public virtual void Interact()
    {
        //��ȣ �ۿ� Ÿ�Կ� ���� �⺻ ����
        switch (interactionType)
        {
            case InteractionType.Item:
                Collectltem();
                break;
            case InteractionType.Machine:
                OperatMachine();
                break;
            case InteractionType.Builing:
                AccessBuilding();
                break;
                case InteractionType.NPC:
                TalkToNPC();
                    break;



        }
    }
    public string GetInteractionText()
    {
        return interactionTxet;
    }


    protected virtual void HighlightObject()
    {
        if (objectRenderer != null && !isHighlighted)
        {
            objectRenderer.material.color = highlightColor;
            objectRenderer.material.SetFloat("_Emission", highlightlntensity);
            isHighlighted = true;
        }

    }

    protected virtual void RemoveHighlight()
    {
        if (objectRenderer != null && !isHighlighted)
        {
            objectRenderer.material.color = originalColor;
            objectRenderer.material.SetFloat("_Emission", 0f);
            isHighlighted = false;
        }

    }

    protected virtual void Collectltem()
    {
        Debug.Log($"{objectName}��(��) ȹ���߽��ϴ�!");
        Destroy(gameObject);
    }

    protected virtual void OperatMachine()
    {
        Debug.Log($"{objectName}��(��) �۵����׽��ϴ�!");
        if (objectRenderer != null )
        {
            objectRenderer.material.color = Color.green;
        }
    }

    protected virtual void AccessBuilding()
    {
        Debug.Log($"{objectName}��(��) �� �����߽��ϴ�");
        transform.Rotate(Vector3.up * 90f);
    }

    protected virtual void TalkToNPC()
    {
        Debug.Log($"{objectName}�� ��ȭ�� �����մϴ�");
    }
}
