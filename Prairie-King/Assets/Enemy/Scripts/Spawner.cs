using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ZombiePrefab;
    public GameObject DinosaurPrefab; // ← Nuevo campo visible en el Inspector

    [SerializeField] private float SpawnRate;
    [SerializeField] private Transform[] Gates;
    [SerializeField] private List<Transform> GateList = new List<Transform>();

    public Timercontroller timer;
    public Instruction instruction;

    private void Awake()
    {
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timercontroller>();
        instruction = GameObject.Find("Instruction").GetComponent<Instruction>();
        SpawnRate = Random.Range(2, 10);
        GateList = new List<Transform>(Gates);
    }

    private void Update()
    {
        if (instruction.isActiveAndEnabled == false)
        {
            if (timer.TimeRemaining <= 0) return;
            if (GateList.Count <= 0) GateList = new List<Transform>(Gates);
            SpawnRate -= Time.deltaTime;
            if (SpawnRate <= 0)
            {
                Spawn();
                SpawnRate = Random.Range(3, 10);
            }
        }
    }

    private void Spawn()
    {
        if (GateList.Count == 0) return;
        int take = Random.Range(1, Mathf.Min(7, GateList.Count));

        for (int i = 0; i < take; ++i)
        {
            int randomGate = Random.Range(0, GateList.Count);

            // 50% de probabilidad de que aparezca un dinosaurio o un zombi
            if (Random.value < 0.5f && DinosaurPrefab != null)
            {
                Instantiate(DinosaurPrefab, GateList[randomGate].position, GateList[randomGate].rotation);
            }
            else if (ZombiePrefab != null)
            {
                Instantiate(ZombiePrefab, GateList[randomGate].position, GateList[randomGate].rotation);
            }

            GateList.RemoveAt(randomGate);
        }
    }
}
