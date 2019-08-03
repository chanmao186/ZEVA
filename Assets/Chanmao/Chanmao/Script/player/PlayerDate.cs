using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PlayerData
{
    public string ID { get; set; }
    public float Heath { get; set; }
    public float _Heath { get; set; }
    public float SliceDistance { get; set; }
    public int SliceNum { get; set; }
}

public class Root
{
    public List<PlayerData> PlayerStates { get; set; }
}
public class PlayerStates{
    public PlayerData Default { get; set; }

    public PlayerData Current { get; set; }
}

