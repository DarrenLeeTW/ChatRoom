# ChatRoom

這是一個使用 **ASP.NET Core MVC** 與 **SignalR** 打造的簡易即時聊天室範例，展示如何管理連線、傳送訊息並更新線上使用者清單。

## 先決條件

- 在本機安裝 [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) 或更新版本。

## 建置與執行

1. 還原套件並建置專案：
   ```bash
   dotnet build ChatRoom/ChatRoom.csproj
   ```
2. 執行應用程式：
   ```bash
   dotnet run --project ChatRoom/ChatRoom.csproj
   ```
   預設會在 `https://localhost:5001` 提供服務。

## 專案結構

- `Program.cs` / `Startup.cs` – 設定並啟動 ASP.NET Core 主機與 SignalR Hub。
- `Hubs/ChatHub.cs` – 處理連線與訊息的 Hub 實作。
- `Controllers/` 與 `Views/` – MVC 控制器及 Razor View。
- `wwwroot/` – 前端使用的靜態檔案。

本範例可作為學習 ASP.NET Core 及 SignalR 即時通訊的起點。

## OAuth 設定

範例已整合 Google 與 Facebook OAuth 登入，需在 `appsettings.json` 中填入對應的
`ClientId`、`ClientSecret` 或 `AppId`、`AppSecret` 後才能正常使用。
