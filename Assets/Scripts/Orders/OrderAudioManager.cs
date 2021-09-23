using UnityEngine;
using UnityEngine.Rendering;

public class OrderAudioManager : MonoBehaviour
{
    //This class handles all of the logic for order audio.

    [Header("Audio")]
    public AudioSource audioSource;
    #region Audio Clips
    [Header("Order Start Audio")]
    [SerializeField] private AudioClip[] oneStarStartAudioClips;
    [SerializeField] private AudioClip[] twoStarStartAudioClips;
    [SerializeField] private AudioClip[] threeStarStartAudioClips;
    [Header("Order One Finished Audio")]
    [SerializeField] private AudioClip[] oneStarTaskOneFinished;
    [SerializeField] private AudioClip[] oneStarTaskTwoFinished;
    [SerializeField] private AudioClip[] oneStarTaskThreeFinished;
    [SerializeField] private AudioClip[] oneStarTaskFourFinished;
    [SerializeField] private AudioClip[] oneStarTaskFiveFinished;
    [SerializeField] private AudioClip[] oneStarTaskSixFinished;
    [Header("Order Two Finished Audio")]
    [SerializeField] private AudioClip[] twoStarTaskOneFinished;
    [SerializeField] private AudioClip[] twoStarTaskTwoFinished;
    [SerializeField] private AudioClip[] twoStarTaskThreeFinished;
    [SerializeField] private AudioClip[] twoStarTaskFourFinished;
    [SerializeField] private AudioClip[] twoStarTaskFiveFinished;
    [SerializeField] private AudioClip[] twoStarTaskSixFinished;
    [Header("Order Three Finished Audio")]
    [SerializeField] private AudioClip[] threeStarTaskOneFinished;
    [SerializeField] private AudioClip[] threeStarTaskTwoFinished;
    [SerializeField] private AudioClip[] threeStarTaskThreeFinished;
    [SerializeField] private AudioClip[] threeStarTaskFourFinished;
    [SerializeField] private AudioClip[] threeStarTaskFiveFinished;
    [Header("Order Music")]
    [SerializeField] private AudioClip[] oneStarMusic;
    [SerializeField] private AudioClip[] twoStarMusic;
    [SerializeField] private AudioClip[] threeStarMusic;
    #endregion

    private FoodDamage foodDamage;
    private AudioSource drivingMusic;

    private AudioClip currentMusicClip;
    private bool playMusic = false;

    private void Start()
    {
        foodDamage = GetComponent<FoodDamage>();
        drivingMusic = GameObject.Find("CM FreeLook1/Audio Source/Music").GetComponent<AudioSource>();
    }

    private void Update()
    {
        StartOrderMusic();
    }

