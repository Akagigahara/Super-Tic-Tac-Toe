# Super-Tic-Tac-Toe
This is a small console version of the game Super Tic Tac Toe. The UI is made through the tools provided by [Spectre Console](https://spectreconsole.net/).

## Game Rules
The overall concept of the game is the same as in normal Tic Tac Toe; the player who gets three of their symbol in a row win. 

The change Super Tic Tac Toe has, is that it consists of 9 small games played in one big grid. When a player wins a small game, that part of the big grid gets their symbol assigned. In order to win, a player needs to win 3 small games in any of the usual valid Tic Tac Toe constellations.

Additionally, the next grid to be played in is determined by the last position played. E.g. o plays the top right position in the bottom left grid, x now has to chose a position within the top right grid. When the first position of a match is chosen and whenever a player would have to play in a grid that is already finished (a won game), the player can choose whichever grid they want to play in.

Here are the algorithmic instructions:
1. First player chooses grid to play in
2. Player chooses position in grid to play
3. Move to the grid corresponding to that position
4. switch players
5. repeat 2-5 until finished
