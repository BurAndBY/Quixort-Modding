using Newtonsoft.Json;

namespace QuixortModding
{
    internal class Teams
    {
        /// <summary>
        /// Content header.
        /// </summary>
        public class TeamFormatData
        {
            [JsonProperty(Order = 1, PropertyName = "content")]
            public List<QuixortTeamName> Content = new();
        }

        /// <summary>
        /// Actual Team Data.
        /// </summary>
        public class QuixortTeamName
        {
            /// <summary>
            /// Whether this pair of team names should be removed if the Filter US Centric option is on.
            /// </summary>
            [JsonProperty(Order = 1, PropertyName = "countrySpecific")]
            public bool USCentric { get; set; } = false;

            /// <summary>
            /// ID number, not sure what this is used for, as it doesn't seem to be incremental?
            /// </summary>
            [JsonProperty(Order = 2, PropertyName = "id")]
            public int ID { get; set; } = 24240;

            /// <summary>
            /// Unknown, is never set to anything in Quixort.
            /// </summary>
            [JsonProperty(Order = 3, PropertyName = "isValid")]
            public string Valid { get; set; } = "";

            /// <summary>
            /// Name for the team on the left.
            /// </summary>
            [JsonProperty(Order = 4, PropertyName = "teamOne")]
            public string TeamOneName { get; set; } = "One";

            /// <summary>
            /// Name for the team on the right.
            /// </summary>
            [JsonProperty(Order = 5, PropertyName = "teamTwo")]
            public string TeamTwoName { get; set; } = "One";

            /// <summary>
            /// Whether this pair of team names should be removed if Family Friendly mode is on.
            /// </summary>
            [JsonProperty(Order = 6, PropertyName = "x")]
            public bool Explicit { get; set; } = false;

            /// <summary>
            /// Makes the VS Debuger show the team names.
            /// </summary>
            public override string ToString() => $"{TeamOneName}|{TeamTwoName}";
        }

        /// <summary>
        /// Imports data from a text file to teams.
        /// </summary>
        /// <param name="args">Console arguments (needed for the file path).</param>
        /// <param name="text">Text file we're parsing.</param>
        public static void Import(string[] args, string[] text)
        {
            // Set up a Team Data.
            TeamFormatData teamData = new();

            // Loop through the provided text file.
            for (int i = 1; i < text.Length; i++)
            {
                // Split each entry based on the | character.
                string[] split = text[i].Split('|');

                // Set up a new team for this line.
                QuixortTeamName team = new()
                {
                    ID = i + 24240,
                    TeamOneName = $"TEAM {split[0].ToUpper()}",
                    TeamTwoName = $"TEAM {split[1].ToUpper()}"
                };
                teamData.Content.Add(team);
            }

            // Save this team file.
            File.WriteAllText($"{Path.GetDirectoryName(args[0])}\\{Path.GetFileNameWithoutExtension(args[0])}.jet", JsonConvert.SerializeObject(teamData, Formatting.Indented));
        }
    }
}
