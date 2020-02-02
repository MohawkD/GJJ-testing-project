using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public Tilemap tilemap;
    
    public static GameManager instance = null;
    public Player m_player;
    public Player m_player_2;

    private HexGridMap m_hexGridMap;

    private bool isGameRunning = false;

    public GameObject child;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)  
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
//        DontDestroyOnLoad(child);
        
        m_hexGridMap = new HexGridMap(tilemap);
        InitGame();
    }

    private void InitGame()
    {
        child.SetActive(true);
        isGameRunning = true;

        m_player.currentPosition = m_player.StartPoint;
        m_player.transform.position = m_hexGridMap.GetGridPosition(m_player.currentPosition);
        m_player.Init();
        
        m_player_2.currentPosition = m_player_2.StartPoint;
        m_player_2.transform.position = m_hexGridMap.GetGridPosition(m_player_2.currentPosition);
        m_player_2.Init();

    }

    // Start is called before the first frame update

    public Vector3 getPosition(Vector2Int gridCoordinate)
    {
        return m_hexGridMap.GetGridPosition(gridCoordinate);
    }
    // Update is called once per frame
    void Update()
    {
        if (isGameRunning)
        {
            if (m_player != null)
            {
                m_player.UpdatePosition(m_hexGridMap.GetGridPosition(m_player.currentPosition));
            }

            if (m_player_2 != null)
            {
                m_player_2.UpdatePosition(m_hexGridMap.GetGridPosition(m_player_2.currentPosition));
            }
        }
    }
    
   void OnSceneLoaded(Scene scene, LoadSceneMode mode)
   {
       InitGame();
   }

   private void OnEnable()
   {
       SceneManager.sceneLoaded += OnSceneLoaded;
   }

//   private void OnDisable()
//   {
//       SceneManager.sceneLoaded -= OnSceneLoaded;
//   }

   public void GameFinish()
    {
        isGameRunning = false;
        Invoke(nameof(LoadFinishScene), 3.0f);
    }

    void LoadFinishScene()
    {
        child.SetActive(false);
        SceneManager.LoadScene("Finish");
    }
}
