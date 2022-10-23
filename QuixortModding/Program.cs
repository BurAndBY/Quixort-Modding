using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace QuixortModding
{
    // TODO: Seperate this into its own classes so this can be more than just a Team Name Importer.
    internal class Program
    {
        public class TeamFormatData
        {
            [JsonProperty(Order = 1, PropertyName = "content")]
            public List<QuixortTeamName> Content = new();
        }
        public class QuixortTeamName
        {
            [JsonProperty(Order = 1, PropertyName = "countrySpecific")]
            public bool USCentric { get; set; } = false;

            [JsonProperty(Order = 2, PropertyName = "id")]
            public int ID { get; set; } = 24240;

            [JsonProperty(Order = 3, PropertyName = "isValid")]
            public string Valid { get; set; } = "";

            [JsonProperty(Order = 4, PropertyName = "teamOne")]
            public string TeamOneName { get; set; } = "One";

            [JsonProperty(Order = 5, PropertyName = "teamTwo")]
            public string TeamTwoName { get; set; } = "One";

            [JsonProperty(Order = 6, PropertyName = "x")]
            public bool Explicit { get; set; } = false;

            public override string ToString() => $"{TeamOneName}|{TeamTwoName}";
        }

        static void Main(string[] args)
        {
            // Complain if we don't have a file to parse.
            if (args.Length == 0)
            {
                Console.WriteLine("Drag a txt file onto this program, where each line is a prompt to be converted.\nFor formatting information, refer to the GitHub README.");
                Console.ReadKey();
                return;
            }

            // Split text file.
            var text = File.ReadAllLines(args[0]);

            // Set up a Team Data.
            TeamFormatData teamData = new();

            // Loop through the provided text file.
            for (int i = 0; i < text.Length; i++)
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