using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborDefinition001 : MonoBehaviour, IColorDefinition
{
    private List<string> possibleNeighbors = new List<string> { "aba", "aba", "aba", "aba" };

    public List<string> GetPossibleNeighbors()
    {
        return possibleNeighbors;
    }

    public void Rotate(int degree)
    {
        possibleNeighbors = new List<string> { "aba", "aba", "aba", "aba" };
    }
}
