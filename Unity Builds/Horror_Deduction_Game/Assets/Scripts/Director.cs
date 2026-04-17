using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.Audio;
using static Evidence_Data;
using static EvidenceData;

public class Director : MonoBehaviour
{
    [SerializeField] public List<Monster_Data> monsters;
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
    [SerializeField] public Camera_Animator cameraAnimator;
    [SerializeField] private GameObject currentFolder;
    [SerializeField] private Enemy_Director enemyDirector;

    public VideoPlayer videoPlayer;
    public GameObject audioSource;

    private readonly System.Random rand = new System.Random();

    public EvidenceData chosenTrait = new EvidenceData();
    public List<EvidenceData> unchosenTraits = new List<EvidenceData>();
    public List<Evidence_Data> randomList = new List<Evidence_Data>();
    public int maxDifferenceStrength;
    public int currentDifferenceStrength;

    [SerializeField] private List<Evidence_Data> storiesList;
    [SerializeField] private List<ImageClassifier> imagesList;
    [SerializeField] private List<ImageClassifier> fillerImagesList;
    [SerializeField] private GameObject imagePrefab;
    public void Start()
    {
        GenerateGuesses();
        GenerateEvidence();
        GenerateNewEvidence();
        DisableGuessingMenu();
    }

    public List<Monster_Data> GetReadyMonsters()
    {
        List<Monster_Data> readyMonsters = new List<Monster_Data>();

        foreach (Monster_Data monster in monsters)
        {
            if (monster.monsterReady)
            {
                readyMonsters.Add(monster);
            }
        }

        return readyMonsters;
    }

    public void GenerateNewEvidence()
    {

        List<Monster_Data> monsterList = GetReadyMonsters();

        if (monsterList.Count == 0)
        {
            Debug.LogError("No monsters are marked as ready!");
            return;
        }

        int randomMonster = Random.Range(0, monsterList.Count);
        currentMonster = monsterList[randomMonster];


        //List<Evidence_Data> newEvidence = FindValidEvidence(monsterList[randomMonster]);
        List<Evidence_Data> newEvidence = new List<Evidence_Data>();
        newEvidence.Add(GenerateStoryList(monsterList[randomMonster]));
        newEvidence.Add(GenerateImageList(monsterList[randomMonster]));

        Debug.Log(newEvidence.Count);
        CreateUnchosenList();
        List<Evidence_Data> extraStories = GetAlternateStories();

        foreach (Evidence_Data story in extraStories)
        {
            newEvidence.Add(story);
        }

        newEvidence = GenerateRandomLoop(newEvidence);

        GameObject newFolder = Instantiate(folder, folderSpawnPosition, Quaternion.identity);
        Folder folderScript = newFolder.GetComponent<Folder>();
        folderScript.videoPlayer = videoPlayer;
        folderScript.audioSource = audioSource;
        folderScript.evidence = newEvidence;
        folderScript.cameraAnimator = cameraAnimator;
        folderScript.director = this;
        folderScript.InitializeEvidence();
        currentFolder = newFolder;
    }

    public void CreateUnchosenList()
    {
        unchosenTraits = new List<EvidenceData> ();
        foreach(EvidenceData data in currentMonster.storyList)
        {
            if (data.evidenceRelevant == true) 
            {
                if (data.evidenceName != chosenTrait.evidenceName)
                {
                    unchosenTraits.Add(data);
                }
            }
        }
    }

    public List<Evidence_Data> GetAlternateStories()
    {
        List<Evidence_Data> randomizedPossibleEvidence = GenerateRandomLoop(possibleEvidence);
        List<Evidence_Data> validEvidence = new List<Evidence_Data>();

        foreach(Evidence_Data evidence in randomizedPossibleEvidence)
        {
            if(evidence.evidenceType == Evidence_Data.Evidence.Story)
            {
                int i = 0;
                bool isValid = false;
                int incrementalDifferenceStrength = 0;
                foreach (EvidenceData data in evidence.storyList)
                {
                    
                    if (data.evidenceRelevant)
                    {
                        if (data.evidenceRelevant == currentMonster.storyList[i].evidenceRelevant)
                        {
                            if(data.evidenceName == chosenTrait.evidenceName)
                            {
                                //Debug.Log("Data Name: " + data.evidenceName + "\nChosen Trait Name: " + chosenTrait.evidenceName);
                                isValid = true;
                            }
                            else
                            {
                                incrementalDifferenceStrength += 1;
                            }
                        }
                    }
                    i++;

                    
                }

                //Debug.Log(incrementalDifferenceStrength);
                if (isValid == true && (incrementalDifferenceStrength + currentDifferenceStrength) < maxDifferenceStrength)
                {
                    validEvidence.Add(evidence);
                    currentDifferenceStrength += incrementalDifferenceStrength;
                }
            }

            //Debug.Log(validEvidence.Count);
            

            if(validEvidence.Count >= 2) 
            { 
                break; 
            }
        }

        return validEvidence;
    }


    public Evidence_Data GenerateStoryList(Monster_Data monster)
    {
        List<Evidence_Data> randomizedPossibleEvidence = GenerateRandomLoop(storiesList);
        currentDifferenceStrength = 0;
        Evidence_Data validStory = null;

        foreach (Evidence_Data story in randomizedPossibleEvidence)
        {
            List<EvidenceData> chosenEvidenceTraits = new List<EvidenceData>();
            bool isValid = false;
            int i = 0;

            foreach (EvidenceData data in monster.storyList)
            {
                if (story.storyList[i].evidenceRelevant == true)
                {
                    if (data.evidenceRelevant == true)
                    {
                        chosenEvidenceTraits.Add(data);
                        isValid = true;
                        currentDifferenceStrength += 1;
                    }
                }
                i++;
            }

            if (isValid)
            {
                int randomTrait = Random.Range(0, chosenEvidenceTraits.Count);
                chosenTrait = chosenEvidenceTraits[randomTrait];
                validStory = story;
            }
        }
        return validStory;
    }

