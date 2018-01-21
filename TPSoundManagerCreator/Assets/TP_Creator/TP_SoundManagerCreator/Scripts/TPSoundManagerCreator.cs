using System.Collections.Generic;
using UnityEngine;

namespace TP_SoundManager
{
    [RequireComponent(typeof(AudioSource))]
    public class TPSoundManagerCreator : MonoBehaviour
    {
        public TPSoundBundle ActualSoundBundle;
        public List<TPSoundBundle> SoundBundles = new List<TPSoundBundle>();
        AudioSource Source;
        AudioSource ThemeSource;

#if UNITY_EDITOR
        public void OnValidate()
        {
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
            if (ActualSoundBundle == null) ActualSoundBundle = SoundBundles[0];
            Source.playOnAwake = false;
        }

        AudioClip FindClipByName(string name)
        {
            int length = ActualSoundBundle.Sounds.Length;
            for (int i = 0; i < length; i++)
            {
                if (name == ActualSoundBundle.Sounds[i].AudioClipName)
                {
                    return ActualSoundBundle.Sounds[i].AudioClip;
                }
            }
            return null;
        }

        public TPSoundBundle GetBundleByName(string name)
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

        public void SetActualBundle(TPSoundBundle _SoundBundle)
        {
            ActualSoundBundle = _SoundBundle;
        }

        public TPSoundBundle GetActualBundle()
        {
            return ActualSoundBundle;
        }

