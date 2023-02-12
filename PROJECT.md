# Het Depot
_Opdracht INFPRJ23B (Hogeschool Rotterdam)_


## Inleiding
Het fictieve Depot Beunmans Van Boijingen is het eerste publiek toegankelijk kunstdepot ter wereld. Het depot staat naast
Museum Beunmans Van Boijingen in het Museumpark in Rotterdam. Bezoekers zien het resultaat van 173 jaar
verzamelen. Meer dan 151.000 verzamelde kunstwerken opgeslagen bij elkaar, gerangschikt en gestructureerd
in veertien depotruimten met vijf klimaten. Naast de objecten zijn alle werkzaamheden die komen kijken bij het
beheer en onderhoud van een collectie te zien.

- *(Deze inleiding is heel interessant, maar bevat geen concrete informatie die relevant is voor het project.)*


## Rondleiding
Bezoekers moeten uiterlijk de dag ervoor online een entreebewijs kopen. Eenmaal in het Depot kunnen ze
kiezen om zelf rond te lopen en/of mee te gaan met een rondleiding. Per rondleiding kunnen maar een beperkt
aantal bezoekers mee. Er zijn drie rondleidingen per uur en ze starten om :00, :20 en :40. De openingstijden zijn
van 11:00 tot 17:30.

- **Hoeveel personen is "een beperkt aantal bezoekers" concreet?**
- **Hoe lang duurt een rondleiding, en is dit relevant? Mag er bijvoorbeeld nog een rondleiding om 17:20 starten als de sluitingstijd 17:30 is?**
    - Ik stel voor om een lijst op te stellen met exacte tijden van rondleidingen, dus *11:00, 11:20, 11:40, 12:00, 12:20, 12:40*, enz. Dit zorgt voor maximale flexibiliteit en versimpelt de applicatie doordat de openingstijden en duur van de rondleidingen niet meer van belang zijn.
    - Voor het aantal bezoekers per rondleiding: Keuze tussen instelbaar per exacte tijd (nadeel: alles apart aan moeten passen als de standaard wijzigt) of simpelweg een enkele instelling voor alle tijdstippen. Waar gaat de voorkeur naar uit?
- **Het lijkt wenselijk dat deze gegevens (aantal rondleidingen en bezoekers, tijdstippen van rondleidingen) ergens in een instellingen-bestand opgeslagen worden, zodat deze in de toekomst gewijzigd kunnen worden (uit de tekst onder *Afdelingshoofd* blijkt dat dat nodig kan zijn). Klopt dit, en is er een voorkeur voor een bepaald bestandsformaat/schema? (Indien geen duidelijke voorkeur: dan bepalen wij.)**
    - Bijvoorbeeld een bestand "instellingen.json" in de Working Directory van de applicatie.
    - Het bieden van een interface om zulke instellingen aan te passen valt wat mij betreft NIET binnen de scope van het project. Het is dus aan het Afdelingshoofd om wanneer nodig het bestand met instellingen aan te passen of te vervangen.
- **Hoe kunnen we controleren of een unieke code een geldige code is (geen typfouten en niet iets willekeurigs dat een bezoeker zomaar invult)? Hebben de codes een bepaald formaat?**
    - Het kopen van een entreebewijs en genereren van unieke codes valt buiten de scope van het project. De applicatie gaat ervan uit dat bezoekers al een unieke code hebben ontvangen.


## Bezoekers
Bezoekers kunnen aan het begin van hun bezoek in het Depot ter plaatse reserveren voor een van de
rondleidingen via een van de consoles in het entreegebied. Bezoekers zien dan eerst wat de volgende
rondleidingen zijn en of er nog plaats is voor hun gezelschap. Vervolgens kunnen ze een plaats reserveren voor
een van de rondleidingen met hun unieke code. De applicatie controleert uiteraard eerst of die unieke code
hoort bij een van de entreebewijzen van die dag. Als bezoekers zich bedenken, dan kunnen ze hun reservering
annuleren en, als ze dat willen, een nieuwe reservering maken.
- **Worden "Volgende rondleidingen" alleen voor dezelfde dag opgegeven, of is het ook mogelijk om te reserveren voor een latere dag?**
    - Uit de tekst blijkt dat een entreebewijs (en dus unieke code) alleen op een bepaalde dag geldig is. Moeten bezoekers ook voor een andere dag dan vandaag kunnen reserveren?
- **Moeten volgeboekte rondleidingen worden weergegeven, of kunnen deze worden verborgen?**
- **Zijn er beperkingen op annuleren/wijzigen? Bijvoorbeeld: tot een bepaalde tijd vantevoren?**
    - Dit kan een aanpasbare instelling zijn.
