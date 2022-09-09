using System.Collections;using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviour
{
    public static RoomsManager instance;

    public RoomsArray[] RoomsPrefab;
    public List<RoomsArray> RoomsInstance;
    GameObject currentRoom;

    GameObject Player;

    [SerializeField]
    GameObject VictoryScreen;

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void StartGame()
    {
        if(RoomsPrefab.Length <= 0)
        {
            return;
        }
        RoomsInstance = new List<RoomsArray>();
        for (int i = 0; i < RoomsPrefab.Length; ++i)
        {
            RoomsArray tempArray = new RoomsArray();
            tempArray.RoomsRow = new GameObject[RoomsPrefab[i].RoomsRow.Length];
            for (int j = 0; j < RoomsPrefab[i].RoomsRow.Length; ++j)
            {
                GameObject r = Instantiate<GameObject>(RoomsPrefab[i].RoomsRow[j], transform);
                tempArray.RoomsRow[j] = r;
                Room room = r.GetComponent<Room>();
                room.Coords = new Vector2(i, j);
                room.RoomPrefab = RoomsPrefab[i].RoomsRow[j];
                r.SetActive(false);
            }
            RoomsInstance.Add(tempArray);
        }
        RoomsInstance[0].RoomsRow[0].SetActive(true);
        currentRoom = RoomsInstance[0].RoomsRow[0];
    }

    public void GoToNextRoom(Door.Direction dir)
    {
        Vector2 tempCoords = currentRoom.GetComponent<Room>().Coords;
        switch (dir)
        {
            case Door.Direction.UP:
                if (tempCoords.x - 1 >= 0)
                {
                    currentRoom.SetActive(false);
                    currentRoom = RoomsInstance[(int)tempCoords.x - 1].RoomsRow[(int)tempCoords.y];
                    currentRoom.SetActive(true);
                    Player.transform.position = currentRoom.GetComponent<Room>().SpawnUp.position;
                }
                break;
            case Door.Direction.DOWN:
                if (tempCoords.x + 1 < RoomsInstance[(int)tempCoords.x].RoomsRow.Length)
                {
                    currentRoom.SetActive(false);
                    currentRoom = RoomsInstance[(int)tempCoords.x + 1].RoomsRow[(int)tempCoords.y];
                    currentRoom.SetActive(true);
                    Player.transform.position = currentRoom.GetComponent<Room>().SpawnDown.position;
                }
                break;
            case Door.Direction.LEFT:
                if (tempCoords.y - 1 >= 0)
                {
                    currentRoom.SetActive(false);
                    currentRoom = RoomsInstance[(int)tempCoords.x].RoomsRow[(int)tempCoords.y - 1];
                    currentRoom.SetActive(true);
                    Player.transform.position = currentRoom.GetComponent<Room>().SpawnLeft.position;
                }
                break;
            case Door.Direction.RIGHT:
                if(tempCoords.y + 1 < RoomsInstance.Count)
                {
                    currentRoom.SetActive(false);
                    currentRoom = RoomsInstance[(int)tempCoords.x].RoomsRow[(int)tempCoords.y + 1];
                    currentRoom.SetActive(true);
                    Player.transform.position = currentRoom.GetComponent<Room>().SpawnRight.position;

                }
                break;
            case Door.Direction.VICTORY:
                VictoryScreen.SetActive(true);
                break;
        }
        Room r = currentRoom.GetComponent<Room>();
        tempCoords = currentRoom.GetComponent<Room>().Coords;
        if (r.bIsReset)
        {
            
            currentRoom = Instantiate<GameObject>(r.RoomPrefab, transform);
            RoomsInstance[(int)tempCoords.x].RoomsRow[(int)tempCoords.y] = currentRoom;
        }
    }

    public void PlayerStatusChanged(bool playerIsHidden)
    {
        currentRoom.GetComponent<Room>().RoomEnemyChange(playerIsHidden);
    }
}


[System.Serializable] 
public class RoomsArray
{
    public GameObject[] RoomsRow;
}