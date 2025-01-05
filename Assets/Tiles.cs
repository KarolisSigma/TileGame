using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public Material sharedMat;
    private Material greenmat;
    private Material redmat;
    private Material blackmat;
    public Material sharedmatduplicate;
    public GameObject prefab;
    public Vector2 size;
    public Transform parent;

    public GameObject roomPrefab;

    private List<GameObject> tiles =new List<GameObject>();
    private List<Material> tileMats = new List<Material>();
    private List<Renderer> tileRenderers = new List<Renderer>();


    private int emission=2;
    private Color red = Color.red;
    private Color green = Color.green;
    public AudioSource beep;
    public AudioSource badbeep;
    public AudioSource losebeep;
    public AudioSource countdown;
    public Transform player;

    public float time;
    public float density;
    private Player playercode;

    public Score score;
    


    void Start()
    {
        greenmat = new Material(sharedMat);
        greenmat.SetColor("_EmissionColor", Color.green*emission);
        greenmat.enableInstancing = true;

        redmat = new Material(sharedMat);
        redmat.SetColor("_EmissionColor", Color.red*emission);
        redmat.enableInstancing=true;

        blackmat = new Material(sharedMat);
        blackmat.SetColor("_EmissionColor", Color.black*0);
        blackmat.enableInstancing=true;


        sharedmatduplicate = new Material(sharedMat);
        sharedmatduplicate.SetColor("_EmissionColor", Color.black*0);
        sharedmatduplicate.enableInstancing=true;

        playercode = FindObjectOfType<Player>();
        MakeTiles();



       StartGame();
        //flash(Color.yellow, 2, 0.5f);
        //MakeRoom();
        //lose();

    }


    void StartGame(){
        StartCoroutine(waitforstart());
    }
    IEnumerator waitforstart(){
        countdown.Play();
        yield return new WaitForSeconds(3);
        StartCoroutine(changingtiles());
    }

    void MakeTiles(){
        red*=emission;
        green*=emission;
        int iteration = 0;
        for (int x = 0; x < size.x; x++)
        {
            for (int z = 0; z < size.y; z++)
            {

                GameObject g = Instantiate(prefab, new Vector3(x-(size.x-1)/2f, -0.5f, z-(size.y-1)/2f), Quaternion.identity);
                g.name = iteration.ToString();

                g.isStatic=true;
                Renderer renderer =g.GetComponent<Renderer>();
                renderer.sharedMaterial=blackmat;

                tileRenderers.Add(renderer);
                g.transform.SetParent(parent);
                tiles.Add(g);
                iteration++;
            }
        }
    }

    void MakeRoom(){
        GameObject room = Instantiate(roomPrefab, new Vector3(0,0.99f,0), Quaternion.identity);
        room.transform.localScale = new Vector3(size.x, 3, size.y);
    }





    IEnumerator changingtiles(){
        bool running = true;
        
        ChangeTiles(density);

        while(running){
            
            yield return new WaitForSeconds(time - 0.5f);

            if(!CheckIfRed()){
               beep.Play();
                score.AddScore();
                //yield return new WaitForSeconds(0.5f);


            }
            else{
                playercode.AddHearts(-1);
                score.ResetMultiplier();
                if(playercode.hearts<=0){
                    turnofftiles();
                    lose();
                    running=false;
                    losebeep.Play();
                    
                }
                else{
                    badbeep.Play();
                }
                
            }
            yield return new WaitForSeconds(0.5f);
            if(running){
                ChangeTiles(density);
            }

            time-=0.05f;
            density+=0.05f;
            density = Mathf.Clamp(density, 0, 0.8f);
            time = Mathf.Clamp(time, 2f, 10);
            

        }
    }


    void ChangeTiles(float density){

        

        List<int> ints  = new List<int>();
        int length = tiles.Count;

        int count = Mathf.RoundToInt(length*density);
        count = Mathf.Clamp(count, 0, length-1);
        
        

        int temp;
        for (int i = 0; i < count; i++)
        {
            temp=UnityEngine.Random.Range(0, length);
            while(ints.Contains(temp)){
                temp = UnityEngine.Random.Range(0, length);
            }
            ints.Add(temp);
        }

        for (int i = 0; i < length; i++)
        {
            if(!ints.Contains(i)){
                tiles[i].tag="Untagged";

                tileRenderers[i].sharedMaterial=greenmat;
            }
        }

        foreach (int ind in ints)
        {
            tiles[ind].tag="red";
            
            tileRenderers[ind].sharedMaterial = redmat;
        }


    }


    void turnofftiles(){
        foreach(Renderer renderer in tileRenderers){
            renderer.sharedMaterial = blackmat;
        }
    }

    #region functions

    
    public LayerMask groundLayer;
   public  bool CheckIfRed(){
        RaycastHit hit;

        Vector3 origin = player.position;
        Vector3 direction = Vector3.down; 


        if (Physics.Raycast(origin, direction, out hit, Mathf.Infinity, groundLayer))
        {
            if (hit.collider.CompareTag("red"))
            {
                
                return true;
            }else{
                //showtile(hit);
            }
        }
        return false;
       
    }

    public Material testmaterial;
    void lose(){
       
        // foreach(Renderer renderer in tileRenderers){
        //    renderer.sharedMaterial = sharedmatduplicate;
        //// }

      
       for (int i = 0; i < tileRenderers.Count; i++)
        {

                tileRenderers[i].sharedMaterial=sharedmatduplicate;
            
        }
        
            completed=0;
            // Ensure the material has emission enabled
            sharedmatduplicate.EnableKeyword("_EMISSION");

            // Animate the emission color with strength
            DOTween.To(() => sharedmatduplicate.GetColor("_EmissionColor"), // Get the current emission color
                       x => sharedmatduplicate.SetColor("_EmissionColor", x), // Set the updated emission color
                       Color.red * 5, // Target color with intensity
                       1) // Duration (1 second)
                   .SetLoops(2, LoopType.Yoyo) // Loop back and forth
                   .SetEase(Ease.OutSine)  // Smooth transition
                   .OnComplete(() => OnTweenComplete());
        
    }
    private int completed =0;
    void OnTweenComplete()
    {

            StartCoroutine(turnoff());
            
        
    }

    IEnumerator turnoff(){
        yield return new WaitForSeconds(1);
        turnofftiles();
    }


    void flash(Color color, float strength, float duration){
        foreach (Material mat in tileMats)
        {
            // Ensure the material has emission enabled
            mat.EnableKeyword("_EMISSION");

            // Animate the emission color with strength
            DOTween.To(() => mat.GetColor("_EmissionColor"), // Get the current emission color
                       x => mat.SetColor("_EmissionColor", x), // Set the updated emission color
                       color * strength, // Target color with intensity
                       duration) // Duration (1 second)
                   .SetLoops(2, LoopType.Yoyo) // Loop back and forth
                   .SetEase(Ease.InOutSine);  // Smooth transition
        }
    }

    
    void showtile(RaycastHit hit){

        Material mat = tileMats[Convert.ToInt16(hit.collider.name)];

        mat.EnableKeyword("_EMISSION");

        Color col = mat.GetColor("_EmissionColor");
        mat.SetColor("_EmissionColor", col*4f);
        // Animate the emission color with the target intensity
        DOTween.To(() => mat.GetColor("_EmissionColor"), // Get current emission color
                   x => mat.SetColor("_EmissionColor", x), // Set the updated emission color
                   col, // Target color (increase intensity by a factor of 5)
                   0.5f) // Duration (0.5 seconds)
               .SetLoops(1, LoopType.Yoyo) // Loop twice, going up and down
               .SetEase(Ease.InFlash);  // Smooth bouncing transition
    }
    #endregion
}
