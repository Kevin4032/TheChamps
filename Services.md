# Services
Er zijn vier services;  
- Repository  
- Settings  
- People  
- Tours 

Gedachte is dat onze applicatie 'stateless' is, dat we geen data vasthouden in objecten, dat we lezen van disk. Dat gaat goed omdat we met 1 console zitten. De unittesten gaan bijvoorbeeld niet altijd goed, omdat ze sneller worden uitgevoerd / parallel worden uitgevoerd en er nog een filehandle open is.

## Repository
Lezen en schrijven naar disk.
Idee is dat onze controllers alleen `TourService` en `PeopleService` benaderen, dat het feitelijke lezen en schrijven van daaruit wordt gedaan.

Validatie van het inlezen van data gebeurd met de `DepotDataValidator`.

## Settings
De configuratie van de 'rondleidingtijden', de teksten die getoond worden in de console en het maximum aantal deelnemers per rondleiding.

## PeopleService
Voor het aanroepen van de 'persoonsgegevens';
Er is 1 afdelingshoofd
Er is 1 gids
Er zijn 0 of meer bezoekers

## TourService
Dit is eigenlijk de 'main' service, het doen van reserveringen en aanmeldingen.
Validatie in de controllers.


