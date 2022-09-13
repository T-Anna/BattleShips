# BattleShips
This is my implementation of one-sided BattleShips game where player has to sink ships placed by computer.

Program creates 10x10 grid and place 3 ships on it:
- 1x Battleship (5 fields long)
- 2x Destroyer (4 fields long)

Program then asks player about coordinates of their shot in order to hit computer's ships. Ship is sunk when all fields it is placed on are hit.
Player enters coordinates as 2 characters - for example 'A5' (capital letter as a column and number as a row). 

Note: program is not proof against incorrect format of the input.

# How to run the application and tests

In order to build and run the application from the command line navigate to folder <b><i>\BattleShips\BattleShips</i></b> and run command <b><i>dotnet run</i></b>

In order to run the tests from from the command line navigate to folder <b><i>\BattleShips\BattleShipsTests</i></b> and run command <b><i>dotnet test</i></b>