- **Moeten we überhaupt de systeemklok hiervoor gebruiken?**
    - Of zeggen we simpelweg dat reserveringen mogelijk zijn totdat een gids de rondleiding gestart heeft?


## Gidsen
Gidsen kunnen de volgende rondleiding starten. Alle bezoekers die gereserveerd hebben geven dan een voor
een nog een keer hun unieke code in. Als iemand inderdaad een reservering heeft voor de rondleiding, dan
wordt in het bestand met reserveringen opgeslagen dat deze bezoeker de rondleiding is gestart. De applicatie
maakt dan een geluid als teken voor de gids dat deze bezoeker mee mag en ze krijgt dan een labjas van de gids
zodat tijdens de rondleiding zichtbaar is wie er bij de rondleiding horen. Als iemand geen reservering heeft voor
de de rondleiding, dan krijgt de bezoeker de reden te zien. De applicatie maakt dan geen geluid en de bezoeker
mag niet mee met de rondleiding. Uiteraard mogen bezoekers maar 1 keer mee met een rondleiding.
- **Hoe kunnen Gids en Afdelingshoofd toegang krijgen tot functionaliteiten? Moet dit beveiligd worden?**
    - Bijvoorbeeld toegangscode/wachtwoord (één enkele, of een lijst van meerdere, uit instellingen)? Of mag dit simpelweg onbeveiligd ("Maak keuze 3 om een rondleiding te starten", verder geen controle)?
- **Is er een gewenst formaat/schema waar het te genereren bestand met reserveringen moet voldoen? Als dit er niet is (behalve dat het JSON moet zijn), bepalen wij dat zelf.**
    - Bijvoorbeeld een bestand "reserveringen-YYYY-MM-DD.json".
- **"Een geluid" is technisch ingewikkeld en past slecht bij een console-applicatie. Is er een gewenst geluid (daarvan een geluidsbestand), of volstaat het gebruik van bijvoorbeeld [C#'s Console.Beep](https://learn.microsoft.com/en-us/dotnet/api/system.console.beep?view=net-6.0)?**


## Afdelingshoofd

Het afdelingshoofd levert elke dag voordat de eerste bezoekers arriveren een JSON-bestand aan met de unieke
codes van de entreebewijzen van die dag. Het afdelingshoofd heeft verder regelmatig informatie nodig over de
bezettingsgraad van rondleidingen om de frequentie te optimaliseren.
- **Is er een voorbeeldbestand, of is er een specificatie voor hoe dit bestand eruit ziet? Of moeten wij zelf specificeren waar het bestand van het afdelingshoofd aan moet voldoen?**
    - Uiteraard moet dit een JSON-bestand zijn.
- **Is er behoefte aan een specifieke interface waarmee het Afdelingshoofd een bestand in kan laden, of volstaat het dat het bestand op een vaste plaats onder een vooraf bekende naam wordt opgeslagen?**
    - Bijvoorbeeld: het Afdelingshoofd plaatst een bestand van bekende specificaties als "entreecodes-YYYY-MM-DD.json" in de huidige Working Directory en hoeft verder de applicatie niet op te starten.
- **Is het voor het Afdelingshoofd noodzakelijk om informatie over bezettingsgraad rechtstreeks uit de applicatie zelf te ontvangen? Of valt dit buiten de scope van het project?**
    - Deze informatie zal sowieso beschikbaar zijn uit het bestand met reserveringen dat door de applicatie worden geschreven. (Zie tekst onder *Gidsen* hierboven.)
- **Voorkeur voor het NIET specifiek inbouwen van bovenstaande twee punten (interface voor aanbrengen en inzien van gegevens) omdat dit de ontwikkeling van de applicatie fors versimpelt.**


## Opdracht

Maak een console applicatie in C# voor het depot die alle reserveringen voor de rondleidingen opslaat in JSON-formaat.

- **Een console-applicatie past natuurlijk mooi bij de retrofuturistische uitstraling van Het Depot, maar wat betekent dit concreet voor de applicatie? Kunnen we ervan uitgaan dat de applicatie op een moderne Windows 10 of 11 PC draait?**
- **Zijn er eisen voor de inhoud en locatie waar bestanden vandaan gelezen en naartoe geschreven moeten worden? Mag dit de huidige Working Directory zijn?**
- **Is het noodzakelijk om rekening te houden met het gebruik van meerdere Consoles tegelijkertijd? Is het mogelijk dat meerdere instances van de applicatie tegelijk gebruik moeten maken van dezelfde bestanden?**
