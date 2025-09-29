using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Name Quest" , menuName = "Quest System/Quest")]
public class QuestData : ScriptableObject
{
    [Header("기본 정보")]
    public string questTitle = "새로운 퀘스트";                //퀘스트 제목
    [TextArea(2, 4)]
    public string description = "퀘스트 설명을 입력하세요";    //퀘스트 내용
    public Sprite questIcon;                                   //퀘스트 아이콘

    [Header("퀘스트 설정")]                                   //퀘스트 종류
    public QuestType questType;
    public int targetAmount = 1;                               //목표 수량(수집용)

    [Header("배달 퀘스트용 (Delivery)")]
    public Vector3 deliveryPosition;
    public float deliveryRedius = 3f;

    [Header("수집/상호작용 퀘스트용")]
    public string targetTag = "";

    [Header("보상")]
    public int experienceReward = 100;
    public string rewardMessage = "퀘스트 완료";

    [Header("퀘스트 연결")]
    public QuestData nextQuest;                               //다음퀘스트

    //런타임 데어터 (저장되지 않음)
    [System.NonSerialized] public int currentProgress = 0;
    [System.NonSerialized] public bool isActive = false;
    [System.NonSerialized] public bool isCompleted = false;

    //퀘스트 초기화
    public void Initialize()
    {
        currentProgress = 0;
        isActive = false;
        isCompleted = false;
    }

    public bool IsCompleted()
    {
        switch(questType)
        {
            case QuestType.Delivery:
                return currentProgress >= 1;
            case QuestType.Collect:
            case QuestType.Insteract:
                return currentProgress >= targetAmount;
            default:
                return false;
        }
    }

    public string GetProgressText()
    {
        switch (questType)
        {
            case QuestType.Delivery:
                return isCompleted ? "배달완료!" : "목적지로 이동하세요";
            case QuestType.Collect:
                return $"{currentProgress} / {targetAmount}";
            case QuestType.Insteract:
                return $"{currentProgress} / {targetAmount}";
            default:
                return "";
        }
    }
       
        
}
