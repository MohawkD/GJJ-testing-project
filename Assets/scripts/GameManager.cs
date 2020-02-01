using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public Tilemap tilemap;
    
    public static GameManager instance = null;
    public Player m_player;

    private HexGridMap m_hexGridMap;

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
        
        m_hexGridMap = new HexGridMap(tilemap);
        
        InitGame();
    }

    private void InitGame()
    {
        m_player.currentPosition = Vector2Int.zero;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_player.gameObject.transform.position = m_hexGridMap.GetGridPosition(m_player.currentPosition);
    }
}
