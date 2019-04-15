using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : NetworkManager {
    private bool canRotate = true;

    [SerializeField]
    private Camera mainCamera;
    private float rotationCameraRadius = 24f;
    private float rotationCameraSpeed = 8f;
    private float rotation;

    private Transform mainCameraTransform;

    [SerializeField]
    private GameObject PointsBooster;


    [SerializeField]
    private Transform[] buffSpawnPoints;

    public override void OnStartClient(NetworkClient client)
    {
        canRotate = false;
    }

    public override void OnStartHost()
    {
        canRotate = false;
    }

    public override void OnStopClient()
    {
        canRotate = true;
    }

    public override void OnStopHost()
    {
        canRotate = true;
    }

    private Transform GetRandomSpawningPoint()
    {
        var rnd = new System.Random();
        var randomSpawningPointIndex = (int)rnd.Next(0, buffSpawnPoints.Length);
        return buffSpawnPoints[(int)randomSpawningPointIndex];
    }

    private void SpawnBuff(GameObject buffPrefab)
    {

       var go = (GameObject)Instantiate(buffPrefab, GetRandomSpawningPoint().position, GetRandomSpawningPoint().rotation);
       NetworkServer.Spawn(go);
    }

    private IEnumerator SpawnAsync()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            SpawnBuff(PointsBooster);
            SpawnBuff(PointsBooster);
        }
    }

    private void Start()
    {
        mainCameraTransform = mainCamera.GetComponent<Transform>();
        StartCoroutine(SpawnAsync());
    }

    private void Update()
    {
        if (!canRotate)
        {
            return;
        }

        mainCameraTransform.position = new Vector3(-100, 100, -10);

        rotation += rotationCameraSpeed * Time.deltaTime;
        mainCameraTransform.rotation = Quaternion.Euler(0f, rotation, 0f);
        mainCameraTransform.Translate(0f, rotationCameraRadius, -rotationCameraRadius);
        mainCameraTransform.LookAt(Vector3.zero);
        
    }
}
