using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexGridMap
{
    private Dictionary<Vector2Int, Vector3> m_griddict = new Dictionary<Vector2Int, Vector3>();
        
    public HexGridMap(Tilemap tileMap)
    {        
        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {                    
                    m_griddict.Add(new Vector2Int(n, p), place);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
    }

    public Vector3 GetGridPosition(Vector2Int xy)
    {
        if (m_griddict.ContainsKey(xy))
        {
            return m_griddict[xy];
        }
        else
        {
            //TODO: check border
            throw  new System.ArgumentException("this is no gird at position:" + xy);
        }
    }

    public Vector3 GetGridPosition(int x, int y)
    {
        return GetGridPosition(new Vector2Int(x, y));
    }

}
