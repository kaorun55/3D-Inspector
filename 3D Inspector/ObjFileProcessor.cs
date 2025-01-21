using System;
using System.Collections.Generic;
using System.IO;

public class ObjFileProcessor
{
    public static string Process(string filePath)
    {
        try
        {
            var vertices = new List<float[]>();
            int faceCount = 0;

            foreach (var line in File.ReadLines(filePath))
            {
                if (line.StartsWith("v ")) // Vertex
                {
                    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    vertices.Add(new float[]
                    {
                        float.Parse(parts[1]),
                        float.Parse(parts[2]),
                        float.Parse(parts[3])
                    });
                }
                else if (line.StartsWith("f ")) // Face
                {
                    faceCount++;
                }
            }

            float[] dimensions = CalculateDimensions(vertices);

            // ファイルサイズを取得
            long fileSize = new FileInfo(filePath).Length;

            return $"OBJファイル解析結果:\n" +
                   $"ファイルサイズ: {fileSize} bytes\n" +
                   $"ディメンジョン: X={dimensions[0]}, Y={dimensions[1]}, Z={dimensions[2]}\n" +
                   $"メッシュ数: {faceCount}";
        }
        catch (Exception ex)
        {
            throw new Exception($"Error processing OBJ file: {ex.Message}");
        }
    }

    private static float[] CalculateDimensions(List<float[]> vertices)
    {
        float minX = float.MaxValue, minY = float.MaxValue, minZ = float.MaxValue;
        float maxX = float.MinValue, maxY = float.MinValue, maxZ = float.MinValue;

        foreach (var vertex in vertices)
        {
            minX = Math.Min(minX, vertex[0]);
            minY = Math.Min(minY, vertex[1]);
            minZ = Math.Min(minZ, vertex[2]);

            maxX = Math.Max(maxX, vertex[0]);
            maxY = Math.Max(maxY, vertex[1]);
            maxZ = Math.Max(maxZ, vertex[2]);
        }

        return new float[] { maxX - minX, maxY - minY, maxZ - minZ };
    }
}
