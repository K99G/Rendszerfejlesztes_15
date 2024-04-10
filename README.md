**Rendszerarchitektúra**

A rendszer két fő komponensre oszlik:

    Szerver (API):
    ASP.NET Core-t használva a szerver egy RESTful API-ként funkcionál,lehetővé téve adatműveleteket egy MariaDB adatbázison.
    Kezeli a CRUD műveleteket entitásokon, mint például a menedzserek, projektek, fejlesztők és feladatok.
    
    Kliens (Blazor WebAssembly): 
    Egyoldalas alkalmazás (SPA), amely a szerver API-ját használja, felületet biztosítva a rendszerrel való interakcióhoz.
    Lehetővé teszi a felhasználók számára, hogy megtekinthessék és kezelhessék a projekteket, feladatokat és személyzetet.
    
**Fejlesztési Környezet**

    Keretrendszer: .NET 8.0
    Programozási Nyelvek: C#
    Kliens Technológia: Blazor WebAssembly
    Szerver Technológia: ASP.NET Core
    Adatbázis: MariaDB, XAMPP(8.2.12) használatával(apachefriends.org)
    ORM: Entity Framework Core

**Adatbázis Konfiguráció**

    Xampp által localis SQL szerveren fut.
    A MariaDB adatbázis az Entity Framework Core használatával, 
    a Pomelo Entity Framework Core MySQL provider segítségével érhető el. 
    Az ApplicationDbContext osztály MySQL-t használ.
    
    A csatlakozási adatokat a ConnectionString tárolja:
    ("ConnectionString": "server=localhost;port=3306;database=redminedb;user=root;password=adminpw;")
    
**Adatbázis Séma**

Az adatbázis a következő entitásokból áll:

    Manager: A rendszer menedzsment képességekkel rendelkező felhasználóit képviseli.
    Developer: A rendszer fejlesztői felhasználóit képviseli.
    Project: Egy projektet képvisel, amely több feladatot foglal magába.
    Task: Egy egyedi munkaelemet képvisel.
    ProjectDeveloper: A projektek és fejlesztők közötti sok-sok kapcsolatot képviseli.

    A kapcsolatokat az ApplicationDbContext OnModelCreating metódusában határozzák meg,
    biztosítva a kapcsolódó kulcsok és navigációs tulajdonságok megfelelő beállításait a relációs integritás érdekében.

**API Végpontok**

A szerver RESTful végpontokat biztosít az entitások kezelésére:

    Menedzserek: /api/managers
    Projektek: /api/projects
    Feladatok: /api/tasks

    A szerver csak HTTP protokollon van konfigurálva, a Swagger engedélyezve van az API dokumentációjához és teszteléséhez.
    A CORS úgy van konfigurálva, hogy engedélyezze a kliens alkalmazás kéréseit,
    biztosítva a kereszt-origin hozzáférés biztonságos kezelését.

    
**Kliens Alkalmazás**

    A Blazor WebAssembly kliens az API-val kommunikál, 
    hogy bemutassa a felhasználói interfészt a rendszer interakciójához.
    Oldalakat kínál a menedzserek, projektek és feladatok listázására, aszinkron kéréseket használva az adatok szerverről történő lekéréséhez.

Kulcs Funkciók

    Adatmegjelenítés: A menedzserek, projektek és feladatok listái a releváns részletekkel jelennek meg.
    Betöltési Állapot: Jelzi, amikor az adatokat a szerverről kérjük le.

**Ez a projekt bemutat egy Server-client alkalmazást, amely egy feladatkezelő rendszerre összpontosít bemutatva,
egy Blazor WebAssembly kliens és egy ASP.NET Core szerver integrációját,egy MariaDB adatbázis háttérrel az adatok perzisztenciájához.**
  
