# CookBook

Završni projekat NSZ Obuke - .Net Grupa II

  CookBook

CookBook je osmišljena kao aplikacija za deljenje, organizaciju i prikazivanje recepata koje dodaju korisnici. Za dodavanje novih recepata korisnici moraju biti ulogovani. Takodje, postoji i posebna vrsta korisnika, to su administratori koji mogu da uredjuju recepte i imaju pregled svih korisnika. Gosti aplikacije, koji nisu korisnici imaju mogućnost da vide sve recepte, ali kada neko želi da doda novi recept mora biti registrovan. Sami korisnici aplikacije mogu dodavati nove recepte, menjati i brisati postojeće pod uslovom da su ih oni dodali.


Aplikacija je izrađena u ASP.NET-u framework-u, korišćena je tehnologija MVC. Od tehnologija se još koriste i: 


    -Entity Framework
    -Jquery
    -Bootstrap
    
    
-Layout Page sadrži navbar u kome se nalazi link za početnu stranu Home, dropdown lista u kojoj su kategorije jela gde se klikom na jednu od kategorija vodi na stranicu gde su izdvojeni recepti iz konkretne kategorije. Pored toga, layout strana sadrži i search polje kao i linkove "Login" i "Registruj se" koji vode na stranice za logovanje ili registraciju novih korisnika. 

Kada se korisnik uloguje, preusmerava se na profilnu stranu na kojoj vidi sve recepte koje je dodavao. Pored svakog recepta koje je dodao korisnik nalazi se dugme za izmenu recepta kao i dugme za brisanje recepta. Tu je takodje dugme za dodavanje novog recepta koje vodi na posebnu stranu za dodavanje novog recepta. Prilikom dodavanja novog recepta klikom na dugme "Dodaj recept" prikaže se stranica sa detaljima konkretnog recepta. 

Prilikom dodavanja novog recepta korisnik može dodati maksimalno 5 slika. Slike se čuvaju u File sistemu na serveru. Putanja do slike kao i id recepta za koji je slika vezana se čuvaju u bazi podataka u tabeli Images. Baza podataka takođe ima i tabelu Recipes u koju se smeštaju recepti (ime recepta, kategorija kojoj pripada, sastojci, postupak), zatim tabelu RecipeType u koju su pobrojane kategorije recepata a koja je povezana sa tabelom Recipes. Baza sadrži i tabelu IngredientMeasures koja služi za popunjavanje dropdown liste koja se koristi kod dodavanja novog recepta.


























