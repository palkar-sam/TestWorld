using Popups;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GenericPopupView : PopupBase
{
    [SerializeField] private Button okayBtn;
    [SerializeField] private Text headerText;
    [SerializeField] private Text descriptionText;

    private Action OnClose;

    public void Init(string headerStr, string desc, Action onClose)
    {
        headerText.text = headerStr;
        descriptionText.text = desc;
        OnClose = onClose;
    }

    private void Start()
    {
        okayBtn.onClick.AddListener(OnOkayButtonclick);
    }

    private void OnOkayButtonclick()
    {
        Hide();
    }

    public override void Show()
    {
        //Init("Default Header", "Default Description", null);
    }

    public override void Hide()
    {
        // Close the popup or perform any other action
        gameObject.SetActive(false);
        OnClose?.Invoke();
    }
}
