using System.Collections.Generic;

namespace RightTriangleApi.Models
{
    public class RightTriangleVertices
    {        
        public (int x, int y) V1 { get; set; }
        public (int x, int y) V2 { get; set; }
        public (int x, int y) V3 { get; set; }
        public Dictionary<string, int[]> Coordinates =>
            new Dictionary<string, int[]>() {
                { "V1", new int[] { V1.x, V1.y } },
                { "V2", new int[] { V2.x, V2.y } },
                { "V3", new int[] { V3.x, V3.y } }
        };
        public RightTriangleVertices(int v1x, int v1y, int v2x, int v2y, int v3x, int v3y)
        {
            V1 = (v1x, v1y);
            V2 = (v2x, v2y);
            V3 = (v3x, v3y);
        }
    }
}
