using UnityEngine;
using System.Collections;

public class Archer : Player
{
    private float attackRadius = 10f;
    protected override void OnEnterAttack1()
    {
        base.OnEnterAttack1();
        Invoke("PlayAttack1Effect", 0.7f);
    }

    private void PlayAttack1Effect()
    {

        Vector3 targetPos = mRoot.position + attackRadius * mRoot.forward.normalized;
        EffectManager.Instance.CreateEffect(GameDefine.archerSkill1
            , middlePoint.position, mRoot.forward, targetPos, harmNumber);

        OnEnterIdle();
    }

    protected override void OnEnterAttack2()
    {
        base.OnEnterAttack2();
        Invoke("PlayAttack2Effect", 0.8f);
    }

    private void PlayAttack2Effect()
    {
        Vector3 targetPos1 = mRoot.position + attackRadius * mRoot.forward.normalized;
        float middleAngle = Vector3.Angle(mRoot.forward, Vector3.right);
        middleAngle = mRoot.forward.z > 0 ? middleAngle : -middleAngle;

        float x2 = mRoot.position.x + attackRadius * Mathf.Cos((middleAngle + 30f) * Mathf.Deg2Rad);
        float z2 = mRoot.position.z + attackRadius * Mathf.Sin((middleAngle + 30f) * Mathf.Deg2Rad);

        float x3 = mRoot.position.x + attackRadius * Mathf.Cos((middleAngle - 30f) * Mathf.Deg2Rad);
        float z3 = mRoot.position.z + attackRadius * Mathf.Sin((middleAngle - 30f) * Mathf.Deg2Rad);

        Vector3 targetPos2 = new Vector3(x2, targetPos1.y, z2);
        Vector3 targetPos3 = new Vector3(x3, targetPos1.y, z3);

        EffectManager.Instance.CreateEffect(GameDefine.archerSkill2, middlePoint.position, transform.forward, targetPos1, harmNumber);
        EffectManager.Instance.CreateEffect(GameDefine.archerSkill2, middlePoint.position, transform.forward, targetPos2, harmNumber);
        EffectManager.Instance.CreateEffect(GameDefine.archerSkill2, middlePoint.position, transform.forward, targetPos3, harmNumber);
        OnEnterIdle();
    }
}
