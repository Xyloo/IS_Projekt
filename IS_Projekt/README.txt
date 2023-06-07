OS: Windows 10/11 + Docker Desktop + WSL2
Autorzy:
Cezary Gozdalski
Tomasz Gęszka

## Technologie
- Docker, Docker Compose
- ASP.NET Core 7.0
	- Entity Framework Core
- Angular

## Uruchomienie
Wymagany jest Docker Desktop w przypadku Windowsa oraz WSL2. W przypadku Linuxa wystarczy Docker.
Należy wywołać polecenie 'docker compose up --build' w folderze głównym.
Po uruchomieniu aplikacja będzie dostępna pod adresem http://localhost:5000
(aplikacja jest gotowa, kiedy w terminalu widoczne są komunikaty
is_projekt-backend-1        | info: Microsoft.Hosting.Lifetime[14]
is_projekt-backend-1        |       Now listening on: http://[::]:80
is_projekt-backend-1        | info: Microsoft.Hosting.Lifetime[0]
is_projekt-backend-1        |       Application started. Press Ctrl+C to shut down.
is_projekt-backend-1        | info: Microsoft.Hosting.Lifetime[0]
is_projekt-backend-1        |       Hosting environment: Development
is_projekt-backend-1        | info: Microsoft.Hosting.Lifetime[0]
is_projekt-backend-1        |       Content root path: /app
)
Do projektu dołączone są również gotowe pliki .xml oraz .json do importu. Baza danych domyślnie jest pusta.
Do importu wymagane jest konto administratora, domyślnie utworzone. Username oraz hasło to 'admin'.