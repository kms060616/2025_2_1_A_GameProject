using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemData item;
    public int amount;

    [Header("UI References")]
    public Image itemIcon;
    public Text amountText;
    public GameObject emptySlotImage;

    public Button slotButton;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSloutUI();
        slotButton.onClick.AddListener(OnSlotClick);
    }

    void OnSlotClick()
    {
        if(item != null)
        {
            ItemUsePopup.Instance.ShowPopup(item, this);
        }
    }

    public void AddAmount(int value)
    {
        amount += value;
        UpdateSloutUI();
    }

    public void RemoveAmount(int value)
    {
        amount -= value;

        if (amount <= 0)
        {
            ClearSlot();
        }
        else
        {
            UpdateSloutUI();
        }
    }

    public void ClearSlot()
    {
        item = null;
        amount = 0;
        UpdateSloutUI();
    }

    

    public void SetItem(ItemData newItem, int newAmount)
    {
        item = newItem;
        amount = newAmount;
        UpdateSloutUI();
    }

    void UpdateSloutUI()
    {
        if(item != null)
        {
            itemIcon.sprite = item.itemIcon;
            itemIcon.enabled = true;

            amountText.text = amount > 1 ? amount.ToString() : "";
            if(emptySlotImage != null )
            {
                emptySlotImage.SetActive(false);
            } 

        }
        else
        {
            itemIcon.enabled = false;
            amountText.text = "";
            if (emptySlotImage != null )
            {
                emptySlotImage.SetActive(true);
            }
        }
    }
    
}
