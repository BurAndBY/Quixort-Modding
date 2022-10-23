# Quixort Modding
Code related to modifying the game Quixort in The Jackbox Party Pack 9.

### Team Names
Simply provide a text file beginning with `[Teams]` with each pair of teams on a single line, seperated by a `|` character. This will then generate a `.jet` file (really just a `.json` file) next to your text file (with the same file name) that can replace the original `LineupTeamNamePair.jet` file in your Quixort content directory (`{INSTALLDIR}\The Jackbox Party Pack 9\games\Lineup\content\en`). Adding (us) to a line will mark the team pair as being US centric (a feature not normally used by team names, but still supported) and adding (explicit) will mark the team pair as being explicit and thus not valid if Family Friendly mode is enabled.

The names will both be converted to all upper case to match the original team names, and the word TEAM will be added to the start.

### Tutorials
Provide a text file beginning with `[Tutorial]`, with each following line containing the prompt name, then the prompt options, each seperated by a `|` character. This will then generate a `.jet` file (really just a `.json` file) next to your text file (with the same file name) that can replace the original `LineupTutorialSequence.jet` file in your Quixort content directory (`{INSTALLDIR}\The Jackbox Party Pack 9\games\Lineup\content\en`). Adding (us) to a line will mark the prompt as being US centric and adding (explicit) will mark the prompt as being explicit and thus not valid if Family Friendly mode is enabled.

### Prompts
For prompts, the text file needs to begin with `[Prompts]`, then have the things for each prompt on a single line, with each prompt seperated by a line break. As with the Team Names and Tutorials, each entry for a single prompt needs to be seperated with a `|` character. This will generate a `.jet` file (really just a `.json` file) next to your text file (with the same file name) that can replace the original `LineupLongSequence.jet` or `LineupShortSequence.jet` files in your Quixort content directory (`{INSTALLDIR}\The Jackbox Party Pack 9\games\Lineup\content\en`). The folders generated in the `CustomQuixortPrompts` directory need to also be copied to the approriate folder (either `{INSTALLDIR}\The Jackbox Party Pack 9\games\Lineup\content\en\LineupLongSequence` or `{INSTALLDIR}\The Jackbox Party Pack 9\games\Lineup\content\en\LineupShortSequence` depending on which was replaced.

The first entry in a prompt is the actual prompt name, the second is the label that will appear on the left side of the sorting area, and the third is the label on the right side.

The fourth value is the category; although multiple categories are supported via Comma Seperated Values, only the first one seems to apply, and will appear as CATEGORY_{name} if it doesn't normally exist.

The fifth value is the difficulty value, which can be set to easy, medium or hard in the vanilla game.

Each value following this is an actual item that will be sorted for this prompt, consisting of three Comma Seperated Values. The first determines the information shown for this item in the final sort (such as a release year for a movie), the second value determines the name for the item displayed on the monitor, and the thrid determines the name for the item shown on the block. If the first value is set to `trash`, then this item is counted as a fake one that needs to be trashed by the player(s).

Adding (us) to a line will mark the prompt as being US centric and adding (explicit) will mark the prompt as being explicit and thus not valid if Family Friendly mode is enabled.

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

```
[Prompts]
Test Prompt|Left|Right|Category 1,Category 2|Easy|Furtherst,Left 3,Left 3|Further,Left 2,Left 2|Far,Left 1,Left 1|Middle,None,None|Far,Right 1,Right 1|Further,Right 2,Right 2|Furtherst,Right 3,Right 3|trash,Up,Up|trash,Down,Down
Test Prompt 2|Left|Right|Category 1,Category 2|Easy|Furtherst,Left 3,Left 3|Further,Left 2,Left 2|Far,Left 1,Left 1|Middle,None,None|Far,Right 1,Right 1|Further,Right 2,Right 2|Furtherst,Right 3,Right 3|trash,Up,Up|trash,Down,Down(us)(explicit)
```