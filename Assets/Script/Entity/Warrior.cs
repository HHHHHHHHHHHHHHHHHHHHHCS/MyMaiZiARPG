using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Warrior : Player
{
    private float attackRadius = 2;

    protected override void OnEnterAttack1()
    {
        base.OnEnterAttack1(); 
        Invoke("PlayerAttackSkill1", 0.7f);

    }
    protected override void OnEnterAttack2()
    {
        base.OnEnterAttack2();
        Invoke("PlayerAttackSkill2", 0.7f);
    }

    private void PlayerAttackSkill1()
    {
        CalculaterHarm(AttackType.Normal);
        EffectManager.Instance.CreateEffect(GameDefine.warriorSkill1
            , middlePoint.position, mRoot.forward);
        OnEnterIdle();
    }

    private void PlayerAttackSkill2()
    {
        CalculaterHarm(AttackType.Skill);
        EffectManager.Instance.CreateEffect(GameDefine.warriorSkill2
            , bottomPoint.position, mRoot.forward);
        OnEnterIdle();
    }

    private void CalculaterHarm(AttackType attackType)
    {
        List<Enemy> enemyList = EntityManager.Instance.enemyList;
        float sqrAttackRange = attackRadius * attackRadius;
        Vector3 posDis;

        if (attackType == AttackType.Normal)
        {//普通攻击范围，当前玩家面向的半圆
            foreach (Enemy enemy in enemyList)
            {
                if (enemy == null)
                {
                    continue;
                }
                posDis = mRoot.transform.position - enemy.mRoot.transform.position;
                float SqrDis = Vector3.SqrMagnitude(posDis);
                if (SqrDis <= sqrAttackRange)
                {
                    Vector3 forward = transform.forward;
                    if (Vector3.Dot(forward, posDis) > 0)
                    {
                        enemy.GetHurt(harmNumber);
                    }
                }


            }
        }
        else if (attackType == AttackType.Skill)
        {//技能攻击全圆，当前玩家的周围全圆

            foreach (Enemy enemy in enemyList)
            {
                if (enemy == null)
                {
                    continue;
                }
                posDis = mRoot.transform.position - enemy.mRoot.transform.position;
                float SqrDis = Vector3.SqrMagnitude(posDis);
                if (SqrDis <= sqrAttackRange)
                {
                    enemy.GetHurt(harmNumber);
                }
            }
        }
    }
}
