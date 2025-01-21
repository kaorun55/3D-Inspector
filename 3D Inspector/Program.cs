using System;
using System.IO;

// コマンドライン引数の確認
if (args.Length == 0)
{
    Console.WriteLine("ファイルパスを指定してください。");
    return;
}

string filePath = args[0];

// ファイルの存在確認
if (!File.Exists(filePath))
{
    Console.WriteLine("指定されたファイルが見つかりません。");
    return;
}

// ファイル形式に応じて処理を実行
string extension = Path.GetExtension(filePath).ToLower();
string result;

try
{
    result = extension switch
    {
        ".las" => LasFileProcessor.Process(filePath),
        ".glb" => GlbFileProcessor.Process(filePath),
        ".obj" => ObjFileProcessor.Process(filePath),
        _ => throw new NotSupportedException("サポートされていないファイル形式です。")
    };

    // 結果をコンソールに表示
    Console.WriteLine(result);

    // 結果をテキストファイルとして保存
    string outputFilePath = filePath + ".txt";
    File.WriteAllText(outputFilePath, result);
    Console.WriteLine($"解析結果を以下のファイルに保存しました: {outputFilePath}");
}
catch (Exception ex)
{
    Console.WriteLine($"エラー: {ex.Message}");
}
