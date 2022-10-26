using System.Collections.Immutable;
using scene_raycasting_3D.model;
using scene_raycasting_3D.utility;

namespace scene_raycasting_3D.geometry;

internal class Polygon
{
    private ImmutableList<Vertex> Vertices { get; }
    
    public Polygon(ImmutableList<Vertex> vertices)
    {
        Vertices = vertices;
    }

    public void Fill(DirectBitmap bitmap, Color color)
    {
        if (Vertices.Count < 3) return;
        var sortedVertexIndices = Enumerable.Range(0, Vertices.Count).ToList();
        sortedVertexIndices.Sort((i,j) => Vertices[i].Y.CompareTo(Vertices[j].Y));
        var yMin = Vertices[sortedVertexIndices.First()].Y;
        var yMax = Vertices[sortedVertexIndices.Last()].Y;

        var k = 0; // Last checked Vertex in the sorted list  
        var yCurrent = yMin;
        var aetList = new List<AetStruct>();

        for (; yCurrent <= yMax; yCurrent++)
        {
            
            var nextVertexId = sortedVertexIndices[k];
            var nextVertex = Vertices[nextVertexId];
            while (nextVertex.Y == yCurrent - 1)
            {
                if (Vertices.GetModInd(nextVertexId - 1).Y > nextVertex.Y)
                {
                    float m = (float)(Vertices.GetModInd(nextVertexId - 1).X - nextVertex.X) /
                              (Vertices.GetModInd(nextVertexId - 1).Y - nextVertex.Y); 
                    aetList.Add(new AetStruct {Dx = m, X = nextVertex.X, YMax = Vertices.GetModInd(nextVertexId-1).Y});
                }
                if (Vertices.GetModInd(nextVertexId + 1).Y > nextVertex.Y)
                {
                    float m = (float)(Vertices.GetModInd(nextVertexId + 1).X - nextVertex.X) /
                              (Vertices.GetModInd(nextVertexId + 1).Y - nextVertex.Y); 
                    aetList.Add(new AetStruct {Dx = m, X = nextVertex.X, YMax = Vertices.GetModInd(nextVertexId+1).Y});
                }
                
                k++;
                nextVertexId = sortedVertexIndices[k];
                nextVertex = Vertices[nextVertexId];
            }
            

            aetList.Sort((aet1, aet2) => aet1.X.CompareTo(aet2.X));
            for (int i = 0; i < aetList.Count; i += 2)
            {
                for(int j = (int)aetList[i].X; j < (int)aetList[i+1].X; j++)
                    bitmap.SetPixel(j,yCurrent, color);
            }
            aetList.RemoveAll(aet => aet.YMax <= yCurrent);
            aetList.ForEach(aet => aet.X += aet.Dx);
        }

        Vertices.ForEach(v => bitmap.SetPixel(v.X, v.Y, Color.Lime));
    }
}