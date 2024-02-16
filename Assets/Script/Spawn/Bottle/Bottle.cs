using UnityEngine;

namespace Script.Spawn
{
    [CreateAssetMenu(fileName = "Bottle", menuName = "Bottle", order = 51)]
    public class Bottle : ScriptableObject
    {
        public int Power;
        public TypeDrink TypeDrink;
    }

    public enum TypeDrink
    {
        Negative,
        Positive,
        DeadDrink,
        GoldDrink,
    }
}