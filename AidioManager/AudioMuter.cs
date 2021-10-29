using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Audio/Audio Muter Component")]
public class AudioMuter : MonoBehaviour {
	
    public bool is_music = false; // Данный флаг дает понять нашему классу, является ли AudioSource звуком или музыкой.


    private AudioSource _as;
    private float _base_volume = 1F;

    void Start()
	{
        _as = this.gameObject.GetComponent<AudioSource>();
        _base_volume = _as.volume;

        AudioManager.instance.OnAudioSettingsChanged += _audioSettingsChanged;

        //проверить текущее состояние звуков/музыки
        _audioSettingsChanged();
    }

    void OnDestroy()
	{
        AudioManager.instance.OnAudioSettingsChanged -= _audioSettingsChanged;
    }

    private void _audioSettingsChanged()
	{
        if (is_music)
            _as.volume = (AudioManager.settings.music) ? _base_volume : 0F;
        if (!is_music)
            _as.volume = (AudioManager.settings.sounds) ? _base_volume : 0F;
    }
}