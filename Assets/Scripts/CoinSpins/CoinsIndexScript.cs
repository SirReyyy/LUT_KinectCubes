using UnityEngine;
using System.Collections;
using System;

public class CoinsIndexScript : MonoBehaviour
{
    [Tooltip("Camera used for screen-to-world calculations. This is usually the main camera.")]
    public Camera screenCamera;

    [Tooltip("Whether the presentation slides may be changed with gestures (SwipeLeft, SwipeRight & SwipeUp).")]
    public bool slideChangeWithGestures = true;
    [Tooltip("Whether the presentation slides may be changed with keys (PgDown & PgUp).")]
    public bool slideChangeWithKeys = true;
    [Tooltip("Speed of rotation, when the presentation cube spins.")]
    public int spinSpeed = 5;
    private bool isSpinning = false;

    [HideInInspector]
    public int coinRowId, coinColId;
    private int coinRowStart = 0, coinColStart = 0;
    private int coinRowEnd, coinColEnd;

    private CoinsIndexListener gestureListener;
    private CoinsIndexCoordinates indexCoordinates;
    private Quaternion initialRotation;
    private int stepsToGo = 0;

    private float rotationStep;
    private Vector3 rotationAxis;

    private Renderer coinRenderer;
    public Material[] materialsList;


    void Start()
    {
        // hide mouse cursor
        //Cursor.visible = false;

        // by default set the main-camera to be screen-camera
        if (screenCamera == null)
        {
            screenCamera = Camera.main;
        }

        initialRotation = screenCamera ? Quaternion.Inverse(screenCamera.transform.rotation) * transform.rotation : transform.rotation;
        isSpinning = false;

        // get the gestures listener
        gestureListener = gameObject.GetComponent<CoinsIndexListener>();
        indexCoordinates = gameObject.GetComponentInParent<CoinsIndexCoordinates>();
        coinRenderer = GetComponent<Renderer>();

        coinRowEnd = indexCoordinates.rows;
        coinColEnd = indexCoordinates.cols;
    }

    void Update()
    {
        // dont run Update() if there is no gesture listener
        if (!gestureListener)
            return;
        
        if (!isSpinning)
        {
            if (slideChangeWithKeys)
            {
                if (Input.GetKeyDown(KeyCode.PageDown))
                    RotateLeft();
                else if (Input.GetKeyDown(KeyCode.PageUp))
                    RotateRight();
            }

            if (slideChangeWithGestures && gestureListener)
            {
                if (gestureListener.IsSwipeLeft())
                    StartCoroutine(LeftMarquee());
                else if (gestureListener.IsSwipeRight())
                    StartCoroutine(RightMarquee());
                else if (gestureListener.IsSwipeUp())
                    StartCoroutine(UpMarquee());
                else if (gestureListener.IsSwipeDown())
                    StartCoroutine(DownMarquee());
            }
        }
        else
        {
            // spin the presentation
            if (stepsToGo > 0)
            {
                //if(Time.realtimeSinceStartup >= nextStepTime)
                {
                    if (screenCamera)
                        transform.RotateAround(transform.position, screenCamera.transform.TransformDirection(rotationAxis), rotationStep);
                    else
                        transform.Rotate(rotationAxis * rotationStep, Space.World);

                    stepsToGo--;
                    //nextStepTime = Time.realtimeSinceStartup + Time.deltaTime;
                }
            }
            else
            {
                Quaternion coinRotation = Quaternion.Euler(rotationAxis * rotationStep * 180f / spinSpeed) * initialRotation;
                transform.rotation = screenCamera ? screenCamera.transform.rotation * coinRotation : coinRotation;
                isSpinning = false;
            }
        }
    }

    // rotates cube left
    private void RotateLeft()
    {
        Debug.Log("Rotate Left");
        
        // rotate the presentation
        isSpinning = true;
        initialRotation = screenCamera ? Quaternion.Inverse(screenCamera.transform.rotation) * transform.rotation : transform.rotation;

        rotationStep = spinSpeed; // new Vector3(0, spinSpeed, 0);
        rotationAxis = Vector3.up;
        
        stepsToGo = 180 / spinSpeed;
        //nextStepTime = 0f;
    }

    // rotates cube right
    private void RotateRight()
    {
        Debug.Log("Rotate Right");

        // rotate the presentation
        isSpinning = true;
        initialRotation = screenCamera ? Quaternion.Inverse(screenCamera.transform.rotation) * transform.rotation : transform.rotation;

        rotationStep = -spinSpeed; // new Vector3(0, -spinSpeed, 0);
        rotationAxis = Vector3.up;

        stepsToGo = 180 / spinSpeed;
        //nextStepTime = 0f;
    }

    // rotates cube up
    private void RotateUp()
    {
        Debug.Log("Rotate Up");

        // rotate the presentation
        isSpinning = true;
        initialRotation = screenCamera ? Quaternion.Inverse(screenCamera.transform.rotation) * transform.rotation : transform.rotation;

        rotationStep = spinSpeed; // new Vector3(spinSpeed, 0, 0);
        rotationAxis = Vector3.right;

        stepsToGo = 180 / spinSpeed;
        //nextStepTime = 0f;
    }

    private void RotateDown()
    {
        Debug.Log("Rotate Down");

        // rotate the presentation
        isSpinning = true;
        initialRotation = screenCamera ? Quaternion.Inverse(screenCamera.transform.rotation) * transform.rotation : transform.rotation;

        rotationStep = -spinSpeed; // new Vector3(spinSpeed, 0, 0);
        rotationAxis = Vector3.right;

        stepsToGo = 180 / spinSpeed;
        //nextStepTime = 0f;
    }
    
    
    //-- IEnumerators for each direction

    public IEnumerator LeftMarquee() {
        for (int index = coinColEnd; index >= coinColStart; index--)
        {
            if (coinColId == index)
                RotateLeft();

            yield return new WaitForSeconds(1);
        }
    }

    public IEnumerator RightMarquee() {
        for (int index = coinColStart; index <= coinColEnd; index++)
        {
            if(coinColId == index)
                RotateRight();

            yield return new WaitForSeconds(1);
        }
    }

    public IEnumerator UpMarquee() {
        for (int index = coinRowEnd; index >= coinRowStart; index--)
        {
            if (coinRowId == index)
                RotateUp();

            yield return new WaitForSeconds(1);
        }
    }

    public IEnumerator DownMarquee() {
        for (int index = coinRowStart; index <= coinRowEnd; index++)
        {
            if (coinRowId == index)
                RotateDown();

            yield return new WaitForSeconds(1);
        }
    }


    public void ChangeColor() {
        if (materialsList.Length > 0)
        {
            int index = UnityEngine.Random.Range(0, materialsList.Length);
            coinRenderer.material = materialsList[index];
        }
    }

}
