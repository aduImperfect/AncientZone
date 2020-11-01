using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPoints : MonoBehaviour
{
    public int gridXDim;
    public int gridZDim;
    public Vector3 gridGap;

    public Vector3 gridStartingPoint;

    public List<Vector3> gridPoints;

    public bool reMap;

    // Start is called before the first frame update
    void Start()
    {
        this.gridXDim = (int)this.gameObject.GetComponent<Grid>().cellSize.x;
        this.gridZDim = (int)this.gameObject.GetComponent<Grid>().cellSize.z;
        this.gridGap = this.gameObject.GetComponent<Grid>().cellGap;
        this.gridStartingPoint = this.gameObject.GetComponent<Grid>().transform.position;

        this.gridPoints = new List<Vector3>();

        this.reMap = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.reMap)
        {
            this.gridXDim = (int)this.gameObject.GetComponent<Grid>().cellSize.x;
            this.gridZDim = (int)this.gameObject.GetComponent<Grid>().cellSize.z;
            this.gridGap = this.gameObject.GetComponent<Grid>().cellGap;
            this.gridStartingPoint = this.gameObject.GetComponent<Grid>().transform.position;

            this.ReMapGrid();
            this.reMap = false;
        }

        this.DrawGrid();
    }

    void ReMapGrid()
    {
        this.gridPoints.Clear();

        for (int ix = 0; ix < this.gridXDim; ++ix)
        {
            for (int iz = 0; iz < this.gridZDim; ++iz)
            {
                this.gridPoints.Add(new Vector3(this.gridStartingPoint.x + this.gridGap.x * ix, this.gridStartingPoint.y, this.gridStartingPoint.z + this.gridGap.z * iz));
            }
        }
    }

    void DrawGrid()
    {
        for (int ix = 0; ix < this.gridXDim; ++ix)
        {
            for (int iz = 0; iz < this.gridZDim; ++iz)
            {
                if (ix == 0)
                {
                    int valSt = iz;
                    int valEd = iz + this.gridXDim * (this.gridXDim - 1);

                    Debug.DrawLine(this.gridPoints[valSt], this.gridPoints[valEd]);
                }

                if (iz == 0)
                {
                    int valSt = this.gridXDim * ix;
                    int valEd = this.gridXDim * ix + (this.gridZDim - 1);

                    Debug.DrawLine(this.gridPoints[valSt], this.gridPoints[valEd]);
                }
            }
        }
    }
}
