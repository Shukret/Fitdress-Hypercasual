using UnityEngine;

namespace Game
{
    public interface IDamageable
    {
        void Damage(Transform attackTransform);
    }
}