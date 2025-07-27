using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameAssets", menuName = "GameAssets/GameItems")]
public class GameAssets : ScriptableObject
{
    [SerializeField] private List<GameItems> _items;

    public List<GameItems> ItemsAssets => _items;

    public Sprite GetItemSprite(int id)
    {
        Sprite sprite = _items.Find(item => item.Id == id).ItemImage;
        return sprite;
    }
}


[Serializable]
public class GameItems
{
    [SerializeField] private int id;
    [SerializeField] private Sprite itemImage;

    public int Id => id;
    public Sprite ItemImage => itemImage;
}