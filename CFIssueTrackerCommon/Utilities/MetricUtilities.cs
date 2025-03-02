using CFIssueTrackerCommon.Models;
using System.Drawing;

namespace CFIssueTrackerCommon.Utilities
{
    internal static class MetricUtilities
    {  
        /// <summary>
       /// Returns colors for dimensions
       /// </summary>
       /// <returns></returns>
        public static List<System.Drawing.Color> GetDimensionColors()
        {
            var colors = new List<System.Drawing.Color>();
            var properties = typeof(System.Drawing.Color).GetProperties().Where(p =>
                            p.PropertyType == typeof(System.Drawing.Color) &&
                            p.ReflectedType == typeof(System.Drawing.Color)).ToList();
            foreach (var property in properties)
            {
                var color = (System.Drawing.Color)property.GetValue(null);
                if (color != System.Drawing.Color.Transparent) colors.Add(color);
            }
            return colors;
        }

        /// <summary>
        /// Gets dimension color (RGBA format) from input color (Color value, color name, RGBA values)
        /// </summary>
        /// <param name="color"></param>
        /// <param name="colors"></param>
        /// <returns></returns>
        public static string GetRGBADimensionColor(string color, List<System.Drawing.Color> colors)
        {
            // Check if list of RGBA values
            if (color.Contains(",")) return color;  // RGBA format

            // Check if color value (RGBA value)
            if (Int32.TryParse(color, out var colorValue))
            {
                var colorItem = Color.FromArgb(colorValue);
                return $"{colorItem.R},{colorItem.G},{colorItem.B},{colorItem.A}";
            }

            // Check if color name
            var colorByName = colors.FirstOrDefault(c => c.Name == color);
            if (colorByName != null)
            {
                return $"{colorByName.R},{colorByName.G},{colorByName.B},{colorByName.A}";
            }
            return "";
        }

        /// <summary>
        /// Gets all combinations of dimensions. Works with up to 4 dimensions
        /// </summary>
        /// <param name="dimensionInfosByProperty">Lists of dimension items (One list per dimension)</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static List<List<DimensionInfo>> GetDimensionCombinations(List<List<DimensionInfo>> dimensionInfosByProperty)
        {
            if (dimensionInfosByProperty.Count > 4)
            {
                throw new NotSupportedException("Only combinations of 3 dimensions are supported");
            }

            // TODO: Handle any number of dimensions. Could probably do it with recursion if I could be bothered!!
            var combinations = new List<List<DimensionInfo>>();
            for (int index1 = 0; index1 < dimensionInfosByProperty[0].Count; index1++)
            {
                if (dimensionInfosByProperty.Count == 1)   // Only 1 dimension
                {
                    combinations.Add(new List<DimensionInfo>() { dimensionInfosByProperty[0][index1] });
                }
                else   // At least 2 dimensions
                {                    
                    for (int index2 = 0; index2 < dimensionInfosByProperty[1].Count; index2++)
                    {
                        if (dimensionInfosByProperty.Count == 2)   // Only 2 dimensions
                        {
                            combinations.Add(new List<DimensionInfo>() { dimensionInfosByProperty[0][index1],
                                                                dimensionInfosByProperty[1][index2] });
                        }
                        else   // At least 3 dimensions
                        {
                            for (int index3 = 0; index3 < dimensionInfosByProperty[2].Count; index3++)
                            {
                                if (dimensionInfosByProperty.Count == 3)   // Only 3 dimensions
                                {
                                    combinations.Add(new List<DimensionInfo>() { dimensionInfosByProperty[0][index1],
                                                                dimensionInfosByProperty[1][index2],
                                                                dimensionInfosByProperty[2][index3] });
                                }
                                else    // At least 4 dimensions
                                {
                                    for (int index4 = 0; index4 < dimensionInfosByProperty[3].Count; index4++)
                                    {
                                        combinations.Add(new List<DimensionInfo>() { dimensionInfosByProperty[0][index1],
                                                                dimensionInfosByProperty[1][index2],
                                                                dimensionInfosByProperty[2][index3],
                                                                dimensionInfosByProperty[3][index4]});
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return combinations;
        }
    }
}
