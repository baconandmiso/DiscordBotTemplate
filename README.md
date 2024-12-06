# DiscordBot
[DiscordNET](https://docs.discordnet.dev/)を用いてBotを開発するときに使用するテンプレート。  
[HostBuilder](https://learn.microsoft.com/ja-jp/dotnet/core/extensions/generic-host?tabs=appbuilder)を使用しています。  

## 使用方法
1. このリポジトリをクローン または このテンプレートから新しいリポジトリを作成
2. `appsettings.json`の`DiscordBot-Token`を自分のBotのTokenに置き換える。
    また、 `GuildId`も自分のサーバーIDに置き換える。
3. コンパイルして、実行します。
4. `/ping`コマンドが登録されるので、実行。
