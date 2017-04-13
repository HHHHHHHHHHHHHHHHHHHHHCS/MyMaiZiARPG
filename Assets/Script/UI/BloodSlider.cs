using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BloodSlider : MonoBehaviour
{
    private Slider mSlider;
    private Transform bindUnit;

    public void Init(Slider _slider, Transform _bindUnit)
    {
        mSlider = _slider;
        bindUnit = _bindUnit;

    }

    void Update()
    {
        Vector3 pos = bindUnit.position;
        pos = Camera.main.WorldToScreenPoint(pos);
        pos = MousePosToUI(pos);
        if (pos.z > 0)
        {
            if (transform.localScale == Vector3.zero)
            {
                transform.localScale = Vector3.one;
            }

            transform.localPosition = pos;
        }
        else if (transform.localScale == Vector3.one)
        {
            transform.localScale = Vector3.zero;


        }
    }

    public void RefreshSlider(float lifePer)
    {
        if (lifePer < 0)
        {
            mSlider.value = 0;
            return;
        }
        if (lifePer > 1)
        {
            mSlider.value = 1;
            return;
        }
        mSlider.value = lifePer;
    }

    public void RefreshSlider(int nowHp, int maxHp)
    {
        float lifePer = 0;
        if (maxHp > 0)
        {
            lifePer = (float)nowHp / maxHp;
        }

        if (lifePer < 0)
        {
            mSlider.value = 0;
            return;
        }
        if (lifePer > 1)
        {
            mSlider.value = 1;
            return;
        }
        mSlider.value = lifePer;
    }

    /// <summary>
    /// 位置转换 因为屏幕中心是0,0  但这时候要左下角计算  所以要重新计算锚点
    /// </summary>
    /// <param name="mousePos">鼠标位置</param>
    /// <returns>UI位置</returns>
    public static Vector3 MousePosToUI(Vector3 mousePos)
    {
        Vector3 pos = mousePos;
        pos.x -= Screen.width >> 1;
        pos.y -= Screen.height >> 1;
        float mScale = Screen.width / 1024f;
        if (pos.x != 0)
        {
            pos.x /= mScale;
        }
        if (pos.y != 0)
        {
            pos.y /= mScale;
        }

        return pos;
    }
}
