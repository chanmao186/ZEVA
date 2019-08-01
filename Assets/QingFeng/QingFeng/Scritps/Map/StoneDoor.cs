using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDoor : MonoBehaviour
{
    public bool isOpen = false;
    public Transform stoneDoor;
    // Start is called before the first frame update
    void Start()
    {
        stoneDoor = GetComponent<Transform>();
    }

    public void OpenTheDoor(float  num)
    {
        stoneDoor.Translate(new Vector3(0, -num , 0));
    }

    public void CloseTheDoor(float num)
    {
        stoneDoor.Translate(new Vector3(0, num, 0));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
