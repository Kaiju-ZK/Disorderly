using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPool : MonoBehaviour
{
    public enum sideType
    {
        Weapon,
        Medicine,
        Single,
        Electricity,
        Boss
    }
    public GameObject[] UpEnter;
    public GameObject[] CenterEnter;
    public GameObject[] DownEnter;
    public GameObject[] Weapon;
    private bool wea = false;
    public GameObject[] Medicine;
    private bool med = false;
    public GameObject[] Single;
    private bool sin = false;
    public GameObject[] Electricity;
    private bool ele = false;
    public GameObject[] Boss;
    public int count;
    public int counter = 0;
    public List<int> type;
    public List<int> side;

    private void Start()
    {
        int rand;
        count = Random.Range(3, Random.Range(4,8));
        int[] mas = new int[count];
        int ran;
        for (int i = 0; i < count; i++) 
        {
            mas[i] = i + 2;
        }
        for (int i = 0; i < 13; i++)
        {
            ran = Random.Range(0, count - 1);
            int temp = mas[i % count];
            mas[i % count] = mas[ran];
            mas[ran] = temp;
        }
        for (int i = 0; i < count; i++)
        {
            print(mas[i]);
        }
        rand = Random.Range(0, 2);
        print(rand);
        for (int i = 0; i <= rand; i++)
        {
            if (i != rand)
            {
                SideGen(mas[i]);
            }
            if (i == rand)
            {
                side.Add(mas[i]);
                type.Add(5);
            }
        }
    }
    private void SideGen(int mas)
    {
        int randomRoom;
        side.Add(mas);
        randomRoom = Random.Range(1, 4);
        if (randomRoom == 1 && wea == false)
        {
            type.Add(1);
            return;
        }
        else if (wea == true)
            randomRoom++;
        if (randomRoom == 2 && med == false)
        {
            type.Add(2);
            return;
        }
        else if (med == true)
            randomRoom++;
        if (randomRoom == 3 && sin == false)
        {
            type.Add(3);
            return;
        }
        else if (sin == true)
            randomRoom++;
        if (randomRoom == 4 && ele == false)
        {
            type.Add(4);
            return;
        }
        else if (ele == true)
            randomRoom = 1;
        if (randomRoom == 1 && wea == false)
        {
            type.Add(1);
            return;
        }
        else if (wea == true)
            randomRoom++;
        if (randomRoom == 2 && med == false)
        {
            type.Add(2);
            return;
        }
        else if (med == true)
            randomRoom++;
        if (randomRoom == 3 && sin == false)
        {
            type.Add(3);
            return;
        }
    }
}
