using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Plugins;

public class FlyEffect : Effect
{
    public override void InitEffect(string resName,  Vector3 pos, Vector3 dir, Vector3 targetPos)
    {
        base.InitEffect(resName, pos, dir, targetPos);
        Move(targetPos);
    }

    public override void InitEffect(string resName, Transform parent, Vector3 pos, Vector3 dir, Vector3 targetPos)
    {
        base.InitEffect(resName, parent, pos, dir, targetPos);
        Move(targetPos);
    }

    private void Move(Vector3 targetPos)
    {
        HOTween.Init();
        TweenParms parm = new TweenParms();
        parm.Prop("position", targetPos);
        parm.Ease(EaseType.Linear);
        parm.OnComplete(OnArrive);
        HOTween.To(mRoot, 2, parm);
    }

    private void OnArrive()
    {
        lifeTime = 0;
    }
}
