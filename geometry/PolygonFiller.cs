using Assimp;
using scene_raycasting_3D.model;
using scene_raycasting_3D.utility;
using System.Drawing;
using static scene_raycasting_3D.utility.Utility;

namespace scene_raycasting_3D.geometry;

public class PolygonFiller
{
    public float Kd { get; set; }
    public float Ks { get; set; }
    public float M { get; set; }
    public Color ObjectColor { get; set; }
    public Color LightColor { get; set; }

    public bool UseNormalInterpolation { get; set; }
    public bool UseModifiedNormals { get; set; }
    
    public Utilities ut;
    public PolygonFiller()
    {
        Kd = 0.5f;
        Ks = 0.5f;
        M = 50;
        ObjectColor = Color.White;
        LightColor = Color.White;
        UseNormalInterpolation = false;
        UseModifiedNormals = false;
        ut = new Utilities();
    }

    public void Fill(Polygon polygon, Vector3D theSun, Vector3D camera, DirectBitmap bitmap, Vector2D offset)
    {
        if (polygon.Vertices.Count < 3) return;
        var vertices = polygon.Vertices;

        var normals = polygon.Normals;
        var vertexNormalMap = polygon.Vertices.Zip(normals, (v, n) => new { v, n })
            .ToDictionary(x => x.v, x => x.n);

        var colors = vertexNormalMap.Select(pair => 
            DeriveVertexColor(polygon, pair.Key, pair.Value, theSun, camera)
        ).ToList();

        var sortedVertexIndices = Enumerable.Range(0, vertices.Count).ToList();
        sortedVertexIndices.Sort((i,j) => vertices[i].Y.CompareTo(vertices[j].Y));
        int yMin = (int)vertices[sortedVertexIndices.First()].Y;
        int yMax = (int)vertices[sortedVertexIndices.Last()].Y;

        int k = 0; // Last checked Vertex in the sorted list  
        int yCurrent = yMin;
        var aetList = new List<AetStruct>();

        for (; yCurrent <= yMax + 1; yCurrent++)
        {
            if (yMax >= bitmap.Height) return;
            if (k == sortedVertexIndices.Count) break;
            int nextVertexId = sortedVertexIndices[k];
            var nextVertex = vertices[nextVertexId];
            while (k < sortedVertexIndices.Count && (int)nextVertex.Y == yCurrent - 1)
            {
                if ((int)vertices.GetModInd(nextVertexId - 1).Y >= (int)nextVertex.Y)
                {
                    float m = (vertices.GetModInd(nextVertexId - 1).X - nextVertex.X) /
                              (vertices.GetModInd(nextVertexId - 1).Y - nextVertex.Y); 
                    aetList.Add(new AetStruct {Dx = m, X = nextVertex.X, YMax = (int)vertices.GetModInd(nextVertexId-1).Y});
                }
                if ((int)vertices.GetModInd(nextVertexId + 1).Y >= (int)nextVertex.Y)
                {
                    float m = (vertices.GetModInd(nextVertexId + 1).X - nextVertex.X) /
                              (vertices.GetModInd(nextVertexId + 1).Y - nextVertex.Y); 
                    aetList.Add(new AetStruct {Dx = m, X = nextVertex.X, YMax = (int)vertices.GetModInd(nextVertexId+1).Y});
                }
                
                k++;
                if (k == sortedVertexIndices.Count) break;
                nextVertexId = sortedVertexIndices[k];
                nextVertex = vertices[nextVertexId];
            }
            

            aetList.Sort((aet1, aet2) => aet1.X.CompareTo(aet2.X));
            for (int i = 0; i < aetList.Count; i += 2)
            {
                for(int j = (int)aetList[i].X; j < (int)aetList[i+1].X; j++)
                    if (bitmap.VectorInBound(j + offset.X, yCurrent + offset.Y))
                    {
                        var col = UseNormalInterpolation ? 
                            DeriveNormalAndColorInTriangle(polygon, vertexNormalMap, vertices, new Vector2D(j,yCurrent), theSun, camera) : 
                            DeriveColorInTriangle(vertices, new Vector2D(j, yCurrent), colors);
                        bitmap.SetPixel(j+offset.X,yCurrent+offset.Y, col);
                    }
                        
            }
            aetList.RemoveAll(aet => aet.YMax <= yCurrent);
            aetList.ForEach(aet => aet.X += aet.Dx);
        }
    }

