using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.Audio;
public class Director : MonoBehaviour
{
    [SerializeField] private List<Monster_Data> monsters;
    [SerializeField] private Evidence_Data guessingData;
    [SerializeField] private Monster_Data currentMonster;
    [SerializeField] private Monster_Data currentGuess;
    [SerializeField] private MonsterGuess currentButtonGuess;
    [SerializeField] private List<MonsterGuess> possibleGuesses;
    [SerializeField] private List<Evidence_Guess_UI> evidenceGuesses;
    [SerializeField] private GameObject possibleGuessesParent;
    [SerializeField] private GameObject guessPrefab;
    [SerializeField] private GameObject evidencePrefab;

    [SerializeField] private GameObject storyView;
    [SerializeField] private GameObject policeReportView;
    [SerializeField] private GameObject videoView;
    [SerializeField] private GameObject audioView;
    [SerializeField] private GameObject imageView;

    [SerializeField] private GameObject guessingMenu;
    [SerializeField] private List<Evidence_Data> possibleEvidence;
    [SerializeField] private GameObject folder;
    [SerializeField] private Vector3 folderSpawnPosition;
    [SerializeField] private Camera_Animator cameraAnimator;
    [SerializeField] private GameObject currentFolder;

    public VideoPlayer videoPlayer;
    public AudioSource audioSource;

    private readonly System.Random rand = new System.Random();

    public void Start()
    {
        GenerateGuesses();
        GenerateEvidence();
        GenerateNewEvidence();
        DisableGuessingMenu();
    }
    public void GenerateNewEvidence()
    {
        int randomMonster = Random.Range(0, monsters.Count);
        currentMonster = monsters[randomMonster];
        List<Evidence_Data> newEvidence = FindValidEvidence(monsters[randomMonster]);
        GameObject newFolder = Instantiate(folder, folderSpawnPosition, Quaternion.identity);
        Folder folderScript = newFolder.GetComponent<Folder>();
        folderScript.videoPlayer = videoPlayer;
        folderScript.audioSource = audioSource;
        folderScript.evidence = newEvidence;
        folderScript.cameraAnimator = cameraAnimator;
        folderScript.InitializeEvidence();
        currentFolder = newFolder;
    }

    public List<Evidence_Data> FindValidEvidence(Monster_Data monster)
    {
        List<Evidence_Data> randomizedPossibleEvidence = GenerateRandomLoop(possibleEvidence);
        List<Evidence_Data> validEvidence = new List<Evidence_Data>();
        int randomEvidenceAmount = Random.Range(3, 5);
        int currentEvidenceAmount = 0;
        List<Evidence_Data.Evidence> evidenceTypes = new List<Evidence_Data.Evidence>();
        foreach(Evidence_Data evidence in randomizedPossibleEvidence)
        {
            if(currentEvidenceAmount >= randomEvidenceAmount)
            {
                break;
            }
            else
            {
                bool isValid = true;
                int i = 0;
                bool evidenceTypeNotAddedYet = true;

                foreach(Evidence_Data.Evidence type in evidenceTypes)
                {
                    if(type == evidence.evidenceType)
                    {
                        evidenceTypeNotAddedYet = false;
                    }
                }
                if(evidenceTypeNotAddedYet)
                {
                    switch (evidence.evidenceType)
                    {
                        case Evidence_Data.Evidence.Story:
                            foreach (EvidenceData data in monster.storyList)
                            {
                                if (evidence.storyList[i].evidenceRelevant == true)
                                {
                                    if (data.evidenceRelevant == false)
                                    {
                                        isValid = false;
                                    }
                                }
                                i++;
                            }
                            if (isValid)
                            {
                                validEvidence.Add(evidence);
                                evidenceTypes.Add(evidence.evidenceType);
                            }
                            break;

                        case Evidence_Data.Evidence.PoliceReport:
                            foreach (EvidenceData data in monster.policeReportList)
                            {
                                if (evidence.policeReportList[i].evidenceRelevant == true)
                                {
                                    if (data.evidenceRelevant == false)
                                    {
                                        isValid = false;
                                    }
                                }
                                i++;
                            }
                            if (isValid)
                            {
                                validEvidence.Add(evidence);
                                evidenceTypes.Add(evidence.evidenceType);
                            }
                            break;

                        case Evidence_Data.Evidence.Video:
                            foreach (EvidenceData data in monster.videoList)
                            {
                                if (evidence.videoList[i].evidenceRelevant == true)
                                {
                                    if (data.evidenceRelevant == false)
                                    {
                                        isValid = false;
                                    }
                                }
                                i++;
                            }
                            if (isValid)
                            {
                                validEvidence.Add(evidence);
                                evidenceTypes.Add(evidence.evidenceType);
                            }
                            break;

                        case Evidence_Data.Evidence.Audio:
                            foreach (EvidenceData data in monster.audioList)
                            {
                                if (evidence.audioList[i].evidenceRelevant == true)
                                {
                                    if (data.evidenceRelevant == false)
                                    {
                                        isValid = false;
                                    }
                                }
                                i++;
                            }
                            if (isValid)
                            {
                                validEvidence.Add(evidence);
                                evidenceTypes.Add(evidence.evidenceType);
                            }
                            break;

                        case Evidence_Data.Evidence.Image:
                            foreach (EvidenceData data in monster.imageList)
                            {
                                if (evidence.imageList[i].evidenceRelevant == true)
                                {
                                    if (data.evidenceRelevant == false)
                                    {
                                        isValid = false;
                                    }
                                }
                                i++;
                            }
                            if (isValid)
                            {
                                validEvidence.Add(evidence);
                                evidenceTypes.Add(evidence.evidenceType);
                            }
                            break;
                    }
                }
            }
        }
        
        return validEvidence;
    }

