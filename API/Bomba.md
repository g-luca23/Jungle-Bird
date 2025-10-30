# Documentazione Classe `Bomba`

## **Descrizione**

Classe per la gestione della rappresentazione del nodo bomba.

---

## **Proprietà**

| Nome               | Tipo     | Descrizione                                 |
| ------------------ | -------- | ------------------------------------------- |
| `esplosione`| `AnimatedSprite2D` | rappresenta l'animazione della bomba. |
| `counter`  | `int` | rappresenta il contatore della bomba. |
---

## **Metodi**
| valore di ritorno | Nome               | Argomenti  | Descrizione                                 |
| ------------------ | ------------------ | ---------- | ------------------------------------------- |
|  `void`  | `Ready`  | `()` | Funzione che viene eseguita nel momento in cui viene creato il nodo. |
|  `void`  | `UpdateCounter`  | `()` | Decrementa counter ad ogni chiamata.|

---

### Nome della Funzione: `UpdateCounter();`

```csharp
int UpdateCounter();
```

#### **Descrizione**
Questa funzione diminuisce il contatore della bomba ogni volta che viene chiamata, e aggiorna lo sprite del nodo bomba.

#### **Parametri**
| Nome | Tipo   | Descrizione                |
|------|--------|----------------------------|
| ``  | ``  |     |

---

# Documentazione classe `BombHandler`:


## **Descrizione**

Classe per l'inizializzazione e la modifica del nodo bomba.

---

## **Proprietà**

| Nome               | Tipo     | Descrizione                                 |
| ------------------ | -------- | ------------------------------------------- |
| `bomb`| `Bomba` | rappresenta il nodo bomba già esistente. |
| `InitialCounter`  | `int` | rappresenta il contatore della iniziale bomba. |
---

## **Metodi**
| valore di ritorno | Nome               | Argomenti  | Descrizione                                 |
| ------------------ | ------------------ | ---------- | ------------------------------------------- |
|  `void`  | `setup`  | `(Bomba b)` | Assegna il nodo bomba già esistente per l'eventuale modifica. |
|  `void`  | `setup`  | `()` | Istanzia un nuovo nodo bomba.|
|  `void`  | `UpdateCounter`  | `()` | Decrementa counter ad ogni chiamata.|
|  `int`  | `GetCounter`  | `()` | Ritorna il contatore della bomba.|
|  `int`  | `GetInitialCounter`  | `()` | Ritorna il contatore iniziale della bomba.|
|  `void`  | `SetCounter`  | `(int c)` | Setta il contatore della bomba.|




### Nome della Funzione: `setup();`

```csharp
static void(Bomba b);
```

#### **Descrizione**
Assegna il nodo bomba alla classe per poterlo settare.

#### **Parametri**
| Nome | Tipo   | Descrizione                |
|------|--------|----------------------------|
| b  | Bomba    | Nodo bomba                      |


#### **Valore di Ritorno**
La funzione non ritorna nulla.


---
### Nome della Funzione: `GetCounter();`

```csharp
static int GetCounter();
```

#### **Descrizione**
Questa funzione ritorna il contatore.

#### **Parametri**
| Nome | Tipo   | Descrizione                |
|------|--------|----------------------------|
| ``  | ``  |     |

#### **Valore di Ritorno**
Un numero intero che rappresenta il contatore della bomba.


---
### Nome della Funzione: `GetInitialCounter();`

```csharp
static int GetInitialCounter();
```

#### **Descrizione**
Questa funzione ritorna il contatore di partenza.

#### **Parametri**
| Nome | Tipo   | Descrizione                |
|------|--------|----------------------------|
| ``  | ``  |     |

#### **Valore di Ritorno**
Un numero intero che rappresenta il contatore iniziale della bomba.

---
### Nome della Funzione: `SetCounter(int c);`

```csharp
static void SetCounter(int c);
```

#### **Descrizione**
Questa funzione prende in input un intero e lo assegna al contatore.

#### **Parametri**
| Nome | Tipo   | Descrizione                |
|------|--------|----------------------------|
| `c`  | `int`  | valore del contatore da assegnare  |

#### **Valore di Ritorno**
La funzione non ritorna nulla.


---

## Best Practices
- **Coerenza:** Segui sempre questa struttura per documentare le tue API.
- **Chiarezza:** Usa descrizioni precise e non ambigue.
- **Tipi di Dati:** Specifica sempre i tipi di dati per parametri e valori di ritorno.
- **Esempi:** Aggiungi esempi di utilizzo se la funzione è complessa o ha casi particolari.


