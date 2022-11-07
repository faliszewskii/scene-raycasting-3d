using Assimp;
using scene_raycasting_3D.model;
using scene_raycasting_3D.utility;
using static scene_raycasting_3D.utility.Utility;

namespace scene_raycasting_3D.geometry;

public static class PolygonFillAlgorithm
{
    public static void Fill(Polygon polygon, Vector3D theSun, Vector3D camera, DirectBitmap bitmap, Color color, Vector2D offset)
    {
        if (polygon.Vertices.Count < 3) return;
        var vertices = polygon.Vertices;
        
        var vertexNormalMap = polygon.Vertices.Zip(polygon.Normals, (v, n) => new { v, n })
            .ToDictionary(x => x.v, x => x.n);

        var colors = vertexNormalMap.Select(pair => 
            DeriveVertexColor(pair.Key, pair.Value, theSun, camera, color)
        ).ToList();

        var sortedVertexIndices = Enumerable.Range(0, vertices.Count).ToList();
        sortedVertexIndices.Sort((i,j) => vertices[i].Y.CompareTo(vertices[j].Y));
        int yMin = (int)vertices[sortedVertexIndices.First()].Y;
        int yMax = (int)vertices[sortedVertexIndices.Last()].Y;

        int k = 0; // Last checked Vertex in the sorted list  
        int yCurrent = yMin;
        var aetList = new List<AetStruct>();

        for (; yCurrent <= yMax; yCurrent++)
        {
            
            int nextVertexId = sortedVertexIndices[k];
            var nextVertex = vertices[nextVertexId];
            while ((int)nextVertex.Y == yCurrent - 1)
            {
                if (vertices.GetModInd(nextVertexId - 1).Y > nextVertex.Y)
                {
                    float m = (vertices.GetModInd(nextVertexId - 1).X - nextVertex.X) /
                              (vertices.GetModInd(nextVertexId - 1).Y - nextVertex.Y); 
                    aetList.Add(new AetStruct {Dx = m, X = nextVertex.X, YMax = (int)vertices.GetModInd(nextVertexId-1).Y});
                }
                if (vertices.GetModInd(nextVertexId + 1).Y > nextVertex.Y)
                {
                    float m = (vertices.GetModInd(nextVertexId + 1).X - nextVertex.X) /
                              (vertices.GetModInd(nextVertexId + 1).Y - nextVertex.Y); 
                    aetList.Add(new AetStruct {Dx = m, X = nextVertex.X, YMax = (int)vertices.GetModInd(nextVertexId+1).Y});
                }
                
                k++;
                nextVertexId = sortedVertexIndices[k];
                nextVertex = vertices[nextVertexId];
            }
            

            aetList.Sort((aet1, aet2) => aet1.X.CompareTo(aet2.X));
            for (int i = 0; i < aetList.Count; i += 2)
            {
                for(int j = (int)aetList[i].X; j < (int)aetList.GetModInd(i+1).X; j++)
                    if (bitmap.VectorInBound(j + offset.X, yCurrent + offset.Y))
                    {
                        //var col = DeriveColorInPolygon(vertices, new Vector2D(j, yCurrent), colors);
                        var col = DeriveNormalAndColorInTriangle(vertexNormalMap, vertices, new Vector2D(j,yCurrent), theSun, camera, color);
                        bitmap.SetPixel(j+offset.X,yCurrent+offset.Y, col);
                    }
                        
            }
            aetList.RemoveAll(aet => aet.YMax <= yCurrent);
            aetList.ForEach(aet => aet.X += aet.Dx);
        }
        Enumerable.Range(0, vertices.Count).ToList()
            .FindAll(i => bitmap.VectorInBound(vertices[i].X+offset.X, vertices[i].Y +offset.Y))
            .ForEach(i => bitmap.SetPixel(vertices[i].X+offset.X, vertices[i].Y +offset.Y, colors[i]));
    }

