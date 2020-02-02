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
    public Player m_player_2;

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
        m_player.currentPosition = m_player.StartPoint;
        m_player.transform.position = m_hexGridMap.GetGridPosition(m_player.currentPosition);
        m_player.Init();
        
        m_player_2.currentPosition = m_player_2.StartPoint;
        m_player_2.transform.position = m_hexGridMap.GetGridPosition(m_player_2.currentPosition);
        m_player_2.Init();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector3 getPosition(Vector2Int gridCoordinate)
    {
        return m_hexGridMap.GetGridPosition(gridCoordinate);
    }
    // Update is called once per frame
    void Update()
    {
//        m_player.gameObject.transform.position = m_hexGridMap.GetGridPosition(m_player.currentPosition);
//        m_player_2.gameObject.transform.position = m_hexGridMap.GetGridPosition(m_player_2.currentPosition);
        m_player.UpdatePosition(m_hexGridMap.GetGridPosition(m_player.currentPosition));
        m_player_2.UpdatePosition(m_hexGridMap.GetGridPosition(m_player_2.currentPosition));
    }
}
