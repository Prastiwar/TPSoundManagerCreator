using UnityEngine;
using TP_SoundManager;

public class DemoSoundScript : MonoBehaviour
{
    TPSoundManagerCreator creator;
    [SerializeField] TPSoundBundle SecondBundle;

    void Awake()
    {
        creator = FindObjectOfType<TPSoundManagerCreator>();
    }

    public void ChangeFirstBundle()
    {
        creator.SetActualBundle(creator.GetBundleByName("FirstBundle"));
    }

    public void ChangeSecondBundle()
    {
        creator.SetActualBundle(SecondBundle);
    }

    public void PlaySound()
    {
        creator.PlayOneShot("First");
    }
}
