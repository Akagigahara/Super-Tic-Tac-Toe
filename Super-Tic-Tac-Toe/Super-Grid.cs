using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Tic_Tac_Toe
{
	public class Super_Grid
	{
		public GameUtils.GridState gridState = GameUtils.GridState.open;
		public readonly Small_Grid[] small_Grids;
		readonly Small_Grid topLeft = new();
		readonly Small_Grid top = new();
		readonly Small_Grid topRight = new();
		readonly Small_Grid left = new();
		readonly Small_Grid middle = new();
		readonly Small_Grid right = new();
		readonly Small_Grid bottomLeft = new();
		readonly Small_Grid bottom = new();
		readonly Small_Grid bottomRight = new();

		public Super_Grid()
		{
			small_Grids = [topLeft, top, topRight, 
						   left, middle, right, 
						   bottomLeft, bottom, bottomRight];
		}

		public Super_Grid(Small_Grid[] small_Grids)
		{
			topLeft = small_Grids[0];
			top = small_Grids[1];
			topRight = small_Grids[2];
			left = small_Grids[3];
			middle = small_Grids[4];
			right = small_Grids[5];
			bottomLeft = small_Grids[6];
			bottom = small_Grids[7];
			bottomRight = small_Grids[8];

			this.small_Grids = [topLeft, top, topRight,
						   left, middle, right,
						   bottomLeft, bottom, bottomRight];
		}

		public void CheckGameState()
		{
			if (topLeft.gridState == top.gridState && top.gridState == topRight.gridState && top.gridState != GameUtils.GridState.open)
			{
				gridState = topLeft.gridState;
			}
			else if (left.gridState == middle.gridState && middle.gridState == right.gridState && middle.gridState != GameUtils.GridState.open)
			{
				gridState = left.gridState;
			}
			else if (bottomLeft.gridState == bottom.gridState && bottom.gridState == bottomRight.gridState && bottom.gridState != GameUtils.GridState.open)
			{
				gridState = bottomLeft.gridState;
			}
			else if (topLeft.gridState == left.gridState && left.gridState == bottomLeft.gridState && left.gridState != GameUtils.GridState.open)
			{
				gridState = topLeft.gridState;
			}
			else if (middle.gridState == top.gridState && top.gridState == bottom.gridState && top.gridState != GameUtils.GridState.open)
			{
				gridState = top.gridState;
			}
			else if (topRight.gridState == right.gridState && right.gridState == bottomRight.gridState && right.gridState != GameUtils.GridState.open)
			{
				gridState = right.gridState;
			}
			else if (topLeft.gridState == middle.gridState && middle.gridState == bottomRight.gridState && middle.gridState != GameUtils.GridState.open)
			{
				gridState = middle.gridState;
			}
			else if (topRight.gridState == middle.gridState && middle.gridState == bottomLeft.gridState && middle.gridState != GameUtils.GridState.open)
			{
				gridState = middle.gridState;
			}
		}
	}
}
