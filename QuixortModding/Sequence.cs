using Newtonsoft.Json;

namespace QuixortModding
{
    internal class Sequence
    {
        /// <summary>
        /// Content header.
        /// </summary>
        public class SequenceFormatData
        {
            [JsonProperty(Order = 1, PropertyName = "content")]
            public List<SequenceEntry> Content { get; set; } = new();
        }

        /// <summary>
        /// Actual Prompt Data.
        /// </summary>
        public class SequenceEntry
        {
            /// <summary>
            /// The categories this prompt is in.
            /// </summary>
            [JsonProperty(Order = 1, PropertyName = "categories")]
            public List<string> Categories { get; set; } = new();

            /// <summary>
            /// Whether this prompt should be removed if the Filter US Centric option is on.
            /// </summary>
            [JsonProperty(Order = 2, PropertyName = "countrySpecific")]
            public bool USCentric { get; set; } = false;

            /// <summary>
            /// The difficulty of this prompt, defaults to medium.
            /// </summary>
            [JsonProperty(Order = 3, PropertyName = "difficulty")]
            public string Difficulty { get; set; } = "medium";

            /// <summary>
            /// ID number, not sure what this is used for, as it doesn't seem to be incremental?
            /// </summary>
            [JsonProperty(Order = 4, PropertyName = "id")]
            public int ID { get; set; } = 24240;

            /// <summary>
            /// Unknown, is never set to anything in Quixort.
            /// </summary>
            [JsonProperty(Order = 5, PropertyName = "isValid")]
            public string Valid { get; set; } = "";

            /// <summary>
            /// The items that need sorting in this prompt.
            /// </summary>
            [JsonProperty(Order = 6, PropertyName = "items")]
            public List<SequenceItem> Items { get; set; } = new();

            /// <summary>
            /// The value in the left side label for this prompt.
            /// </summary>
            [JsonProperty(Order = 7, PropertyName = "leftLabel")]
            public string Least { get; set; } = "";

            /// <summary>
            /// The displayed name for this prompt.
            /// </summary>
            [JsonProperty(Order = 8, PropertyName = "prompt")]
            public string Prompt { get; set; } = "";

            /// <summary>
            /// The value in the left side label for this prompt.
            /// </summary>
            [JsonProperty(Order = 9, PropertyName = "rightLabel")]
            public string Most { get; set; } = "";

            /// <summary>
            /// The items that are fake in this prompt.
            /// </summary>
            [JsonProperty(Order = 10, PropertyName = "trash")]
            public List<SequenceTrash> Trash { get; set; } = new();

            /// <summary>
            /// Whether this prompt should be removed if Family Friendly mode is on.
            /// </summary>
            [JsonProperty(Order = 11, PropertyName = "x")]
            public bool Explicit { get; set; } = false;

            /// <summary>
            /// Makes the VS Debuger show the prompt.
            /// </summary>
            public override string ToString() => Prompt;
        }

        /// <summary>
        /// A real item that is sorted in this prompt.
        /// </summary>
        public class SequenceItem
        {
            /// <summary>
            /// The value displayed when showing the final ordering.
            /// </summary>
            [JsonProperty(Order = 1, PropertyName = "displayValue")]
            public string Value { get; set; } = "";

            /// <summary>
            /// The name shown for this item on the top display?
            /// </summary>
            [JsonProperty(Order = 2, PropertyName = "longText")]
            public string Long { get; set; } = "";

            /// <summary>
            /// The name shown for this item on the block?
            /// </summary>
            [JsonProperty(Order = 3, PropertyName = "shortText")]
            public string Short { get; set; } = "";

            /// <summary>
            /// Makes the VS Debuger show the item's longText value.
            /// </summary>
            public override string ToString() => Long;
        }

        /// <summary>
        /// A fake item that is meant to be trashed in this prompt.
        /// </summary>
        public class SequenceTrash
        {
            /// <summary>
            /// The name shown for this item on the top display?
            /// </summary>
            [JsonProperty(Order = 1, PropertyName = "longText")]
            public string Long { get; set; } = "";

