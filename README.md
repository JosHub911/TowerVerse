# Tower Defense

**Naam:**   
**Klas:** SD2B  

---

## 1. Titel en Elevator Pitch

**Titel:** TowerVerse  
**Elevator pitch:** Je bent een groep jagers uit verschillende dimensies. Je doel is om spawns (monsters die de multiverse willen verstoren) weg te jagen. Je hebt 3 levels; elk level geeft je coins waarmee je towers kunt kopen.

---

## 2. Wat maakt jouw tower defense uniek

- Je speelt de main character.
- Je kan hem besturen met pijltjes of WASD.
- Je kan attacks uitvoeren met die character.

---

## 3. Schets van je level en UI

*(Hier kun je later een schets/image invoegen)*

---

<img width="887" height="512" alt="OnePage" src="https://github.com/user-attachments/assets/efa3630a-712d-4114-9598-ae714ae79bdf" />

---


## 4. Torens

| Toren       | Effect                                                                 | Bereik | CD       |
|------------|------------------------------------------------------------------------|--------|----------|
| Lijmfles   | Maakt enemies langzamer, lage afstand, grote hit                        | Kort   | 2 sec    |
| H2O fles   | Kan focusen op 1 enemy, minimale afstand                               | Middel | 0.2 sec  |
| Bril       | Schiet een grote laser, heel ver                                       | Lang   | 4 sec    |

---

## 4.5 Characters

| Character    | Effect/Stats                             |
|-------------|------------------------------------------|
| Space Guy   | Kan schieten, medium damage, short cd    |
| Warrior     | High damage, short hitbox, medium cd     |

---

## 5. Vijanden

| Enemy       | Snelheid       | Damage         | Extra info                         |
|------------|----------------|----------------|-----------------------------------|
| Skeleton   | Middel          | Middel         | Average guy                        |
| Ghoul      | Sloom           | Hoog           | -                                 |
| Bat        | Snel            | Klein          | Vliegt over de grote laser         |

---

## 6. Gameplay loop

1. Start je level.  
2. Enemies komen; je begint met geld.  
3. Koop towers en characters.  
4. Als je genoeg geld hebt kan je 1 character kopen en besturen.  

---

## 7. Progressie

- Elk level spawnt sterkere en meer enemies.  
- Elk level geeft meer coins.  

---

## 8. Risico’s en oplossingen volgens PIO

| Probleem                          | Impact                  | Oplossing                                              |
|----------------------------------|------------------------|-------------------------------------------------------|
| Sommige characters werken niet    | Niet complete character | Simpele mechanics toevoegen                            |
| Hitbox problemen                  | Slechte hitbox         | ChatGPT gebruiken of iemand vragen voor hulp          |

---

## 9. Planning per sprint en mechanics

**Sprint 1:**  
- Base framework  
- Alle resources  

**Sprint 2:**  
- Alles in elkaar zetten  
- Eerste test level met towers  

**Sprint 3:**  
- Alle 3 levels klaar  
- Characters werken aan mechanics  

**Sprint 4:**  
- Menu met character selection  
- 3 levels geconnecteerd  

**Sprint 5:**  
- Werkende characters  
- Alle levels getest op bugs  

---

## 10. Inspiratie

- Clash Royale  
- Orcs Must Die!  

---

## 11. Technisch ontwerp mini

- Enemies bewegen naar een kant (van links naar rechts).  
- Towers vallen enemies aan.  
- Als een tower stuk is, gaan enemies naar het main point.  
- Je hebt een speelbare character die je kunt kopen met coins.  

### 11.1 Vijandbeweging over het pad

- Kan naar een toren gaan of direct naar het defendpoint.  
- Doel is om vijand te elimineren.  
- Vijand blijft doorvechten als hij niet geslowed is.  

### 11.2 Doel kiezen en schieten

- Towers kunnen op 1 enemy focussen of een groep.  
- Characters kun je zelf besturen.  
- Kans om te sterven.  
- Toren verdedigen om naar het volgende level te gaan.  

### 11.3 Waves en spawnen

- Elk level heeft 2 waves.  
- Elk level spawnt steeds meer enemies.  
- 1 wave niet halen = Game Over.  
- Alle waves overleven = volgende level.  

### 11.4 Economie en levens

- Je hebt 1 leven.  
- Als je leven op is = DOD.  
- Verdedig defendpoint om leven te behouden.  

### 11.5 UI basis

- Simple cartoony.  
- Healthbar en torenplaats UI.  
- Simpele UI die werkt.


