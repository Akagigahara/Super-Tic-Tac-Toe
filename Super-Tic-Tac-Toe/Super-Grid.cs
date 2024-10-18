using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Tic_Tac_Toe
{
	internal class Super_Grid
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

		public override string ToString() =>
				$"""
				{topLeft} || {top} || {topRight}
				===++===++===
				{left} || {middle} || {right}
				===++===++===
				{bottomLeft} || {bottom} || {bottomRight}
				""";
		public void CheckGameState()
		{
			if (topLeft.gridState == top.gridState && top.gridState == topRight.gridState)
			{
				gridState = topLeft.gridState;
			}
			else if (left.gridState == middle.gridState && middle.gridState == right.gridState)
			{
				gridState = left.gridState;
			}
			else if (bottomLeft.gridState == bottom.gridState && bottom.gridState == bottomRight.gridState)
			{
				gridState = bottomLeft.gridState;
			}
			else if (topLeft.gridState == left.gridState && left.gridState == bottomLeft.gridState)
			{
				gridState = topLeft.gridState;
			}
			else if (middle.gridState == top.gridState && top.gridState == bottom.gridState)
			{
				gridState = top.gridState;
			}
			else if (topRight.gridState == middle.gridState && middle.gridState == bottomRight.gridState)
			{
				gridState = middle.gridState;
			}
			else if (topLeft.gridState == middle.gridState && middle.gridState == bottomRight.gridState)
			{
				gridState = middle.gridState;
			}
			else if (topRight.gridState == middle.gridState && middle.gridState == bottomLeft.gridState)
			{
				gridState = middle.gridState;
			}
		}
	}
}