    public List<Evidence_Data> GenerateRandomLoop(List<Evidence_Data> listToShuffle)
    {

        for (int i = listToShuffle.Count - 1; i > 0; i--)
        {
            int k = rand.Next(i + 1);
            Evidence_Data value = listToShuffle[k];
            listToShuffle[k] = listToShuffle[i];
            listToShuffle[i] = value;
        }
        return listToShuffle;
    }

    public void GenerateEvidence()
    {
        foreach(EvidenceData evidence in guessingData.storyList)
        {
            GameObject checkbox = Instantiate(evidencePrefab, transform.position, Quaternion.identity);
            checkbox.transform.SetParent(storyView.transform);
            Evidence_Guess_UI checkboxEvidence = checkbox.GetComponent<Evidence_Guess_UI>();
            checkboxEvidence.guessingValue = evidence;
            checkboxEvidence.director = this;
            checkboxEvidence.text.text = evidence.evidenceName;
            evidenceGuesses.Add(checkboxEvidence);
        }

        foreach (EvidenceData evidence in guessingData.policeReportList)
        {
            GameObject checkbox = Instantiate(evidencePrefab, transform.position, Quaternion.identity);
            checkbox.transform.SetParent(policeReportView.transform);
            Evidence_Guess_UI checkboxEvidence = checkbox.GetComponent<Evidence_Guess_UI>();
            checkboxEvidence.guessingValue = evidence;
            checkboxEvidence.director = this;
            checkboxEvidence.text.text = evidence.evidenceName;
            evidenceGuesses.Add(checkboxEvidence);
        }

        foreach (EvidenceData evidence in guessingData.videoList)
        {
            GameObject checkbox = Instantiate(evidencePrefab, transform.position, Quaternion.identity);
            checkbox.transform.SetParent(videoView.transform);
            Evidence_Guess_UI checkboxEvidence = checkbox.GetComponent<Evidence_Guess_UI>();
            checkboxEvidence.guessingValue = evidence;
            checkboxEvidence.director = this;
            checkboxEvidence.text.text = evidence.evidenceName;
            evidenceGuesses.Add(checkboxEvidence);
        }


        foreach (EvidenceData evidence in guessingData.audioList)
        {
            GameObject checkbox = Instantiate(evidencePrefab, transform.position, Quaternion.identity);
            checkbox.transform.SetParent(audioView.transform);
            Evidence_Guess_UI checkboxEvidence = checkbox.GetComponent<Evidence_Guess_UI>();
            checkboxEvidence.guessingValue = evidence;
            checkboxEvidence.director = this;
            checkboxEvidence.text.text = evidence.evidenceName;
            evidenceGuesses.Add(checkboxEvidence);
        }

        foreach (EvidenceData evidence in guessingData.imageList)
        {
            GameObject checkbox = Instantiate(evidencePrefab, transform.position, Quaternion.identity);
            checkbox.transform.SetParent(imageView.transform);
            Evidence_Guess_UI checkboxEvidence = checkbox.GetComponent<Evidence_Guess_UI>();
            checkboxEvidence.guessingValue = evidence;
            checkboxEvidence.director = this;
            checkboxEvidence.text.text = evidence.evidenceName;
            evidenceGuesses.Add(checkboxEvidence);
        }

    }

