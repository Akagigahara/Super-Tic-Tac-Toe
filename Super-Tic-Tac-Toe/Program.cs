// See https://aka.ms/new-console-template for more information
using Spectre.Console;
using Super_Tic_Tac_Toe;

Console.WriteLine("Game Initialized.");
Super_Grid Game = new();
char currentPlayer = 'o';
char artificialOpponent = 'o';
bool IsInvalidPlay = false;
bool IsNewGame = true;
bool HasFreeChoice = false;
Small_Grid? playedGrid = null;
Small_Grid? previousGrid = null;
ushort round = 0;

//Choose game mode to enter corresponding loop
switch (AnsiConsole.Prompt<string>(new SelectionPrompt<string>()
	.Title("Select Gamemode")
	.AddChoices([
		"Singleplayer", "Hot Seat"
		])))
{
	case "Singleplayer":
		if (AnsiConsole.Prompt(new SelectionPrompt<string>()
			.Title("Do you want to play first or second?")
			.AddChoices(
				[
					"First", "Second"
				]
			)) == "Second")
		{
			SwitchPlayers();
		}
		Singleplayer();//Enter the singleplayer loop
		break;
	case "Hot Seat":
		Hotseat();//Enter local multiplayer setup
		break;
	default:
		throw new Exception("Critical Error, unknown game mode selected");
}


RenderGame(); // Render game for endstate 

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

	AnsiConsole.Clear();
	AnsiConsole.Write(new Columns(new Text($" Current Round: {round}"),
		new Text($"Current Grid: { Array.IndexOf(Game.small_Grids, playedGrid) switch 
			{
				0 => "Top Left",
				1 => "Top",
				2 => "Top Right",
				3 => "Left",
				4 => "Middle",
				5 => "Right",
				6 => "Bottom Left",
				7 => "Bottom",
				8 => "Bottom Right",
				_ => "invalid grid"
			}}"),
		new Text($" Current Player: {currentPlayer}").RightJustified()));
	AnsiConsole.Write(RenderedGame.Centered());
	AnsiConsole.WriteLine();
	AnsiConsole.WriteLine();
}

void Hotseat()
{
	while (Game.gridState == GameUtils.GridState.open) //main game loop
	{
		round++;
		SwitchPlayers();

		RenderGame();

		if (IsNewGame || HasFreeChoice)	SuperGridSelection();

		do SmallGridSelection(); while (IsInvalidPlay);

		previousGrid!.CheckGameState();
		Game.CheckGameState();
		IsNewGame = false;
	}

}
void Singleplayer()
{
	while(Game.gridState == GameUtils.GridState.open) //main game loop
	{
		round++;
		SwitchPlayers();
		RenderGame();
		if(currentPlayer == artificialOpponent) //enter AI logic
		{
			if (IsNewGame || HasFreeChoice)
			{
				string[] AOSuperGridSelectable = new[]
							{
								Game.small_Grids[0].gridState == GameUtils.GridState.open ? "Top Left" : "", Game.small_Grids[1].gridState == GameUtils.GridState.open ? "Top" : "", Game.small_Grids[2].gridState == GameUtils.GridState.open ? "Top Right" : "",
								Game.small_Grids[3].gridState == GameUtils.GridState.open ? "Left" : "", Game.small_Grids[4].gridState == GameUtils.GridState.open ? "Middle" : "", Game.small_Grids[5].gridState == GameUtils.GridState.open ? "Right" : "",
								Game.small_Grids[6].gridState == GameUtils.GridState.open ? "Bottom Left" : "", Game.small_Grids[7].gridState == GameUtils.GridState.open ? "Bottom" : "", Game.small_Grids[8].gridState == GameUtils.GridState.open ? "Bottom Right" : ""
							}.Where(x => x != "").ToArray();
				AOSuperGridSelection(AOSuperGridSelectable[DateTime.Now.Millisecond % AOSuperGridSelectable.Length]);
			}

			do
			{
				string[] AOSmallGridSelectable = new[]
							{
								playedGrid!.topLeft == ' ' ? "Top Left" : "", playedGrid!.top == ' ' ? "Top" : "", playedGrid!.topRight == ' ' ? "Top Right" : "",
								playedGrid!.left == ' ' ? "Left" : "", playedGrid!.middle == ' ' ? "Middle" : "", playedGrid!.right == ' ' ? "Right" : "",
								playedGrid!.bottomLeft == ' ' ? "Bottom Left" : "", playedGrid!.bottom == ' ' ? "Bottom" : "", playedGrid!.bottomRight == ' ' ? "Bottom Right" : ""
							}.Where(x => x != "").ToArray();

				AOSmallGridSelection(AOSmallGridSelectable[DateTime.Now.Millisecond % AOSmallGridSelectable.Length]);
			}
			while (IsInvalidPlay);
		}
		else//Enter player logic
		{
			if (IsNewGame || HasFreeChoice) SuperGridSelection();

			do SmallGridSelection(); while (IsInvalidPlay);
		}
		previousGrid!.CheckGameState();
		Game.CheckGameState();
		IsNewGame = false;

	}
}

void SuperGridSelection()
{
	switch (AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("Please chose the place in the SuperGrid to play in.")
					.AddChoices(new[]
						{
							Game.small_Grids[0].gridState == GameUtils.GridState.open ? "Top Left" : "", Game.small_Grids[1].gridState == GameUtils.GridState.open ? "Top" : "", Game.small_Grids[2].gridState == GameUtils.GridState.open ? "Top Right" : "",
							Game.small_Grids[3].gridState == GameUtils.GridState.open ? "Left" : "", Game.small_Grids[4].gridState == GameUtils.GridState.open ? "Middle" : "", Game.small_Grids[5].gridState == GameUtils.GridState.open ? "Right" : "",
							Game.small_Grids[6].gridState == GameUtils.GridState.open ? "Bottom Left" : "", Game.small_Grids[7].gridState == GameUtils.GridState.open ? "Bottom" : "", Game.small_Grids[8].gridState == GameUtils.GridState.open ? "Bottom Right" : ""
						}.Where(x => x != "").ToArray()
	)))
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
	HasFreeChoice = false;
}

void AOSuperGridSelection(string AOchoice)
{
	switch (AOchoice)
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

	HasFreeChoice = false;
}

void SmallGridSelection()
{
	previousGrid = playedGrid;

	switch (AnsiConsole.Prompt(
					new SelectionPrompt<string>()
						.Title("Please chose the place in the SmallGrid to play in.")
						.AddChoices(new[]
							{
								playedGrid!.topLeft == ' ' ? "Top Left" : "", playedGrid!.top == ' ' ? "Top" : "", playedGrid!.topRight == ' ' ? "Top Right" : "",
								playedGrid!.left == ' ' ? "Left" : "", playedGrid!.middle == ' ' ? "Middle" : "", playedGrid!.right == ' ' ? "Right" : "",
								playedGrid!.bottomLeft == ' ' ? "Bottom Left" : "", playedGrid!.bottom == ' ' ? "Bottom" : "", playedGrid!.bottomRight == ' ' ? "Bottom Right" : ""
							}.Where(x => x != "").ToArray()
						)))
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
}

void AOSmallGridSelection(string AOChoice)
{
	previousGrid = playedGrid;

	switch (AOChoice)
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
}

void SwitchPlayers()
{
	currentPlayer = currentPlayer switch
	{
		'x' => 'o',
		'o' => 'x',
		_ => throw new Exception("Player Swap failed."),
	};
}

public class GameUtils
{
	public enum GridState
	{
		open,
		cross,
		circle
	}
}