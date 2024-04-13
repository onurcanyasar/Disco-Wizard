using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class HelperScript : MonoBehaviour
{
    public static List<int[]>GetNeighbours(int centerX, int centerY, int range)
    {
        int[] min = new[] {centerX - 1, centerY - 1};
        int[] max = new[] {centerX + range, centerY + range};
        List<int[]> res = new List<int[]>();

        for (int x = min[0]; x <= max[0]; x++)
        {
            for (int y = min[1]; y <= max[1]; y++)
            {
                res.Add(new int[]{x, y});
            }
        }

        return res;
    }
}
