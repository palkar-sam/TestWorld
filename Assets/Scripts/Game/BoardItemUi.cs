using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoardItemUi : MonoBehaviour, IPointerDownHandler
{
    public static bool IsBoardItemAnimationPlaying = false;

    [SerializeField] private AnimationController animController;
    [SerializeField] private Image itemImage;
    [SerializeField] private string flipState = "Flip";
    [SerializeField] private string hideState = "Hide";
    [SerializeField] private string idleState = "Idle";
    [SerializeField] private float initialDelay = 1.0f;

    public int ItemId => _itemId;
    public int ItemIndex => _itemIndex;

    public event Action<BoardItemUi> OnItemClick;

    private int _itemId;
    private int _itemIndex;

    private void Start()
    {
        animController.PlayAnimation(idleState);
        //itemImage.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsBoardItemAnimationPlaying) return;

        StartCoroutine(StartAnim(0.1f, true, () => {
            OnItemClick?.Invoke(this);
        }));
    }

    public void SetData(int index, int itemId, Sprite sprite)
    {
        _itemIndex = index;
        _itemId = itemId;
        itemImage.sprite = sprite;
        StartCoroutine(StartAnim(initialDelay, false));
    }

    public void ResetItem()
    {
       StartCoroutine(StartAnim(initialDelay, false));
    }

    public void ShowHideAnim(float delay, Action callBack)
    {
        StartCoroutine(StartHideAnim(delay, callBack));
    }

    private IEnumerator StartHideAnim(float delay, Action callBack)
    {
        yield return new WaitForSeconds(delay);

        PlayHideAnimation(callBack);
    }

    private void PlayHideAnimation(Action callBack)
    {
        animController.PlayAnimation(hideState, (stateName) => {
            callBack?.Invoke();
        });
    }

    private IEnumerator StartAnim(float delay, bool isShowItemImage, Action OnComplete = null)
    {
        IsBoardItemAnimationPlaying = true;

        yield return new WaitForSeconds(delay);

        PlayHideAnimation(() =>
        {
            itemImage.gameObject.SetActive(isShowItemImage);
        });

        SoundManager.Instance.PlaySFX(GameManager.Instance.GameAssets.GetSoundClip(SoundIds.FLIP_SFX));

        yield return new WaitForSeconds(0.5f);

        ShowAnimation(() => {
            OnComplete?.Invoke();
            IsBoardItemAnimationPlaying = false;
        });
    }

    private void ShowAnimation(Action callBack)
    {
        animController.PlayAnimation(flipState, (stateName) => {
            callBack?.Invoke();
        });
    }
}
