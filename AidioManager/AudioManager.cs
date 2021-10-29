using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[AddComponentMenu("Game Managers/Audio Manager")]
public class AudioManager : MonoBehaviour{

    public static AudioManager instance = null;
    public static AudioSettingsModel settings = null;
    private static string _settings_path = "";

    void Awake(){
        _settings_path = Application.persistentDataPath + "/audioSettings.gdf";

        if (instance == null)
		{
            instance = this;
        }

        DontDestroyOnLoad(gameObject);

        InitializeSettings();
    }

    private void InitializeSettings(){
        if (settings == null) settings = new AudioSettingsModel();
        if (File.Exists(_settings_path))
		{
            loadSettings();
        }
    }

    public void loadSettings()
	{
        string _data = File.ReadAllText(_settings_path);
        settings = JsonUtility.FromJson<AudioSettingsModel>(_data);
    }

    public void saveSettings()
	{
        string _json_data = JsonUtility.ToJson(settings);
        File.WriteAllText(_settings_path, _json_data);
    }

    public delegate void AudioSettingsChanged();
    public event AudioSettingsChanged OnAudioSettingsChanged;

    public void toggleSounds(bool enabled)
	{
        settings.sounds = enabled;
        saveSettings(_settings_path, settings);
        if (OnAudioSettingsChanged != null) OnAudioSettingsChanged();
    }

    public void toggleMusic(bool enabled)
	{
        settings.music = enabled;
        saveSettings(_settings_path, settings);
        if (OnAudioSettingsChanged != null) OnAudioSettingsChanged();
    }
}