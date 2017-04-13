using UnityEngine;
using System.Collections;

public class Player : Entity
{
    public float normalTime=1;
    public float skillTime = 3;
    private NavMeshAgent agent;
    private Vector3 targetPos = Vector3.zero;
    private MainPanel mainPanel;

    public override void InitEntity(Vector3 pos, Vector3 dir)
    {
        base.InitEntity(pos, dir);
        agent = transform.GetComponent<NavMeshAgent>();
        Camera.main.GetComponent<CameraFollow>().InitCamera(transform);
        mainPanel = FindObjectOfType<MainPanel>();
        mainPanel.InitSkillTime(normalTime, skillTime);
        curLife = totalLife = 100;
        harmNumber = 10;
        RefreshSlider();
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (CheckPress(Input.mousePosition, ref targetPos))
            {
                Move(targetPos);
            }

        }

        if (Input.GetMouseButtonDown(1)&&mainPanel.IsNormalOK())
        {
            agent.Stop();
            OnEnterAttack1();
        }

        if (Input.GetKeyDown(KeyCode.Space) && mainPanel.IsSkillOK())
        {
            agent.Stop();
            OnEnterAttack2();
        }
    }

    void LateUpdate()
    {
        if (mState == EntityState.Run && Vector2.Distance(new Vector2(mRoot.position.x, mRoot.position.z)
            , new Vector2(targetPos.x, targetPos.z)) <= 0.15f)
        {
            agent.Stop();
            OnEnterIdle();
        }
    }

    private bool CheckPress(Vector3 inputPos, ref Vector3 outPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(inputPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Terrain"))
            {

                outPos = hit.point;
                return true;
            }
        }
        return false;

    }


    private void Move(Vector3 targetPos)
    {
        agent.Resume();
        mRoot.LookAt(targetPos);
        OnEnterRun();
        agent.SetDestination(targetPos);
    }

    protected override void OnEnterAttack1()
    {
        base.OnEnterAttack1();
        mainPanel.OnPlayerUseSkill(mState);
    }

    protected override void OnEnterAttack2()
    {
        base.OnEnterAttack2();
        mainPanel.OnPlayerUseSkill(mState);
    }

    protected override void OnEnterDeath(float time = 1.5F)
    {
        agent.Stop();
        base.OnEnterDeath(time);

    }


}
