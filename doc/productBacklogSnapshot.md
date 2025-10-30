# User Stories - Bird Sort Color Game

---

## US0
**As a player, I want the game to have a main menu, so I can be greeted when I open the game.**  
- Il gioco deve avere come vista iniziale un menu principale che presenta un design introduttivo del gioco, un bottone per iniziare a giocare e un accesso alle impostazioni.  
**Stima:** 2

---

## US1
**As a player, I want the game to have a level view, so I can see the level I want to play and play it.**  
- La struttura dell'applicazione deve essere composta da, oltre a un menu principale, la schermata di gioco che mostra il livello corrente permettendo di giocare. Dal menu principale si può accedere al livello e dal livello si può ritornare al menu.  
**Stima:** 2

---

## US2
**As a player, I want the option to access a Pause menu while I'm playing, so I can change settings, take breaks, and go back to the main menu.**  
- Durante il gioco si può accedere ad un menu di pausa tramite un bottone presente sullo schermo. Questo menu permette di cambiare alcune impostazioni quali gli effetti sonori, la musica e simili. Il menu permette inoltre di riprendere la partita oppure di ritornare al menu principale.  
**Stima:** 2

---

## US3
**As a player, I want to click on a bird to select it, so that I can move it to a different branch.**  
- Il giocatore può cliccare su un ramo per posizionare un uccello già selezionato, permettendo di muovere gli uccelli e ordinarli man mano ad ogni mossa.  
**Stima:** 1

---

## US4
**As a player, I want to click on a branch to move the selected bird there, so that I can sort the birds by color.**  
- Il giocatore può muovere l'uccello selezionato su dei rami con almeno un posto libero, a scopo di ordinare gli uccelli per colore.  
**Stima:** 3

---

## US5
**As a player, I want adjacent birds of the same color to move together, so that I can efficiently group them.**  
- Uccelli dello stesso colore che si trovano vicini si muovono insieme quando spostati, diventano un'unità. Se si prova a spostare un gruppo di uccelli su un ramo che non dispone di abbastanza spazi liberi, ciò non viene permesso.  
**Stima:** 3

---

## US6
**As a player, I want all birds of the same color to fly away when grouped, so that I can complete the level.**  
- Il giocatore sposta gli uccelli, e quando tutti gli uccelli di un certo colore sono raggruppati essi volano via insieme, lasciando spazio libero.  
**Stima:** 2

---

## US7
**As a player, I want the level to end when all birds have flown away, so that I can see my score and progress.**  
- Una volta che tutti i gruppi di uccelli volano via, lasciando i rami vuoti, il livello finisce. Al termine del livello il giocatore vede una schermata che raffigura il suo punteggio, i suoi progressi e azioni possibili.  
**Stima:** 2

---

## US8
**As a player, I want branches to have limited space, so that I need to strategize my moves.**  
- I rami che costituiscono il livello hanno spazio limitato, solo un certo numero di uccelli riescono a occuparne ognuno. Se si prova a spostare un uccello su un ramo pieno, il gioco non lo permette.  
**Stima:** 2

---

## US9
**As a player, I want different levels with varying numbers of branches and birds, so that I can experience new challenges as I progress.**  
- I livello devono essere tutti diversi, con variabili come il numero di uccelli, il numero di diversi tipi di uccelli, il numero di rami e la disposizione iniziale degli uccelli. Questo assicura una nuova esperienza ogni volta, mantenendo l'attenzione del giocatore.  
**Stima:** 5

---

## US10 - DELETED
**As a player, I want levels to increase in difficulty as I progress, so that the game remains challenging and engaging.**  
- Man mano che il giocatore completa livelli, la generazione di questi li deve rendere sempre più difficili da completare. Lo scopo è di sfidare il giocatore man mano che prende la mano per il gioco, tenendolo coinvolto.  
**Stima:** 4

---

## US11
**As a player, I want to earn points based on how few moves I make or how quickly I complete the level, so that I am motivated to play strategically.**  
- Il sistema di ricompensa del gioco è sotto forma di punti esperienza, che il giocatore ottiene completando livelli. La quantità si basa sulla velocità e il numero di mosse per il completamento del livello, uniti alla difficoltà di esso.  
**Stima:** 2

---

