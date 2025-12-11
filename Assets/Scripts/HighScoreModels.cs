using System;
using System.Collections.Generic;

[Serializable]
public class HighscoreEntry
{
    public string initials;
    public int score;
}

[Serializable]
public class HighscoreData
{
    public List<HighscoreEntry> entries = new List<HighscoreEntry>();
}
