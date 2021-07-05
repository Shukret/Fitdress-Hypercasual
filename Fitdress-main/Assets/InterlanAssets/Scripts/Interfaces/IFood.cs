using UnityEngine;

namespace Game
{
    public interface IFood
    {
        Currency.Type type { get; }
        GameObject eatObject { get; }
    }
}