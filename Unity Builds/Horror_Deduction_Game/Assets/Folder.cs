using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.Audio;
public class Folder : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public Camera_Animator cameraAnimator;
    public List<Evidence_Data> evidence;
    public List<GameObject> physicalEvidence;
    public Vector3 folderPoint;

    public Vector3 storyPoint;
    public Vector3 policeReportPoint;
    public Vector3 videoPoint;
    public Vector3 audioPoint;
    public Vector3 imagePoint;

    public void InitializeEvidence()
    {
        for (int i = 0; i < evidence.Count; i++)
        {
            GameObject evidencePrefab = Instantiate(evidence[i].evidencePrefab, folderPoint, Quaternion.identity);
            ProcessEvidence(evidencePrefab,evidence[i]);
            physicalEvidence.Add(evidencePrefab);
        }
    }

    public void ProcessEvidence(GameObject evidenceObject, Evidence_Data evidence)
    {
        Evidence_Data.Evidence evidenceType = evidence.evidenceType;

        switch(evidenceType)
        {
            case Evidence_Data.Evidence.Story:
                ProcessStory(evidenceObject, evidence);
                break;
            case Evidence_Data.Evidence.PoliceReport:
                ProcessPoliceReport(evidenceObject, evidence);
                break;
            case Evidence_Data.Evidence.Video:
                ProcessVideo(evidenceObject, evidence);
                break;
            case Evidence_Data.Evidence.Audio:
                ProcessAudio(evidenceObject, evidence);
                break;
            case Evidence_Data.Evidence.Image:
                break;
        }
    }

    public void ProcessStory(GameObject evidenceObject, Evidence_Data evidence)
    {
        Story_Evidence storyEvidence = evidenceObject.GetComponent<Story_Evidence>();
        storyEvidence.evidenceSO = evidence;
        storyEvidence.InitializePaper();
    }

    public void ProcessPoliceReport(GameObject evidenceObject, Evidence_Data evidence)
    {
        Police_Report_Evidence policeReportEvidence = evidenceObject.GetComponent<Police_Report_Evidence>();
        policeReportEvidence.evidenceSO = evidence;
        policeReportEvidence.InitializePoliceReport();
    }

    public void ProcessVideo(GameObject evidenceObject, Evidence_Data evidence)
    {
        Video_Evidence videoEvidence = evidenceObject.GetComponent<Video_Evidence>();
        videoEvidence.videoPlayer = videoPlayer;
        videoEvidence.evidenceSO = evidence;
        videoEvidence.clip = evidence.videoClip;
        videoEvidence.cameraAnimator = cameraAnimator;
    }

    public void ProcessAudio(GameObject evidenceObject, Evidence_Data evidence)
    {
        Audio_Evidence audioEvidence = evidenceObject.GetComponent<Audio_Evidence>();
        audioEvidence.audioClip = evidence.audioClip;
        audioEvidence.audioSource = audioSource;
    }

    public void ProcessImage()
    {

    }

    public void DestroyEvidence()
    {
        foreach(GameObject ev in physicalEvidence)
        {
            Destroy(ev);
        }

        physicalEvidence = new List<GameObject>();
    }

    public void OpenFolder()
    {
        foreach(GameObject evidenceData in physicalEvidence)
        {
            if (evidenceData.TryGetComponent<Story_Evidence>(out Story_Evidence story))
            {
                story.gameObject.transform.position = storyPoint;
            }
            else if (evidenceData.TryGetComponent<Police_Report_Evidence>(out Police_Report_Evidence police))
            {
                police.gameObject.transform.position = policeReportPoint;
            }
            else if (evidenceData.TryGetComponent<Video_Evidence>(out Video_Evidence video))
            {
                video.gameObject.transform.position = videoPoint;
            }
            else if (evidenceData.TryGetComponent<Audio_Evidence>(out Audio_Evidence audio))
            {
                audio.gameObject.transform.position = audioPoint;
            }
            else if (evidenceData.TryGetComponent<Image_Evidence>(out Image_Evidence image))
            {
                image.gameObject.transform.position = imagePoint;
            }
        }
    }
}
