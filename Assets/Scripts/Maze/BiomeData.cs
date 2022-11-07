using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BiomeData.asset", menuName = "BiomeData")]
public class BiomeData : ScriptableObject
{
    [System.Serializable] 
    public class SameRoomVariants
    {
        public string name;
        public List<RoomSystem> variants;
    }

    public RoomSystem StartRoom;
    public SameRoomVariants prefabNormaRoom;
    public List<SameRoomVariants> prefabSpecialRoom;
    public SameRoomVariants prefabBossRoom;
}
