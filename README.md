# Projektuppgift - Gym Booking Manager

Ett gym vill ha hjälp med ett nytt bokningssystem kring deras träningsverksamhet. Systemet ska kunna hantera registrering och bokning av aktiviteter, samt datalagring av information om nödvändiga entiteter mellan skilda programkörningar. Några exempel på aktiviteter i dagsläget:
- Gruppträning med flera deltagare och en träningsledare.
- Individuell konsultation/träning med personlig tränare.
- Bokning/hyrning av träningsredskap/utrymme av två slag;
    - Fast avgift, för alla.
    - "Gratis", men endast för kunder med medlemskap eller dagspass.



Att notera är att arbetet redan har påbörjats, men uppgiften har nu överlåtits till ert team - därav existerar ett [projektskelett](#givna-diagram-och-kod) med stora hål (och kanske vissa brister). Målet är att utveckla en **prototyp** för "affärslogiken"; d.v.s. klasser och objekt som kan hålla koll på och manipulera data i ett mellan-lager (mellan UI och Datalagring). Därav skrivs projektet som en **single-user konsolapplikation**.



## Kravspecifikation

Hela kravspecifikationen hittar ni [här](https://github.com/MatteusLaurent/Project-Gym-Booking-Manager/blob/master/Gym%20Booking%20Manager/Documentation/Software%20Requirement%20Specification.md).

## Givna diagram och kod
Skapa en [förgrening](https://docs.github.com/en/get-started/quickstart/fork-a-repo) (med **private**-åtkomst) av följande [Git-repo](https://github.com/MatteusLaurent/Project-Gym-Booking-Manager). Repo:t innehåller en hastigt utförd start av tidigare projektansvariga, skriven innan projektet nu har vidarelämnats till er. Bekanta er med innehållet. Sammanfattning av medföljande:
- En Visual Studio solution (lösning) som utgörs av två projektkataloger: **Gym Booking Manager** och **Tests**.
    - **Gym Booking Manager**: Huvudprojektet. Notera underkatalogen **Documentation**.
        - **Documentation**: Innehåller [kravspecifikation](https://github.com/MatteusLaurent/Project-Gym-Booking-Manager/blob/master/Gym%20Booking%20Manager/Documentation/Software%20Requirement%20Specification.md), och diverse (o)fullständiga UML-diagram. Notera att .md (Markdown) och .uxf (UML eXchange Format) i Visual Studio endast öppnas som råtext. Se [verktyg](#verktyg).
    - **Tests**: Två kategorier: Unit-tester som testar klassers interna logik & tester som testar mer systemövergripande funktionalitet.
- <a id="dl-gs"></a>Ett gränssnitt för datalagring: `GymDatabaseContext`. Den har en underliggande implementation som använder lagring med lokala filer.


## Uppgift
Studera kravspecifikationen samt existerande UML-diagram och kod. 
- Saknas information i kravspecifikationen? Vilken information från kravspecifikationen reflekteras inte i givna diagram respektive kod? Vilka delar är påbörjade fast ofullständiga och behöver slipas?

Fixa och vidareutveckla diagrammen:
 - Use Case diagram med text-beskrivningar.
     - Lägg till Scenario-beskrivning för minst tre Use Case:s.
 - Klassdiagram. 
 - Skapa ett aktivitetsdiagram eller sekvensdiagram för en lämplig aspekt av systemet.

Notera att diagramskapande är en inledande och en iterativ process. Det måste inte bli rätt från början, och saker kan komma att ändras när ni börjar koda. **Uppdatera även era diagram allteftersom och försök att skapa så stor överrensstämmelse som möjligt mellan kod och dokumentation.**

När ni känner att era diagram har tagit form kan ni börja koda. Notera att vi utvecklar en prototyp som en konsolapplikation. Förväntas av programmet:
- Implementerar logik som nyttjar lämpliga principer i OOP-paradigmet.
- En enkel användarkontext utan fullständig inloggning.
- En enkel programloop där användaren kan välja mellan olika alternativ som ungefär reflekterar Use Cases; användarkontexten begränsar möjligheterna.
- Data och information hämtas/lagras/uppdateras, under och mellan programkörningar, genom [datalagringsgränssnittet](#dl-gs).

Tester:
- Testprojektet antar viss funktionalitet. Fixa existerande tester så att de funkar att köra och kan testa programmet.

Sammanfattade mål:
- En samling UML-diagram som guidar er design samt ger en grov överblick av programmet.
- En fungerande prototyp som kan utföra specifikationens funktionella krav.

## Extrauppgifter
Om man blir "klar" tidigt (eller om man vill jobba vidare efter projektet) så finns det många saker man skulle kunna kika på. Del av uppgiften är [genomförandet](#genomförande), så ni bör nyttja tiden som är disponerad för projektet. Några exempel för extrauppgifter:
- Inloggning utöver användarnamn. 
- Utöka testprojektet med korta isolerade tester för era klassers metoder (Unit Testing).
- Optional Functional Requirements från kravspecifikationen.
- Analys (börja kanske med lämpligt UML-diagram) av "databas"-implementationen. Försök sen svara på vissa lösa frågor:
    - Utförs onödigt arbete?
    - Går det att optimera (konsekvensfritt?)?
    - Om applikationen var multi-user och kunde köra flera användarsessioner parallellt, vad för problem kan dyka upp?
    - ?
- Egna idéer.

## Syfte
Att få träna på OOP-analys, -design, och -implementation i grupp. Ytterligare, träna på att studera och vidareutveckla existerande kodskelett och UML-diagram.

## Genomförande
- Ni är fyra till fem teammedlemmar som ska vidare-designa och -utveckla en prototyp utifrån specifikationen inom två veckors tid. Till er hjälp (och hinder) har ni det givna kodskelettet med inkluderad dokumentation.
- Använd Agil projektmetod.
    - Utse roterande rollerna mötesansvarig och Scrum Master. Mötesansvarig schemalägger och kallar till Zoom-möte.
    - Daily-scrum i början på dagen där läraren bjuds in (se [schema](http://moodle.molk.se/mod/page/view.php?id=4087) på moodle).
    - Retrospective-möte i slutet på dagen utan läraren.
    - Reflektera över och summera dagens aktiviteter i en personlig dagbok (~5 minuter).
    - Spendera gärna lite extra tid den sista projekt-dagen på mötet och dagboksskrivandet. En sorts post-mortem reflektion.
- **Tips:** Se till att ni skapar en gemensam helhetsbild av projektet under diagramprocessen. Ni kan då skapa tasks (vi jobbar agilt!) och fördela arbetsbördan på ett dynamiskt och smart sätt. Kommunikation är viktig under hela resan.

## Verktyg
Kod:  **Visual Studio 2022**.

UML-diagram (.uxf): **UMLet** eller **VS Code med UMLet extension**.

Markdown (.md): Text-editor med inbyggd/plugin support för .md. Finns många alternativ att välja emellan. **GitHub** direkt i browsern funkar också.

Central versionshantering (.git): **GitHub**. Skapa en förgrening (fork) från given kod.

Sprintplanering/Scrumboard: **Trello / Jira / Clubhouse** eller liknande.

Möten: **Zoom** för live-/standup-möten.

## Bedömning
Överrensstämmelsen mellan design (dokumenten) och implementation (koden) är i fokus. För koden görs en bedömning utifrån principerna för OOP.

**G:** Har jobbat enligt [genomförande](#genomförande) för att uppfylla punkterna i [Uppgift](#uppgift)-sektionen. Rimliga OOP-principer har applicerats i design och implementation.

**VG:** Har inkluderat både sekvens- och aktivitets-diagram. OOP-principer har applicerats med gott omdöme i samtliga delar av design och implementation och påvisar särskild förståelse för området. Har utökat testprojektet med rimliga unit-tester för de skrivna klasserna, samt ett par tester som testar mer övergripande programförlopp. 


## Vad ska lämnas in?
När ni har skapat er förgrening av Git-repo:t, inkludera **MatteusLaurent** och **TonyAtMolk** som Collaborator:s. Använd Git-systemet när ni jobbar, så att er kod och era diagram finns uppdaterade på ert repo. Egna UML-diagram som ni skapar ska ligga i underkatalogen **Gym Booking Manager/Documentation/**.

För slutinlämning skapar ni ett zip-arkiv utav repo:ts master branch (på GitHub: Gröna knappen 'Code' -> 'Download ZIP').



## Redovisning
På fredag 17/2 2023 klockan 18:00 ska projektet vara inlämnat för varje grupp, vilket sker genom en gruppinlämning på Moodle för projektuppgiften.

Enligt ovan sektion så skapar ni ett zip-arkiv av ert projekt, förslagsvis direkt från GitHub. Ladda upp zip-filen på Moodle för er grupp. Namnge er zip-fil enligt nedan format.

**MJU22_OPC_Projekt_01_Grp\<ert gruppnummer\>.zip**

Exempel:

**MJU22_OPC_Projekt_01_Grp6.zip**