    public Evidence_Data GenerateImageList(Monster_Data monster)
    {
        List<ImageClassifier> randomizedImageList = GenerateRandomImageLoop(imagesList);
        List<EvidenceData> monsterImageTraitList = GenerateRandomTraitLoop(monster.imageList);
        EvidenceData chosenTrait = null;
        ImageDataPoint chosenImage = null;
        ImageClassifier chosenClassifier = null;

        foreach (EvidenceData trait in monsterImageTraitList)
        {
            if(trait.evidenceRelevant == true)
            {
                chosenTrait = trait;
            }
        }

        foreach(ImageClassifier image in randomizedImageList)
        {
            bool isValid = false;
            foreach(ImageDataPoint data in image.imageDataPoints)
            {
                if(data.evidenceName == chosenTrait.evidenceName && data.active == true && chosenTrait.evidenceRelevant == true)
                {
                    isValid = true;
                    chosenImage = data;
                    image.chosen = true;
                    chosenClassifier = image;
                    break;
                }
            }

            if(isValid)
            {
                break;
            }
        }

        Evidence_Data imageData = new Evidence_Data();
        imageData.evidencePrefab = imagePrefab;
        imageData.evidenceType = Evidence.Image;
        int i = 0;
        foreach(EvidenceData evidence in imageData.imageList)
        {
            if(evidence.evidenceName == chosenTrait.evidenceName)
            {
                imageData.imageList[i].evidenceRelevant = true;
                i++;
                break;
            }
        }

        imageData.image.chosenImage = chosenImage;
        imageData.image.image1 = chosenClassifier;
        List<ImageClassifier> randomizedFillerImageList = GenerateRandomImageLoop(fillerImagesList);
        int j = 0;
        foreach(ImageClassifier image in randomizedFillerImageList)
        {
            if (j == 0)
            {
                imageData.image.image2 = image;
            }
            else if (j == 1)
            {
                imageData.image.image3 = image;
                break;
            }
            j++;
        }

        return imageData;
    }

    public List<Evidence_Data> FindValidEvidence(Monster_Data monster)
    {
        List<Evidence_Data> randomizedPossibleEvidence = GenerateRandomLoop(possibleEvidence);
        randomList = randomizedPossibleEvidence;
        List<Evidence_Data> validEvidence = new List<Evidence_Data>();
        int randomEvidenceAmount = Random.Range(3, 5);
        currentDifferenceStrength = 0;
        List<Evidence_Data.Evidence> evidenceTypes = new List<Evidence_Data.Evidence>();
        foreach(Evidence_Data evidence in randomizedPossibleEvidence)
        {
            Debug.Log(validEvidence.Count);
            if(validEvidence.Count >= randomEvidenceAmount)
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
                            List<EvidenceData> chosenEvidenceTraits= new List<EvidenceData>();
                            isValid = false;
                            foreach (EvidenceData data in monster.storyList)
                            {
                                if (evidence.storyList[i].evidenceRelevant == true)
                                {
                                    if (data.evidenceRelevant == true)
                                    {
                                        chosenEvidenceTraits.Add(data);
                                        isValid = true;
                                        currentDifferenceStrength += 1;
                                    }
                                }
                                i++;
                            }
                            if (isValid)
                            {
                                validEvidence.Add(evidence);
                                evidenceTypes.Add(evidence.evidenceType);
                                int randomTrait = Random.Range(0, chosenEvidenceTraits.Count);
                                chosenTrait = chosenEvidenceTraits[randomTrait];
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

    public List<EvidenceData> GenerateRandomTraitLoop(List<EvidenceData> listToShuffle)
    {

        for (int i = listToShuffle.Count - 1; i > 0; i--)
        {
            int k = rand.Next(i + 1);
            EvidenceData value = listToShuffle[k];
            listToShuffle[k] = listToShuffle[i];
            listToShuffle[i] = value;
        }
        return listToShuffle;
    }

    public List<ImageClassifier> GenerateRandomImageLoop(List<ImageClassifier> listToShuffle)
    {

        for (int i = listToShuffle.Count - 1; i > 0; i--)
        {
            int k = rand.Next(i + 1);
            ImageClassifier value = listToShuffle[k];
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

        /* 

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
        */

        


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
            enemyDirector.CheckIfMonsterActive(currentMonster);
        }
        else
        {
            if(!enemyDirector.MonsterActive(currentMonster))
            {
                enemyDirector.SpawnMonster(currentMonster);
            }
            else
            {
                enemyDirector.PlayerGuessedWrongTwice(currentMonster);
            }
        }

        currentFolder = null;
        ClearGuessingMenu();
        FilterPossibleGuesses();
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

    public void ClearGuessingMenu()
    {
        foreach(Evidence_Guess_UI guess in evidenceGuesses)
        {
            guess.ClearToggle();
        }

        foreach(EvidenceData story in guessingData.storyList)
        {
            story.evidenceRelevant = false;
        }

        foreach (EvidenceData image in guessingData.imageList)
        {
            image.evidenceRelevant = false;
        }

        foreach (EvidenceData audio in guessingData.audioList)
        {
            audio.evidenceRelevant = false;
        }


    }
}
