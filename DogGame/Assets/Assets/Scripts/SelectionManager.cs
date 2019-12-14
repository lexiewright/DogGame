using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using OVRSimpleJSON;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public Camera camera;
    public float sightLength;

    public GameObject couchDialogue;
    public GameObject stoveDialogue;
    public GameObject bedDialogue;
    public GameObject firePlaceDialogue;
    public GameObject shoeDialogue;
    public GameObject winDialogue;
    public TextMeshProUGUI pauseMenuText;

    private bool firePlaceHasBeenSeen = false;
    private bool stoveHasBeenSeen = false;
    private bool bedHasBeenSeen = false;
    private bool couchHasBeenSeen = false;
    private bool shoeHasBeenSeen = false;

    public string couchBool;
    public string bedBool;
    public string firePlaceBool;
    public string stoveBool;

    [SerializeField] private Material highlightMaterial;
    [SerializeField] private string selectableTag = "Selectable";
    private Material originalMaterial;
    private Transform _selection;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        couchBool = couchHasBeenSeen.ToString();
        bedBool = bedHasBeenSeen.ToString();
        stoveBool = stoveHasBeenSeen.ToString();
        firePlaceBool = firePlaceHasBeenSeen.ToString();
    }

    private void Update()
    {
        //Input keys for testing save and load without oculus
        if(Input.GetKeyDown(KeyCode.F11))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            Load();
        }

        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
           // selectionRenderer.material = originalMaterial;
            _selection = null;

            if (winDialogue.activeSelf == true && couchDialogue.activeSelf == true)
            {
                couchDialogue.SetActive(false);
            }
            else
            {
                return;
            }
            ///<summary>
            ///Uncomment to disable dialogue when you arent looking at the object
            /// </summary>
            //if(couchDialogue.activeSelf == true)
            //{
            //    couchDialogue.SetActive(false);
            //}
            //else if (bedDialogue.activeSelf == true)
            //{
            //    bedDialogue.SetActive(false);
            //}
            //else if (lampDialogue.activeSelf == true)
            //{
            //    lampDialogue.SetActive(false);
            //}
            //else if (firePlaceDialogue.activeSelf == true)
            //{
            //    firePlaceDialogue.SetActive(false);
            //}
            //else
            //{
            //    return;
            //}
           
        }

        RaycastHit seen;
        Ray rayDirection = new Ray(transform.position, transform.forward);
        
        if (Physics.Raycast(rayDirection, out seen, sightLength))
        {
            var selection = seen.transform;
            if (selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    originalMaterial = selectionRenderer.material;
                    //selectionRenderer.material = highlightMaterial;
                }
                //print(seen.collider.gameObject.name.ToString());
                
                _selection = selection;

                //Checks what we are looking at and sets turns on the dialogue if selectable object
                if (_selection.gameObject.name == "Couch_2" && stoveHasBeenSeen == true)
                {
                    winDialogue.SetActive(true);
                    selectionRenderer.material = highlightMaterial;
                    couchHasBeenSeen = true;
                    couchBool = couchHasBeenSeen.ToString();
                    couchDialogue.SetActive(false);
                    audioSource.Play(0);
                    
                    
                }
                else if (_selection.gameObject.name == "Stove" && firePlaceHasBeenSeen == true)
                {
                    stoveDialogue.SetActive(true);
                    selectionRenderer.material = highlightMaterial;
                    stoveHasBeenSeen = true;
                    stoveBool = stoveHasBeenSeen.ToString();
                    audioSource.Play(0);
                }
                else if (_selection.gameObject.name == "Bed_2")
                {
                    selectionRenderer.material = highlightMaterial;
                    bedDialogue.SetActive(true);
                    bedHasBeenSeen = true;
                    bedBool = bedHasBeenSeen.ToString();
                    audioSource.Play(0);
                    
                }
                else if (_selection.gameObject.name == "FirePlace" && bedHasBeenSeen==true)
                {
                    firePlaceDialogue.SetActive(true);
                    selectionRenderer.material = highlightMaterial;
                    firePlaceHasBeenSeen = true;
                    firePlaceBool = firePlaceHasBeenSeen.ToString();
                    audioSource.Play(0);
                }
                else if (_selection.gameObject.name == "Shoe" && stoveHasBeenSeen == true)
                {
                    shoeDialogue.SetActive(true);
                    selectionRenderer.material = highlightMaterial;
                    shoeHasBeenSeen = true;
                    audioSource.Play(0);
                }
                else
                {
                    return;
                }

                


            }

        }
    }

    public void Save()
    {
        JSONObject saveJson = new JSONObject();
        saveJson.Add("couchBool", couchBool);
        saveJson.Add("bedBool", bedBool);
        saveJson.Add("firePlaceBool", firePlaceBool);
        saveJson.Add("stoveBool", stoveBool);

        Debug.Log(saveJson.ToString());

        //Save JSON IN COMPUTER
        string path = Application.persistentDataPath + "/Save.json";
        File.WriteAllText(path, saveJson.ToString());

        pauseMenuText.text = "Game Saved";
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/Save.json";
        string jsonString = File.ReadAllText(path);
        JSONObject saveJson = (JSONObject)JSON.Parse(jsonString);

        //Set Values
        couchBool = saveJson["couchBool"];
        bedBool = saveJson["bedBool"];
        firePlaceBool = saveJson["firePlaceBool"];
        stoveBool = saveJson["stoveBool"];

        pauseMenuText.text = "Game Loaded";

        if (bedBool == "True")
        {
            bedHasBeenSeen = true;
            bedDialogue.SetActive(true);
        }
        else bedHasBeenSeen = false;

        if (firePlaceBool == "True")
        {
            firePlaceHasBeenSeen = true;
            firePlaceDialogue.SetActive(true);
        }
        else firePlaceHasBeenSeen = false;

        if (stoveBool == "True")
        {
            stoveHasBeenSeen = true;
            stoveDialogue.SetActive(true);
        }
        else stoveHasBeenSeen = false;

        if (couchBool == "True")
        {
            couchHasBeenSeen = true;
            winDialogue.SetActive(true);
        }
        else couchHasBeenSeen = false;
    }

}
