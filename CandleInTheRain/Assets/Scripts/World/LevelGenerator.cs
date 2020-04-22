using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Vector2 bottomLeftCorner;
    public Vector2 topRightCorner;

    public int amountOfRocks = 32;
    public int amountOfGrass = 32;

    public float minScaleRocks = 0.33f;
    public float minScaleGrass = 0.33f;

    public Transform[] rocksPrefabs;
    public Transform[] grassPrefabs;

    public Transform rocksHolder;
    public Transform grassHolder;

    [ContextMenu("LevelGen/Create rocks")]
    public void CreateRocks()
    {
        for (int i = 0; i < amountOfRocks; i++)
        {
            Transform rock = Instantiate(rocksPrefabs[Random.Range(0, rocksPrefabs.Length)], GetRandomPosition(), Random.rotation, rocksHolder);
            rock.localScale = new Vector3(Random.Range(minScaleRocks, 1f), Random.Range(minScaleRocks, 1f), Random.Range(minScaleRocks, 1f));
        }
    }

    [ContextMenu("LevelGen/Create grass")]
    public void CreateGrass()
    {
        for (int i = 0; i < amountOfGrass; i++)
        {
            float scaleX = Random.Range(minScaleGrass, 1f);
            float scaleY = Random.Range(minScaleGrass, 1f);
            float scaleZ = Random.Range(minScaleGrass, 1f);
            float offsetY = scaleX * 0.25f;
            Transform grass = Instantiate(grassPrefabs[Random.Range(0, grassPrefabs.Length)], GetRandomPosition(offsetY), Quaternion.Euler(0f, -90f, 90f), grassHolder);
            grass.localScale = new Vector3(grass.localScale.x * scaleX, grass.localScale.y * scaleY, grass.localScale.z * scaleZ);
        }
    }

    [ContextMenu("LevelGen/Remove rocks")]
    public void RemoveRocks()
    {
        Transform[] children = rocksHolder.GetComponentsInChildren<Transform>();

        for (int i = 0; i < children.Length; i++)
        {
            if (children[i] != rocksHolder && children[i] != null)
            {
                DestroyImmediate(children[i].gameObject);
            }                
        }
    }

    [ContextMenu("LevelGen/Remove grass")]
    public void RemoveGrass()
    {
        Transform[] children = grassHolder.GetComponentsInChildren<Transform>();

        for (int i = 0; i < children.Length; i++)
        {
            if (children[i] != grassHolder && children[i] != null)
            {
                DestroyImmediate(children[i].gameObject);
            }
        }
    }

    private Vector3 GetRandomPosition(float y = 0f)
    {
        return new Vector3(Random.Range(bottomLeftCorner.x, topRightCorner.x), y, Random.Range(bottomLeftCorner.y, topRightCorner.y));
    }
}