using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : InteractableObject
{
    [Header("���� ����")]
    public int coinValue = 10;
    public string questTag = "Coin";                //����Ʈ������� �� ��ũ

    protected override void Start()
    {
        base.Start();
        objectName = "����";
        interactionTxet = "[E] ���� ȹ��";
        interactionType = InteractionType.Item;
    }

    protected override void Collectltem()
    {
        //����Ʈ �Ŵ����� ������ �˸�
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.AddCollectProgress(questTag);
        }


        transform.Rotate(Vector3.up * 360f);
        Destroy(gameObject , 0.5f);
    }
}
