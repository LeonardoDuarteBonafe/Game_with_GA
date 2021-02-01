using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private System.Random random;
    private const float END_OF_PHASE = 200f;
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 5f;
    public float PLAYER_DISTANCE_UNTIL_THE_END = 100f;
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private CharacterController2D player;

    private bool gameIsOver = false;

    private Vector3 lastStartPosition;
    private Vector3 lastEndPosition;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    private float maxAxisXWidht, minAxisXWidht, maxAxisXChange, widthChange;

    private ObjectGenerator theCoinGenerator;
    private EnemyGenerator theEnemyGenerator;
    private bool gameReady = false;
    public float randomCoinThreshold;
    public float randomEnemyThreshold;
    private float spawnPositionCoin;
    private float spawnPositionEnemy;

    private float penultimateEndPosition;

    public bool HighLow = false;

    public GameObject enemyEagle;
	public GameObject enemySpyke;
	public GameObject enemyFrog;
	public GameObject enemyOpossum;
	public GameObject enemyLife;    

    public Test GA; 
    public List<GameObject> prefabsList = new List<GameObject>();

    private int randomElement; // Qual objeto vai ser instanciado
    private float randomX; //Onde vai instanciar os objetos

    public int valoresDoRange = 10;
    private int numberOfEnemies;
    private List<string> enemiesListed;

    private void Awake()
    {
        valoresDoRange = GA.sizeTarget;
        numberOfEnemies = 0;
        enemiesListed = new List<string>();

        widthChange = 0f;
        heightChange = 0f;

        maxAxisXChange = .5f;
        minAxisXWidht = 0.5f;
        maxAxisXWidht = 2f;

        theCoinGenerator = FindObjectOfType<ObjectGenerator>();
        theEnemyGenerator = FindObjectOfType<EnemyGenerator>();
        randomCoinThreshold = 25f;
        randomEnemyThreshold = 100f;
        gameReady = false;

        maxHeightChange = 1f;
        PLAYER_DISTANCE_UNTIL_THE_END = 150f;
        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;
        gameIsOver = false;
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        lastStartPosition = levelPart_Start.Find("StartPosition").position;
        int startingSpawnLevelParts = 3;

        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }
        gameReady = true;

        prefabsList = GA.GetElementsPrefab();
        random = new System.Random();
    }
    
    private void Update()
    {
        float distanceFromEnd = Vector3.Distance(player.transform.position, lastEndPosition);

        if (Random.value >= 0.5){
            gameReady = true;
        }
        else {
            gameReady = false;
        }

        // Debug.Log("LastEndPosition value: " + lastEndPosition.x);
        //Debug.Log("Player: " + player.transform.position + " || End Position: " + lastEndPosition + " || Distancia: " + distanceFromEnd);
        if (!gameIsOver)
        {
            if (lastEndPosition.x >= PLAYER_DISTANCE_UNTIL_THE_END)
            {
                /*SpawnLastLevelPart();
                // Debug.Log("Last Start Position: " + lastStartPosition + "- Last End Position: " + lastEndPosition);
            
                // Random para gerar um inimigo ou alguma vida

               
                // Instantiate(prefabsList[0], new Vector3(lastEndPosition.x, lastEndPosition.y, lastEndPosition.z), Quaternion.identity);
                // prefabsList.RemoveAt(0);

                gameIsOver = true;*/
            }
            else
            {
                //if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
                if ((lastEndPosition.x - player.transform.position.x) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
                {
                    // Spawn another level part
                    SpawnLevelPart();
                    // Debug.Log("Last Start Position: " + lastStartPosition.x + "- Last End Position: " + lastEndPosition.x);

                    // Debug.Log("Número de itens: " + prefabsList.Count);
                    if(prefabsList.Count == 0)
                    {
                        SpawnLastLevelPart();
                        gameIsOver = true;
                    }
                    else
                    {
                        enemiesListed.Clear();
                        for (int i = 0; i < numberOfEnemies; i++)
                        {
                            if (Random.Range(0, 10) >= 0 && prefabsList.Count > 0)
                            {

                                //randomElement = Random.Range(0, prefabsList.Count);
                                randomElement = Random.Range(0, valoresDoRange);

                                Debug.Log("elemento aleatório: " + prefabsList[randomElement].name.ToString());
                                if(enemiesListed.Count > 0)
                                {
                                    foreach (string enemyName in enemiesListed)
                                    {
                                        if (prefabsList[randomElement].name.ToString().Equals(enemyName)){
                                            if (prefabsList[randomElement].name.ToString().Contains("eagle"))
                                            {
                                                heightChange += 0.3f;
                                            }
                                            else
                                            {
                                                lastStartPosition.x += 0.2f;
                                            }
                                        }
                                    }
                                }
                                enemiesListed.Add(prefabsList[randomElement].name.ToString());
                                theEnemyGenerator.SpawnEnemy(prefabsList[randomElement], lastEndPosition, lastStartPosition, penultimateEndPosition, widthChange, heightChange);
                                prefabsList.RemoveAt(randomElement);

                                valoresDoRange--;
                                if (valoresDoRange == 0)
                                {
                                    valoresDoRange = GA.sizeTarget;
                                }

                                /*switch (prefabsList[randomElement].name){

                                    case "eaglePlataform":
                                        //Instantiate(prefabsList[randomElement], new Vector3(lastEndPosition.x + 0.2f, lastEndPosition.y, lastEndPosition.z), Quaternion.identity);
                                        theEnemyGenerator.SpawnEnemy(prefabsList[randomElement], lastEndPosition, lastStartPosition, penultimateEndPosition, widthChange, heightChange);
                                        prefabsList.RemoveAt(randomElement);
                                    break;

                                    case "lifePoint":
                                        randomX = Random.Range(lastStartPosition.x, lastEndPosition.x);
                                        //Instantiate(prefabsList[randomElement], new Vector3(randomX, lastEndPosition.y + 0.05f, lastEndPosition.z), Quaternion.identity);
                                        theEnemyGenerator.SpawnEnemy(prefabsList[randomElement], lastEndPosition, lastStartPosition, penultimateEndPosition, widthChange, heightChange);
                                        prefabsList.RemoveAt(randomElement);
                                    break;

                                    case "spykes":

                                        //randomX = Random.Range(lastStartPosition.x + 0.3f, lastEndPosition.x - 0.3f);
                                        //Instantiate(prefabsList[randomElement], new Vector3(randomX, lastEndPosition.y, lastEndPosition.z), Quaternion.identity);
                                        theEnemyGenerator.SpawnEnemy(prefabsList[randomElement], lastEndPosition, lastStartPosition, penultimateEndPosition, widthChange, heightChange);
                                        prefabsList.RemoveAt(randomElement);
                                    break;

                                    default:
                                        //randomX = Random.Range(lastStartPosition.x, lastEndPosition.x);
                                        //Instantiate(prefabsList[randomElement], new Vector3(randomX, lastEndPosition.y, lastEndPosition.z), Quaternion.identity);
                                        theEnemyGenerator.SpawnEnemy(prefabsList[randomElement], lastEndPosition, lastStartPosition, penultimateEndPosition, widthChange, heightChange);
                                        prefabsList.RemoveAt(randomElement);
                                    break;

                                }*/
                            }
                        } 
                    }
                }
            }
        }

        /*if(lastEndPosition.x >= END_OF_PHASE){

                SpawnFinalPart();

        }*/

    }

    private void PlatformHeight()
    {
        //Testing x variation
        widthChange = Random.Range(maxAxisXWidht, minAxisXWidht);
        if(widthChange > maxAxisXWidht)
        {
            widthChange = maxAxisXWidht;
        }
        else
        {
            if(widthChange < minAxisXWidht)
            {
                widthChange = minAxisXWidht;
            }
        }


        float randomNumber = Random.Range(maxHeightChange, -maxHeightChange);
        //int roundedValue = Mathf.RoundToInt(randomNumber);
        //Debug.Log("NUMERO RANDOM: " + randomNumber + " || VALOR DO rounded: " + roundedValue);
        // Debug.Log("NUMERO RANDOM: " + randomNumber);
        heightChange += randomNumber;
        //heightChange += 0f;
        //heightChange += roundedValue;

        if (heightChange > maxHeight)
        {
            heightChange = maxHeight;
        }
        else
        {
            if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }
        }

        // Debug.Log("Valor da altura: " + heightChange + "  |||| VALOR do X: " + widthChange);
    }

    private void SpawnLevelPart()
    {

        //Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count - 1)];
        //Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        //lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        PlatformHeight();
        Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count -1 )];
        if (chosenLevelPart.name.Contains("15"))
        {
            numberOfEnemies = 3;
        }
        else if(chosenLevelPart.name.Contains("10"))
        {
            numberOfEnemies = 2;
        }
        else if (chosenLevelPart.name.Contains("5"))
        {
            numberOfEnemies = 1;
        }
        Debug.Log("Number of enemies: " + numberOfEnemies + " || plataforma: " + chosenLevelPart.name);
        Transform lastLevelPartTransform;
        lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));

        //Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
        /*
        if (HighLow)
        {
            lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, lastEndPosition.y + maxHeightChange, lastEndPosition.z));
            }

        else
        {
            lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, lastEndPosition.y + maxHeightChange, lastEndPosition.z));
            HighLow = !HighLow;
        }
        */

        penultimateEndPosition = lastEndPosition.x;
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        lastStartPosition = lastLevelPartTransform.Find("StartPosition").position;
        
        if (gameReady)
        {
            if (Random.Range(0f, 30f) < randomCoinThreshold)
            {
                // Debug.Log("Coin respawn");
                spawnPositionCoin = lastStartPosition.x  + (lastEndPosition.x - lastStartPosition.x)/2;
                theCoinGenerator.SpawnCoins(new Vector3(spawnPositionCoin + widthChange, heightChange + 0.8f, lastEndPosition.z));
            }
        }

        
        // Debug.Log(lastStartPosition + " - " +  lastEndPosition);
    }

    private void SpawnLastLevelPart()
    {
            Transform chosenLevelPart = levelPartList[levelPartList.Count-1];
            Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
            lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
            lastStartPosition = lastLevelPartTransform.Find("StartPosition").position;
    }

    private void SpawnFinalPart(){

            Transform chosenLevelPart = levelPartList[levelPartList.Count];
            Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + widthChange, heightChange, lastEndPosition.z));
            lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
            lastStartPosition = lastLevelPartTransform.Find("StartPosition").position;

    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
