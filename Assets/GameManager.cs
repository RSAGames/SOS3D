using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject playerCanvas;
    [SerializeField] private GameObject playerHealthBar;
    [SerializeField] private GameObject playerHealthBarBackground;
    [SerializeField] private GameObject playerHealthBarInner;
    private GameObject currentPatient = null;
    [SerializeField] private int numberOfPatients;
    [SerializeField] private int numberOfActivePatients;
    [SerializeField] private GameObject[] patients;
    [SerializeField] private Life_Manager lifeManager;
    [SerializeField] private GameObject[] prefabsToCreate;
    [SerializeField] private int numberOfPrefabsToCreate;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform[] navigationPoints;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Transform[] cprPoints;
    private bool tests_bool = true;
    
    // Start is called before the first frame update
    void Start()
    {
        prefabs = new GameObject[numberOfPrefabsToCreate];
        lifeManager = GameObject.Find("Lifes").GetComponent<Life_Manager>();
        if (instance == null)
        {
            Debug.Log("GameManager instance created");
            instance = this;
        }
        else
        {
            Debug.Log("GameManager instance already exists");
        }

        if (player == null)
        {
            Debug.Log("Player not found");
        }
        else
        {
            Debug.Log("Player found");
        }

        if (playerCamera == null)
        {
            Debug.Log("Player camera not found");
        }
        else
        {
            Debug.Log("Player camera found");
        }

        if (playerCanvas == null)
        {
            Debug.Log("Player canvas not found");
        }
        else
        {
            Debug.Log("Player canvas found");
        }

        if (playerHealthBar == null)
        {
            Debug.Log("Player health bar not found");
        }
        else
        {
            Debug.Log("Player health bar found");
        }

        if (playerHealthBarBackground == null)
        {
            Debug.Log("Player health bar background not found");
        }
        else
        {
            Debug.Log("Player health bar background found");
        }

        if (playerHealthBarInner == null)
        {
            Debug.Log("Player health bar inner not found");
        }
        else
        {
            Debug.Log("Player health bar inner found");
        }    

        if (lifeManager == null)
        {
            Debug.Log("Life manager not found");
        }
        else
        {
            Debug.Log("Life manager found");
        }

        if (prefabsToCreate.Length == 0)
        {
            Debug.Log("No prefabs to create");
        }
        else
        {
            Debug.Log("Prefabs to create found");
        }

        patients = new GameObject[numberOfPatients];

        for (int i = 0; i < numberOfPrefabsToCreate; i++)
        {
            int randomIndex = Random.Range(0, prefabsToCreate.Length);
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
            // Debug.Log("Creating prefab " + prefabsToCreate[randomIndex].name + " at spawn point " + spawnPoints[randomSpawnPointIndex].name);
            GameObject InstantiatedPrefab = Instantiate(prefabsToCreate[0], spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
            prefabs[i] = InstantiatedPrefab;
        }

        for (int i=0 ; i<numberOfPatients ; i++){
            GameObject patient = Instantiate(prefabsToCreate[1], cprPoints[i].position, cprPoints[i].rotation);
            patients[i] = patient;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetCurrentPatient();
    }
    
    
    void SetCurrentPatient(){
        if (currentPatient == null){
            currentPatient = ChooseRandomPatient();
            SetPath(currentPatient.transform);
            player.GetComponent<CPR>().SetPatient(currentPatient);
        }
    }

    GameObject ChooseRandomPatient(){
        int randomIndex = Random.Range(0, patients.Length);
        GameObject patient = patients[randomIndex];
        while (patient == null){
            randomIndex = Random.Range(0, patients.Length);
            patient = patients[randomIndex];
        }
        patient.GetComponent<Animator>().SetBool("Dying", true);
        patient.GetComponent<Animator>().SetBool("isLying", true);
        patients[randomIndex] = null;
        return patient;
    }



private bool IsPatientInList(GameObject patient)
{
    // Check if the patient is already in the patients list by comparing game object IDs
    for (int i = 0; i < patients.Length; i++)
    {
        if (patients[i] != null && patients[i].GetInstanceID() == patient.GetInstanceID())
        {
            return true;
        }
    }

    return false;
}
    
    private void SetPath(Transform destinatioin)
    {
        // get Show_Path script of child object of player named Wolf3DAvatar
        Show_Path showPath = player.transform.Find("Wolf3D_Avatar").gameObject.GetComponent<Show_Path>();
        showPath.SetTarget(destinatioin);
        showPath.SetLineActive(true); // set line active
        // set destination of path

    }
}


