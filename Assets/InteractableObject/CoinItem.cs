using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : InteractableObject
{
    [Header("���� ����")]
    public int coinValue = 10;

    protected override void Start()
    {
        base.Start();
        objectName = "����";
        interactionTxet = "[E] ���� ȹ��";
        interactionType = InteractionType.Item;
    }

    protected override void Collectltem()
    {
        transform.Rotate(Vector3.up * 360f);
        Destroy(gameObject , 0.5f);
    }
}
