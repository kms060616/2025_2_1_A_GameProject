using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementSlot : MonoBehaviour
{
    [Header("UI References")]
    public Image iconImage;
    public Text nameText;
    public Text descriptionText;
    public Text progressText;
    public Slider progressSlider;

    public void SetAchivement(AchievementData achivement , float progress)
    {
        if (nameText != null)
            nameText.text = achivement.achievementName;

        if (descriptionText != null)
            descriptionText.text = achivement.description;

        if(iconImage != null)
            iconImage.sprite = achivement.icon;

        if (progressSlider != null)
            progressSlider.value = achivement.isUnlocked ? 1f : progress;

        if (progressText != null)
        {
            if (achivement.isUnlocked)
            {
                progressText.text = "¿Ï·á!";
            }
            else
            {
                int current = Mathf.FloorToInt(progress * achivement.requiredAmount);
                progressText.text = current + "/" + achivement.requiredAmount;
            }
        }    
            
           
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
