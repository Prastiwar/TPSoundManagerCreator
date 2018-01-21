using UnityEngine;
using TP_SoundManager;

public class DemoSoundScript : MonoBehaviour
{
    TPSoundManagerCreator creator;
    [SerializeField] TPSoundBundle SecondBundle;
    [SerializeField] TPSoundBundle ThemeBundle;

    void Awake()
    {
        creator = FindObjectOfType<TPSoundManagerCreator>();
        creator.ActualSoundBundleTheme = ThemeBundle;
        creator.PlayTheme(0);
    }

    public void ChangeFirstBundle()
    {
        creator.ActualSoundBundleFX = creator.GetSoundBundleByName("FirstBundle");
    }

    public void ChangeSecondBundle()
    {
        creator.ActualSoundBundleFX = SecondBundle;
    }

    public void PlaySound()
    {
        creator.PlayOneShot("First");
    }
}
