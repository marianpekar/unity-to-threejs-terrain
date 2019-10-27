using UnityEditor;
public class EditorUtilityDisplayProgressBar
{
    public string Title { get; set; }
    public string Description { get; set; }
    public float Progress { get; set; }
    public float Total { get; set; }

    public void Update()
    {
        if(Progress < Total)
            EditorUtility.DisplayProgressBar(Title, string.Format("{0} ({1}/{2})", Description, Progress, Total), Progress / Total);
        else
        {
            Reset();
        }
    }

    void Reset()
    {
        EditorUtility.ClearProgressBar();
        Progress = 0;
        Total = 0;
    }
}

