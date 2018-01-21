using System.Collections.Generic;
using UnityEngine;

namespace TP_SoundManager
{
    [RequireComponent(typeof(AudioSource))]
    public class TPSoundManagerCreator : MonoBehaviour
    {
        public TPSoundBundle ActualSoundBundleFX;
        public TPSoundBundle ActualSoundBundleTheme;
        public List<TPSoundBundle> SoundBundles;
        public AudioSource Source { get; private set; }
        public AudioSource ThemeSource { get; private set; }

#if UNITY_EDITOR
        public void OnValidate()
        {
            SoundBundles = FindAssetsByType<TPSoundBundle>();
            Refresh();
        }
        List<T> FindAssetsByType<T>() where T : UnityEngine.Object
        {
            List<T> assets = new List<T>();
            string[] guids = UnityEditor.AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[i]);
                T asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if (asset != null)
                {
                    assets.Add(asset);
                }
            }
            return assets;
        }
#endif

        void Awake()
        {
            Refresh();
        }

        public void Refresh()
        {
            if (SoundBundles == null) SoundBundles = new List<TPSoundBundle>();
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

        AudioClip FindClipByName(string name)
        {
            int length = ActualSoundBundleFX.Sounds.Length;
            for (int i = 0; i < length; i++)
            {
                if (name == ActualSoundBundleFX.Sounds[i].AudioClipName)
                {
                    return ActualSoundBundleFX.Sounds[i].AudioClip;
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

        public void PlayOneShot(int index)
        {
            Source.PlayOneShot(ActualSoundBundleFX.Sounds[index].AudioClip);
        }

        public void PlayOneShot(string nameOfClip)
        {
            Source.PlayOneShot(FindClipByName(nameOfClip));
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

        public void PlayTheme(int index)
        {
            if (ThemeSource.isPlaying)
                ThemeSource.Stop();
            ThemeSource.clip = ActualSoundBundleTheme.Sounds[index].AudioClip;
            ThemeSource.Play();
        }

        public void PlayTheme(string nameOfClip)
        {
            if (ThemeSource.isPlaying)
                ThemeSource.Stop();
            ThemeSource.clip = FindClipByName(nameOfClip);
            ThemeSource.Play();
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