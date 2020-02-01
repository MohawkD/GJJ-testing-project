using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpSpawner : MonoBehaviour
{
    public static powerUpSpawner instance;

    private float time = 0.0f;
    private float timeToPowerUp = 0.0f;
    public List<GameObject> powerUpImages;
    public Dictionary<Vector2Int, string> powerUpLocations = new Dictionary<Vector2Int, string>();
    private Dictionary<Vector2Int, GameObject> spawnedPowerUps = new Dictionary<Vector2Int, GameObject>();
    public float powerUpSpawnTime = 5.0f;

    private string[] powerUpNames = {"speed", "freeze"};

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public string getCoordinateContent(Vector2Int coordinate)
    {
        string content = "none";
        if(powerUpLocations.ContainsKey(coordinate)) {
            content = powerUpLocations[coordinate];
        }
        Debug.Log(content);
        if(content != "none") {
            removePowerUp(coordinate);
        }
        return content;
    }
    // Start is called before the first frame update
    void Start()
    {
        timeToPowerUp = Random.Range(powerUpSpawnTime - 2.5f, powerUpSpawnTime + 2.5f);
        timeToPowerUp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
 
        if (time >= timeToPowerUp) {
            time = 0.0f;
            SpawnPowerUp();
            timeToPowerUp = Random.Range(powerUpSpawnTime - 2.5f, powerUpSpawnTime + 2.5f);
            Debug.Log("Next power up in " + timeToPowerUp + "seconds");
        }
    }

    private Vector2Int getSpawnLocation() {
        while(true)
        {
            int x = Random.Range(-5, 6);
            int y = Random.Range(-5, 6);
            Vector2Int location = new Vector2Int(x, y);
            if(!spawnedPowerUps.ContainsKey(location)) {
                return(location);
            }
        }
    }
    private void SpawnPowerUp()
    {
        Vector2Int spawnLocation = getSpawnLocation();
        int powerUpIndex = Random.Range(0, powerUpNames.Length);
        powerUpLocations.Add(spawnLocation, powerUpNames[powerUpIndex]);
        Vector3 spawn_transform = GameManager.instance.getPosition(spawnLocation);
        GameObject spawnedPowerUp = Instantiate(powerUpImages[powerUpIndex], spawn_transform, Quaternion.identity);
        spawnedPowerUps.Add(spawnLocation, spawnedPowerUp);
        Debug.Log("Power up spawned");
    }

    public void removePowerUp(Vector2Int coordinate)
    {
        powerUpLocations.Remove(coordinate);
        GameObject powerUp = spawnedPowerUps[coordinate];
        spawnedPowerUps.Remove(coordinate);
        Destroy(powerUp);

    }

}
