using Radishmouse;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio_Evidence : MonoBehaviour
{
    public GameObject audioMenu;
    public List<AudioClip> audioClips = new List<AudioClip>();
    public List<AudioSource> audioSources = new List<AudioSource>();
    public GameObject audioPlayer;
    public float audioLength;
    public Slider audioSlider;
    public bool changingSlider;
    public float currentLength;

    public AudioMixer audioMixer;

    public float distortFrequency;
    public Tuning_Dial dialDistortion;
    public Tuning_Dial dialStatic;

    public float frequencyMax;
    public float frequencyMin;
    public float frequencyDistortionDifference;
    public float frequencyStaticDifference;
    public float lowPassMax;
    public float lowPassMin;
    public float lowPassRate;
    public float distortionMax;
    public float distortionMin;
    public float distortionRate;
    public float pitchShiftMax;
    public float pitchShiftMin;
    public float pitchShiftRate;

    public float volumeMax;
    public float volumeMin;

    public float pitchShift;
    public float distortion;
    public float lowpass;

    public float correctnessThreshold;

    public float staticFrequency;

    [Header("Tuning Features")]
    public float frequency;
    public UILineRenderer lineRendererDistortion;
    public UILineRenderer lineRendererStatic;
    public float noise;
    public float errorAmount;

    public Image spectrogramImage;
    public Sprite loadingImage;
    public Sprite spectrogramSprite;
    public bool analyzingSpectrogram;
    public void DrawLineDistortion(float error)
    {
        if (error <= correctnessThreshold)
        {
            lineRendererDistortion.color = Color.green;

            for (int i = 0; i < lineRendererDistortion.points.Length; i++)
            {
                lineRendererDistortion.points[i].y = 0f;
            }
        }
        else
        {
            lineRendererDistortion.color = Color.red;
            float noiseAmount = noise * error;
            for (int i = 0; i < lineRendererDistortion.points.Length; i++)
            {
                float t = (float)i / (lineRendererDistortion.points.Length - 1);
                float y =
                    Mathf.Sin((t + Time.time) * frequency * Mathf.PI * 2f) +
                    UnityEngine.Random.Range(-noiseAmount, noiseAmount);

                lineRendererDistortion.points[i] = new Vector3(lineRendererDistortion.points[i].x, y, 0);
            }
        }
        lineRendererDistortion.SetVerticesDirty();
    }

    public void DrawLineStatic(float error)
    {
        if (error <= correctnessThreshold)
        {
            lineRendererStatic.color = Color.green;

            for (int i = 0; i < lineRendererStatic.points.Length; i++)
            {
                lineRendererStatic.points[i].y = 0f;
            }
        }
        else
        {
            lineRendererStatic.color = Color.red;
            float noiseAmount = noise * error;
            for (int i = 0; i < lineRendererStatic.points.Length; i++)
            {
                float t = (float)i / (lineRendererStatic.points.Length - 1);
                float y =
                    Mathf.Sin((t + Time.time) * frequency * Mathf.PI * 2f) +
                    UnityEngine.Random.Range(-noiseAmount, noiseAmount);

                lineRendererStatic.points[i] = new Vector3(lineRendererStatic.points[i].x, y, 0);
            }
        }
        lineRendererStatic.SetVerticesDirty();
    }
    private void Update()
    {

        if (audioSources[0].isPlaying && !changingSlider)
        {
            audioSlider.value = audioSources[0].time;

        }

        float errorDistortionSound = Mathf.Abs(distortFrequency - dialDistortion.frequency);
        if(errorDistortionSound > correctnessThreshold)
        {
            AdjustDistortion();
        }
        else
        {
            CorrectAudio();
        }

        float errorStaticSound = Mathf.Abs(staticFrequency - dialStatic.frequency);
        errorAmount = errorStaticSound;
        if (errorStaticSound > correctnessThreshold)
        {
            AdjustStatic(errorStaticSound);
        }
        else
        {
            CorrectStaticAudio();
        }


        lineRendererDistortion.gameObject.SetActive(true);
        float errorDistortion = Mathf.Abs(dialDistortion.frequency - distortFrequency);

        if (audioSources[0].isPlaying)
        {
            DrawLineDistortion(errorDistortion);
        }
        else
        {
            lineRendererDistortion.color = Color.black;

            for (int i = 0; i < lineRendererDistortion.points.Length; i++)
            {
                lineRendererDistortion.points[i].y = 0f;
            }
        }
        lineRendererStatic.gameObject.SetActive(true);
        float errorStatic = Mathf.Abs(dialStatic.frequency - staticFrequency);

        if (audioSources[0].isPlaying)
        {
            DrawLineStatic(errorStatic);
        }
        else
        {
            lineRendererStatic.color = Color.black;

            for (int i = 0; i < lineRendererStatic.points.Length; i++)
            {
                lineRendererStatic.points[i].y = 0f;
            }
        }

        if(analyzingSpectrogram)
        {
            spectrogramImage.sprite = loadingImage; 
        }


    }
    public void Start()
    {
        staticFrequency = UnityEngine.Random.Range(frequencyMin, frequencyMax);
        GetAudioComponents();


        float longestAudio = 0;
        foreach(AudioClip clip in audioClips)
        {
            float tempAudioLength = clip.length;

            if(tempAudioLength > longestAudio)
            {
                longestAudio = tempAudioLength;
            }
        }

        audioLength = longestAudio;
        audioSlider.maxValue = audioLength;

        AudioDistortion();
        AudioStatic();
    }

    public void GetAudioComponents()
    {
        audioSources = audioPlayer.GetComponents<AudioSource>().ToList();
    }


    public void PlayAudio()
    {
        int i = 0;
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = audioClips[i];
                audioSource.Play();
                i++;
            }
            else
            {
                audioSource.clip = audioClips[i];
                audioSource.UnPause();
                i++;
            }
        }
    }

    public void StopAudio()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
        }
    }

    public void PauseAudio()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Pause();
        }
    }

    public void ReverseAudio()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            float currentPitch = audioSource.pitch;
            float newPitch = currentPitch * -1f;
            audioSource.pitch = newPitch;
            PlayAudio();
        }
    }

    public void PlayAtSpecificPoint()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSlider.value < audioLength)
            {
                audioSource.time = audioSlider.value;
                PlayAudio();
            }
        }
    }

    public void AnalyzeSpectrogram()
    {
        analyzingSpectrogram = true;
        StartCoroutine(WaitingToAnalyzeSpectrogram());
    }

    public IEnumerator WaitingToAnalyzeSpectrogram()
    {
        yield return new WaitForSeconds(audioLength);
        analyzingSpectrogram = false;
        spectrogramImage.sprite = spectrogramSprite;
    }

    public void AudioDistortion()
    {
        distortFrequency = UnityEngine.Random.Range(frequencyMin, frequencyMax);
        float distortionDifferenceMax = Mathf.Abs(distortFrequency - frequencyMax);
        float distortionDifferenceMin = Mathf.Abs(distortFrequency - frequencyMin);

        if(distortionDifferenceMax > distortionDifferenceMin)
        {
            frequencyDistortionDifference = distortionDifferenceMax; 
        }
        else
        {
            frequencyDistortionDifference = distortionDifferenceMin;
        }

        lowPassRate = (lowPassMax - lowPassMin) / frequencyDistortionDifference;
        pitchShiftRate = (pitchShiftMax - pitchShiftMin) / frequencyDistortionDifference;
        distortionRate = (distortionMax - distortionMin) / frequencyDistortionDifference;
    }

    public void AdjustDistortion()
    {
        float error = Mathf.Abs(distortFrequency - dialDistortion.frequency);
        float t = Mathf.InverseLerp(0f, frequencyDistortionDifference, error);

        lowpass = Mathf.Lerp(lowPassMax, lowPassMin, t);
        pitchShift = Mathf.Lerp(pitchShiftMax, pitchShiftMin, t);
        distortion = Mathf.Lerp(distortionMax, distortionMin, t);

        audioMixer.SetFloat("Lowpass", lowpass);
        audioMixer.SetFloat("PitchShift1", pitchShift);
        audioMixer.SetFloat("PitchShift2", pitchShift);
        audioMixer.SetFloat("Distortion", distortion);
    }

    public void CorrectAudio()
    {
        audioMixer.SetFloat("PitchShift1", pitchShiftMax);
        audioMixer.SetFloat("PitchShift2", pitchShiftMax);
        audioMixer.SetFloat("Distortion", distortionMax);
        audioMixer.SetFloat("Lowpass", lowPassMax);
    }

    public void AudioStatic()
    {
        float distortionDifferenceMax = Mathf.Abs(staticFrequency - frequencyMax);
        float distortionDifferenceMin = Mathf.Abs(staticFrequency - frequencyMin);

        if (distortionDifferenceMax > distortionDifferenceMin)
        {
            frequencyStaticDifference = distortionDifferenceMax;
        }
        else
        {
            frequencyStaticDifference = distortionDifferenceMin;
        }
    }

    public void AdjustStatic(float error)
    {
        float t = Mathf.InverseLerp(0f, frequencyStaticDifference, error);
        audioSources[2].volume = Mathf.Lerp(volumeMax, volumeMin, t);
    }

    public void CorrectStaticAudio()
    {
        audioSources[2].volume = volumeMax;
    }

    public void ChangingSlider()
    {
        changingSlider = true;
        StopAudio();
    }
    
    public void NotChangingSlider()
    {
        changingSlider = false;
    }

    public void CloseAudio()
    {
        StopAudio();
        audioMenu.SetActive(false);
    }

    public void OpenAudio()
    {
        audioMenu.SetActive(true);
    }



}
