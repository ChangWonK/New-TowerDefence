  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView : MonoBehaviour
{
    public Camera UICamera;

    private RectTransform contentsParent;
    private RectTransform[] contents;

    private float[] contentsPositionX;
    private float contentsDistance = 1100;
    private float clickStartPosX;
    private float clickMinDistanceToScroll = 0.5f;

    private int currentContentsNum = 0;

    private Vector3 targetPostion;

    private bool isMoved = false;


    void Awake()
    {
        contentsParent = transform.GetChild(0).GetComponent<RectTransform>();
        contents = contentsParent.GetComponentsInChildren<RectTransform>();

        contentsPositionX = new float[contents.Length];

        
    }

    void Start()
    {
        LocationContents();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickStartPosX = UICamera.ScreenToWorldPoint(Input.mousePosition).x;
            isMoved = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isMoved)
            {
                SetEndPosition();
            }
            isMoved = false;
        }

        if (isMoved)
        {

        }
    }

    private void LocationContents()
    {
        float currentPosX = 0;

        for (int i = 1; i < contents.Length; i++)
        {
            Debug.Log(i);
            contents[i].localPosition = new Vector2(currentPosX, contents[i].localPosition.y);
            contentsPositionX[i-1] = -currentPosX;
            currentPosX += contentsDistance;

        }
    }

    private void LerpToTargetPosition()
    {

    }

    private void SetEndPosition()
    {

        if ((clickStartPosX - UICamera.ScreenToWorldPoint(Input.mousePosition).x) < clickMinDistanceToScroll)
        {
            if (currentContentsNum > 0)
                currentContentsNum--;
        }
        else if ((clickStartPosX - UICamera.ScreenToWorldPoint(Input.mousePosition).x) > clickMinDistanceToScroll)
        {
            if (currentContentsNum < 3)
                currentContentsNum++;
        }

        targetPostion.x = contentsPositionX[currentContentsNum];
    }
}
