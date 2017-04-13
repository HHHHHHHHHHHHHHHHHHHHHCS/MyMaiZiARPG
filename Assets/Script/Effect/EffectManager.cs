using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectManager : MonoBehaviour
{
    private static EffectManager _Instance;
    private List<Effect> effectList = new List<Effect>();
    public static EffectManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<EffectManager>();
                if (_Instance == null)
                {
                    GameObject obj = new GameObject("EffectManager");
                    _Instance = obj.AddComponent<EffectManager>();
                }
            }
            return _Instance;
        }
    }

    public Effect CreateEffect(string resName, Vector3 pos, Vector3 dir)
    {
        Effect effect = new Effect();
        effect.InitEffect(resName, pos, dir);
        effectList.Add(effect);
        return effect;
    }

    public Effect CreateEffect(string resName, Transform parent
        , Vector3 pos, Vector3 dir)
    {
        Effect effect = new Effect();
        effect.InitEffect(resName, parent, pos, dir);
        effectList.Add(effect);
        return effect;
    }

    public Effect CreateEffect(string resName
        , Vector3 pos, Vector3 dir, Vector3 targetPos,int harm = 0)
    {
        FlyEffect effect = new FlyEffect();
        effect.InitEffect(resName, pos, dir, targetPos);
        effect.SetHarm(harm);
        effectList.Add(effect);
        return effect;
    }

    public Effect CreateEffect(string resName, Transform parent
    , Vector3 pos, Vector3 dir, Vector3 targetPos, int harm = 0)
    {
        FlyEffect effect = new FlyEffect();
        effect.InitEffect(resName, parent, pos, dir, targetPos);
        effect.SetHarm(harm);
        effectList.Add(effect);
        return effect;
    }

    void Update()
    {
        if (effectList.Count > 0)
        {
            for (int i = effectList.Count - 1; i >= 0; i--)
            {
                if (effectList[i] == null || effectList[i].lifeTime <= GameDefine.skillFXForeverLifeTime)
                {
                    continue;
                }
                if (effectList[i].lifeTime <= 0)
                {
                    GameObject.Destroy(effectList[i].mRoot.gameObject);
                    effectList.Remove(effectList[i]);
                }
                else
                {
                    effectList[i].lifeTime -= Time.deltaTime;
                }

            }
        }
    }
}
