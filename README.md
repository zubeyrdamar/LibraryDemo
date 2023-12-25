# Library Demo

Bu proje kullanıcıların kitaplara ve kitaplarla ilgili kısa bilgilere ulaşabilecekleri, aynı zamanda talep ettikleri kitapları ödünç alabilecekleri bir uygulamadır.


## Installation 

* Projeyi lokal cihazınızda çalıştırmak için projeyi indirin veya klonlayın.
* "Configure Startup Projects" ayarları içerisinden 'multiple startup projects' seçeneğini seçiniz. Bu alan içerisinde 'Library.Api' ve 'Library.UI' "Start" seçeneğine sahip olmalı.
* UI katmanında "appsettings.json" dosyasında tanımladığımız "ApiUrl": "http://localhost:5224/api" yerine kendi api url adresinizi veriniz. (Projede değişiklik yapmadıysanız gerekmeyebilir. Library.Api.http dosyası içerisinden api url'inize erişebilirsiniz.)
* Migrationları ve Database'inizi ayarlayınız.

## Migrations & Database

DataAccess katmanına gidin

```bash
cd Library.DataAccess
```

Migration oluşturun

```bash
dotnet ef migrations add LibraryInitialCreate --context LibraryDbContext
```

Database güncelleyin

```bash
dotnet ef database update --context LibraryDbContext
```

Api Katmanına gidin

```bash
cd ../
cd Library.Api
```

Migration oluşturun

```bash
dotnet ef migrations add IdentityInitialCreate --context ApplicationIdentityDbContext
```

Database güncelleyin

```bash
dotnet ef database update --context ApplicationIdentityDbContext
```
    
## Documentation

Daha fazla bilgi için: 
https://docs.google.com/document/d/1zF05G_qcDLvxJKib0C1cQtDeShFKNZVHx4L1O6ALskE/edit?usp=sharing
  
