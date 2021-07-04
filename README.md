# BIT

## Cel aplikacji  
Tradycyjnie w Papierowe RPGi gra się siedząc przy jednym stole, jednak pandemia zmusiła wszystkich fanów tej rozrywki do przeniesienia się na Discorda. Utrudnia to znacznie w zasadzie każdy z aspektów gry, od interakcji między graczami (w której zawsze ważny był osobisty kontakt) po techniczne sprawy, jak poruszanie figurkami przez graczy. W tych technicznych kwestiach właśnie chcemy pomóc naszą aplikacją.  
Program ma na celu umożliwienie prowadzenia sesji zdalnych przy wykorzystaniu tabeli inicjatywy i battle mapy, dzięki, której gracze będą mogli aktywnie wizualnie uczestniczyć w zdalnej sesji RPG.  
Podstawowy sposób wykorzystania aplikacji będzie wyglądał tak, że Mistrz Gry ma ją uruchomioną na swoim komputerze i udostępnia graczom ekran z Battle Mapą (np. przez Discorda). Wygodniejszym, ale jeszcze na ten moment nie w pełni wspieranym trybem pracy jest program uruchomiony na serwerze, gdzie każdy z użytkowników może używać aplikacji w swojej przeglądarce.  
  
## Sposób uruchomienia
Aplikacja jest tutaj w nieskompilowanej formie. Aby ją uruchomić potrzebne jest Visual Studio z zainstalowanym pakietem .NET Framework 4.8.  
Przed uruchomieniem należy zmienić ścieżkę do folderu z bazą danych. Znajduje się ona w pliku WebMVC/Web.config w 14 linijce, w polu "connectionString". Należy ją zmienić na ścieżkę folderu Program_Data w plikach aplikacji.
