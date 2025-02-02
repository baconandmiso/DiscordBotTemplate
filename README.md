# DiscordBot
[DiscordNET](https://docs.discordnet.dev/)を用いてBotを開発するときに使用するテンプレート。  
[HostBuilder](https://learn.microsoft.com/ja-jp/dotnet/core/extensions/generic-host?tabs=appbuilder)を使用しています。  

## 使用方法
1. このリポジトリをクローン または このテンプレートから新しいリポジトリを作成
2. `appsettings.Development.json` または `appsettings.Production.json`の`DiscordBot-Token`を自分のBotのTokenに置き換える。
    また、 `GuildId`も自分のサーバーIDに置き換える。
3. コンパイルして、実行します。
4. `/ping`コマンドが登録されるので、実行。

## 設定ファイルについて
| ファイル名 |  |
| ---------| -- |
| appsettings.json | 開発・本番環境共通の設定ファイル |
| appsettings.Development.json | 開発環境用の設定ファイル |
| appsettings.Production.json | 本番環境用の設定ファイル |
