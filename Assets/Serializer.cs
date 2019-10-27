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
#if UNITY_EDITOR
                progressBar.Progress++;
                progressBar.Update();
#endif

                if(density == 1)
                {
                    result += string.Format("\"{0}\",", array2d[j, i]);
                    continue;
                }

                if (i != j)
                    result += string.Format("\"{0}\",", array2d[j, i]);

            }


        // remove last ',' and add closing ']'
        result = result.Remove(result.Length - 1) + "]"; 
        return result;
    }                
}