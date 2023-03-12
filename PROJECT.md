# Het Depot
_Opdracht INFPRJ23B (Hogeschool Rotterdam)_


## Inleiding
Het fictieve Depot Beunmans Van Boijingen is het eerste publiek toegankelijk kunstdepot ter wereld. Het depot staat naast
Museum Beunmans Van Boijingen in het Museumpark in Rotterdam. Bezoekers zien het resultaat van 173 jaar
verzamelen. Meer dan 151.000 verzamelde kunstwerken opgeslagen bij elkaar, gerangschikt en gestructureerd
in veertien depotruimten met vijf klimaten. Naast de objecten zijn alle werkzaamheden die komen kijken bij het
beheer en onderhoud van een collectie te zien.


## Rondleiding
Bezoekers moeten uiterlijk de dag ervoor online een entreebewijs kopen. Eenmaal in het Depot kunnen ze
kiezen om zelf rond te lopen en/of mee te gaan met een rondleiding. Per rondleiding kunnen maar een beperkt
aantal bezoekers mee. Er zijn drie rondleidingen per uur en ze starten om :00, :20 en :40. De openingstijden zijn
van 11:00 tot 17:30.


## Bezoekers
Bezoekers kunnen aan het begin van hun bezoek in het Depot ter plaatse reserveren voor een van de
rondleidingen via een van de consoles in het entreegebied. Bezoekers zien dan eerst wat de volgende
rondleidingen zijn en of er nog plaats is voor hun gezelschap. Vervolgens kunnen ze een plaats reserveren voor
een van de rondleidingen met hun unieke code. De applicatie controleert uiteraard eerst of die unieke code
hoort bij een van de entreebewijzen van die dag. Als bezoekers zich bedenken, dan kunnen ze hun reservering
annuleren en, als ze dat willen, een nieuwe reservering maken.


## Gidsen
Gidsen kunnen de volgende rondleiding starten. Alle bezoekers die gereserveerd hebben geven dan een voor
een nog een keer hun unieke code in. Als iemand inderdaad een reservering heeft voor de rondleiding, dan
wordt in het bestand met reserveringen opgeslagen dat deze bezoeker de rondleiding is gestart. De applicatie
maakt dan een geluid als teken voor de gids dat deze bezoeker mee mag en ze krijgt dan een labjas van de gids
zodat tijdens de rondleiding zichtbaar is wie er bij de rondleiding horen. Als iemand geen reservering heeft voor
de de rondleiding, dan krijgt de bezoeker de reden te zien. De applicatie maakt dan geen geluid en de bezoeker
mag niet mee met de rondleiding. Uiteraard mogen bezoekers maar 1 keer mee met een rondleiding.


## Afdelingshoofd

Het afdelingshoofd levert elke dag voordat de eerste bezoekers arriveren een JSON-bestand aan met de unieke
codes van de entreebewijzen van die dag. Het afdelingshoofd heeft verder regelmatig informatie nodig over de
bezettingsgraad van rondleidingen om de frequentie te optimaliseren.


## Opdracht

Maak een console applicatie in C# voor het depot die alle reserveringen voor de rondleidingen opslaat in JSON-formaat.