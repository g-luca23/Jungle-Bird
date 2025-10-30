# Documentazione Classe `Malus`

## **Descrizione**

Classe per la gestione dei malus degli uccelli.

---

## **Proprietà**

| Nome               | Tipo     | Descrizione                                 |
| ------------------ | -------- | ------------------------------------------- |
| `check`| `bool` | indica se l'uccello ha un malus senza andare a vedere tutti le proprietà |
| `key`  | `bool` | indica se l'uccello ha la chiave. |
| `cage` | `bool` | indica se l'uccello è ingabbiato. |
| `bomb` | `bool` | indica se l'uccello ha la bomba.|
| `clock`| `bool` | indica se l'uccello ha la la sveglia. |
| `sleep`| `bool` | indica se l'uccello è addormentato. |

---

## **Metodi**
| valore di ritorno | Nome               | Argomenti  | Descrizione                                 |
| ------------------ | ------------------ | ---------- | ------------------------------------------- |
|    ``    | `malus`| `(bool check = false, bool key = false, bool cage = false, bool bomb = false, bool clock = false, bool sleep = false)` | Costruttore della classe. Inizializza i malus con valori predefiniti o specificati. |
|  `void`  | `setsleep`  | `()` | Attiva il malus "sleep" se nessun altro malus è attivo. |
|  `void`  | `setcloack`  | `()` | Attiva il malus "clock" se nessun altro malus è attivo.|
|  `void`  | `setkey`  | `()` | Attiva il malus "key" se nessun altro malus è attivo. |
|  `void`  | `setcage`  | `()` | Attiva il malus "cage" se nessun altro malus è attivo. |
|  `void`  | `setbomb` | `()` | Attiva il malus "bomb" se nessun altro malus è attivo. |
|  `void`  | `unset` | `()` | Disattiva tutti i malus e reimposta l'attributo check a false. |
|  `void`  | `sblocca`| `()` | Sblocca l'oggetto disattivando i malus "cage" e "sleep", e reimposta check a false. |
| `string` | `currentStatus`| `()` | Restituisce una stringa che rappresenta il malus attivo correntemente, oppure "idle" se nessuno è attivo. |

---

### Nome della Funzione: `start_movement`

```gdscript
func start_movement(destination: Vector2) -> void
```

#### **Descrizione**
Avvia il movimento dell'oggetto verso una destinazione specificata. La funzione gestisce l'animazione, aggiorna lo stato di movimento e invia segnali quando il movimento inizia.

#### **Parametri**
| Nome          | Tipo      | Descrizione                              |
|---------------|-----------|------------------------------------------|
| `destination` | `Vector2` | La posizione di destinazione dell'oggetto. |

#### **Valore di Ritorno**
Nessun valore di ritorno.

#### **Dettagli Aggiuntivi**
**Scopo**:
- Inizializza il movimento dell'oggetto verso la posizione specificata da `destination`.
- Aggiorna lo stato associato al ramo corrente e invia il segnale `bird_left_branch`.
- Avvia un'animazione specifica (`flying`) e abilita il loop di processo continuo.

**Best Practice**:
- Prima di avviare il movimento, verifica se l'oggetto è già in movimento (`is_moving`).
- Gestisci correttamente le risorse condivise come `branch_link` e `Global.branch_selected`.

#### **Esempio di Utilizzo**
```gdscript
# Esempio: Avvia il movimento verso una nuova posizione
var new_destination = Vector2(100, 200)
start_movement(new_destination)
```

---

### Nome della Funzione: `_process`

```gdscript
func _process(delta: float) -> void
```

#### **Descrizione**
Aggiorna lo stato e il movimento dell'oggetto a ogni frame. Gestisce l'animazione, l'interpolazione della posizione per un movimento fluido e il comportamento in base alla selezione globale.

#### **Parametri**
| Nome    | Tipo     | Descrizione                              |
|---------|----------|------------------------------------------|
| `delta` | `float`  | Il tempo trascorso dal frame precedente. |

#### **Valore di Ritorno**
Nessun valore di ritorno.

#### **Dettagli Aggiuntivi**
**Scopo**:
- Aggiorna la posizione dell'oggetto utilizzando l'interpolazione lineare (`lerp`) per creare un movimento fluido.
- Gestisce l'animazione in base allo stato attuale dell'oggetto e alla selezione globale (`Global.bird_selected`).
- Interrompe il loop di processo quando il movimento è completo.

**Best Practice**:
- Usa il controllo del tempo (`elapsed_time` e `move_duration`) per garantire che l'interpolazione sia corretta.
- Disabilita l'elaborazione continua (`set_process(false)`) quando il movimento è terminato, per ottimizzare le prestazioni.

