public static class Serializer
{
    public static string ToJSONArray(float[,] array2d)
    {
        string result = "[";

        for(int i = 0; i < array2d.GetLength(0); i++)
            for(int j = 0; j < array2d.GetLength(1); j++)
                result += string.Format("\"{0}\",", array2d[i,j]);

        // remove last ',' and add closing ']'
        result = result.Remove(result.Length - 1) + "]"; 
        return result;
    }                
}
