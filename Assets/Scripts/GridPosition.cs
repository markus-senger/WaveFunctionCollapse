using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class GridPosition
    {
        public int gridX { get; private set; }
        public int gridY { get; private set; }

        public float x { get; private set; }
        public float y { get; private set; }
        public bool filled { get; set; } = false;
        public float scale { get; private set; }

        public List<string> colorDefinitions { get; set; }
        public List<GameObject> possibleParts { get; set; }

        public GridPosition(float x, float y, float scale, int gridX, int gridY, List<GameObject> possibleParts)
        {
            this.possibleParts = new List<GameObject>(possibleParts);
            this.x = x;
            this.y = y;
            this.scale = scale;
            this.gridX = gridX;
            this.gridY = gridY;
        }
    }
}
