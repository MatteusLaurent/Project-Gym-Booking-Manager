# Projektuppgift - Gym Booking Manager

*Till lärare - Notera att delar med kursiv stil ska diskuteras/åtgärdas/tas bort innan överlämning till studenterna.*

Ett gym vill ha hjälp med ett nytt bokningssystem kring deras träningsverksamhet. Systemet ska kunna hantera registrering och bokning av aktiviteter, samt datalagring av information om nödvändiga entiteter mellan skilda programkörningar. Några exempel på aktiviteter i dagsläget:
- Gruppträning med flera deltagare och en träningsledare.
- Individuell konsultation/träning med personlig tränare.
- Bokning/hyrning av träningsredskap/utrymme av två slag;
    - Fast avgift, för alla.
    - "Gratis", men endast för kunder med medlemsskap eller dagspass.



Att notera är att arbetet har redan påbörjats, men uppgiften har nu överlåtits till ert team - därav existerar ett [projektskelett](#givna-diagram-och-kod) med stora hål (och kanske vissa brister). Målet är att utveckla en **prototyp** för "affärslogiken"; d.v.s. klasser och objekt som kan hålla koll på och manipulera data i ett mellan-lager (mellan UI och Datalagring). Därav skrivs projektet som en **single-user konsolapplikation**.



## Kravspecifikation

Hela kravspecifiktationen hittar ni [här](https://github.com/MatteusLaurent/Project-Gym-Booking-Manager/blob/master/Gym%20Booking%20Manager/Documentation/Software%20Requirement%20Specification.md).

## Givna diagram och kod
Skapa en [förgrening](https://docs.github.com/en/get-started/quickstart/fork-a-repo) av följande [Git-repo](https://github.com/MatteusLaurent/Project-Gym-Booking-Manager). Repo:t innehåller en hastigt utförd start av tidigare projektansvariga, skriven innan projektet nu har vidarelämnats till er. Bekanta er med innehållet. Sammanfattning av medföljande:
- En Visual Studio solution (lösning) som utgörs av två projektkataloger: **Gym Booking Manager** och **Tests**.
    - **Gym Booking Manager**: Huvudprojektet. Notera underkatalogen **Documentation**.
        - **Documentation**: Innehåller [kravspecifikation](https://github.com/MatteusLaurent/Project-Gym-Booking-Manager/blob/master/Gym%20Booking%20Manager/Documentation/Software%20Requirement%20Specification.md), och diverse (o)fullständiga UML-diagram. Notera att .md (Markdown) och .uxf (UML eXchange Format) i Visual Studio endast öppnas som råtext. Se [verktyg](#verktyg).
    - **Tests**: Två kategorier: Tester som testar redan implementerade klasser & tester utformade för att testa <ins>önskade</ins> static-metoder i main-programmet.
- <a id="dl-gs"></a>Ett gränssnitt för datalagring: `GymDatabaseContext`. Den har en underliggande implementation som använder lagring med lokala filer.
- *PASS*


## Uppgift
Studera kravspecifikationen samt existerande UML-diagram och kod. 
- Saknas information i kravspecifikationen? Vilken information från kravspecifikationen reflekteras inte i givna diagram respektive kod? Vilka delar är påbörjade fast ofullständiga och behöver slipas?

Fixa och vidareutveckla diagrammen:
 -  Use Case diagram med text-beskrivningar.
 -  Klassdiagram. 
 - Aktivitetsdiagram och/eller sekvensdiagram. *En av? Eller båda? Om de får medföljande exempel här så båda förslagsvis.*
 - *Scenario (textdokument?)*

Notera att diagramskapande är en inledande och en iterativ process. Det måste inte bli rätt från början, och saker kan komma att ändras när ni börjar koda. **Uppdatera även era diagram allteftersom och försök att skapa så stor överrensstämmelse som möjligt mellan kod och dokumentation.**

När ni känner att era diagram har tagit form kan ni börja koda. Notera att vi utvecklar en prototyp som en konsolapplikation. Förväntas av programmet:
- Implementerar logik som nyttjar lämpliga principer i OOP-paradigmet.
- En enkel användarkontext utan fullständig inloggning.
- En enkel programloop där användaren kan välja mellan olika alternativ som ungefär reflekterar Use Cases; användarkontexten begränsar möjligheterna.
- Data och information hämtas/lagras/uppdateras, under och mellan programkörningar, genom [datalagringsgränssnittet](#dl-gs).
- Testprojektet förväntar sig vissa funktioner (static-metoder) i programklassen som ska kunna köras utan fel. *Ifyllning av testerna? Får de ändra testerna?*

Sammanfattade mål:
- En samling UML-diagram som guidar er design samt ger en grov överblick av programmet.
- En fungerande prototyp som kan utföra specifikationens funktionella krav.

## Extrauppgifter
Om man blir "klar" tidigt så finns det många saker man skulle kunna kika på. Del av uppgiften är [genomförandet](#genomförande), så ni bör nyttja tiden som är disponerad för projektet. Några exempel för extrauppgifter:
- Inloggning utöver användarnamn. 
- Utöka testprojektet med egna isolerade tester för era klasser (Unit Testing).
- Optional Functional Requirements från kravspecifikationen.
- Analys (börja kanske med lämpligt UML-diagram) av "databas"-implementationen. Försök sen svara på vissa lösa frågor:
    - Utförs onödigt arbete?
    - Går det att optimera (konsekvensfritt?)?
    - Om applikationen var multi-user och kunde köra flera användarsessioner parallellt, vad för problem kan dyka upp?
- Egna idéer.

## Syfte
Att få träna på OOP-analys, -design, och -implementation i grupp. Ytterligare, träna på att studera och vidareutveckla existerande kodskelett och UML-diagram.

## Genomförande
- Ni är fyra till fem teammedlemmar som ska vidareutveckla en prototyp utifrån specifikationen inom två veckors tid. Till er hjälp (och hinder) har ni det givna kodskelettet med inkluderad dokumentation.
- Använd Agil projektmetod.
    - Daily-scrum i början på dagen där läraren bjuds in.
    - Retrospective-möte i slutet på dagen utan läraren. Summera även dagens aktiviteter i en **personlig** dagbok.
    - Utse roterande rollerna mötesansvarig och Scrum Master. Mötesansvarig schemalägger och kallar till Zoom-möte.
- *PASS*



## Verktyg
**Visual Studio 2022** för kod.

**UMLet** eller **VS Code med UMLet extension** för UML-diagram.

**GitHub**, **VS Code**, eller annan text-editor med inbyggd/plugin support för .md, Markdown-filer (kommande Visual Studio 17.5 ska få inbyggd Markdown support).

**GitHub** för att spara koden centralt. Skapa en förgrening (fork) från given kod. *Annat gitsystem går även bra?*

**Trello / Jira / Clubhouse** eller liknande för sprintplanering och uppgifter.

**Zoom** för live-/standup-möten.

**??** för dagboksföring.



## Bedömning
Överrensstmmelsen mellan design (dokumenten) och implementation (koden) är i fokus. För koden görs en bedömning utifrån principerna för OOP. Koden ska klara av de givna testerna *(?)*.

*VG/G/IG?* ges som betyg på uppgiften.

## Vad ska lämnas in?
En inbjudan till er förgrening av Git-repo:t.

Egna UML-diagram ska ligga i underkatalogen **Gym Booking Manager/Documentation/**.

*Andra dokument eller anteckningar relevanta för utförandet? Dagboken är för egen övning, inte inlämning?*

## Redovisning
*Exakt slutdatum, tid, och inlämningsformat TBD*

På fredag 17/2 2023 klockan 18:00 ska projektet vara inlämnat för varje grupp, vilket sker genom en gruppinlämning på Moodle för projektuppgiften.

Samla alla dokument *(enligt ovan sektion)* i en egen mapp och lägg den i projektmappen för koden *(again, enligt ovan sektion efter förtydligande)*. Spara allt i ett zip-arkiv med filformatet ZIP. Ladda upp zip-filen på Moodle för er grupp. Döp katalogen/mappen enligt formatet nedan och när ni packar zip-filen får den samma namn.

**MJU22_OOP_01_Grp\<ert gruppnummer\>.zip**

Exempel:

**MJU22_OOP_01_Grp6.zip**
