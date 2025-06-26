using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoFarming : MonoBehaviour
{
    public int growthStage;
    public int dayPlanted = -1;

    public bool hasDayBeenCounted = false;

    private bool canInteract = false;
    public bool hasBeenWatered = false;

    public int potatoYield = 2;
    public int goldYield = 5;
    public float goldChance = 5;

    public bool isGolden = false;

    public bool canPlant = true;
    public bool canWater = false;
    public bool canFertilize = false;
    public bool canHarvest = false;

    public Sprite[] potatoStages;
    public Sprite[] wateredStages;
    public Sprite[] fertStages;

    public SpriteRenderer plantStage;

    public GameObject plantIndicator;
    public GameObject waterIndicator;
    public GameObject fertIndicator;
    public GameObject harvestIndicator;


    public GameObject plantVisual;
    public GameObject harvestVisual;
    public GameObject goldHarvestVisual;
    public GameObject plantingAnimation;
    public GameObject wateringAnimation;
    public GameObject fertilizingAnimation;
    public GameObject harvestAnimation;

    private TimeManager TM;
    private DayChecker DC;
    private Player P;

    // Start is called before the first frame update
    void Start()
    {
        TM = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        DC = GameObject.Find("TimeManager").GetComponent<DayChecker>();
        P = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dayPlanted == -1)
        {
            dayPlanted = DC.Day;
        }
        if ((DC.Day - dayPlanted) > 0 && !hasDayBeenCounted && hasBeenWatered)
        {
            growthStage++;
            canWater = true;
            plantStage.sprite = potatoStages[growthStage];
            hasDayBeenCounted = true;
            hasBeenWatered = false;
            if (Random.Range(1, 100) + goldChance > 50)
            {
                isGolden = true;
            }
            else
                goldChance += goldChance;
            
        }

        if (canInteract && canPlant && Input.GetKeyDown(KeyCode.F))
        {
            P.score--;
            P.scoreUI.text = "Potatoes: " + P.score;
            plantVisual.SetActive(true);
            //plantingAnimation.SetActive(true);
            plantIndicator.SetActive(false);
            TM.timeCanProgress = false;
            canPlant = false;
            StartCoroutine(canNowWater());
            plantStage.sprite = potatoStages[growthStage];
            P.GetComponent<SpriteRenderer>().enabled = false;
            P.gameObject.transform.position = plantingAnimation.transform.position;
            plantingAnimation.SetActive(true);
            P.inDialog = true;

            
        }

        if (canInteract && P.waterCount > 0 && canWater && Input.GetKeyDown(KeyCode.F))
        {
            plantVisual.SetActive(true);
            //wateringAnimation.SetActive(true);
            waterIndicator.SetActive(false);
            TM.timeCanProgress = false;
            canWater = false;
            P.waterCount--;
            P.waterUI.text = "" + P.waterCount;
            StartCoroutine(canNowFert());
            plantStage.sprite = wateredStages[growthStage];
            P.GetComponent<SpriteRenderer>().enabled = false;
            P.gameObject.transform.position = wateringAnimation.transform.position;
            wateringAnimation.SetActive(true);
            P.inDialog = true;

            
        }

        if (canInteract && P.fertCount > 0 && canFertilize && Input.GetKeyDown(KeyCode.F))
        {
            growthStage++;
            canFertilize = false;
            P.fertCount--;
            P.fertUI.text = "" + P.fertCount;
            fertIndicator.SetActive(false);
            if (growthStage < 2)
            {
                plantVisual.SetActive(true);
                //fertilizingAnimation.SetActive(true);
                TM.timeCanProgress = false;
                StartCoroutine(canNowFert());
                plantStage.sprite = fertStages[growthStage];
                P.GetComponent<SpriteRenderer>().enabled = false;
                P.gameObject.transform.position = fertilizingAnimation.transform.position;
                fertilizingAnimation.SetActive(true);
                P.inDialog = true;
                if (Random.Range(1, 100) + goldChance > 50)
                {
                    isGolden = true;
                }
                else
                    goldChance += goldChance;
                
                
            }
            else
            {
                P.GetComponent<SpriteRenderer>().enabled = false;
                P.gameObject.transform.position = fertilizingAnimation.transform.position;
                fertilizingAnimation.SetActive(true);
                P.inDialog = true;
                
                StartCoroutine(canNowHarvest());
            }

        }

        if (canInteract && canHarvest && Input.GetKeyDown(KeyCode.F))
        {
            plantVisual.SetActive(false);
            if (isGolden)
                goldHarvestVisual.SetActive(false);
            else
                harvestVisual.SetActive(false);
            harvestIndicator.SetActive(false);
            TM.timeCanProgress = false;
            canHarvest = false;
            StartCoroutine(canNowPlant());
            plantStage.sprite = potatoStages[0];
            P.GetComponent<SpriteRenderer>().enabled = false;
            P.gameObject.transform.position = harvestAnimation.transform.position;
            harvestAnimation.SetActive(true);
            P.inDialog = true;
            
            
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = true;
            if (canPlant)   
                plantIndicator.SetActive(true);
            else if (canWater)   
                waterIndicator.SetActive(true);
            else if (canFertilize)   
                fertIndicator.SetActive(true);
            else if (canHarvest)   
                harvestIndicator.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = false;   
            if (canPlant)   
                plantIndicator.SetActive(false);
            else if (canWater)   
                waterIndicator.SetActive(false);
            else if (canFertilize)   
                fertIndicator.SetActive(false);
            else if (canHarvest)   
                harvestIndicator.SetActive(false);
            
        }
    }
    IEnumerator canNowWater()
    {
        yield return new WaitForSeconds(1.33f);
        P.GetComponent<SpriteRenderer>().enabled = true;
        TM.timeCanProgress = true;
        P.inDialog = false;
        plantingAnimation.gameObject.SetActive(false);
        wateringAnimation.gameObject.SetActive(false);
        fertilizingAnimation.gameObject.SetActive(false);
        harvestAnimation.gameObject.SetActive(false);
        canWater = true;
    }

    IEnumerator canNowFert()
    {
        yield return new WaitForSeconds(1.33f);
        P.GetComponent<SpriteRenderer>().enabled = true;
        TM.timeCanProgress = true;
        P.inDialog = false;
        plantingAnimation.gameObject.SetActive(false);
        wateringAnimation.gameObject.SetActive(false);
        fertilizingAnimation.gameObject.SetActive(false);
        harvestAnimation.gameObject.SetActive(false);
        canFertilize = true;
    }
    
    IEnumerator canNowHarvest()
    {
        yield return new WaitForSeconds(1.33f);
        P.GetComponent<SpriteRenderer>().enabled = true;
        TM.timeCanProgress = true;
        P.inDialog = false;
        plantingAnimation.gameObject.SetActive(false);
        wateringAnimation.gameObject.SetActive(false);
        fertilizingAnimation.gameObject.SetActive(false);
        harvestAnimation.gameObject.SetActive(false);
        if (isGolden)
            goldHarvestVisual.SetActive(true);
        else
            harvestVisual.SetActive(true);
        canHarvest = true;
    }

    IEnumerator canNowPlant()
    {
        yield return new WaitForSeconds(1.33f);
        growthStage = 0;
        canPlant = true;
        P.GetComponent<SpriteRenderer>().enabled = true;
        TM.timeCanProgress = true;
        P.inDialog = false;
        plantingAnimation.gameObject.SetActive(false);
        wateringAnimation.gameObject.SetActive(false);
        fertilizingAnimation.gameObject.SetActive(false);
        harvestAnimation.gameObject.SetActive(false);
        if (isGolden)
        {
            P.score += goldYield;
            P.scoreUI.text = "Potatoes: " + P.score;
            goldHarvestVisual.SetActive(false);
           
        } 
        else
        {
            P.score += potatoYield;
            P.scoreUI.text = "Potatoes: " + P.score;
            harvestVisual.SetActive(false);
        }
            
        isGolden = false;
    }

   
}