        public void PlayOneShot(int index)
        {
            Source.PlayOneShot(ActualSoundBundle.Sounds[index].AudioClip);
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

        //public void PlayDelayed(float delay)
        //{
        //    Source.PlayDelayed(delay);
        //}

        //public void PlayScheduled(double time)
        //{
        //    Source.PlayScheduled(time);
        //}

        //public void Loop(bool boolean)
        //{
        //    Source.loop = boolean;
        //}

        //public bool Loop()
        //{
        //    return Source.loop;
        //}

        //public void Mute(bool boolean)
        //{
        //    Source.mute = boolean;
        //}

        //public bool Mute()
        //{
        //    return Source.mute;
        //}

        //public void SetScheduledEndTime(double time)
        //{
        //    Source.SetScheduledEndTime(time);
        //}

        //public void SetScheduledStartTime(double time)
        //{
        //    Source.SetScheduledStartTime(time);
        //}

        //public bool SetSpatializerFloat(int index, float value)
        //{
        //    return Source.SetSpatializerFloat(index, value);
        //}

        //public void SetCustomCurve(AudioSourceCurveType type, AnimationCurve curve)
        //{
        //    Source.SetCustomCurve(type, curve);
        //}

        //public void SetAmbisonicDecoderFloat(int index, float value)
        //{
        //    Source.SetAmbisonicDecoderFloat(index, value);
        //}

        //public void bypassEffects(bool boolean)
        //{
        //    Source.bypassEffects = boolean;
        //}

        //public bool bypassEffects()
        //{
        //    return Source.bypassEffects;
        //}

        //public void bypassListenerEffects(bool boolean)
        //{
        //    Source.bypassListenerEffects = boolean;
        //}

        //public bool bypassListenerEffects()
        //{
        //    return Source.bypassListenerEffects;
        //}

        //public void bypassReverbZones(bool boolean)
        //{
        //    Source.bypassReverbZones = boolean;
        //}

        //public bool bypassReverbZones()
        //{
        //    return Source.bypassReverbZones;
        //}

        //public void dopplerLevel(float scale)
        //{
        //    Source.dopplerLevel = scale;
        //}

        //public float dopplerLevel()
        //{
        //    return Source.dopplerLevel;
        //}

        //public void ignoreListenerPause(bool boolean)
        //{
        //    Source.ignoreListenerPause = boolean;
        //}

        //public bool ignoreListenerPause()
        //{
        //    return Source.ignoreListenerPause;
        //}

        //public void ignoreListenerVolume(bool boolean)
        //{
        //    Source.ignoreListenerVolume = boolean;
        //}

        //public bool ignoreListenerVolume()
        //{
        //    return Source.ignoreListenerVolume;
        //}

        //public void SetDistance(float minDistance, float maxDistance)
        //{
        //    Source.minDistance = minDistance;
        //    Source.maxDistance = maxDistance;
        //}

        //public void GetDistance(out float minDistance, out float maxDistance)
        //{
        //    minDistance = Source.minDistance;
        //    maxDistance = Source.maxDistance;
        //}

        //public void outputAudioMixerGroup(UnityEngine.Audio.AudioMixerGroup AudioMixer)
        //{
        //    Source.outputAudioMixerGroup = AudioMixer;
        //}

        //public UnityEngine.Audio.AudioMixerGroup outputAudioMixerGroup()
        //{
        //    return Source.outputAudioMixerGroup;
        //}

        //public void panStereo(float pans)
        //{
        //    Source.panStereo = pans;
        //}

        //public float panStereo()
        //{
        //    return Source.panStereo;
        //}

        //public void pitch(float amount)
        //{
        //    Source.pitch = amount;
        //}

        //public float pitch()
        //{
        //    return Source.pitch;
        //}

        //public void priority(int amount)
        //{
        //    Source.priority = amount;
        //}

        //public int priority()
        //{
        //    return Source.priority;
        //}

        //public void reverbZoneMix(float amount)
        //{
        //    Source.reverbZoneMix = amount;
        //}

        //public float reverbZoneMix()
        //{
        //    return Source.reverbZoneMix;
        //}

        //public void rolloffMode(AudioRolloffMode mode)
        //{
        //    Source.rolloffMode = mode;
        //}

        //public AudioRolloffMode rolloffMode()
        //{
        //    return Source.rolloffMode;
        //}

        //public void spatialBlend(float amount)
        //{
        //    Source.spatialBlend = amount;
        //}

        //public float spatialBlend()
        //{
        //    return Source.spatialBlend;
        //}

        //public void spatialize(bool boolean)
        //{
        //    Source.spatialize = boolean;
        //}

        //public bool spatialize()
        //{
        //    return Source.spatialize;
        //}

        //public void spatializePostEffects(bool boolean)
        //{
        //    Source.spatializePostEffects = boolean;
        //}

        //public bool spatializePostEffects()
        //{
        //    return Source.spatializePostEffects;
        //}

        //public void spread(float amount)
        //{
        //    Source.spread = amount;
        //}

        //public float spread()
        //{
        //    return Source.spread;
        //}

        //public void time(float amount)
        //{
        //    Source.time = amount;
        //}

        //public float time()
        //{
        //    return Source.time;
        //}

        //public void timeSamples(int amount)
        //{
        //    Source.timeSamples = amount;
        //}

        //public int timeSamples()
        //{
        //    return Source.timeSamples;
        //}

        //public void velocityUpdateMode(AudioVelocityUpdateMode mode)
        //{
        //    Source.velocityUpdateMode = mode;
        //}

        //public AudioVelocityUpdateMode velocityUpdateMode()
        //{
        //    return Source.velocityUpdateMode;
        //}

        //public void volume(float amount)
        //{
        //    Source.volume = amount;
        //}

        //public float volume()
        //{
        //    return Source.volume;
        //}

        //public bool isVirtual()
        //{
        //    return Source.isVirtual;
        //}

        //public bool isPlaying()
        //{
        //    return Source.isPlaying;
        //}


        // **** Theme **** ///

        public void PlayTheme(int index)
        {
            ThemeSource.clip = ActualSoundBundle.Sounds[index].AudioClip;
            ThemeSource.Play();
        }

        public void PlayTheme(string nameOfClip)
        {
            ThemeSource.clip = FindClipByName(nameOfClip);
            ThemeSource.Play();
        }

        public void PlayTheme(ulong delay)
        {
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