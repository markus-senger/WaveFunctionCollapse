using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFunctionCollapse : MonoBehaviour
{
    [SerializeField]
    private GameObject imageBorder;

    [SerializeField]
    private GameObject img01;

    [SerializeField]
    private GameObject img02;

    [SerializeField]
    private GameObject img03;

    [SerializeField]
    private GameObject img04;

    [SerializeField]
    private GameObject img05;

    [SerializeField]
    private GameObject img06;

    private float partSize;

    private List<GameObject> partList = new List<GameObject>();

    private GridPosition[,] grid;

    private int dim;

    private void Start()
    {
        dim = Data.dim;

        partList.Add(img01);

        partList.Add(img02);
        GameObject img0201 = Instantiate(img02);
        img0201.transform.Rotate(0, 0, 90);
        GameObject img0202 = Instantiate(img02);
        img0202.transform.Rotate(0, 0, 180);
        img0202.GetComponent<IColorDefinition>().Rotate(180);
        GameObject img0203 = Instantiate(img02);
        img0203.transform.Rotate(0, 0, 270);
        img0203.GetComponent<IColorDefinition>().Rotate(270);
        partList.Add(img0201);
        partList.Add(img0202);
        partList.Add(img0203);
        img0201.SetActive(false);
        img0202.SetActive(false);
        img0203.SetActive(false);

        partList.Add(img03);
        GameObject img0301 = Instantiate(img03);
        img0301.transform.Rotate(0, 0, 90);
        img0301.GetComponent<IColorDefinition>().Rotate(90);
        GameObject img0302 = Instantiate(img03);
        img0302.transform.Rotate(0, 0, 180);
        img0302.GetComponent<IColorDefinition>().Rotate(180);
        GameObject img0303 = Instantiate(img03);
        img0303.transform.Rotate(0, 0, 270);
        img0303.GetComponent<IColorDefinition>().Rotate(270);
        partList.Add(img0301);
        partList.Add(img0302);
        partList.Add(img0303);
        img0301.SetActive(false);
        img0302.SetActive(false);
        img0303.SetActive(false);

        /*partList.Add(img04);
        GameObject img0401 = Instantiate(img04);
        img0401.transform.Rotate(0, 0, 90);
        img0401.GetComponent<IColorDefinition>().Rotate(90);
        GameObject img0402 = Instantiate(img04);
        img0402.transform.Rotate(0, 0, 180);
        img0402.GetComponent<IColorDefinition>().Rotate(180);
        GameObject img0403 = Instantiate(img04);
        img0403.transform.Rotate(0, 0, 270);
        img0403.GetComponent<IColorDefinition>().Rotate(270);
        partList.Add(img0401);
        partList.Add(img0402);
        partList.Add(img0403);
        img0401.SetActive(false);
        img0402.SetActive(false);
        img0403.SetActive(false);*/

        partList.Add(img05);

        partList.Add(img06);
        GameObject img0601 = Instantiate(img06);
        img0601.transform.Rotate(0, 0, 90);
        img0601.GetComponent<IColorDefinition>().Rotate(90);
        GameObject img0602 = Instantiate(img06);
        img0602.transform.Rotate(0, 0, 180);
        img0602.GetComponent<IColorDefinition>().Rotate(180);
        GameObject img0603 = Instantiate(img06);
        img0603.transform.Rotate(0, 0, 270);
        img0603.GetComponent<IColorDefinition>().Rotate(270);
        partList.Add(img0601);
        partList.Add(img0602);
        partList.Add(img0603);
        img0601.SetActive(false);
        img0602.SetActive(false);
        img0603.SetActive(false);

        grid = new GridPosition[dim, dim];
        partSize = imageBorder.transform.localScale.x / dim;

        float startX = -imageBorder.transform.localScale.x / 2f + partSize / 2f;
        float startY = imageBorder.transform.localScale.y / 2f - partSize / 2f;

        float x = startX;
        float y = startY;

        for(int i = 0; i < dim; i++)
        {
            for(int j = 0; j < dim; j++)
            {
                grid[i, j] = new GridPosition(x, y, partSize, j, i, partList);
                x += partSize;
            }
            y -= partSize;
            x = startX;
        }

        Random.InitState(System.DateTime.Now.Millisecond);
    }

    private void Update()
    {
        int min = int.MaxValue;
        List<GridPosition> nextPositions = new List<GridPosition>();
        for(int i = 0; i < dim; i++)
        {
            for(int j = 0; j < dim; j++)
            {
                if (grid[i, j].possibleParts.Count <= min && !grid[i, j].filled)
                {
                    if (grid[i, j].possibleParts.Count < min)
                    {
                        nextPositions.Clear();
                        min = grid[i, j].possibleParts.Count;
                    }
                    nextPositions.Add(grid[i, j]);
                }
            }
        }

        if (nextPositions.Count > 0)
        {           
            GridPosition nextPosition = nextPositions[Random.Range(0, nextPositions.Count)];
            if (nextPosition != null)
            {
                nextPosition.filled = true;
                GameObject next = Instantiate(nextPosition.possibleParts[Random.Range(0, min)]);
                next.SetActive(true);

                next.GetComponent<IColorDefinition>().Rotate((int) next.transform.rotation.eulerAngles.z);

                next.transform.localPosition = new Vector3(nextPosition.x, nextPosition.y, 0);
                next.transform.localScale = new Vector3(partSize, partSize, 1);
                nextPosition.colorDefinitions = next.GetComponent<IColorDefinition>().GetPossibleNeighbors();

                UpdatePossibleNeighbors(nextPosition);
            }
        }
    }

    private void UpdatePossibleNeighbors(GridPosition position)
    {
        List<GameObject> newPossibleNeighbors = new List<GameObject>();

        if (position.gridY - 1 >= 0 && !grid[position.gridY - 1, position.gridX].filled) {
            foreach (var part in partList)
            {
                part.GetComponent<IColorDefinition>().Rotate((int)part.transform.rotation.eulerAngles.z);
                if (!position.colorDefinitions[0].Equals(part.GetComponent<IColorDefinition>().GetPossibleNeighbors()[2]))
                {
                    if (!newPossibleNeighbors.Contains(part))
                        newPossibleNeighbors.Add(part);
                }
            }

            grid[position.gridY - 1, position.gridX].possibleParts.RemoveAll(p => newPossibleNeighbors.Contains(p));           
        }


        newPossibleNeighbors.Clear();
        if (position.gridY + 1 < dim && !grid[position.gridY + 1, position.gridX].filled)
        {
            foreach (var part in partList)
            {
                part.GetComponent<IColorDefinition>().Rotate((int)part.transform.rotation.eulerAngles.z);
                if (!position.colorDefinitions[2].Equals(part.GetComponent<IColorDefinition>().GetPossibleNeighbors()[0]))
                {
                    if (!newPossibleNeighbors.Contains(part))
                        newPossibleNeighbors.Add(part);
                }
            }

            grid[position.gridY + 1, position.gridX].possibleParts.RemoveAll(p => newPossibleNeighbors.Contains(p));
        }

        newPossibleNeighbors.Clear();
        if (position.gridX + 1 < dim && !grid[position.gridY, position.gridX + 1].filled)
        {
            foreach (var part in partList)
            {
                part.GetComponent<IColorDefinition>().Rotate((int)part.transform.rotation.eulerAngles.z);
                if (!position.colorDefinitions[1].Equals(part.GetComponent<IColorDefinition>().GetPossibleNeighbors()[3]))
                {
                    if (!newPossibleNeighbors.Contains(part))
                        newPossibleNeighbors.Add(part);
                }
            }

            grid[position.gridY, position.gridX + 1].possibleParts.RemoveAll(p => newPossibleNeighbors.Contains(p));
        }

        newPossibleNeighbors.Clear();
        if (position.gridX - 1 >= 0 && !grid[position.gridY, position.gridX - 1].filled)
        {
            foreach (var part in partList)
            {
                part.GetComponent<IColorDefinition>().Rotate((int)part.transform.rotation.eulerAngles.z);
                var list = part.GetComponent<IColorDefinition>().GetPossibleNeighbors(); 
                if (!position.colorDefinitions[3].Equals(part.GetComponent<IColorDefinition>().GetPossibleNeighbors()[1]))
                {
                    if (!newPossibleNeighbors.Contains(part))
                        newPossibleNeighbors.Add(part);
                }
            }

            grid[position.gridY, position.gridX - 1].possibleParts.RemoveAll(p => newPossibleNeighbors.Contains(p));
        }
    }
}
