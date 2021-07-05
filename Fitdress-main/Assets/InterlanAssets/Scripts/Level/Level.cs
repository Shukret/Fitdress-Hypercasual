using System;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Game
{
    public class Level : MonoBehaviour
    {
        public BodySize targetBodySize;
        public FinishScript finishScript;

        public Material skyMaterial;
        
        public GameObject[] foodPrefabs;

        public Material startClothMaterial;
        public Material finishClothMaterial;
        
        private void Awake()
        {
            if (skyMaterial)
            {
                RenderSettings.skybox = skyMaterial;
            }
            
            finishScript.Init();
            finishScript.onPassed += OnFinishScriptPassed;

            finishScript.clothRenderer.material = finishClothMaterial;
            
            PlayerController.main.clothController.ChangeMaterial(startClothMaterial, finishClothMaterial);
            
            var foods = GetComponentsInChildren<Food>();

            for (var foodIndex = 0; foodIndex < foods.Length; foodIndex++)
            {
                var random = UnityEngine.Random.Range(0, foodPrefabs.Length);
                var foodPrefab = foodPrefabs[random];
                foods[foodIndex].Init(foodPrefab);
            }
        }

        private void OnFinishScriptPassed(FinishScript fin, PlayerController playerController)
        {
            fin.Init(playerController);
        }
    }
}