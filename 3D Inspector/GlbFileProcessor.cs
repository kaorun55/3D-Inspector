using System;
using System.IO;
using SharpGLTF.Schema2;

public class GlbFileProcessor
{
    public static string Process(string filePath)
    {
        try
        {
            var model = ModelRoot.Load(filePath);

            float minX = float.MaxValue, minY = float.MaxValue, minZ = float.MaxValue;
            float maxX = float.MinValue, maxY = float.MinValue, maxZ = float.MinValue;

            foreach (var mesh in model.LogicalMeshes)
            {
                foreach (var primitive in mesh.Primitives)
                {
                    var positions = primitive.GetVertexAccessor("POSITION");
                    if (positions == null) continue;

                    foreach (var vertex in positions.AsVector3Array())
                    {
                        minX = Math.Min(minX, vertex.X);
                        minY = Math.Min(minY, vertex.Y);
                        minZ = Math.Min(minZ, vertex.Z);

                        maxX = Math.Max(maxX, vertex.X);
                        maxY = Math.Max(maxY, vertex.Y);
                        maxZ = Math.Max(maxZ, vertex.Z);
                    }
                }
            }

            // メッシュの総数を取得
            // GLTFの各メッシュには、複数のプリミティブ（Primitive）が含まれています。それぞれのプリミティブがインデックスバッファを持っており、そこから面数を計算します。三角形の面数は、インデックスバッファの要素数を3で割ることで求められます。
            int totalFaceCount = 0;

            // 各メッシュをループ処理
            foreach (var mesh in model.LogicalMeshes)
            {
                foreach (var primitive in mesh.Primitives)
                {
                    // インデックスバッファから面数を計算
                    var indices = primitive.GetIndices();
                    if (indices == null) continue; // インデックスがない場合をスキップ

                    // 面数を加算（インデックス数 ÷ 3）
                    totalFaceCount += indices.Count / 3;
                }
            }


            // ファイルサイズを取得
            long fileSize = new FileInfo(filePath).Length;

            return $"GLBファイル解析結果:\n" +
                   $"ファイルサイズ: {fileSize} bytes\n" +
                   $"ディメンジョン: X={(float)(maxX - minX)}, Y={(float)(maxY - minY)}, Z={(float)(maxZ - minZ)}\n" +
                   $"メッシュ数: {totalFaceCount}";
        }
        catch (Exception ex)
        {
            throw new Exception($"Error processing GLB file: {ex.Message}");
        }
    }
}
