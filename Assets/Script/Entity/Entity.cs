using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum EntityState
{
    Idle,
    Run,
    Hit,
    Attack1,
    Attack2,
    Dead
}

public enum AttackType
{
    Normal,
    Skill
}


public class Entity : MonoBehaviour
{

    public Transform mRoot;
    public EntityState mState;
    protected Animator anim;
    protected Transform topPoint;
    protected Transform middlePoint;
    protected Transform bottomPoint;
    protected BloodSlider bloodSlider;

    protected int totalLife;
    protected int curLife;
    protected int harmNumber;

    private static Transform bloodSliderFather;


    public virtual void InitEntity(Vector3 pos, Vector3 dir)
    {
        if (bloodSliderFather == null)
        {
            bloodSliderFather = GameObject.Find(GameDefine.bloodSliderPath).transform;
        }

        mRoot = transform;
        anim = mRoot.GetComponent<Animator>();
        mRoot.position = pos;
        mRoot.rotation = Quaternion.Euler(dir);

        topPoint = mRoot.Find("Points/Top");
        middlePoint = mRoot.Find("Points/Middle");
        bottomPoint = mRoot.Find("Points/Bottom");
        OnEnterIdle();


        InitBloodSlider();
    }

    protected virtual BloodSlider InitBloodSlider()
    {
        GameObject objLoad = GameManager.Instance.LoadResources<GameObject>(GameDefine.bloodSlider);
        if (objLoad != null)
        {
            GameObject obj = Instantiate(objLoad);
            obj.transform.SetParent(bloodSliderFather);
            obj.transform.localScale = Vector3.one;
            bloodSlider = obj.GetComponent<BloodSlider>();
            if (bloodSlider == null)
            {
                bloodSlider = obj.AddComponent<BloodSlider>();
            }
            bloodSlider.Init(bloodSlider.GetComponent<Slider>(), topPoint);

            RefreshSlider();

        }
        return bloodSlider;
    }

    protected virtual void RefreshSlider()
    {
        if(bloodSlider!=null)
        {
            bloodSlider.RefreshSlider(curLife, totalLife);
        }

    }

    public virtual void PlayAnimation(string animName)
    {
        if (animName == string.Empty)
        {
            Debug.Log("传入动作为空");
            return;
        }
        anim.Play(animName);

    }

    protected virtual void OnEnterIdle()
    {
        mState = EntityState.Idle;
        PlayAnimation(GameDefine.animIdle);
    }

    protected virtual void OnEnterRun()
    {
        mState = EntityState.Run;
        PlayAnimation(GameDefine.animRun);
    }

    protected virtual void OnEnterHit()
    {
        mState = EntityState.Hit;
        PlayAnimation(GameDefine.animHit);
        Invoke("OnEnterIdle", 1f);
    }

    protected virtual void OnEnterDeath(float time = 1.5f)
    {

        mState = EntityState.Dead;
        Dead(time);
        PlayAnimation(GameDefine.animDeath);
    }

    protected virtual void OnEnterAttack1()
    {
        mState = EntityState.Attack1;
        PlayAnimation(GameDefine.animAttack1);
    }

    protected virtual void OnEnterAttack2()
    {
        mState = EntityState.Attack2;
        PlayAnimation(GameDefine.animAttack2);
    }

    public virtual void GetHurt(int hurt)
    {
        curLife = curLife - hurt > 0 ? curLife - hurt : 0;
        RefreshSlider();
        if (curLife <= 0)
        {
            OnEnterDeath();
        }
        else
        {
            OnEnterHit();
        }

    }

    protected virtual void Dead(float time = 1.5f)
    {
        Invoke("DestorySelf", time);
    }

    protected virtual void DestorySelf()
    {
        Destroy(gameObject);
    }
}
