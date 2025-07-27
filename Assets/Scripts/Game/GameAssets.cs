using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameAssets", menuName = "GameAssets/GameItems")]
public class GameAssets : ScriptableObject
{
    [SerializeField] private List<GameItems> _items;
    [SerializeField] private List<GameSounds> _sounds;

    public List<GameItems> ItemsAssets => _items;

    public Sprite GetItemSprite(int id)
    {
        Sprite sprite = _items.Find(item => item.Id == id).ItemImage;
        return sprite;
    }

    public AudioClip GetSoundClip(SoundIds id)
    {
        AudioClip clip = _sounds.Find(item => item.SoundIds == id).SoundClip;
        return clip;
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

[Serializable]
public class GameSounds
{
    [SerializeField] private SoundIds id;
    [SerializeField] private AudioClip clip;

    public SoundIds SoundIds => id;
    public AudioClip SoundClip => clip;
}

public enum SoundIds
{
    MUSIC,
    FLIP_SFX,
    COLLECT_SFS
}