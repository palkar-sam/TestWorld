using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using static UnityEditor.Progress;

public class BoardUi : MonoBehaviour
{
    [SerializeField] private BoardItemUi boardItem;
    [SerializeField] private Transform boardItemCont;
    [SerializeField] private GridLayoutGroup gridLayOutGroup;

    public event Action<int, int> OnScoreUpdate;

    private List<BoardItemUi> items = new List<BoardItemUi>();
    private List<int> _collectedIds = new List<int>();
    private List<int> _allIdsCollectedList = new List<int>();
    private List<BoardItemUi> _last2ItemsList = new List<BoardItemUi>();
    private int _collectedCount;
    private int _totalItems;
    private int _totalClicks;

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
        int rows = gridLayOutGroup.constraintCount = (int)GameManager.Instance.CurrentGameOptions;
        _totalItems = rows * rows;
        BoardItemUi item;

        int[,] matrix = EquallyDistributedMatrix.GetMatrix(rows);

        int id = 0;
        int index = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < rows; col++)
            {
                id = matrix[row, col];
                item = Instantiate(boardItem, boardItemCont) as BoardItemUi;
                item.SetData(index, id, GameManager.Instance.GameAssets.GetItemSprite(id));
                items.Add(item);
                item.OnItemClick += OnItemClick;
                index++;
            }
        }
    }

    private void OnItemClick(BoardItemUi item)
    {
        item.OnItemClick -= OnItemClick;
        _allIdsCollectedList.Add(item.ItemIndex);
        _totalClicks++;

        bool isMatched = false;
        if (_collectedIds.Contains(item.ItemId))
        {
            _collectedIds.Remove(item.ItemId);
            _collectedCount++;
            isMatched = true;
        }
        else
        {
            _collectedIds.Add(item.ItemId);
        }

        _last2ItemsList.Add(item);
        if (_last2ItemsList.Count == 2)
        {
            for(int i = 0; i< _last2ItemsList.Count; i++)
            {   
                if (isMatched)
                {
                    _last2ItemsList[i].ShowHideAnim(0.2f, null);
                }
                else
                {
                    _last2ItemsList[i].ResetItem();
                    _last2ItemsList[i].OnItemClick += OnItemClick;
                    _allIdsCollectedList.Remove(_last2ItemsList[i].ItemIndex);
                }
            }

            _last2ItemsList.Clear();
        }

        OnScoreUpdate?.Invoke(_collectedCount, _totalClicks);

        if (_allIdsCollectedList.Count == _totalItems)
            EventManager.TriggerEvent(GameEvents.ON_BOARD_COMPLETE);
    }

    private void DestroyItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Destroy(items[i].gameObject);
        }

        items.Clear();
        _allIdsCollectedList.Clear();
        _totalItems = 0;
        _collectedCount = 0;
        _totalClicks = 0;
    }
}
