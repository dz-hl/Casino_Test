using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public int index;
    [SerializeField] public int id;
    public void SetSlot(int id2, Sprite image, int ind)
    {
        id = id2;
        GetComponent<SpriteRenderer>().sprite = image;
        index = ind;
    }
}
