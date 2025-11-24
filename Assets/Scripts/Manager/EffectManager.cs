using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance { get; private set; }

    [System.Serializable]

    public class EffectData
    {
        public string effectName;
        public GameObject effectPrefabs;
        public float defaultDuration = 2f;

    }

    [Header("이펙트 목록")]
    [SerializeField] private List<EffectData> effectList = new List<EffectData>();
    private Dictionary<string, EffectData> effectDictionary = new Dictionary<string, EffectData>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitallizeDictionary();

        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void InitallizeDictionary()
    {
        effectDictionary.Clear();
        foreach (var effect in effectList)
        {
            if (effectDictionary.ContainsKey(effect.effectName))
            {
                effectDictionary.Add(effect.effectName, effect);
            }
            else
            {
                Debug.LogWarning($"중복된 이펙트 이름 : {effect.effectName}");
            }
        }
    }

    public GameObject PlayerEffect(string effectName , Vector3 position, Quaternion rotation)
    {
        if (effectDictionary.TryGetValue(effectName, out EffectData data))
        {
            GameObject effect = Instantiate(data.effectPrefabs, position, rotation);
            Destroy(effect, data.defaultDuration);
            return effect;
        }
        else
        {
            Debug.LogWarning($"이펙트를  찾을수 없습니다. : {effectName}");
            return null;
        }
    }


    public GameObject PlayerEffect(string effectName, Vector3 position, Quaternion rotation, float duration)
    {
        if (effectDictionary.TryGetValue(effectName, out EffectData data))
        {
            GameObject effect = Instantiate(data.effectPrefabs, position, rotation);
            Destroy(effect, data.defaultDuration);
            return effect;
        }
        else
        {
            Debug.LogWarning($"이펙트를  찾을수 없습니다. : {effectName}");
            return null;
        }
    }

    public GameObject PlayerEffect(string effectName, Vector3 position)
    {
        return PlayerEffect(effectName, position, Quaternion.identity);
    }

    public GameObject PlayerEffect(string effectName, Vector3 position, float duration)
    {
        return PlayerEffect(effectName, position, Quaternion.identity, duration);
    }

    public void PlayEffectWithDelay(string effectName, Vector3 position, Quaternion rotation, float delay, float duration)
    {
        StartCoroutine(PlayerEffectDelayed(effectName, position, rotation, delay, duration));
    }

    private IEnumerator PlayerEffectDelayed(string effectName , Vector3 position, Quaternion ritation, float delay, float duration)
    {
        yield return new WaitForSeconds(delay);

        if (duration > 0)
        {
            PlayerEffect(effectName , position, ritation, duration);
        }
        else
        {
            PlayerEffect(effectName, position, ritation);
        }

    }

}
