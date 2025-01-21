# 3D Inspector - 3Dファイル解析ツール

このツールは、OBJ、GLB、LASファイルを読み込み、以下の情報を自動で取得・表示するC#スクリプトです。

- **ディメンジョン**（XYZ軸の最大値に基づく範囲）
- **ファイルサイズ**
- **メッシュ数**（GLB、OBJファイル）
- **点群数**（LASファイル）

また、取得した情報はコンソールに出力されるとともに、解析結果をテキストファイルとして保存します。

---

## 特長

- **対応フォーマット**:
  - `.obj`: Wavefront OBJ形式の3Dファイル
  - `.glb`: GLTFバイナリ形式の3Dファイル
  - `.las`: 点群データ形式

- **自動解析**:
  - XYZ軸の最大値に基づいてディメンジョンを計算。
  - メッシュや点群数を自動的に取得。
  - ファイルサイズを解析。

- **出力**:
  - 解析結果をコンソールに表示。
  - 入力ファイルと同じディレクトリに解析結果を `.txt` ファイルとして保存。

---

## 必要環境

- **.NET 6** 以上
- **依存ライブラリ**
  - **SharpGLTF**（GLBファイル処理用）
  - **LASSharp**（LASファイル処理用）

---
