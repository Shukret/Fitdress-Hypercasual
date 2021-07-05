using UnityEngine;

namespace Game
{
    public class Crystal : MonoBehaviour, IFood
    {
        public Currency.Type type => Currency.Type.Crystals;
        public GameObject eatObject => gameObject;
    }
}