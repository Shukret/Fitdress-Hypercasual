using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class ClothController : MonoBehaviour
    {
        public Transform startCloth;
        [FormerlySerializedAs("cloth")] public Transform finishCloth;

        [SerializeField] private Renderer startClothRenderer;
        [SerializeField] private Renderer finishClothRenderer;
        
        public void ChangeMaterial(Material startClothMaterial, Material finishClothMaterial)
        {
            startClothRenderer.material = startClothMaterial;
            finishClothRenderer.material = finishClothMaterial;
        }
    }
}