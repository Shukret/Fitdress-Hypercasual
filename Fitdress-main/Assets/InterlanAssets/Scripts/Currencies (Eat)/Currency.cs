using System;
using UnityEngine;

namespace Game
{
    public static class Currency
    {
        public delegate void OnCurrencyChange(Type type, ChangeType changeType);
        public static event OnCurrencyChange onCurrencyChange;

        private static int crystals;
        private static int humans;

        private static int crystalsToAdd;
        private static int humansToAdd;

        public static void Init()
        {
            crystalsToAdd = 0;
            humansToAdd = 0;
        }
        
        public static void CurrencyAdd(Type type, int amount)
        {
            switch (type)
            {
                case Type.Crystals:
                    crystalsToAdd += amount;
                    break;
                case Type.Food:
                    humansToAdd += amount;
                    break;
            }
            
            onCurrencyChange?.Invoke(type, ChangeType.Add);
        }
        
        public static void CurrencyRemove(Type type, int amount)
        {
            switch (type)
            {
                case Type.Crystals:
                    crystalsToAdd -= amount;
                    break;
                case Type.Food:
                    humansToAdd -= amount;
                    break;
            }
            
            onCurrencyChange?.Invoke(type, ChangeType.Remove);
        }

        public static int Crystals => crystals + crystalsToAdd;
        public static int Humans => humans + humansToAdd;

        public static void Save()
        {
            crystals += crystalsToAdd;
            crystalsToAdd = 0;
            
            humans += humansToAdd;
            humansToAdd = 0;
            
            onCurrencyChange?.Invoke(Type.Crystals, ChangeType.Save);
            onCurrencyChange?.Invoke(Type.Food, ChangeType.Save);
        }

        public enum Type
        {
            Crystals, Food
        }
        
        public enum ChangeType
        {
            Add, Remove, Save
        }
    }
}