## US12
**As a player, i want to adjust the volume of the game from the pause menu, so i can customize my experience.**  
- Il giocatore ha accesso a uno slider per il volume nel menu di pausa, per regolare il volume di tutti i sound effects del gioco.
**Stima:** 1

---

## US13
**As a player, I want to see a level counter, so I can keep track of my progress as I play.**  
- Il gioco deve rendere visibile il livello in cui si trova il giocatore, così da dargli un senso di progresso. Questo indicatore dovrebbe apparire nel menù base del gioco, durante il livello e nel menù visibile su completamento di un livello.  
**Stima:** 1

---

## US14
**As a player, I want to share my past scores on social media, so I can connect with people about this game.**  
- Il giocatore, tramite dei bottoni presenti in app, può condividere sui social media il suo punteggio in un livello, presente o passato. Questo per promuovere un aspetto sociale di comunità per i giocatori.  
**Stima:** 5

---

## US15
**As a player, I want to access an optimal move suggestion, so that I can learn how to solve a level efficiently.**  
- Il giocatore ha a disposizione la soluzione ottimale per il livello, che mostra la sequenza ottimale di mosse per completare il livello. Questi calcoli avvengono subito, e la soluzione viene mostrata al giocatore solo se sceglie di visualizzarla.  
**Stima:** 5

---

## US16
**As a player, I want birds to have idle animations, so I can have an immersive experience while I plan the next move.**  
- Gli uccelli, anche quando sono fermi sui rami, hanno animazioni minimali per conferire un senso di realtà. Ciò contribuisce all'ambiente generale del gioco e l'esperienza del giocatore.  
**Stima:** 2

---

## US17
**As a player, I want the birds to have a flight animation for when they move between branches, so that I can have a more dynamic experience.**  
- Gli uccelli, quando vengono spostati da un ramo all'altro, mostrano un'animazione di volo per simulare il volo di un uccello. Questa animazione inizia quando l'uccello parte e termina quando l'uccello si posa su un ramo.  
**Stima:** 3

---

## US18
**As a player, I want all the birds to have different and radiant colors, so I can easily identify the colors I have to sort.**  
- Gli uccelli devono essere chiaramente distinguibili in base al loro colore. Ogni livello conterrà un certo numero di tipi di uccello, e per ogni tipo un numero fisso di uccelli di quel tipo. I colori corrispondono ai gruppi di uccelli da raggruppare.  
**Stima:** 2

---

## US19
**As a player, I want the birds to make a sound effect when I select them, so I can have feedback for the selection of the bird.**  
- Gli uccelli, quando selezionati (US1), emettono un suono di feedback per segnalare al player l'avvenuta selezione. Questo contribuisce all'ambiente di gioco che crea l'esperienza del giocatore.  
**Stima:** 1

---

## US20
**As a player, I want the birds to make a sound effect when I sort all the birds of the correct color on a branch, so I can feel motivated for completing all the branches.**  
- Quando tutti gli uccelli di un tipo vengono raggruppati e volano via, il giocatore deve sentire un suono di feedback per segnalare il completamento di parte del livello. Questo suono è volto a dare soddisfazione al player per aver completato parte del livello.
**Stima:** 1

---

## US21
**As a player, i want some levels to contain special birds, so that the levels are more challenging.**  
- Alcuni livelli contengono "malus", ovvero bird speciali che aggiungono difficoltà al livello. I tipi di malus sono:  
    -un uccello in gabbia che non può spostarsi via, e un altro con la chiave da posizionare accanto;  
    -un uccello addormentato che non può spostarsi, e un altro con una sveglia da posizionare accanto;  
    -un uccello ha una bomba che fa un "countdown" di mosse, se non vola via entro il numero di mosse il livello è perso;

    Frequenza: un malus ogni 8 partite, due malus assieme ogni 15 partite

**Stima:** 5

---

## US22
**As a player, i want to see the same level if i exit and re-enter, so i can have a uniform and fair experience.**  
- La generazione dei livelli è casuale, ma una volta che il player accede al livello corrente esso viene salvato, così che se torna al menu e poi ritorna al livello esso rimane lo stesso. Questo è per evitare che il player faccia rigenerare il livello finchè gli capita una difficoltà minore. Una volta che il livello è completato e si passa al prossimo, quello nuovo viene salvato e il vecchio sovrascritto.
**Stima:** 3

---