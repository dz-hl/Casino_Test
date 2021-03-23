using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SlotGenerator : MonoBehaviour
{
    [SerializeField] Sprite[] images;
    [SerializeField] private Slot originalSlot;
    public Text winText;
    public const int gridColumns = 5;
    public const int gridRows = 3;
    private int[,] idArray = new int[gridRows, gridColumns];
    public float offsetX = 0.1f;
    public float offsetY = 0.5f;
    public Button spinButton;


    
    //Win conditions
    int[][,] lines = new int[][,] {
    new int[,] { { 1, 0 }, { 1, 1 }, { 1, 2 }, { 1, 3 }, { 1, 4 } },
    new int[,] { { 0, 0 }, { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 } },
    new int[,] { { 2, 0 }, { 2, 1 }, { 2, 2 }, { 2, 3 }, { 2, 4 } },
    new int[,] { { 0, 0 }, { 1, 1 }, { 2, 2 }, { 1, 3 }, { 0, 4 } },
    new int[,] { { 2, 0 }, { 1, 1 }, { 0, 2 }, { 1, 3 }, { 2, 4 } },
    new int[,] { { 1, 0 }, { 0, 1 }, { 0, 2 }, { 0, 3 }, { 1, 4 } },
    new int[,] { { 1, 0 }, { 2, 1 }, { 2, 2 }, { 2, 3 }, { 1, 4 } },
    new int[,] { { 0, 0 }, { 0, 1 }, { 1, 2 }, { 2, 3 }, { 2, 4 } },
    new int[,] { { 2, 0 }, { 2, 1 }, { 1, 2 }, { 0, 3 }, { 0, 4 } },
    new int[,] { { 1, 0 }, { 2, 1 }, { 1, 2 }, { 0, 3 }, { 1, 4 } },
    new int[,] { { 1, 0 }, { 0, 1 }, { 1, 2 }, { 2, 3 }, { 1, 4 } },
    new int[,] { { 0, 0 }, { 1, 1 }, { 1, 2 }, { 1, 3 }, { 0, 4 } },
    new int[,] { { 2, 0 }, { 1, 1 }, { 1, 2 }, { 1, 3 }, { 2, 4 } },
    new int[,] { { 0, 0 }, { 1, 1 }, { 0, 2 }, { 1, 3 }, { 0, 4 } },
    new int[,] { { 2, 0 }, { 1, 1 }, { 2, 2 }, { 1, 3 }, { 2, 4 } }
};

    

    
    
    void Start()
    {
        Button spinBtn = spinButton.GetComponent<Button>();
        winText.gameObject.SetActive(false);
    }


    public void GenerateNewSlots()
    {
        Vector3 startPos = originalSlot.transform.position;

        for (int i = 0; i < gridRows; i++)
        {
            for (int j = 0; j < gridColumns; j++)
            {
                Slot slot = Instantiate(originalSlot) as Slot;

                int id = Random.Range(0, images.Length);
                slot.SetSlot(id, images[id], (i+j));
                idArray[i, j] = id;

                float posX = (offsetX * j) + startPos.x;
                float posY = -(offsetY * i) + startPos.y;
                slot.transform.position = new Vector3(posX, posY, startPos.z);
                
            }
        }
    }

    public void DeletePreviousSlots()
    {
        if (Object.FindObjectsOfType(typeof(Slot)) == null)
        {
            return;
        }
        else
        {
            Slot[] slotArray;
            slotArray = Object.FindObjectsOfType(typeof(Slot)) as Slot[];
            for(int i = slotArray.Length-1; i >= 0; i--)
            {
                Destroy(slotArray[i].gameObject);
                
            }
        }

    }

    public void SpinForNewSlots()
    {
        Debug.Log("Spin");
        DeletePreviousSlots();
        GenerateNewSlots();
        for (int i = 0; i < lines.Length; i++)
        {
            if (CheckLines2(lines[i], idArray))
            {
                StartCoroutine(Blink(i));
                Debug.Log("line " + (i+1) + " Win");
                CheckLines2(lines[i], idArray);
            }
        }
    }

    public bool CheckLines2(int[,] line, int[,] ids)
    {
        int row = line[0, 0];
        int column = line[0, 1];
        int id = ids[row, column];

        for(int i=0; i < line.GetLength(0); i++)
        {
            row = line[i, 0];
            column = line[i, 1];
            int id2 = ids[row, column];
            if (id != id2)
            {
                return false;
            }
        }
        
        return true;
    }



    IEnumerator Blink(int i)
    {
        winText.gameObject.SetActive(true);
        winText.text = "Line " + (i + 1) + " is win";
        yield return new WaitForSeconds(2.0f);
        winText.gameObject.SetActive(false);
    }


}
