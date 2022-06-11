using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    [SerializeField] string username;
    [SerializeField] int playerID;

    // Singleton instance
    static PlayerInformation instance;
    public static PlayerInformation Instance { get { return instance; } }

    public string Username { get { return username; } }
    public int PlayerID { get { return playerID; } }

    void Awake()
    {
        // This object will always be present, even when switching levels
        Object.DontDestroyOnLoad(this.gameObject);
        instance = this;
    }

    public void SavePlayerInfo(string username, int playerID)
    {
        this.username = username;
        this.playerID = playerID;
    }  
}
