# Quixort Modding
Code related to modifying the game Quixort in The Jackbox Party Pack 9, currently only able to import custom team names due to personal laziness.

### Team Names
Simply provide a text file with each pair of teams on a single line, seperated by a `|` character. This will then generate a `.jet` file (really just a `.json` file) next to your text file (with the same file name) that can replace the original `LineupTeamNamePair.jet` file in your Quixort content directory (`{INSTALLDIR}\The Jackbox Party Pack 9\games\Lineup\content\en`).

The names will both be converted to all upper case to match the original team names, and the word TEAM will be added to the start.

### File Examples
```
YES|NO
WINNER|LOSER
LEFT|RIGHT
```