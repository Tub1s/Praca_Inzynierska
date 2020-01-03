using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Created using tutorial: https://www.youtube.com/watch?v=sLtelfckEjc

public class CityGenerator : MonoBehaviour
{
    public GameObject[] buildings; // Contains differnet buildings models
    public GameObject xstreets; // Street prefab going along X axis
    public GameObject zstreets; // Street prefab going along Z axis
    public GameObject crossroad; // Crossroad prefab
    public GameObject npcCharacter;
    public NavMeshSurface surface; // Navigation layer of generated map
    public int mapWidth; // Width of the generated map
    public int mapHeight; // Height of the generated map
    public int distanceBetweenBuildings;
    public int numberOfNPCs;
    int[,] mapgrid; // Two dimensional array represting our current map
    public float displaySeed;

    // Random locations to spawn NPC
    
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
    
    void Start()
    {

        float seed = Random.Range(0, 10000000);
        displaySeed = seed;

        mapgrid = new int[mapWidth, mapHeight];

        /*  
            Generating data of the current map
            Randomize which building will be placed at the grid
            Multiply by 10 to match result bracket from 0 - 10
            Divide width/height by number can improve differences in the generation
            Too high values make generated map lose definition 
        */
        for (int height = 0; height < mapHeight; height++)
        {
            for (int width = 0; width < mapWidth; width++)
            {

                // int result = (int)(Mathf.PerlinNoise(width/10.0f + seed, height/10.0f + seed) * 10);
                mapgrid[width, height] = (int)(Mathf.PerlinNoise(width / 10.0f + seed, height / 10.0f + seed) * 10);
            }
        }

        /* 
            Creates random rows with value of -1
            In the generation step it is used to determine where streets will be placed
            At the point of crossing of two -1 lines crossroad is placed
        */
        int x = 0;
        for (int n = 0; n < 50; n++)
        {
            for (int height = 0; height < mapHeight; height++)
            {
                mapgrid[x, height] = -1;
            }
            x += Random.Range(4, 4); // Change to tell generator distance between next roads. Default from tutorial 3,3 
            if (x >= mapWidth) break;
        }

        int z = 0;
        for (int n = 0; n < 50; n++)
        {
            for (int width = 0; width < mapWidth; width++)
            {
                if (mapgrid[width, z] == -1) mapgrid[width, z] = -3; // put in crossroad
                else mapgrid[width, z] = -2;
            }
            z += Random.Range(5, 10); // Change to tell generator distance between crossroads in chunks. Default from tutorial 5,20
            if (z >= mapHeight) break;
        }

        /* Generate city */
        for (int height = 0; height < mapHeight; height++)
        {
            for (int width = 0; width < mapWidth; width++)
            {
                int result = mapgrid[width, height];
                Vector3 position = new Vector3(width * distanceBetweenBuildings, 0, height * distanceBetweenBuildings);
                if (result < -2) Instantiate(crossroad, position, crossroad.transform.rotation);
                else if (result < -1) Instantiate(xstreets, position, xstreets.transform.rotation);
                else if (result < 0) Instantiate(zstreets, position, zstreets.transform.rotation);
                else if (result < 1) Instantiate(buildings[0], position, Quaternion.identity);
                else if (result < 2) Instantiate(buildings[1], position, Quaternion.identity);
                else if (result < 4) Instantiate(buildings[2], position, Quaternion.identity);
                else if (result < 6) Instantiate(buildings[3], position, Quaternion.identity);
                else if (result < 7) Instantiate(buildings[4], position, Quaternion.identity);
                else if (result < 10) Instantiate(buildings[5], position, Quaternion.identity);
            }
        }

        surface.BuildNavMesh();

        // Random points for NPC spawners
        
        for (int n = 0; n < numberOfNPCs; n++)
        {
            Vector3 randomLocation = RandomNavmeshLocation(50);
            Instantiate(npcCharacter, randomLocation, Quaternion.identity);
        }
        

    }

}