using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item" , menuName = "Inventory/Item")]

public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int maxStack = 99;

    //추가
    public bool isUsable = false;           //사용 가능한 아이템 인지 설정
    public int healAmount = 0;              //회복량

}
