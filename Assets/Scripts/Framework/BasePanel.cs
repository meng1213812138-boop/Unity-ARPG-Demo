using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    private Dictionary<string,List<UIBehaviour>> controDic = new Dictionary<string, List<UIBehaviour>>();

    protected virtual void Awake()
    {
        FindChildrenControl<Button>();
        FindChildrenControl<Image>();
        FindChildrenControl<Text>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<ScrollRect>();
        FindChildrenControl<Slider>();
        FindChildrenControl<InputField>();
    }

    public virtual void ShowMe()
    {

    }

    public virtual void HideMe()
    {

    }

    protected virtual void OnClick(string btnName)
    {

    }

    protected virtual void OnToggleValueChange(string toggleName,bool value)
    {

    }

    protected virtual void OnSliderValueChange(string sliderName, float value)
    {

    }

    /// <summary>
    /// 得到对应名字的控件脚本
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controlName"></param>
    /// <returns></returns>
    protected T GetControl<T>(string controlName)where T:UIBehaviour
    {
        if (controDic.ContainsKey(controlName))
        {
            for (int i = 0; i < controDic[controlName].Count; ++i)
            {
                if (controDic[controlName][i] is T)
                    return controDic[controlName][i] as T;
            }
        }
        return null;
    }

    /// <summary>
    /// 找到子对象的方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    private void FindChildrenControl<T>()where T:UIBehaviour
    {
        T[] controls = this.GetComponentsInChildren<T>();
        
        for (int i = 0; i < controls.Length; ++i)
        {
            string objName = controls[i].gameObject.name;
            if (controDic.ContainsKey(objName))
                controDic[objName].Add(controls[i]);
            else
                controDic.Add(objName, new List<UIBehaviour>() { controls[i] });

            if (controls[i] as Button)
            {
                (controls[i] as Button).onClick.AddListener(() =>
                {
                    OnClick(objName);
                });
            }
            else if (controls[i] is Toggle)
            {
                (controls[i] as Toggle).onValueChanged.AddListener((value) =>
                {
                    OnToggleValueChange(objName, value);
                });
            }
            else if (controls[i] is Slider)
            {
                (controls[i] as Slider).onValueChanged.AddListener((value) =>
                {
                    OnSliderValueChange(objName, value);
                });
            }
        }
    }
}
