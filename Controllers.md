De structuur van het programma is opgezet met Controllers. Dit is gebaseerd op het [MVC-model](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93controller). Simpel gezegd is een controller een bepaalde actie die een user uit wil voeren. Als je naar het functioneel ontwerp kijkt, moet ieder blok één controller worden. (In theorie zou een controller uit meerdere _actions_ kunnen bestaan, maar daar ga ik in mijn opzet niet vanuit -- we houden het simpel.)

De bedoeling is dat we voortaan zo min mogelijk wijzigingen doen in Program.cs, maar in plaats daarvan bezig zijn in een controller. Dat werkt zo:

- Er is een abstracte class **Controller**, in de namespace **HetDepot.Controllers** (dat is dus in de submap Controllers). Dit is de basis van alle controllers. Dus _iedere nieuwe Controller extendt vanuit die basis Controller_.
- De basis Controller heeft een abstracte method **Execute** (dat is een method zonder code). Een nieuwe controller _moet_ een override hebben voor de Execute method. Zoals de naam al zegt is dat de method die wordt uitgevoerd zodra de controller aangeroepen wordt. Net zoals de Main van Program.cs dus;
- Een controller kan afhankelijk van zijn eigen logica _zelf bepalen wat de volgende controller wordt_ die moet worden uitgevoerd. Dit kan simpelweg met `NextController = new EenAndereController()`; de property NextController is inherited van de basis Controller;
  - Ik heb een simpel voorbeeld gemaakt in **ExampleController.cs**, dat kan eventueel worden gebruikt als basis.
- **Program.cs** bepaalt welke controller uitgevoerd wordt. Dit gaat met een While loop, en begint met een _default controller_. Dat kan je zien als het hoofdscherm.
  - Op dit moment is dat de TestController, maar dat zal het rondleiding overzicht moeten worden.
- Program.cs voert de _Execute_ method uit van de gewenste controller, en stelt daarna de door die controller opgegeven _NextController_ in als huidige controller. Als de vorige controller geen NextController heeft ingesteld, wordt dit weer de default controller (dus dan ga je terug naar het hoofdscherm);
- Op die manier kan je dus een structuur maken. Onze oude code uit Program.cs heb ik verplaatst naar eigen controllers in de map **Controllers\Tests**:
  - **TestController.cs** bevat de switch statement zoals ik die op het laatst had gemaakt: Het vraagt om een naam en voert de bijbehorende controller uit (of anders de ExampleController);
  - Voor iedereen heb ik een eigen test controller gemaakt: _KarlijnsTestController.cs_, _KevinsTestController.cs_, _RubensTestController.cs_, _TedsTestController.cs_ en _TomsTestController.cs_. Daar mag iedereen zijn eigen tests in doen, op die manier krijgen we geen Merge conflicts meer op Program.cs;
  - Al die tests zijn natuurlijk tijdelijk en niet bedoeld voor de definitieve versie;

Ik hoop dat dat een beetje te snappen is. Lees de code door, en **lees vooral ook de comments in _Program.cs_ en _Controller.cs_.**

Voor de volledigheid: Uiteindelijk is het de bedoeling dat Controllers niet rechtstreeks van en naar de Console lezen en schrijven, maar dat overlaten aan Views. Data zal moeten komen uit Models. Daarmee is dan het MVC-model compleet.