    private Color DeriveVertexColor(Polygon p, Vector3D vertex, Vector3D normal, Vector3D theSun, Vector3D camera)
    {
        var normalSun = new Vector3D(theSun.X, theSun.Y, theSun.Z) - vertex;
        normalSun.Normalize();
        var normalCamera = new Vector3D(camera.X, camera.Y, camera.Z);
        normalCamera.Normalize();
        
        var iL = new Vector3D(LightColor.R / 255f, LightColor.G / 255f, LightColor.B / 255f);
        Vector3D iO;
        if (p.Texture == null)
            iO = new Vector3D(ObjectColor.R / 255f, ObjectColor.G / 255f, ObjectColor.B / 255f);
        else
        {
            int xi = ut.WrapI((int)vertex.X - p.Texture.Width / 2, p.Texture.Width);
            int yi = ut.WrapI((int)vertex.Y - p.Texture.Height / 2, p.Texture.Height);
            var col = p.Texture.GetPixel(xi, yi);
            iO = new Vector3D(col.R / 255f, col.G / 255f, col.B / 255f);
        }
        Vector3D n;
        if (p.NormalTexture != null && UseModifiedNormals)
            n = GetNormalFromBitmap(p.NormalTexture, new Vector3D(vertex.X, vertex.Y, 0), normal);
        else n = normal;
        var v = normalCamera;
        var l = normalSun;
        float cosNl = Math.Max(0, DotProduct(n, l));
        var r = 2 * DotProduct(n, l) * n - l;
        float cosVr = Math.Max(0, DotProduct(v, r));
        
        var i = Kd * iL * iO * cosNl + Ks * iL * iO * (float)Math.Pow(cosVr, M);
        i.X = Math.Max(0, Math.Min(i.X, 1));
        i.Y = Math.Max(0, Math.Min(i.Y, 1));
        i.Z = Math.Max(0, Math.Min(i.Z, 1));
        
        return Color.FromArgb((int)(i.X * 255), (int)(i.Y * 255), (int)(i.Z * 255));
    }

    public float DotProduct(Vector3D v1, Vector3D v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        

    private Color DeriveColorInTriangle(List<Vector3D> vertices, Vector2D point, List<Color> vertexColors)
    {
        var weights = CalculateWeights(vertices, point);
        return ColorMean(vertexColors, weights);
    }

    private Color ColorMean(List<Color> colors, List<float> weights)
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

    private Color DeriveNormalAndColorInTriangle(Polygon p, Dictionary<Vector3D,Vector3D> verticeNormals, List<Vector3D> vertices, Vector2D point, Vector3D theSun, Vector3D camera)
    {
        var weights = CalculateWeights(vertices, point);
        var normal = VectorMean(verticeNormals.Select(pair => pair.Value).ToList(), weights);
        
        
        var height = FloatMean(verticeNormals.Keys.Select(v => v.Z).ToList(), weights);
        return DeriveVertexColor(p, new Vector3D(point, height), normal, theSun, camera);
    }

    private static List<float> CalculateWeights(List<Vector3D> vertices, Vector2D point)
    {
        var v1 = vertices[0];
        var v2 = vertices[1];
        var v3 = vertices[2];
        float w1 = ((v2.Y - v3.Y) * (point.X - v3.X) + (v3.X - v2.X) * (point.Y - v3.Y)) /
                   ((v2.Y - v3.Y) * (v1.X - v3.X) + (v3.X - v2.X) * (v1.Y - v3.Y));
        w1 = Math.Max(0, Math.Min(w1, 1));
        float w2 = ((v3.Y - v1.Y) * (point.X - v3.X) + (v1.X - v3.X) * (point.Y - v3.Y)) /
                   ((v2.Y - v3.Y) * (v1.X - v3.X) + (v3.X - v2.X) * (v1.Y - v3.Y));
        w2 = Math.Max(0, Math.Min(w2, 1-w1));
        float w3 = 1 - w1 - w2;
        var weights = new List<float> { w1, w2, w3 };
        return weights;
    }

    private Vector3D VectorMean(List<Vector3D> vector, List<float> weights)
    {
        var mean = new Vector3D();
        ForRange(0, vector.Count, i =>
        {
            float w = weights[i];
            mean += vector[i] * w;
        });

        return mean;
    }

    private float FloatMean(List<float> nums, List<float> weights)
    {
        using var w = weights.GetEnumerator();
        return nums.Aggregate(0f, (prev, cur) =>
        {
            w.MoveNext();
            return prev + cur * w.Current;
        });
    }
    
    public Vector3D GetNormalFromBitmap(Bitmap normalBitmap, Vector3D v, Vector3D n)
    {
        var nTextColor = normalBitmap.GetPixel(
            ut.WrapI((int)v.X - normalBitmap.Width/2, normalBitmap.Width),
            ut.WrapI((int)v.Y - normalBitmap.Height/2, normalBitmap.Height));
        var nText = new Vector3D(nTextColor.R / 127f - 1, nTextColor.G / 127f - 1, nTextColor.B / 255f);
        var b = (n.X == 0 && n.Y == 0 && n.Z == 1f)
            ? new Vector3D(0, 0, 1)
            : Vector3D.Cross(n, new Vector3D(0, 0, 1));
        var t = Vector3D.Cross(b, n);
        var m = new Matrix3x3(t.X, b.X, n.X, t.Y, b.Y, n.Y, t.Z, b.Z, n.Z);
        return m * nText;
    }
}