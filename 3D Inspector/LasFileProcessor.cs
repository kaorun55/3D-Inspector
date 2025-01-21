using LasSharp;
using System;
using System.IO;
using System.Runtime.InteropServices;

public class LasFileProcessor
{
    [DllImport("laszip64.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int laszip_open_reader([MarshalAs(UnmanagedType.LPStr)] string filename, ref IntPtr reader);

    [DllImport("laszip64.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int laszip_get_header_pointer(IntPtr reader, ref IntPtr header);

    [DllImport("laszip64.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int laszip_get_point_count(IntPtr header, out ulong pointCount);

    [DllImport("laszip64.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int laszip_get_bounding_box(IntPtr header,
        out double minX, out double minY, out double minZ,
        out double maxX, out double maxY, out double maxZ);

    [DllImport("laszip64.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int laszip_close_reader(IntPtr reader);

    public static string Process(string filePath)
    {
        // LASファイルを読み込む
        using var reader = new LasReader(filePath);

        // バウンディングボックスを取得
        double minX = reader.MinX;
        double minY = reader.MinY;
        double minZ = reader.MinZ;
        double maxX = reader.MaxX;
        double maxY = reader.MaxY;
        double maxZ = reader.MaxZ;

        // 点群数を取得
        long pointCount = reader.NumberOfPoints;

        // ファイルサイズを取得
        long fileSize = new FileInfo(filePath).Length;

        // 解析結果を文字列として作成
        string result = $"LASファイル解析結果:\n" +
                                $"ファイルサイズ: {fileSize} bytes\n" +
                                $"ディメンジョン: X={(float)(maxX - minX)}, Y={(float)(maxY - minY)}, Z={(float)(maxZ - minZ)}\n" +
                                $"点群数: {pointCount}";

        return result;
    }
}
