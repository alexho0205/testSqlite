# testSqlite

- 測試目的: 快速佈署於 azure app service 
- 架構 : .net core mvc with sqlite( file mode )

# 佈署方式
- 建立一個 app service 環境
- 將專案檔內 /sqlite/db.db 放置於 app service /wwwroot/sqlite/db.db
- 編輯專案檔內 appsettings.json 變更 DefaultConnection 位置指向 /wwwroot/sqlite/db.db
- 使用 azure plugin for vs2022 打包上傳 app service
- 
