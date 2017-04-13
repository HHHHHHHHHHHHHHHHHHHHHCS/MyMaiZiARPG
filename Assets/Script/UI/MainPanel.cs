using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    private Image normalImage;
    private Image normalMask;
    private Image skillImage;
    private Image skillMask;

    private float normalTime;
    private float skillTime;
    private float curNormalTime;
    private float curSkillTime;

    private Transform mRoot;

    void Awake()
    {
        mRoot = transform;
        normalImage = mRoot.Find("Normal/Image").GetComponent<Image>();
        normalMask = mRoot.Find("Normal/Mask").GetComponent<Image>();
        skillImage = mRoot.Find("Skill/Image").GetComponent<Image>();
        skillMask = mRoot.Find("Skill/Mask").GetComponent<Image>();

    }

    void Update()
    {
        if (curNormalTime > 0)
        {
            curNormalTime -= Time.deltaTime;
        }
        if (curSkillTime > 0)
        {
            curSkillTime -= Time.deltaTime;
        }

        UpdateCoolDownUI();
    }

    public void OnPlayerUseSkill(EntityState state)
    {
        if (state == EntityState.Attack1)
        {
            curNormalTime = normalTime;
        }
        else if (state == EntityState.Attack2)
        {
            curSkillTime = skillTime;
        }
    }

    public void InitSkillTime(float _normalTime, float _skillTime)
    {
        normalTime = _normalTime;
        skillTime = _skillTime;
        curNormalTime = 0;
        curSkillTime = 0;
        UpdateCoolDownUI();
    }

    void UpdateCoolDownUI()
    {
        normalMask.fillAmount = UICoolTimeValue(curNormalTime, normalTime);
        skillMask.fillAmount = UICoolTimeValue(curSkillTime, skillTime);
    }

    float UICoolTimeValue(float curTime, float time)
    {
        if (time < 0 || curTime <= 0)
        {
            return 0;
        }
        return curTime / time;
    }

    public bool IsNormalOK()
    {
        return curNormalTime > 0 ? false : true;
    }

    public bool IsSkillOK()
    {
        return curSkillTime > 0 ? false : true;
    }
}
