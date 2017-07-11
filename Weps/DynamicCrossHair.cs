using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCrossHair : MonoBehaviour {

    static public float spread = -30;

    public const int PISTOL_SHOOTING_SPREAD = 15;
    public const int UZI_SHOOTING_SPREAD = 10;
    public const int JUMP_SPREAD = 20;
    public const int WALK_SPREAD = 10;
    public const int RUN_SPREAD = 15;
    public GameObject crosshair;
    GameObject TopPart;
    GameObject BottomPart;
    GameObject LeftPart;
    GameObject RightPart;

    float initialPosition;

    

    void Start()
    {
        TopPart = crosshair.transform.FindChild("TopPart").gameObject;
        BottomPart = crosshair.transform.FindChild("BottomPart").gameObject;
        LeftPart = crosshair.transform.FindChild("LeftPart").gameObject;
        RightPart = crosshair.transform.FindChild("RightPart").gameObject;

        initialPosition = TopPart.GetComponent<RectTransform>().localPosition.y;

    }

    void Update()
    {
        if(spread != -30 && spread <= 100)
        {
            TopPart.GetComponent<RectTransform>().localPosition = new Vector3(0, initialPosition + spread, 0);
            BottomPart.GetComponent<RectTransform>().localPosition = new Vector3(0, -(initialPosition + spread), 0);
            LeftPart.GetComponent<RectTransform>().localPosition = new Vector3(-(initialPosition + spread), 0, 0);
            RightPart.GetComponent<RectTransform>().localPosition = new Vector3(initialPosition + spread, 0, 0);

        }
        if (spread != -30)
        {
            spread -= 1;
        }
    }
}
