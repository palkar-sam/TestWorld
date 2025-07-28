using System.Collections;
using UnityEngine;

namespace Popups
{
    public abstract class PopupBase : MonoBehaviour
    {
        public abstract void Show();
        public abstract void Hide();
    }
}