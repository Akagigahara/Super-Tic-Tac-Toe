using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Tic_Tac_Toe
{
	internal class Small_Grid
	{
		internal GameUtils.GridState gridState = GameUtils.GridState.open;
		internal char topLeft { get; set; } = ' ';
		internal char top { get; set; } = ' ';
		internal char topRight { get; set; } = ' ';
		internal char left { get; set; } = ' ';
		internal char middle { get; set; } = ' ';
		internal char right { get; set; } = ' ';
		internal char bottomLeft { get; set; } = ' ';
		internal char bottom { get; set; } = ' ';
		internal char bottomRight { get; set; } = ' ';


		public override string ToString()
		{
			string stringified;
			if (gridState == GameUtils.GridState.open)
			{

				stringified = $""" 
							  {topLeft} | {top} | {topRight} 
							  ---+---+---
							  {left} | {middle} | {right}
							  ---+---+---
							  {bottomLeft} | {bottom} | {bottomRight}
							  """;
			}
			else
			{
				switch (gridState)
				{
					case GameUtils.GridState.cross:
						stringified = " x ";
						break;
					case GameUtils.GridState.circle:
						stringified = " o ";
						break;
					default:
						throw new Exception("Something went wrong during stringification of a small grid.");
				}
			}

			return stringified;

		}

		public void CheckGameState()
		{
			if (topLeft == top && top == topRight && top != ' ')
			{
				switch (topLeft)
				{
					case 'x':
						gridState = GameUtils.GridState.cross;
						break;
					case 'o':
						gridState = GameUtils.GridState.circle;
						break;
					default:
						throw new Exception("Game state check invalid.");
				}
			}
			else if (left == middle && middle == right && middle != ' ')
			{
				switch (middle)
				{
					case 'x':
						gridState = GameUtils.GridState.cross;
						break;
					case 'o':
						gridState = GameUtils.GridState.circle;
						break;
					default:
						throw new Exception("Game state check invalid.");
				}
			}
			else if (bottomLeft == bottom && bottom == bottomRight && bottom != ' ')
			{
				switch (bottomLeft)
				{
					case 'x':
						gridState = GameUtils.GridState.cross;
						break;
					case 'o':
						gridState = GameUtils.GridState.circle;
						break;
					default:
						throw new Exception("Game state check invalid.");
				}
			}
			else if (topLeft == left && topLeft == bottomLeft && topLeft != ' ')
			{
				switch (topLeft)
				{
					case 'x':
						gridState = GameUtils.GridState.cross;
						break;
					case 'o':
						gridState = GameUtils.GridState.circle;
						break;
					default:
						throw new Exception("Game state check invalid.");
				}
			}
			else if (middle == top && top == bottom && top != ' ')
			{
				switch (top)
				{
					case 'x':
						gridState = GameUtils.GridState.cross;
						break;
					case 'o':
						gridState = GameUtils.GridState.circle;
						break;
					default:
						throw new Exception("Game state check invalid.");
				}
			}
			else if (topRight == right && right == bottomRight && right != ' ')
			{
				switch (middle)
				{
					case 'x':
						gridState = GameUtils.GridState.cross;
						break;
					case 'o':
						gridState = GameUtils.GridState.circle;
						break;
					default:
						throw new Exception("Game state check invalid.");
				}
			}
			else if (topLeft == middle && middle == bottomRight && middle != ' ')
			{
				switch (topLeft)
				{
					case 'x':
						gridState = GameUtils.GridState.cross;
						break;
					case 'o':
						gridState = GameUtils.GridState.circle;
						break;
					default:
						throw new Exception("Game state check invalid.");
				}
			}
			else if (topRight == middle && middle == bottomLeft && middle != ' ')
			{
				switch (middle)
				{
					case 'x':
						gridState = GameUtils.GridState.cross;
						break;
					case 'o':
						gridState = GameUtils.GridState.circle;
						break;
					default:
						throw new Exception("Game state check invalid.");
				}
			}
		}
	}
}