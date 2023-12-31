using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isNext { get; set; }
    public int MaxSeedNo { get; private set; }
    [SerializeField] private seed[] seedPrefab;

    [SerializeField] private Transform seedPosition;

    void Start()
    {
        for (int i = 0; i < seedPrefab.Length; i++)
        {
            Debug.Log($"Prefab at index {i} is {(seedPrefab[i] == null ? "null" : "not null")}");
        }
        // Rest of your Start method...
        Instance = this;
        isNext = false;
        MaxSeedNo = seedPrefab.Length;
        CreateSeed();
    }
    void Update()
    {
        if (isNext)
        {
            isNext = false;
            Invoke("CreateSeed", 2f);
        }
    }
    private void CreateSeed()
    {
        int i = Random.Range(0, MaxSeedNo - 1);
        Debug.Log($"Creating seed with index {i}"); // Add this line
        Debug.Log($"Prefab at index {i} is {(seedPrefab[i] == null ? "null" : "not null")}"); // Add this line
        seed seedIns = Instantiate(seedPrefab[i], seedPosition);
        seedIns.seedNo = i;
        seedIns.gameObject.SetActive(true);
    }

    public void MergeNext(Vector3 target, int seedNo)
    {
        if (seedNo + 1 < MaxSeedNo)
        {
            Debug.Log($"Merging seed with index {seedNo + 1}"); // Add this line
            Debug.Log($"Prefab at index {seedNo + 1} is {(seedPrefab[seedNo + 1] == null ? "null" : "not null")}"); // Add this line
            seed seedIns = Instantiate(seedPrefab[seedNo + 1], target, Quaternion.identity, seedPosition);
            seedIns.seedNo = seedNo + 1;
            seedIns.isDrop = true;
            seedIns.GetComponent<Rigidbody2D>().simulated = true;
            seedIns.gameObject.SetActive(true);
        }
    }
}