    public void FilterPossibleGuesses()
    {
        foreach(MonsterGuess monsterGuess in possibleGuesses)
        {
            bool guessPossible = true;
            for (int i = 0; i < guessingData.storyList.Count; i++)
            {
                if(guessingData.storyList[i].evidenceRelevant == true)
                {
                    if(monsterGuess.monster.storyList[i].evidenceRelevant == false)
                    {
                        guessPossible = false;
                    }
                }
            }

            for (int i = 0; i < guessingData.policeReportList.Count; i++)
            {
                if (guessingData.policeReportList[i].evidenceRelevant == true)
                {
                    if (monsterGuess.monster.policeReportList[i].evidenceRelevant == false)
                    {
                        guessPossible = false;
                    }
                }
            }

            for (int i = 0; i < guessingData.videoList.Count; i++)
            {
                if (guessingData.videoList[i].evidenceRelevant == true)
                {
                    if (monsterGuess.monster.videoList[i].evidenceRelevant == false)
                    {
                        guessPossible = false;
                    }
                }
            }

            for (int i = 0; i < guessingData.audioList.Count; i++)
            {
                if (guessingData.audioList[i].evidenceRelevant == true)
                {
                    if (monsterGuess.monster.audioList[i].evidenceRelevant == false)
                    {
                        guessPossible = false;
                    }
                }
            }

            for (int i = 0; i < guessingData.imageList.Count; i++)
            {
                if (guessingData.imageList[i].evidenceRelevant == true)
                {
                    if (monsterGuess.monster.imageList[i].evidenceRelevant == false)
                    {
                        guessPossible = false;
                    }
                }
            }


            if (guessPossible == false)
            {
                monsterGuess.CrossoutButton();
            }
            else
            {
                monsterGuess.DisableCrossoutButton();
            }
        }
    }

    public void GenerateGuesses()
    {
        foreach(Monster_Data monster in monsters)
        {
            GameObject guess = Instantiate(guessPrefab, transform.position, Quaternion.identity);
            guess.transform.SetParent(possibleGuessesParent.transform);
            MonsterGuess guessScript = guess.GetComponent<MonsterGuess>();
            guessScript.director = this;
            guessScript.monster = monster;
            guessScript.monsterName.text = monster.monsterName;
            possibleGuesses.Add(guessScript);
        }
    }

    public void UpdateGuess(Monster_Data monster, MonsterGuess guessUI)
    {
        
        if(currentButtonGuess != null)
        {
            currentButtonGuess.UnguessButton();
        }
        currentGuess = monster;
        currentButtonGuess = guessUI;
        currentButtonGuess.GuessButton();
    }

    public void SubmitGuess()
    {
        Folder currentFolderScript = currentFolder.GetComponent<Folder>();
        currentFolderScript.DestroyEvidence();
        Destroy(currentFolder);

        if (currentGuess == currentMonster)
        {
            monsters.Remove(currentMonster);
        }
        else
        {

        }

        currentFolder = null;

        GenerateNewEvidence();
    }

    public void ActivateGuessingMenu()
    {
        guessingMenu.SetActive(true);
    }

    public void DisableGuessingMenu()
    {
        guessingMenu.SetActive(false);
    }

    public void CloseVideoPlayer()
    {
        cameraAnimator.SwitchView(0);
        videoPlayer.Stop();
    }

    public void SpawnMonster()
    {

    }
}
