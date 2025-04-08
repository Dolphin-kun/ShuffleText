# 「テキストをシャッフルしながら登場退場」プラグイン
## 概要
テキストをシャッフルしながら登場退場するエフェクトです。  
登場退場のトグルスイッチをどちらもオフにすると「シャッフルテキスト」エフェクトとしても使用できるようになっています。

## 使い方
> [!NOTE]
> [最新バージョンをダウンロード](https://github.com/Dolphin-kun/ShuffleText/releases/latest)  
> こちらから最新バージョンのymmeをダウンロードしてください。
>
> [こっち](https://ymm4-info.net/ymme/%E3%83%86%E3%82%AD%E3%82%B9%E3%83%88%E3%82%92%E3%82%B7%E3%83%A3%E3%83%83%E3%83%95%E3%83%AB%E3%81%97%E3%81%AA%E3%81%8C%E3%82%89%E7%99%BB%E5%A0%B4%E9%80%80%E5%A0%B4%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3)でもダウンロードできます！

1. テキストアイテムを追加して、`登場退場`カテゴリーにある「テキストをシャッフルしながら登場退場」を選択して追加してください。
2. テキストの`文字ごとに分割`をオンにしてください。

## 効果
### 登場時・退場時
登場時、退場時にエフェクトを適用するか指定します。  
> [!TIP]
> 基本機能と異なるところがあります。
>
> 登場時、退場時のトグルスイッチをどちらもオフにすると「シャッフルテキスト」エフェクトとして使用することができます。

### 効果時間
エフェクトの効果時間を指定します。
基本機能と同じです。

### 間隔
アニメーションテキストの変化する間隔を指定します。

### ずれ
アニメーションテキストが変化する間隔にずれを発生させます。

### 表示方法

|同時|順番|
|-|-|
|同時に変化します|テキストの数に合わせて最初の文字から変化します|

#### 開始時間
`表示方法`を`順番`にすると表示されます。
`開始時間`から[効果時間](#効果時間)までの間で、アニメーションテキストからオリジナルテキストへ変化します。

---

### 表示テキスト
|アルファベット|数値|記号|カスタム|
|-|-|-|-|
|アルファベット大文字・小文字|0~9|よく使われる記号([詳しくは](RandomText.cs#L76))|以下の通りです|

#### カスタム
- カスタム
  - A-Z
  - a-z
  - ひらがな
  - カタカナ
  - 漢字
  - 数字
  - 記号
- テキスト

### フォント・サイズ・文字色・太字・イタリック
基本機能と同じです。


## 注
製作者がC#初心者のためよくわからんコードで書いているかもしれません。
参考にはならないと思います。🙇‍♂️

饅頭遣いさん！
コミュニティプラグインでもよければリクエストします♡♡


## 更新履歴
2025/4/8 v1.0 公開
