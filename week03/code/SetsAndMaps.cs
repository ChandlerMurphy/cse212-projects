using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE

        // Create a HashSet that doesn't allow duplicates
        HashSet<string> seen = new HashSet<string>();
        // Create a list to return the result of our functions
        List<string> result = new List<string>();

        // Now we will check each word to see if it has a reversed version in the list
        foreach (string word in words)
        {
            if (word.Length != 2) continue; // The continue word skips the current word if it meets these conditions.
            if (word[0] == word[1]) continue; 

            char[] reversedChars = new char[] { word[1], word[0] };
            string reversed = new string(reversedChars);

            if (seen.Contains(reversed))
            {
                result.Add($"{reversed} & {word}");
            }
            else
            {
                seen.Add(word);
            }
        }

        return result.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE

            if (fields.Length < 4) continue; // Disregard any entries that aren't meeting our length requirements. 

            // Extract the degree 
            string degree = fields[3].Trim();

            // Check if degree is already in the dictionary
            if (degrees.ContainsKey(degree))
            {
                // Increase the count of that Degree by 1
                degrees[degree]++;
            }
            else
            {
                // If this is the first sighting of the degree then add it and increment by 1
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // TODO Problem 3 - ADD YOUR CODE HERE

        // Simplify both words for us to check 
        string w1 = word1.Replace(" ", "").ToLower();
        string w2 = word2.Replace(" ", "").ToLower();

        // Check length first to see if they could be be anagrams
        if (w1.Length != w2.Length)
        {
            return false;
        }

        // Create a dictionary to count letters in word1
        Dictionary<char, int> letterCounts = new Dictionary<char, int>();

        foreach (char c in w1)
        {
            if (letterCounts.ContainsKey(c))
                letterCounts[c]++;
            else
                letterCounts[c] = 1;
        }

        // 4. Subtract letters in word2 from the dictionary
        foreach (char c in w2)
        {
            if (!letterCounts.ContainsKey(c))
            {
                // If the letter in word2 is not in the word1 dictionary then return false
                return false;
            }

            letterCounts[c]--;

            if (letterCounts[c] < 0)
            {
                // If the letter appears in word2 more than in word1 then return false
                return false;
            }
        }

        // If all the checks pass then return as true.
        return true; 
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        var results = new List<string>();

        if (featureCollection?.Features != null)
        {
            foreach (var feature in featureCollection.Features)
            {
                var props = feature.Properties;

                if (props?.Mag != null && !string.IsNullOrWhiteSpace(props.Place))
                {
                    results.Add($"{props.Place} - Mag {props.Mag}");
                }
            }
        }

        return results.ToArray();
    }
}