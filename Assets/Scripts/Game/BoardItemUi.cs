using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoardItemUi : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private AnimationController animController;
    [SerializeField] private Image itemImage;
    [SerializeField] private string flipState = "Flip";
    [SerializeField] private string hideState = "Hide";
    [SerializeField] private string idleState = "Idle";

    public event Action OnItemClick;

    private int _index;

    private void Start()
    {
        animController.PlayAnimation(idleState);
        itemImage.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnItemClick?.Invoke();
        ShowFlipAnimatio(() => {
            itemImage.gameObject.SetActive(true);
        });
    }

    public void SetData(int index, Sprite sprite)
    {
        _index = index;
        itemImage.sprite = sprite;
    }

    public void ShowFlipAnimatio(Action callBack)
    {
        animController.PlayAnimation(flipState, (stateName) => {
            callBack?.Invoke();
            ShowIdleState();
        });
    }

    public void HideFlipAnimatio()
    {
        animController.PlayAnimation(hideState);
    }

    public void ShowIdleState()
    {
        animController.PlayAnimation(idleState);
    }
}
