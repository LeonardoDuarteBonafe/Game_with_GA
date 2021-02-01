using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Test : MonoBehaviour
{
	[Header("Genetic Algorithm")]
	[SerializeField] public float fitnessTarget = 100;
	[SerializeField] public int sizeTarget;

	private List<int[]> GenesList = new List<int[]>();
	private string[] enemyList = {"spyke", "opossum", "eagle", "lifePoint", "frog"};

	//Bonificação, Num Ações e Complexidade
	private int[] spyke = {1,1,1, 0};
	private int[] opossum = {1,1, 3, 1};
	private int[] eagle = {1, 1, 4, 2};
	private int[] lifePoint = {1,1,-1, 3};
	private int[] frog = {1,1,2, 4};
	private int[] element;

	[SerializeField] int populationSize = 200;
	[SerializeField] float mutationRate = 0.01f;
	[SerializeField] int elitism = 5;

	[Header("Other")]

	//[SerializeField] Text target;
	//[SerializeField] Text bestText;

	//[SerializeField] Text bestFitnessText;
	private float bestFitness = 0;
	//[SerializeField] Text numGenerationsText;

	private int numGenerations = 0; 
	//[SerializeField] Text textPrefab;

	private GeneticAlgorithm ga;
	private System.Random random;

	public GameObject enemyEagle;
	public GameObject enemySpyke;
	public GameObject enemyFrog;
	public GameObject enemyOpossum;
	public GameObject enemyLife;

	public List<GameObject> listOfPrefabs = new List<GameObject>();

	void Start()
	{

		GenesList.Add(spyke);
		GenesList.Add(opossum);
		GenesList.Add(eagle);
		GenesList.Add(lifePoint);
		GenesList.Add(frog);


		//target.text = (fitnessTarget).ToString();
		//numGenerationsText.text = numGenerations.ToString();

		random = new System.Random();
		ga = new GeneticAlgorithm(populationSize, sizeTarget, random, GetElement, FitnessFunction, elitism, mutationRate);

	}

	void Update()
	{
		ga.NewGeneration();

		Debug.Log("Nova Geração");
		numGenerations += 1;

		//numGenerationsText.text = numGenerations.ToString();
		//bestFitnessText.text = ga.BestFitness.ToString();

		//if (ga.BestFitness >= fitnessTarget*1) {
		//Debug.Log("Best fitness: " + ga.BestFitness);
		if (ga.Population[0].Fitness >= fitnessTarget * 1 &&
			ga.Population[1].Fitness >= fitnessTarget * 1 &&
			ga.Population[2].Fitness >= fitnessTarget * 1  
			)
		{
			for (int i = 0; i < 3; i++)
			{
				DNA aux = ga.Population[i];
				int spykeCount = 0;
				int opossumCount = 0;
				int eagleCount = 0;
				int lifeCount = 0;
				int frogCount = 0;

				for (int j = 0; j < aux.Genes.Count; j++)
				{
					switch ((int)aux.Genes[j].GetValue(aux.Genes[j].Length - 1))
					{
						case 0:
							spykeCount++;
							// Instantiate(enemySpyke, new Vector3(j * 1f, i * 1.5f, 0), Quaternion.identity);
							listOfPrefabs.Add(enemySpyke);
							break;
						case 1:
							opossumCount++;
							// Instantiate(enemyOpossum, new Vector3(j * 1.0f, i * 1.5f, 0), Quaternion.identity);
							listOfPrefabs.Add(enemyOpossum);
							break;
						case 2:
							eagleCount++;
							listOfPrefabs.Add(enemyEagle);
							// Instantiate(enemyEagle, new Vector3(j * 1.0f, i * 1.5f, 0), Quaternion.identity);
							break;
						case 3:
							lifeCount++;
							listOfPrefabs.Add(enemyLife);
							// Instantiate(enemyLife, new Vector3(j * 1.0f, i * 1.5f, 0), Quaternion.identity);
							break;
						case 4:
							frogCount++;
							listOfPrefabs.Add(enemyFrog);
							// Instantiate(enemyLife, new Vector3(j * 1.0f, i * 1.5f, 0), Quaternion.identity);
							break;
					}
				}
				Debug.Log("Valores do I = " + i + " com FITNESS = " + aux.Fitness + " : -> SPYKE: " + spykeCount + " || OPOSSUM: " + opossumCount + " || EAGLE: " + eagleCount + " || LIFE: " + lifeCount + " || FROG: " + frogCount);
				// Debug.Log(listOfPrefabs[1].ToString());
				// Instantiate(listOfPrefabs[1], new Vector3(1 * 1.0f, i * 1.5f, 0), Quaternion.identity);
			}
			this.enabled = false;
		}
	}

	public List<GameObject> GetElementsPrefab(){

		return listOfPrefabs;
	}

	private int[] GetElement()
	{
		int i = random.Next(GenesList.Count);
		/*List<int> temp = new List<int>();
		element = null;
        for (int j = 0; j <= GenesList[i].Length; j++)
        {
			if(j == GenesList[i].Length)
            {
				temp.Add(i);
            }
            else
            {
				temp.Add((int)GenesList[i].GetValue(j));
            }
        }*/
        element  = GenesList[i];
        /*for (int k = 0; k < temp.Count; k++)
        {
			element[k] = temp[k];
			Debug.Log("Elemento adicionado aqui: " + element[k]);
        }*/
		//Debug.Log("Valor ANTES DE IR: " + enemyList[i]);

		return element;

	}

	private float FitnessFunction(int index)
	{
		float score = 0;
		DNA individuo = ga.Population[index];

		// Debug.Log("numero de genes de um individuo:" + individuo.Genes.Count);

		// Calcula o score de um elemento
		for(int i = 0; i < individuo.Genes.Count; i++){  
			float temp = 1;

    		//for(int j = 0; j < individuo.Genes[i].Length; j++){ 
    		for(int j = 0; j < 3; j++){ 

				 //Debug.Log("Tamanho da lista de genes: " + individuo.Genes[i].Length);
				// temp *= (int) GenesList[i].GetValue(j); 
				 temp *= (int) individuo.Genes[i].GetValue(j);
				/*if(temp > 4)
                {
					Debug.Log("Valor do temp da fitness: " + temp + " | TAM DO IND: " + individuo.Genes[i].Length + " | VALOR DO I: " + i);
				}*/

			}
			
			// Debug.Log("Score dentro do loop: " + score);
			score += temp;	
		}

		// Debug.Log("Score: " + score);
		return score;
	}

}
