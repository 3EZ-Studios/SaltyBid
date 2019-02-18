using Assets.Scripts.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trinket : ScriptableObject
{
    public abstract string Label { get; }
    public abstract string Description { get; }
    public abstract int Shelf { get; }
    public abstract Sprite Sprite { get; }
    public abstract EventHandler EventHandler { get; }
}