    //Switch statements are used to determine which clip is played in OrderStartedAudio, StartOrderMusic, OrderFinishedAudio, and OrderTimeUpAudio.
    //The order level and order number come from the order UI buttons.
    public void OrderStartedAudio(int orderLevel, int orderNumber)
    {
        if (orderLevel == 1)
        {
            switch (orderNumber)
            {
                case 0:
                    drivingMusic.Stop();
                    audioSource.clip = oneStarStartAudioClips[0];
                    audioSource.Play();
                    currentMusicClip = oneStarMusic[0];
                    playMusic = true;
                    break;
                case 1:
                    drivingMusic.Stop();
                    audioSource.clip = oneStarStartAudioClips[1];
                    audioSource.Play();
                    currentMusicClip = oneStarMusic[1];
                    playMusic = true;
                    break;
                case 2:
                    drivingMusic.Stop();
                    audioSource.clip = oneStarStartAudioClips[2];
                    audioSource.Play();
                    currentMusicClip = oneStarMusic[2];
                    playMusic = true;
                    break;
                case 3:
                    drivingMusic.Stop();
                    audioSource.clip = oneStarStartAudioClips[3];
                    audioSource.Play();
                    currentMusicClip = oneStarMusic[3];
                    playMusic = true;
                    break;
                case 4:
                    drivingMusic.Stop();
                    audioSource.clip = oneStarStartAudioClips[4];
                    audioSource.Play();
                    currentMusicClip = oneStarMusic[4];
                    playMusic = true;
                    break;
                case 5:
                    drivingMusic.Stop();
                    audioSource.clip = oneStarStartAudioClips[5];
                    audioSource.Play();
                    currentMusicClip = oneStarMusic[5];
                    playMusic = true;
                    break;
            }
        }

        if (orderLevel == 2)
        {
            switch (orderNumber)
            {
                case 0:
                    drivingMusic.Stop();
                    audioSource.clip = twoStarStartAudioClips[0];
                    audioSource.Play();
                    currentMusicClip = twoStarMusic[0];
                    playMusic = true;
                    break;
                case 1:
                    drivingMusic.Stop();
                    audioSource.clip = twoStarStartAudioClips[1];
                    audioSource.Play();
                    currentMusicClip = twoStarMusic[1];
                    playMusic = true;
                    break;
                case 2:
                    drivingMusic.Stop();
                    audioSource.clip = twoStarStartAudioClips[2];
                    audioSource.Play();
                    currentMusicClip = twoStarMusic[2];
                    playMusic = true;
                    break;
                case 3:
                    drivingMusic.Stop();
                    audioSource.clip = twoStarStartAudioClips[3];
                    audioSource.Play();
                    currentMusicClip = twoStarMusic[3];
                    playMusic = true;
                    break;
                case 4:
                    drivingMusic.Stop();
                    audioSource.clip = twoStarStartAudioClips[4];
                    audioSource.Play();
                    currentMusicClip = twoStarMusic[4];
                    playMusic = true;
                    break;
                case 5:
                    drivingMusic.Stop();
                    audioSource.clip = twoStarStartAudioClips[5];
                    audioSource.Play();
                    currentMusicClip = twoStarMusic[5];
                    playMusic = true;
                    break;
            }
        }

        if (orderLevel == 3)
        {
            switch (orderNumber)
            {
                case 0:
                    drivingMusic.Stop();
                    audioSource.clip = threeStarStartAudioClips[0];
                    audioSource.Play();
                    currentMusicClip = threeStarMusic[0];
                    playMusic = true;
                    break;
                case 1:
                    drivingMusic.Stop();
                    audioSource.clip = threeStarStartAudioClips[1];
                    audioSource.Play();
                    currentMusicClip = threeStarMusic[1];
                    playMusic = true;
                    break;
                case 2:
                    drivingMusic.Stop();
                    audioSource.clip = threeStarStartAudioClips[2];
                    audioSource.Play();
                    currentMusicClip = threeStarMusic[2];
                    playMusic = true;
                    break;
                case 3:
                    drivingMusic.Stop();
                    audioSource.clip = threeStarStartAudioClips[3];
                    audioSource.Play();
                    currentMusicClip = threeStarMusic[3];
                    playMusic = true;
                    break;
                case 4:
                    drivingMusic.Stop();
                    audioSource.clip = threeStarStartAudioClips[4];
                    audioSource.Play();
                    currentMusicClip = threeStarMusic[4];
                    playMusic = true;
                    break;
            }
        }
    }

