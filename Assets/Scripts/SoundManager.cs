using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioSource musicSource;   // ✅ tek source

    [SerializeField] AudioSource[] voiceEffects;
    [SerializeField] AudioSource[] vocals;

    public bool IsMusicPlay = true;
    public bool IsEffectPlay = true; //ses efekti çalsın mı?

    public IconOnOffManager musicIcon;
    public IconOnOffManager fxIcon;

    private void Awake()
    {
        if (instance != null && instance != this) 
        { 
            Destroy(gameObject); 
            return; 
        }

        instance = this;
    }

    private void Start()
    {
        var randomMusicClip = ChoiceRandomClip(musicClips);
        PlayBackgroundMusic(randomMusicClip);
    }

    public void OutVocalVoice()//vokal seslerini çıkarmak için
    {
        if(IsEffectPlay)
        {
            AudioSource source=vocals[Random.Range(0, vocals.Length)];
       
            source.Stop();
            source.Play();

        }
        

    }




    public void OutVoiceEffect(int whichVoice)//ses efekti çıkar hangi ses üzerinden çıkaracaksa
    {
        if(IsEffectPlay && whichVoice < voiceEffects.Length)//voiceEffects elemanının dizin sayısına göre göndersin yanlışlık olmasın 
        {
            voiceEffects[whichVoice].Stop();
            voiceEffects[whichVoice].Play();//önce sesi durduracağız sonra çalıştırmaya başlayacağız
        }

    }

    AudioClip ChoiceRandomClip(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return null;
        return clips[Random.Range(0, clips.Length)];
    }

    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        if (musicClip == null || musicSource == null || !IsMusicPlay) 
        return;

        musicSource.clip = musicClip;
        musicSource.Play();
    }

    void UpdateMusicFNC()
    {
        if(musicSource.isPlaying != IsMusicPlay)
        {
            if(IsMusicPlay)
            {
                var randomMusicClip = ChoiceRandomClip(musicClips);
                PlayBackgroundMusic(randomMusicClip);
            }
            else
            {
                musicSource.Stop();
            }
        }
    }


    public void MusıcOnOffFNC()
    {
        IsMusicPlay = !IsMusicPlay;
        UpdateMusicFNC();
        musicIcon.iconOnOffFNC(IsMusicPlay);
    }

    public void FxOnOffFNC()
    {
        IsEffectPlay = !IsEffectPlay;

        fxIcon.iconOnOffFNC(IsEffectPlay);//iconların ekranda değişmesini gösteriyor
    }



}
