/**
*   Authored by Tomasz Piowczyk
*   Copyright 2018 You're allowed to make changes in functionality and use for commercial or personal.
*   You're not allowed to claim ownership of this script. 
*   https://github.com/Prastiwar/TPSoundManagerCreator
*   Unity 2017.3.0f
*/
using System.Collections.Generic;
using UnityEngine;

namespace TP.SoundManager
{
    [RequireComponent(typeof(AudioSource))]
    public class TPSoundManagerCreator : MonoBehaviour
    {
        public static bool DebugMode;
        public TPSoundBundle ActualSoundBundleFX;
        public TPSoundBundle ActualSoundBundleTheme;
        public AudioSource Source { get { return _Source; } private set { _Source = value; } }
        public AudioSource ThemeSource { get { return _ThemeSource; } private set { _ThemeSource = value; } }
        [SerializeField] AudioSource _Source;
        [SerializeField] AudioSource _ThemeSource;
        public List<TPSoundBundle> SoundBundles;


        void Awake()
        {
            Refresh();
        }

        public void Refresh()
        {
            if (SoundBundles == null) SoundBundles = new List<TPSoundBundle>();
#if UNITY_EDITOR
            SoundBundles = Utilities.TPFind.FindAssetsByType<TPSoundBundle>();
#endif
            if (Source == null)
            {
                Source = GetComponent<AudioSource>();
                Source.playOnAwake = false;
            }
            if (ThemeSource == null && transform.childCount > 0)
            {
                ThemeSource = transform.GetChild(0).GetComponent<AudioSource>();
                ThemeSource.playOnAwake = false;
                ThemeSource.loop = true;
            }
            if (ActualSoundBundleFX == null) ActualSoundBundleFX = SoundBundles[0];
            if (ActualSoundBundleTheme == null) ActualSoundBundleTheme = SoundBundles[0];
        }


        // **** Finders **** //

        public AudioClip FindAudioClip(TPSoundBundle soundBundle, string nameOfClip)
        {
            int length = soundBundle.Sounds.Length;
            for (int i = 0; i < length; i++)
            {
                if (nameOfClip == soundBundle.Sounds[i].AudioClipName)
                {
                    return soundBundle.Sounds[i].AudioClip;
                }
            }
            return null;
        }

        public AudioClip FindAudioClip(bool fromActualFX, string nameOfClip)
        {
            if(fromActualFX)
                return FindAudioClip(ActualSoundBundleFX, nameOfClip);
            else
                return FindAudioClip(ActualSoundBundleTheme, nameOfClip);
        }

        public AudioClip FindAudioClip(bool fromActualFX, int index)
        {
            if(fromActualFX)
                return ActualSoundBundleFX.Sounds[index].AudioClip;
            else
                return ActualSoundBundleTheme.Sounds[index].AudioClip;
        }

        public AudioClip FindAudioClip(string nameOfBundle, string nameOfClip)
        {
            TPSoundBundle bundle = GetSoundBundleByName(nameOfBundle);
            int length = bundle.Sounds.Length;
            for (int i = 0; i < length; i++)
            {
                if (nameOfClip == bundle.Sounds[i].AudioClipName)
                {
                    return bundle.Sounds[i].AudioClip;
                }
            }
            return null;
        }
        
        public TPSoundBundle GetSoundBundleByName(string name)
        {
            int length = SoundBundles.Count;
            for (int i = 0; i < length; i++)
            {
                if (name == SoundBundles[i].name)
                {
                    return SoundBundles[i];
                }
            }
            return null;
        }



        // *** FX **** //

        public void PlayOneShot(string nameOfBundle, int index)
        {
            Source.PlayOneShot(GetSoundBundleByName(nameOfBundle).Sounds[index].AudioClip);
        }

        public void PlayOneShot(string nameOfBundle, string nameOfClip)
        {
            Source.PlayOneShot(FindAudioClip(nameOfBundle, nameOfClip));
        }

        public void PlayOneShot(int index)
        {
            Source.PlayOneShot(ActualSoundBundleFX.Sounds[index].AudioClip);
        }

        public void PlayOneShot(string nameOfClip)
        {
            Source.PlayOneShot(FindAudioClip(true, nameOfClip));
        }

        public void Play()
        {
            Source.Play();
        }

        public void Play(ulong delay)
        {
            Source.Play(delay);
        }

        public void Stop()
        {
            Source.Stop();
        }

        public void Pause()
        {
            Source.Pause();
        }

        public void UnPause()
        {
            Source.UnPause();
        }



        // *** Theme *** //

        public void PlayTheme()
        {
            if (ThemeSource.isPlaying)
                ThemeSource.Stop();
            ThemeSource.Play();
        }

        public void PlayTheme(string nameOfBundle, int index)
        {
            ThemeSource.clip = GetSoundBundleByName(nameOfBundle).Sounds[index].AudioClip;
            PlayTheme();
        }

        public void PlayTheme(string nameOfBundle, string nameOfClip)
        {
            ThemeSource.clip = FindAudioClip(nameOfBundle, nameOfClip);
            PlayTheme();
        }

        public void PlayTheme(int index)
        {
            ThemeSource.clip = ActualSoundBundleTheme.Sounds[index].AudioClip;
            PlayTheme();
        }

        public void PlayTheme(string nameOfClip)
        {
            ThemeSource.clip = FindAudioClip(false, nameOfClip);
            PlayTheme();
        }

        public void PlayTheme(ulong delay)
        {
            if (ThemeSource.isPlaying)
                ThemeSource.Stop();
            ThemeSource.Play(delay);
        }

        public void PauseTheme()
        {
            ThemeSource.Pause();
        }

        public void UnPauseTheme()
        {
            ThemeSource.UnPause();
        }

        public void StopTheme()
        {
            ThemeSource.Stop();
        }
    }
}