using UnityEngine;
using System.Collections;

public class EffectScript : MonoBehaviour
{
    public float lifeTime;
    public CallBack<Transform> callBack;

    public void SetCallBack(CallBack<Transform> _callBack)
    {
        callBack = _callBack;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Enemy"))
        {
            callBack(col.transform);
        }
    }
}
