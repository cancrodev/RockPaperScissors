using System;
using UnityEngine;

[Serializable]
public class SelectedMoveData
{
    public Sprite Sprite;
    public string Name;
    public string EffectName;

    public SelectedMoveData(string name, Sprite sprite, string effectName = "")
    {
        Sprite = sprite;
        Name = name;
        EffectName = effectName;
    }
}