#### **Esempio di Utilizzo**
```gdscript
# Il motore di gioco chiama automaticamente _process ogni frame
# Non è necessario invocare esplicitamente questa funzione.
```

---

### Nome della Funzione: `AddMalusArray`

```csharp
public void AddMalusArray();
```

#### **Descrizione**
La funzione AddMalusArray aggiunge l'istanza corrente di un uccello alla lista dei malus attivi (LevelStruct.malusbirds) se possiede uno dei modificatori. Inoltre, aggiorna lo stato della flag bombInGame (indica la presenza di una bomba).

#### **Parametri**
| Nome | Tipo   | Descrizione                |
|------|--------|----------------------------|
| ``  | ``  |  |


#### **Valore di Ritorno**
La funzione non restituisce alcun valore (void). La sua azione principale è quella di aggiungere l'oggetto corrente alla lista dei malus e aggiornare lo stato globale del gioco, se necessario.

---

### Nome della Funzione: `flyaway`

```csharp
void flyaway(BranchClick branch);
```

#### **Descrizione**
La funzione flyaway gestisce la rimozione degli uccelli da un ramo, facendoli volare via fuori dalla scena e vengono eliminati dalla scena. alla fine di flyaway vengono rimossi i malus a cui sono collegati con removemalus.

#### **Parametri**
| Nome | Tipo   | Descrizione                |
|------|--------|----------------------------|
| `branch` | `BranchClick` | Il nodo che rappresenta il ramo, contenente uno stack di uccelli da rimuovere.|

#### **Valore di Ritorno**
La funzione non restituisce alcun valore (void). Effettua modifiche sugli oggetti della scena, liberando risorse legate agli uccelli nel ramo.

---

### Nome della Funzione: `removemalus`

```csharp
void removemalus(bool bomb, bool key, bool clock);
```

#### **Descrizione**
La funzione removemalus gestisce la rimozione dei malus attivi presenti nel gioco. A seconda dei parametri passati (bomb, key, clock), vengono effettuate azioni specifiche per ciascun malus: reset del contatore per le bombe, sblocco di uccelli intrappolati (gabbie), o risveglio di uccelli addormentati (sonno). Inoltre, vengono aggiornate le animazioni degli uccelli e, se applicabile, rimossi gli uccelli malus dalla scena di gioco richiamando flyaway.

#### **Parametri**
| Nome | Tipo   | Descrizione                |
|------|--------|----------------------------|
| `bomb`  | `bool`  | argomento che viene passato da removemalus per capire se deve essere modificato il malus bomba |
| `key`  | `bool`  | argomento che viene passato da removemalus per capire se deve essere modificato il malus cage |
| `clock`  | `bool`  | argomento che viene passato da removemalus per capire se deve essere modificato il malus sleep |


#### **Valore di Ritorno**
La funzione non restituisce alcun valore (void). Le sue azioni includono:
Gestione e rimozione dei malus attivi, secondo i parametri ricevuti.
Aggiornamento delle animazioni degli uccelli malus.
Rimozione degli uccelli malus che soddisfano determinate condizioni, come la verifica di un ramo completato.

---

### Nome della Funzione: `setanimations`

```csharp
public void setanimations();
```

#### **Descrizione**
La funzione setanimations aggiorna l'animazione dell'uccello in base al tipo di malus attivo presente nel suo modificatore (Modificatore). Se non è presente alcun malus, l'animazione predefinita ("idle") viene riprodotta. Altrimenti, viene selezionata un'animazione specifica per ogni malus attivo. L'Uso di questa funzione è essenziale per gestire visivamente lo stato degli uccelli in base ai malus attivi. Permette di fornire un feedback visivo al giocatore.

#### **Parametri**
| Nome | Tipo   | Descrizione                |
|------|--------|----------------------------|
| ``  | ``  | |

#### **Valore di Ritorno**
La funzione non restituisce alcun valore (void). La sua azione principale è quella di impostare l'animazione appropriata per l'uccello utilizzando il nodo AnimatedSprite2D e l'oggetto Modificatore.

---

### Nome della Funzione: `updateDirection();`

```csharp
public void updateDirection();
```

#### **Descrizione**
La funzione updateDirection controlla, in base a dove si trova l'uccello, verso dove dovrebbe essere rivolto, sia in volo che fermo. 
Fatto ciò ne aggiorna lo sprite flippandone l'h, in modo tale da girarlo a destra o sinistra.

#### **Parametri**
| Nome | Tipo   | Descrizione                |
|------|--------|----------------------------|
| ``  | ``  | |

#### **Valore di Ritorno**
La funzione non restituisce alcun valore (void).

---