            /// <summary>
            /// The name shown for this item on the block?
            /// </summary>
            [JsonProperty(Order = 2, PropertyName = "shortText")]
            public string Short { get; set; } = "";

            /// <summary>
            /// Makes the VS Debuger show the item's longText value.
            /// </summary>
            public override string ToString() => Long;
        }

        /// <summary>
        /// Imports data from a text file to proper sequence prompts.
        /// </summary>
        /// <param name="args">Console arguments (needed for the file path).</param>
        /// <param name="text">Text file we're parsing.</param>
        public static void Import(string[] args, string[] text)
        {
            // Set up a Sequence Data.
            SequenceFormatData sequenceData = new();

            // Loop through the provided text file.
            for (int i = 1; i < text.Length; i++)
            {
                // Check for the US and Explicit tags.
                bool us = false;
                bool _explicit = false;

                if (text[i].Contains("(us)"))
                {
                    text[i] = text[i].Replace("(us)", "");
                    us = true;
                }
                if (text[i].Contains("(explicit)"))
                {
                    text[i] = text[i].Replace("(explicit)", "");
                    _explicit = true;
                }

                // Split each entry based on the | character.
                string[] split = text[i].Split('|');

                SequenceEntry prompt = new()
                {
                    USCentric = us,
                    Difficulty = split[4].ToLower(),
                    ID = i - 1 + 24240,
                    Least = split[1],
                    Prompt = split[0],
                    Most = split[2],
                    Explicit = _explicit
                };

                // Add this prompt's categories.
                foreach (string category in split[3].Split(','))
                    prompt.Categories.Add(category);

                // Add this prompt's items.
                for (int p = 5; p < split.Length; p++)
                {
                    string[] promptSplit = split[p].Split(',');

                    // Check if this item is a trash one or not.
                    if (promptSplit[0] == "trash")
                    {
                        SequenceTrash trash = new()
                        {
                            Short = promptSplit[1],
                            Long = promptSplit[2]
                        };
                        prompt.Trash.Add(trash);
                    }
                    else
                    {
                        SequenceItem item = new()
                        {
                            Value = promptSplit[0],
                            Short = promptSplit[1],
                            Long = promptSplit[2]
                        };
                        prompt.Items.Add(item);
                    }
                }

                sequenceData.Content.Add(prompt);
            }

            // Save this prompt file.
            File.WriteAllText($"{Path.GetDirectoryName(args[0])}\\{Path.GetFileNameWithoutExtension(args[0])}.jet", JsonConvert.SerializeObject(sequenceData, Formatting.Indented));

            // Create the needed data.jet files.
            // Loop through each prompt.
            foreach (SequenceEntry prompt in sequenceData.Content)
            {
                // Create the directory for this prompt.
                Directory.CreateDirectory($"{Path.GetDirectoryName(args[0])}\\CustomQuixortPrompts\\{prompt.ID}");

                // Set up the writers.
                using Stream dataCreate = File.Open(Path.Combine($"{Path.GetDirectoryName(args[0])}\\CustomQuixortPrompts\\{prompt.ID}", "data.jet"), FileMode.Create);
                using StreamWriter dataInfo = new(dataCreate);

                // Hardcode the data.jet's writing.
                dataInfo.WriteLine("{");
                dataInfo.WriteLine(" \"fields\": [");
                dataInfo.WriteLine("  {");
                dataInfo.WriteLine("   \"t\": \"B\",");
                dataInfo.WriteLine("   \"v\": \"false\",");
                dataInfo.WriteLine("   \"n\": \"HasPromptAudio\"");
                dataInfo.WriteLine("  },");
                dataInfo.WriteLine("  {");
                dataInfo.WriteLine("   \"t\": \"A\",");
                dataInfo.WriteLine("   \"v\": \"prompt\",");
                dataInfo.WriteLine("   \"n\": \"PromptAudio\",");
                dataInfo.WriteLine($"   \"s\": \"{prompt.Prompt}\"");
                dataInfo.WriteLine("  }");
                dataInfo.WriteLine(" ]");
                dataInfo.WriteLine("}");

                // Close the writer.
                dataInfo.Close();
            }
        }
    }
}
