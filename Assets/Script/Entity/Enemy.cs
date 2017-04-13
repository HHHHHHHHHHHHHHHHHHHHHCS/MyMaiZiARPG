using UnityEngine;
using System.Collections;

public class Enemy : Entity
{
    private Vector3[] movePoints;
    private float stayTime = 0;
    private Transform target;
    private float chaseRadius = 3f;
    private NavMeshAgent agent;

    public void InitEnemy(Vector3[] points, Transform player)
    {
        base.InitEntity(transform.position, transform.rotation.eulerAngles);
        movePoints = points;
        target = player;
        stayTime = Random.Range(0f, 2.5f);
        agent = GetComponent<NavMeshAgent>();
        curLife = totalLife = Random.Range(30, 50);
        RefreshSlider();
    }

    void Update()
    {
        if (mState == EntityState.Dead || mState == EntityState.Hit
            ||movePoints == null || movePoints.Length == 0)
        {
            return;
        }
        //检测追击玩家
        if (target != null)
        {
            float targetDis = Vector3.Distance(mRoot.position, target.position);
            if (targetDis <= chaseRadius)
            {
                StartChase();
                return;
            }
        }

        //站立倒计时
        if (stayTime > 0)
        {
            stayTime -= Time.deltaTime;
            return;
        }

        //检测从站立到移动
        if (mState == EntityState.Idle && stayTime <= 0)
        {
            StartMove();
            return;
        }

        //检测从移动到站立
        if (mState == EntityState.Run && agent.remainingDistance <= agent.stoppingDistance)
        {
            StartMove();
            return;
        }
    }

    private void Move(Vector3 targetPos)
    {
        agent.Resume();
        mRoot.LookAt(targetPos);
        OnEnterRun();
        agent.SetDestination(targetPos);
    }

    private void StartMove()
    {
        if(movePoints.Length<0)
        {
            return;
        }
        Vector3 targetPos = movePoints[Random.Range(0, movePoints.Length - 1)];
        float dis = Vector3.Distance(targetPos, mRoot.position);
        if(dis>0)
        {
            Move(targetPos);
        }
    }

    private void StartIdle()
    {
        stayTime = Random.Range(0f, 2.5f);
        agent.Stop();
        OnEnterIdle();
    }

    private void StartChase()
    {
        Vector3 targetPos = target.position;
        Move(target.position);
    }

    protected override void OnEnterDeath(float time = 1.5F)
    {
        agent.Stop();
        base.OnEnterDeath(time);

    }
}
