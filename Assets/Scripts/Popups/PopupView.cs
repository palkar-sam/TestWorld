using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupView : MonoBehaviour
{
    private static PopupView _instance;
    private static readonly object lockObj = new object();

    public static PopupView Instance => _instance;

    [SerializeField] private GenericPopupView genericPopup;

    public void ShowGenericPopups() => genericPopup.Show();

    public void HideGenericPopups() => genericPopup.Hide();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else if (_instance == null)
        {
            lock (lockObj)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