    void StartOrderMusic()
    {
        if (playMusic)
        {
            if (!audioSource.isPlaying && !AudioListener.pause)
            {
                playMusic = false;
                drivingMusic.Stop();
                audioSource.clip = currentMusicClip;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }

    public void OrderFinishedAudio(int orderLevel, int orderNumber)
    {
        audioSource.loop = false;

        if (orderLevel == 1)
        {
            switch (orderNumber)
            {
                case 0:
                    drivingMusic.Stop();
                    if (foodDamage.m_foodQuality >= 60)
                    {
                        Debug.Log(oneStarTaskOneFinished[0].name);
                        audioSource.clip = oneStarTaskOneFinished[0];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 59 && foodDamage.m_foodQuality >= 30)
                    {
                        Debug.Log(oneStarTaskOneFinished[1].name);
                        audioSource.clip = oneStarTaskOneFinished[1];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 29)
                    {
                        Debug.Log(oneStarTaskOneFinished[2].name);
                        audioSource.clip = oneStarTaskOneFinished[2];
                        audioSource.Play();
                    }
                    break;
                case 1:
                    drivingMusic.Stop();
                    if (foodDamage.m_foodQuality >= 60)
                    {
                        audioSource.clip = oneStarTaskTwoFinished[0];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 59 && foodDamage.m_foodQuality >= 30)
                    {
                        audioSource.clip = oneStarTaskTwoFinished[1];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 29)
                    {
                        audioSource.clip = oneStarTaskTwoFinished[2];
                        audioSource.Play();
                    }
                    break;
                case 2:
                    drivingMusic.Stop();
                    if (foodDamage.m_foodQuality >= 60)
                    {
                        audioSource.clip = oneStarTaskThreeFinished[0];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 59 && foodDamage.m_foodQuality >= 30)
                    {
                        audioSource.clip = oneStarTaskThreeFinished[1];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 29)
                    {
                        audioSource.clip = oneStarTaskThreeFinished[2];
                        audioSource.Play();
                    }
                    break;
                case 3:
                    drivingMusic.Stop();
                    if (foodDamage.m_foodQuality >= 60)
                    {
                        audioSource.clip = oneStarTaskFourFinished[0];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 59 && foodDamage.m_foodQuality >= 30)
                    {
                        audioSource.clip = oneStarTaskFourFinished[1];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 29)
                    {
                        audioSource.clip = oneStarTaskFourFinished[2];
                        audioSource.Play();
                    }
                    break;
                case 4:
                    drivingMusic.Stop();
                    if (foodDamage.m_foodQuality >= 60)
                    {
                        audioSource.clip = oneStarTaskFiveFinished[0];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 59 && foodDamage.m_foodQuality >= 30)
                    {
                        audioSource.clip = oneStarTaskFiveFinished[1];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 29)
                    {
                        audioSource.clip = oneStarTaskFiveFinished[2];
                        audioSource.Play();
                    }
                    break;
                case 5:
                    drivingMusic.Stop();
                    if (foodDamage.m_foodQuality >= 60)
                    {
                        audioSource.clip = oneStarTaskSixFinished[0];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 59 && foodDamage.m_foodQuality >= 30)
                    {
                        audioSource.clip = oneStarTaskSixFinished[1];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 29)
                    {
                        audioSource.clip = oneStarTaskSixFinished[2];
                        audioSource.Play();
                    }
                    break;

            }
        }
        if (orderLevel == 2)
        {
            switch (orderNumber)
            {
                case 0:
                    drivingMusic.Stop();
                    if (foodDamage.m_foodQuality >= 60)
                    {
                        audioSource.clip = twoStarTaskOneFinished[0];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 59 && foodDamage.m_foodQuality >= 30)
                    {
                        audioSource.clip = twoStarTaskOneFinished[1];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 29)
                    {
                        audioSource.clip = twoStarTaskOneFinished[2];
                        audioSource.Play();
                    }
                    break;
                case 1:
                    drivingMusic.Stop();
                    if (foodDamage.m_foodQuality >= 60)
                    {
                        audioSource.clip = twoStarTaskTwoFinished[0];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 59 && foodDamage.m_foodQuality >= 30)
                    {
                        audioSource.clip = twoStarTaskTwoFinished[1];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 29)
                    {
                        audioSource.clip = twoStarTaskTwoFinished[2];
                        audioSource.Play();
                    }
                    break;
                case 2:
                    drivingMusic.Stop();
                    if (foodDamage.m_foodQuality >= 60)
                    {
                        audioSource.clip = twoStarTaskThreeFinished[0];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 59 && foodDamage.m_foodQuality >= 30)
                    {
                        audioSource.clip = twoStarTaskThreeFinished[1];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 29)
                    {
                        audioSource.clip = twoStarTaskThreeFinished[2];
                        audioSource.Play();
                    }
                    break;
                case 3:
                    drivingMusic.Stop();
                    if (foodDamage.m_foodQuality >= 60)
                    {
                        audioSource.clip = twoStarTaskFourFinished[0];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 59 && foodDamage.m_foodQuality >= 30)
                    {
                        audioSource.clip = twoStarTaskFourFinished[1];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 29)
                    {
                        audioSource.clip = twoStarTaskFourFinished[2];
                        audioSource.Play();
                    }
                    break;
                case 4:
                    drivingMusic.Stop();
                    if (foodDamage.m_foodQuality >= 60)
                    {
                        audioSource.clip = twoStarTaskFiveFinished[0];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 59 && foodDamage.m_foodQuality >= 30)
                    {
                        audioSource.clip = twoStarTaskFiveFinished[1];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 29)
                    {
                        audioSource.clip = twoStarTaskFiveFinished[2];
                        audioSource.Play();
                    }
                    break;
                case 5:
                    drivingMusic.Stop();
                    if (foodDamage.m_foodQuality >= 60)
                    {
                        audioSource.clip = twoStarTaskSixFinished[0];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 59 && foodDamage.m_foodQuality >= 30)
                    {
                        audioSource.clip = twoStarTaskSixFinished[1];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 29)
                    {
                        audioSource.clip = twoStarTaskSixFinished[2];
                        audioSource.Play();
                    }
                    break;
            }
        }
        if (orderLevel == 3)
        {
            switch (orderNumber)
            {
                case 0:
                    drivingMusic.Stop();
                    if (foodDamage.m_foodQuality >= 60)
                    {
                        audioSource.clip = threeStarTaskOneFinished[0];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 59 && foodDamage.m_foodQuality >= 30)
                    {
                        audioSource.clip = threeStarTaskOneFinished[1];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 29)
                    {
                        audioSource.clip = threeStarTaskOneFinished[2];
                        audioSource.Play();
                    }
                    break;
                case 1:
                    drivingMusic.Stop();
                    if (foodDamage.m_foodQuality >= 60)
                    {
                        audioSource.clip = threeStarTaskTwoFinished[0];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 59 && foodDamage.m_foodQuality >= 30)
                    {
                        audioSource.clip = threeStarTaskTwoFinished[1];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 29)
                    {
                        audioSource.clip = threeStarTaskTwoFinished[2];
                        audioSource.Play();
                    }
                    break;
                case 2:
                    drivingMusic.Stop();
                    if (foodDamage.m_foodQuality >= 60)
                    {
                        audioSource.clip = threeStarTaskThreeFinished[0];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 59 && foodDamage.m_foodQuality >= 30)
                    {
                        audioSource.clip = threeStarTaskThreeFinished[1];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 29)
                    {
                        audioSource.clip = threeStarTaskThreeFinished[2];
                        audioSource.Play();
                    }
                    break;
                case 3:
                    drivingMusic.Stop();
                    if (foodDamage.m_foodQuality >= 60)
                    {
                        audioSource.clip = threeStarTaskFourFinished[0];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 59 && foodDamage.m_foodQuality >= 30)
                    {
                        audioSource.clip = threeStarTaskFourFinished[1];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 29)
                    {
                        audioSource.clip = threeStarTaskFourFinished[2];
                        audioSource.Play();
                    }
                    break;
                case 4:
                    drivingMusic.Stop();
                    if (foodDamage.m_foodQuality >= 60)
                    {
                        audioSource.clip = threeStarTaskFiveFinished[0];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 59 && foodDamage.m_foodQuality >= 30)
                    {
                        audioSource.clip = threeStarTaskFiveFinished[1];
                        audioSource.Play();
                    }
                    if (foodDamage.m_foodQuality <= 29)
                    {
                        audioSource.clip = threeStarTaskFiveFinished[2];
                        audioSource.Play();
                    }
                    break;
            }
        }
    }

    public void OrderTimeUpAudio(int orderLevel, int orderNumber)
    {
        audioSource.loop = false;

        if (orderLevel == 1)
        {
            switch (orderNumber)
            {
                case 0:
                    drivingMusic.Stop();
                    audioSource.clip = oneStarTaskOneFinished[3];
                    audioSource.Play();
                    break;
                case 1:
                    drivingMusic.Stop();
                    audioSource.clip = oneStarTaskTwoFinished[3];
                    audioSource.Play();
                    break;
                case 2:
                    drivingMusic.Stop();
                    audioSource.clip = oneStarTaskThreeFinished[3];
                    audioSource.Play();
                    break;
                case 3:
                    drivingMusic.Stop();
                    audioSource.clip = oneStarTaskFourFinished[3];
                    audioSource.Play();
                    break;
                case 4:
                    drivingMusic.Stop();
                    audioSource.clip = oneStarTaskFiveFinished[3];
                    audioSource.Play();
                    break;
                case 5:
                    drivingMusic.Stop();
                    audioSource.clip = oneStarTaskSixFinished[3];
                    audioSource.Play();
                    break;
            }
        }
        if (orderLevel == 2)
        {
            switch (orderNumber)
            {
                case 0:
                    drivingMusic.Stop();
                    audioSource.clip = twoStarTaskOneFinished[3];
                    audioSource.Play();
                    break;
                case 1:
                    drivingMusic.Stop();
                    audioSource.clip = twoStarTaskTwoFinished[3];
                    audioSource.Play();
                    break;
                case 2:
                    drivingMusic.Stop();
                    audioSource.clip = twoStarTaskThreeFinished[3];
                    audioSource.Play();
                    break;
                case 3:
                    drivingMusic.Stop();
                    audioSource.clip = twoStarTaskFourFinished[3];
                    audioSource.Play();
                    break;
                case 4:
                    drivingMusic.Stop();
                    audioSource.clip = twoStarTaskFiveFinished[3];
                    audioSource.Play();
                    break;
                case 5:
                    drivingMusic.Stop();
                    audioSource.clip = twoStarTaskSixFinished[3];
                    audioSource.Play();
                    break;
            }
        }
        if (orderLevel == 3)
        {
            switch (orderNumber)
            {
                case 0:
                    drivingMusic.Stop();
                    audioSource.clip = threeStarTaskOneFinished[3];
                    audioSource.Play();
                    break;
                case 1:
                    drivingMusic.Stop();
                    audioSource.clip = threeStarTaskTwoFinished[3];
                    audioSource.Play();
                    break;
                case 2:
                    drivingMusic.Stop();
                    audioSource.clip = threeStarTaskThreeFinished[3];
                    audioSource.Play();
                    break;
                case 3:
                    drivingMusic.Stop();
                    audioSource.clip = threeStarTaskFourFinished[3];
                    audioSource.Play();
                    break;
                case 4:
                    drivingMusic.Stop();
                    audioSource.clip = threeStarTaskFiveFinished[3];
                    audioSource.Play();
                    break;
            }
        }
    }
}
