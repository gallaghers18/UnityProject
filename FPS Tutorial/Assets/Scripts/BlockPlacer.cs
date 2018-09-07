using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer : MonoBehaviour {
    [SerializeField]
    private GameObject actionbar;
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject blockHighlightPrefab;
    [SerializeField]
    private GameObject eggPrefab;


    private bool mouseButtonDown0 = false;
    private bool mouseButtonDown1 = false;
    private bool eggSpawned = false;
    private float timeToGo;
    private Actionbar actionbarScript;
    private int wallHeight;
    private int wallLength;
    private GameObject blockHighlight;
    private GameObject egg;

    // Use this for initialization
    void Start () {
        timeToGo = Time.fixedTime + 0.2f;
        actionbarScript = actionbar.GetComponent<Actionbar>();
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        if (Input.GetMouseButtonDown(0))
        {
            mouseButtonDown0 = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseButtonDown0 = false;
        }

        if (mouseButtonDown0 && Time.fixedTime >= timeToGo)
        {
            wallHeight = (2 * actionbarScript.GetCurrentSelect() + 1);
            wallLength = (2 * actionbarScript.GetCurrentSelect() + 1);
            SpawnWall();

            timeToGo = Time.fixedTime + 0.25f;
        }

        if (Input.GetMouseButtonDown(1))
        {
            mouseButtonDown1 = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            mouseButtonDown1 = false;
        }

        if (mouseButtonDown1 && Time.fixedTime >= timeToGo)
        {
            DeleteBlock();
            timeToGo = Time.fixedTime + 0.25f;
        }

        if (Input.GetKeyDown(KeyCode.E) && !eggSpawned)
        {
            SpawnEgg();
            eggSpawned = true;
        }

        ResetBlockHighlight();

    }

   

    void DeleteBlock()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            GameObject objectFound = hit.transform.gameObject;
            if (objectFound.tag == "PlayerMade")
            {
                Destroy(objectFound);

            }
        }
    }

    void SpawnWall()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            Vector3 rawPoint = hit.point + 0.5f * hit.normal;
            Vector3 snappedPoint = new Vector3(Mathf.CeilToInt(rawPoint.x) - 0.5f, Mathf.CeilToInt(rawPoint.y) - 0.5f, Mathf.CeilToInt(rawPoint.z) - 0.5f);
            
            // Decide which orientation we need to place the wall.
            // Use widthDirection and heightDirection down below to distribute blocks.
            Vector3 heightDirection = Vector3.zero;
            Vector3 widthDirection = Vector3.zero;
            if (hit.normal.x != 0)
            {
                heightDirection = Vector3.right * hit.normal.x;
                widthDirection = Vector3.forward;
            }
            else if (hit.normal.z != 0)
            {
                heightDirection = Vector3.forward * hit.normal.z;
                widthDirection = Vector3.right;
            }
            else
            {
                heightDirection = Vector3.up * hit.normal.y;

                Vector3 lookDirection = cam.transform.forward;
                if (Mathf.Abs(lookDirection.x) < Mathf.Abs(lookDirection.z))
                {
                    widthDirection = Vector3.right;
                }
                else
                {
                    widthDirection = Vector3.forward;
                }

            }

            // Loop through placing the blocks.
            for (int i=-wallLength / 2; i< wallLength / 2 + 1; i++)
            {
                for (int j=0; j< wallHeight; j++)
                {
                    Vector3 tempPoint = snappedPoint + i * widthDirection + j * heightDirection;
                    GameObject block = Instantiate(blockPrefab, tempPoint, Quaternion.identity);
                }
               
            }

        }

    }


    private void SpawnEgg()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            Vector3 rawPoint = hit.point + 0.5f * hit.normal;
            Vector3 snappedPoint = new Vector3(Mathf.CeilToInt(rawPoint.x) - 0.5f, Mathf.CeilToInt(rawPoint.y), Mathf.CeilToInt(rawPoint.z) - 0.5f);
            egg = Instantiate(eggPrefab, snappedPoint, Quaternion.identity);
        }
    }

    public GameObject GetEgg()
    {
        return egg;
    }

    private void ResetBlockHighlight()
    {
        Destroy(blockHighlight);

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            Vector3 rawPoint = hit.point + 0.5f * hit.normal;
            Vector3 snappedPoint = new Vector3(Mathf.CeilToInt(rawPoint.x) - 0.5f, Mathf.CeilToInt(rawPoint.y) - 0.5f, Mathf.CeilToInt(rawPoint.z) - 0.5f);
            blockHighlight = Instantiate(blockHighlightPrefab, snappedPoint, Quaternion.identity);
        }
    }

    public void RemoveBlockHighlight()
    {
        Destroy(blockHighlight);
    }

    // At the moment I don't actually use this -- I just use spawnWall for a 1x1 wall
    void SpawnBlock()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            Vector3 rawPoint = hit.point + 0.5f * hit.normal;
            Vector3 snappedPoint = new Vector3(Mathf.CeilToInt(rawPoint.x) - 0.5f, Mathf.CeilToInt(rawPoint.y) - 0.5f, Mathf.CeilToInt(rawPoint.z) - 0.5f);
            GameObject block = Instantiate(blockPrefab, snappedPoint, Quaternion.identity);
        }

    }
}
