using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("���� ����")]
    public int playerScore = 0;
    public int itemsCollected = 0;

    [Header("UI ����")]
    public Text scoreText;
    public Text itemCountText;
    public Text gameStatusText;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectItem()
    {
        itemsCollected++;
        Debug.Log($"������ ����! (�� : {itemsCollected} ��");
    }

    void UpdateUI()
    {
        if(scoreText != null )
        {
            scoreText.text = "���� : " + playerScore;
        }
        if(itemCountText != null)
        {
            itemCountText.text = "������ : " + itemsCollected + "��";
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
