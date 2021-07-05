using System;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace Game
{
    public static class Settings
    {
        private static bool music = true;
        public static bool Music => music;
        
        private static bool vibration = true;
        public static bool Vibration => vibration;

                
        [RuntimeInitializeOnLoadMethod]
        static void OnRuntimeMethodLoad()
        {
            if (PlayerPrefs.HasKey(SettingsType.Music.ToString()))
            {
                music = Convert.ToBoolean(PlayerPrefs.GetInt(SettingsType.Music.ToString()));
                if (music) AudioSettings.Mobile.StartAudioOutput();
                else AudioSettings.Mobile.StopAudioOutput();
            }
            if (PlayerPrefs.HasKey(SettingsType.Vibration.ToString()))
            {
                vibration = Convert.ToBoolean(PlayerPrefs.GetInt(SettingsType.Vibration.ToString()));
            }
            MMVibrationManager.SetHapticsActive(vibration);
        }
        
        public static void Change(SettingsType type)
        {
            switch (type)
            {
                case SettingsType.Music:
                    music = !music;

                    if (music) AudioSettings.Mobile.StartAudioOutput();
                    else AudioSettings.Mobile.StopAudioOutput();

                    PlayerPrefs.SetInt(SettingsType.Music.ToString(), Convert.ToInt32(music));
                    Debug.Log("Music " + Convert.ToInt32(music));
                    
                    break;
                
                case SettingsType.Vibration:
                    vibration = !vibration;
                    MMVibrationManager.SetHapticsActive(vibration);
                    PlayerPrefs.SetInt(SettingsType.Vibration.ToString(), Convert.ToInt32(vibration));
                    Debug.Log("Vibration " + Convert.ToInt32(vibration));
                    break;
            }
        }

        public static bool GetStatus(SettingsType type)
        {
            var value = false;
            switch (type)
            {
                case SettingsType.Music:
                    value = music;

                    break;
                
                case SettingsType.Vibration:
                    value = vibration;
                    break;
            }

            return value;
        }
    }

    public enum SettingsType
    {
        Music, Vibration
    }
}