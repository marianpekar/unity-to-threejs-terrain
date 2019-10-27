public static class Serializer
{
    public static string ToJSONArray(float[,] array2d, int density)
    {
        string result = "[";

#if UNITY_EDITOR
        EditorUtilityDisplayProgressBar progressBar = new EditorUtilityDisplayProgressBar();
        progressBar.Title = "Serializer";
        progressBar.Description = string.Format("Serialize data to JSON array");
        progressBar.Total = (array2d.GetLength(0) / density) * (array2d.GetLength(0) / density);
#endif

        for (int i = 0; i < array2d.GetLength(0); i += density)
            for(int j = 0; j < array2d.GetLength(1); j += density)
            {
                // To avoid strange diagonal line on the reconstructed terrain
                if (i == j)
                    continue;

                result += string.Format("\"{0}\",", array2d[j, i]);

#if UNITY_EDITOR
                progressBar.Progress++;
                progressBar.Update();
#endif
            }


        // remove last ',' and add closing ']'
        result = result.Remove(result.Length - 1) + "]"; 
        return result;
    }                
}