    private static Color DeriveVertexColor(Vector3D vertex, Vector3D normal, Vector3D theSun, Vector3D camera, Color objectColor)
    {
        var normalSun = new Vector3D(theSun.X, theSun.Y, theSun.Z) - vertex;
        normalSun.Normalize();
        var normalCamera = new Vector3D(camera.X, camera.Y, camera.Z);
        normalCamera.Normalize();
        
        float kD = 0.5f;
        float kS = 0.5f;
        float m = 10;
        var iL = new Vector3D(1, 1, 1);
        var iO = new Vector3D(objectColor.R/255f, objectColor.G/255f, objectColor.B/255f);
        var n = normal;
        var v = normalCamera;
        var l = normalSun;
        float cosNl = CrossProduct(n, l) > 0? CrossProduct(n, l): 0;
        var r = 2 * cosNl * n - l;
        float cosVr = CrossProduct(v, r) > 0? CrossProduct(v, r): 0;
        
        var i = kD * iL * iO * cosNl + kS * iL * iO * (float)Math.Pow(cosVr, m);
        
        return Color.FromArgb((int)(i.X * 255), (int)(i.Y * 255), (int)(i.Z * 255));
        //return Color.FromArgb((int)(cosNl * 255), (int)(cosVr * 255), (int)(0 * 255));
    }

    private static float CrossProduct(Vector3D v1, Vector3D v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

    private static Color DeriveColorInTriangle(List<Vector3D> vertices, Vector2D point, List<Color> vertexColors)
    {
        var v1 = vertices[0];
        var v2 = vertices[1];
        var v3 = vertices[2];
        float w1 = ((v2.Y - v3.Y) * (point.X - v3.X) + (v3.X - v2.X) * (point.Y - v3.Y)) /
                   ((v2.Y - v3.Y) * (v1.X - v3.X) + (v3.X - v2.X) * (v1.Y - v3.Y));
        float w2 = ((v3.Y - v1.Y) * (point.X - v3.X) + (v1.X - v3.X) * (point.Y - v3.Y)) /
                   ((v2.Y - v3.Y) * (v1.X - v3.X) + (v3.X - v2.X) * (v1.Y - v3.Y));
        float w3 = 1 - w1 - w2;
        var weights = new List<float>{w1, w2, w3};
        return ColorMean(vertexColors, weights);
    }

    private static Color ColorMean(List<Color> colors, List<float> weights)
    {
        var mean = new Vector3D();
        ForRange(0, colors.Count, i =>
        {
            float w = weights[i];
            mean.X += colors[i].R * w;
            mean.Y += colors[i].G * w;
            mean.Z += colors[i].B * w;
        });
        
        return Color.FromArgb((int)mean.X, (int)mean.Y, (int)mean.Z);
    }

    private static Color DeriveNormalAndColorInTriangle(Dictionary<Vector3D,Vector3D> verticeNormals, List<Vector3D> vertices, Vector2D point, Vector3D theSun, Vector3D camera, Color objectColor)
    {
        
        var v1 = vertices[0];
        var v2 = vertices[1];
        var v3 = vertices[2];
        float w1 = ((v2.Y - v3.Y) * (point.X - v3.X) + (v3.X - v2.X) * (point.Y - v3.Y)) /
                   ((v2.Y - v3.Y) * (v1.X - v3.X) + (v3.X - v2.X) * (v1.Y - v3.Y));
        float w2 = ((v3.Y - v1.Y) * (point.X - v3.X) + (v1.X - v3.X) * (point.Y - v3.Y)) /
                   ((v2.Y - v3.Y) * (v1.X - v3.X) + (v3.X - v2.X) * (v1.Y - v3.Y));
        float w3 = 1 - w1 - w2;
        var weights = new List<float>{w1, w2, w3};
        var normal = VectorMean(verticeNormals.Select(p => p.Value).ToList(), weights);
        var height = FloatMean(verticeNormals.Keys.Select(v => v.Z).ToList(), weights);
        return DeriveVertexColor(new Vector3D(point, height), normal, theSun, camera, objectColor);
    }

    private static Vector3D VectorMean(List<Vector3D> vector, List<float> weights)
    {
        var mean = new Vector3D();
        ForRange(0, vector.Count, i =>
        {
            float w = weights[i];
            mean.X += vector[i].X * w;
            mean.Y += vector[i].Y * w;
            mean.Z += vector[i].Z * w;
        });

        return mean;
    }

    private static float FloatMean(List<float> nums, List<float> weights)
    {
        using var w = weights.GetEnumerator();
        return nums.Aggregate(0f, (prev, cur) =>
        {
            w.MoveNext();
            return prev + cur * w.Current;
        });
    }
}