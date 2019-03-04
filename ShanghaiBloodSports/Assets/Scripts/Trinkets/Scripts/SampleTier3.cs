using UnityEngine;
using UnityEditor;
using Assets.Scripts.Events;

[CreateAssetMenu(menuName = "Trinket/SampleTier3")]
public class SampleTier3 : Trinket
{
    private Sprite sprite;
    private EventHandler handler = new Handler();

    public string _label;
    public string _description;
    public int _shelf;
    public Sprite _sprite;

    public override string Label => _label;
    public override string Description => _description;
    public override int Shelf => _shelf;
    public override Sprite Sprite => _sprite;
    public override EventHandler EventHandler => handler;

    private class Handler : EventHandler
    {
        public override void onEvent(HitEvent e)
        {
            Debug.Log("Hit event occurred");
        }
    }
}
