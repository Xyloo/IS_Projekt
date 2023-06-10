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
Kroki:
1. Należy wywołać polecenie 'docker compose up --build db' w folderze głównym. Po pojawieniu się komunikatu "DB Ready" należy użyć Ctrl+C do zatrzymania kontenera (ważne, aby nie został usunięty - został stworzony kontener z bazą danych). W przypadku pojawienia się błędów może zajść potrzeba zmiany długości polecenia "sleep" (linia 4) w pliku sql-create-db.sh. Microsoft zaleca 90 sekund, autorom wystarczało 10 sekund. Skrypt jest ustawiony na 15 sekund.
2. Należy wywołać polecenie 'docker compose up --build' oraz oczekiwać na komunikat "DB Ready". Po nim aplikacja jest już gotowa do użytku.
Po uruchomieniu aplikacja będzie dostępna pod adresem http://localhost:5000
Do projektu dołączone są również gotowe pliki .xml oraz .json do importu (katalog DataToImport). Baza danych domyślnie jest pusta.
Do importu wymagane jest konto administratora, domyślnie utworzone. Username oraz hasło to 'admin'.

## Opis
Aplikacja umożliwia tworzenie wykresów na podstawie danych z bazy.
Dane dotyczą użytkowania internetu (InternetUse) oraz poziomu korzystania z usług e-commerce (ECommerce) w krajach europejskich.
Dane pochodzą z lat 2002-2022 i mogą być niepełne w zależności od kraju. Mogą też zdarzyć się lata, w których dane nie zostały zebrane.