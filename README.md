# Quixort Modding
Code related to modifying the game Quixort in The Jackbox Party Pack 9.

### Team Names
Simply provide a text file beginning with `[Teams]` with each pair of teams on a single line, seperated by a `|` character. This will then generate a `.jet` file (really just a `.json` file) next to your text file (with the same file name) that can replace the original `LineupTeamNamePair.jet` file in your Quixort content directory (`{INSTALLDIR}\The Jackbox Party Pack 9\games\Lineup\content\en`). Adding (us) to a line will mark the team pair as being US centric (a feature not normally used by team names, but still supported) and adding (explicit) will mark the team pair as being explicit and thus not valid if Family Friendly mode is enabled.

The names will both be converted to all upper case to match the original team names, and the word TEAM will be added to the start.

### Tutorials
Provide a text file beginning with `[Tutorial]`, with each following line containing the prompt name, then the prompt options, each seperated by a `|` character. This will then generate a `.jet` file (really just a `.json` file) next to your text file (with the same file name) that can replace the original `LineupTutorialSequence.jet` file in your Quixort content directory (`{INSTALLDIR}\The Jackbox Party Pack 9\games\Lineup\content\en`). Adding (us) to a line will mark the prompt as being US centric and adding (explicit) will mark the prompt as being explicit and thus not valid if Family Friendly mode is enabled.

### File Examples
```
[Teams]
YES|NO
WINNER|LOSER
LEFT|RIGHT
texas|washington(us)
ass|arse(explicit)
```

```
[Tutorial]
Test Prompt|1|2|3|4|potato|5|6|quack|7|8|9|fizz|buzz|jeff
Test Prompt 2|You|Me|Neither(explicit)(us)
```