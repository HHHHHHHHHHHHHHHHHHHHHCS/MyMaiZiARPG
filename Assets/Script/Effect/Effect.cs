using UnityEngine;
using System.Collections;

public class Effect
{
    public Transform mRoot;
    public float lifeTime;

    private EffectScript mScript;
    private Transform _father;
    private int harm;
    public Transform Father
    {
        get
        {
            if(_father==null)
            {
                _father = EffectManager.Instance.transform;
            }
            return _father;
        }
    }

    public void SetHarm(int _harm)
    {
        harm = _harm;
    }


    public virtual void InitEffect(string resName, Vector3 pos, Vector3 dir)
    {
        GameObject loadObj = GameManager.Instance.LoadResources<GameObject>(resName);
        GameObject obj = Object.Instantiate(loadObj);
        mRoot = obj.transform;
        mRoot.SetParent(Father);
        mRoot.localPosition = pos;
        mRoot.localRotation = Quaternion.Euler(dir);

        mScript = obj.GetComponent<EffectScript>();
        if (mScript == null)
        {
            Debug.LogError("特效未加载");
            return;
        }
        mScript.SetCallBack(OnColliderHandler);
        lifeTime = mScript.lifeTime;

    }

    public virtual void InitEffect(string resName, Transform parent
        , Vector3 pos, Vector3 dir)
    {
        InitEffect(resName, pos, dir);
        mRoot.SetParent(parent);
        mRoot.transform.localPosition = pos;
        mRoot.transform.localRotation = Quaternion.Euler(dir);
    }

    public virtual void InitEffect(string resName
        , Vector3 pos, Vector3 dir, Vector3 targetPos)
    {
        InitEffect(resName, pos, dir);
    }

    public virtual void InitEffect(string resName, Transform parent
        , Vector3 pos, Vector3 dir, Vector3 targetPos)
    {
        InitEffect(resName, parent, pos, dir);
    }

    private void OnColliderHandler(Transform col)
    {
        EffectManager.Instance.CreateEffect(GameDefine.atcherSkillHit, mRoot.position,Quaternion.identity.eulerAngles);
        Enemy enemy = col.GetComponent<Enemy>();
        enemy.GetHurt(harm);
        lifeTime = 0;
    }

}
