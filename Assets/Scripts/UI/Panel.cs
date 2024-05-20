using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Panel : MonoBehaviour
{
    [SerializeField] bool hideOnAwake;
    [SerializeField] GameObject container;

    private void Awake()
    {
        if (hideOnAwake)
            Hide();
        else
            Show();
    }
    protected virtual void OnShowBegin()
    {

    }
    protected virtual void OnHideBegin()
    {

    }
    public void Show()
    {
        OnShowBegin();
        container.SetActive(true);
    }

    public void Hide()
    {
        OnHideBegin();
        container.SetActive(false);
    }
}
