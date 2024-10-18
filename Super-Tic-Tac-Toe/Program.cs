// See https://aka.ms/new-console-template for more information
using Spectre.Console;
using Super_Tic_Tac_Toe;

Console.WriteLine("Game Initialized.");
Super_Grid Game = new();
char currentPlayer = 'o';
bool IsInvalidPlay = false;
bool IsNewGame = true;
bool HasFreeChoice = false;
string SuperGridSelect = "";
Small_Grid? playedGrid = null;
Small_Grid? previousGrid = null;

while (Game.gridState == GameUtils.GridState.open)
{
	AnsiConsole.Clear();
	currentPlayer = currentPlayer switch
	{
		'x' => 'o',
		'o' => 'x',
		_ => throw new Exception("Player Swap failed."),
	};

	RenderGame();

	if (IsNewGame || HasFreeChoice)
	{
		SuperGridSelect = AnsiConsole.Prompt(
			new SelectionPrompt<string>()
				.Title("Please chose the place in the SuperGrid to play in.")
				.AddChoices(new[]
					{
					Game.small_Grids[0].gridState == GameUtils.GridState.open ? "Top Left" : "", Game.small_Grids[1].gridState == GameUtils.GridState.open ? "Top" : "", Game.small_Grids[2].gridState == GameUtils.GridState.open ? "Top Right" : "",
					Game.small_Grids[3].gridState == GameUtils.GridState.open ? "Left" : "", Game.small_Grids[4].gridState == GameUtils.GridState.open ? "Middle" : "", Game.small_Grids[5].gridState == GameUtils.GridState.open ? "Right" : "",
					Game.small_Grids[6].gridState == GameUtils.GridState.open ? "Bottom Left" : "", Game.small_Grids[7].gridState == GameUtils.GridState.open ? "Bottom" : "", Game.small_Grids[8].gridState == GameUtils.GridState.open ? "Bottom Right" : ""
					}.Where(x => x != "").ToArray()
				));
		switch (SuperGridSelect)
		{
			case "Top Left":
				playedGrid = Game.small_Grids[0];
				break;
			case "Top":
				playedGrid = Game.small_Grids[1];
				break;
			case "Top Right":
				playedGrid = Game.small_Grids[2];
				break;
			case "Left":
				playedGrid = Game.small_Grids[3];
				break;
			case "Middle":
				playedGrid = Game.small_Grids[4];
				break;
			case "Right":
				playedGrid = Game.small_Grids[5];
				break;
			case "Bottom Left":
				playedGrid = Game.small_Grids[6];
				break;
			case "Bottom":
				playedGrid = Game.small_Grids[7];
				break;
			case "Bottom Right":
				playedGrid = Game.small_Grids[8];
				break;
			default:
				throw new Exception("Small Grid selection failed.");
		}
	}

	do
	{
		string SmallGridSelect = AnsiConsole.Prompt(
		new SelectionPrompt<string>()
			.Title("Please chose the place in the SmallGrid to play in.")
			.AddChoices(new[]
				{
					playedGrid!.topLeft == ' ' ? "Top Left" : "", playedGrid!.top == ' ' ? "Top" : "", playedGrid!.topRight == ' ' ? "Top Right" : "",
					playedGrid!.left == ' ' ? "Left" : "", playedGrid!.middle == ' ' ? "Middle" : "", playedGrid!.right == ' ' ? "Right" : "",
					playedGrid!.bottomLeft == ' ' ? "Bottom Left" : "", playedGrid!.bottom == ' ' ? "Bottom" : "", playedGrid!.bottomRight == ' ' ? "Bottom Right" : ""
				}.Where(x => x != "").ToArray()
			));

		previousGrid = playedGrid;

		switch (SmallGridSelect)
		{
			case "Top Left":
				playedGrid!.topLeft = currentPlayer;
				playedGrid = Game.small_Grids[0];
				if (playedGrid.gridState != GameUtils.GridState.open) HasFreeChoice = true;
				IsInvalidPlay = false;
				break;
			case "Top":
				playedGrid!.top = currentPlayer;
				playedGrid = Game.small_Grids[1];
				if (playedGrid.gridState != GameUtils.GridState.open) HasFreeChoice = true;
				IsInvalidPlay = false;
				break;
			case "Top Right":
				playedGrid!.topRight = currentPlayer;
				playedGrid = Game.small_Grids[2];
				if (playedGrid.gridState != GameUtils.GridState.open) HasFreeChoice = true;
				IsInvalidPlay = false;
				break;
			case "Left":
				playedGrid!.left = currentPlayer;
				playedGrid = Game.small_Grids[3];
				if (playedGrid.gridState != GameUtils.GridState.open) HasFreeChoice = true;
				IsInvalidPlay = false;
				break;
			case "Middle":
				playedGrid!.middle = currentPlayer;
				playedGrid = Game.small_Grids[4];
				if (playedGrid.gridState != GameUtils.GridState.open) HasFreeChoice = true;
				IsInvalidPlay = false;
				break;
			case "Right":
				playedGrid!.right = currentPlayer;
				playedGrid = Game.small_Grids[5];
				if (playedGrid.gridState != GameUtils.GridState.open) HasFreeChoice = true;
				IsInvalidPlay = false;
				break;
			case "Bottom Left":
				playedGrid!.bottomLeft = currentPlayer;
				playedGrid = Game.small_Grids[6];
				if (playedGrid.gridState != GameUtils.GridState.open) HasFreeChoice = true;
				IsInvalidPlay = false;
				break;
			case "Bottom":
				playedGrid!.bottom = currentPlayer;
				playedGrid = Game.small_Grids[7];
				if (playedGrid.gridState != GameUtils.GridState.open) HasFreeChoice = true;
				IsInvalidPlay = false;
				break;
			case "Bottom Right":
				playedGrid!.bottomRight = currentPlayer;
				playedGrid = Game.small_Grids[8];
				if (playedGrid.gridState != GameUtils.GridState.open) HasFreeChoice = true;
				IsInvalidPlay = false;
				break;
			default:
				IsInvalidPlay = true;
				AnsiConsole.WriteLine("Your Choice was invalid, please choose again");
				break;
		}
	} while (IsInvalidPlay);
	previousGrid.CheckGameState();
	Game.CheckGameState();
	IsNewGame = false;
}

RenderGame();

switch(Game.gridState)
{
	case GameUtils.GridState.cross:
		AnsiConsole.WriteLine("X won this game!");
		break;
	case GameUtils.GridState.circle:
		AnsiConsole.WriteLine("O won this game!");
		break;
	default:
		throw new Exception("Somehow game end was called without a valid winner.");
}

Console.ReadLine();


void RenderGame()
{
	Table RenderedGame = new()
	{
		ShowRowSeparators = true,
	};

	foreach (Small_Grid grid in Game.small_Grids[0..3])
	{
		RenderedGame.AddColumn(grid.ToString()).Centered();
	}
	RenderedGame.AddRow(Game.small_Grids[3].ToString(), Game.small_Grids[4].ToString(), Game.small_Grids[5].ToString()).Centered();
	RenderedGame.AddRow(Game.small_Grids[6].ToString(), Game.small_Grids[7].ToString(), Game.small_Grids[8].ToString()).Centered();
	AnsiConsole.Write(RenderedGame);
	AnsiConsole.WriteLine();
	AnsiConsole.WriteLine();
}

class GameUtils
{
	public enum GridState
	{
		open,
		cross,
		circle
	}
}