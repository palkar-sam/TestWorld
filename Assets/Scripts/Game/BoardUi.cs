using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class BoardUi : MonoBehaviour
{
    [SerializeField] private BoardItemUi boardItem;
    [SerializeField] private Transform boardItemCont;
    [SerializeField] private GridLayoutGroup gridLayOutGroup;

    private List<BoardItemUi> items = new List<BoardItemUi>();

    private void OnEnable()
    {
        CreateBoard();
    }

    private void OnDisable()
    {
        DestroyItems();
    }

    private void CreateBoard()
    {
        DestroyItems();
        gridLayOutGroup.constraintCount = (int)GameManager.Instance.CurrentGameOptions;
        int totalItems = gridLayOutGroup.constraintCount * gridLayOutGroup.constraintCount;
        BoardItemUi item;
        for (int i = 0; i < totalItems; i ++)
        {
            item = Instantiate(boardItem, boardItemCont) as BoardItemUi;
            items.Add(item);
        }
    }

    private void DestroyItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Destroy(items[i].gameObject);
        }

        items.Clear();
    }
}
