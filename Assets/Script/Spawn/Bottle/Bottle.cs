using UnityEngine;

namespace Script.Spawn
{
    [CreateAssetMenu(fileName = "Bottle", menuName = "Bottle", order = 51)]
    public class Bottle : ScriptableObject
    {
        public int Power;
        public int Score;
        public TypeDrink TypeDrink;
        public GameObject Prefab;
        public float Speed;
        public AudioClip AudioClip;
        public AudioClip AudioCrash;
    }

    public enum TypeDrink
    {
        Negative,
        Positive,
        DeadDrink,
        GoldDrink,
    }
}