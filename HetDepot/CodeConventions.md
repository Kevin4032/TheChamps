_Voor alle situaties die worden gespecifieerd hieronder zullen de conventies op 
https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions aan gehouden worden._

# Documentatie
## Classes
In elke class met logica (services etc.) zal boven aan de class een blok comment geschreven worden met hierin een korte uitleg
over de class.

## Volgorde class declaraties
* a. private properties
* b. public properties
* c. constructors
* d. public methods
* e. private methods
## Regions
Als regions worden gedefinieerd; verwijder de region en herontwerp met nieuwe classes.
Regions wijzen in de regel op het niet toepassen van SOLID.
## Comments
Boven public methods worden errors gespecificeerd.
Ook worden andere conventies gehanteerd beschreven in
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
## Interfaces
Inzet van interfaces is het mogelijk maken van inversion of control.
## Styling / linting
De default linting van Visual Studio 2022 wordt gebruikt.
## Solution
### Mappen
Om de layered architecture een tastbaar resultaat te laten zijn wordt in de solution gekozen voor een mappenstructuur waarin de layers expliciet worden gemaakt. Uiteraard is layered architecture een design, is de mappenstructuur visueel ondersteunend.
### Bestanden
Elke class heeft zijn eigen bestand. Relatie bestand < - > class is 1 op 1.

## Testsolution
### Mappen
De mappenstructuur komt overeen met de solution
### Bestanden
Elke public class in de DepotSolution heeft een testclass. De relatie bestand < - > class is 1 op 1.