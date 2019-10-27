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
            EditorUtility.DisplayProgressBar(Title, Description, Progress / Total);
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

