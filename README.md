# OmerBank
OmerBank, modern bir bankacılık uygulamasıdır ve kullanıcıların hesap yönetimi, para transferi, döviz çevirimi ve para yatırma/çekme işlemlerini gerçekleştirmelerine olanak sağlar. Uygulama, ASP.NET Core MVC ve JWT tabanlı kimlik doğrulama ile güvenli bir deneyim sunar.
--API Endpoints
POST /register: Yeni kullanıcı kaydı oluşturur.
POST /login: Kullanıcı girişi yapar ve JWT döner.
GET /account/index: Kullanıcının hesap detaylarını gösterir.
POST /account/transfer: Para transferi işlemi gerçekleştirir.
GET /account/convert: Döviz çevirimi yapar ve güncel döviz bilgilerini getirir.
POST /account/deposit: Hesaba para yatırır.
POST /account/draw: Hesaptan para çeker.
--Kullanılan Teknolojiler
.ASP.NET Core MVC
.Entity Framework Core
.JSON Web Token (JWT)
.MSSQL Server
