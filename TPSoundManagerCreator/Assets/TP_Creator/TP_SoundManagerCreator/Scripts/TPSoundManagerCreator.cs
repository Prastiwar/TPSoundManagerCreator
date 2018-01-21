using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TP_SoundManager
{
    [RequireComponent(typeof(AudioSource))]
    public class TPSoundManagerCreator : MonoBehaviour
    {
        public TPSoundBundle ActualSoundBundle { get; set; }
        public List<TPSoundBundle> SoundBundles = new List<TPSoundBundle>();
        AudioSource Source;
        AudioSource ThemeSource;

#if UNITY_EDITOR
        void OnValidate()
        {
            Find();
        }
        void Find()
        {
            Debug.Log("halo");
            SoundBundles = FindAssetsByType<TPSoundBundle>();
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
            if (Source == null) Source = GetComponent<AudioSource>();
            if (ThemeSource == null) ThemeSource = GetComponentInChildren<AudioSource>();

            Source.playOnAwake = false;
        }

        public void SetActualBundle(TPSoundBundle _SoundBundle)
        {
            ActualSoundBundle = _SoundBundle;
        }

        public void PlayOneShot()
        {
            //AudioSource.PlayOneShot();
        }

        public void Play()
        {
            //AudioSource.Play();
        }

        public void PlayDelay(ulong delay)
        {
            //AudioSource.Play(delay);
        }
    }
}