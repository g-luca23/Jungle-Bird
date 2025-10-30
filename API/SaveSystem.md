# Documentazione Classe `SaveSystem`

## **Descrizione**

Classe con metodi statici per gestire il salvataggio e il caricamento deel livello.

---

## **Metodi**

### Nome della Funzione: `Save`

```csharp
public static void Save()
```

#### **Descrizione**

La funzione Save() apre un file di salvataggio chiamato save_game.save in modalità scrittura e memorizza la difficoltà attuale del livello. Successivamente, salva il numero di rami presenti e il contatore iniziale delle bombe. Per ogni ramo, copia gli uccelli in un array e li salva in ordine inverso, serializzando i dati di ciascun uccello in formato JSON. I dati serializzati vengono memorizzati nel file linea per linea. Una volta completato il salvataggio, il file viene chiuso e viene stampato un messaggio di conferma.


#### **Parametri**
| Nome | Tipo   | Descrizione                |
|------|--------|----------------------------|
| ``  | ``  |    |

#### **Valore di Ritorno**
La funzione è void quindi non ritorna nulla, quello che salva lo salva in save_game.save.

---


### Nome della Funzione: `Load`

```csharp
public static void Load()
```

#### **Descrizione**

La funzione Load() verifica se il file di salvataggio save_game.save esiste e, in caso affermativo, lo apre in modalità lettura. Legge la difficoltà, il contatore dei rami e il contatore delle bombe. Se il contatore delle bombe è valido, configura il gestore delle bombe e imposta il contatore. Successivamente, legge i dati degli uccelli dal file, deserializzandoli da JSON e memorizzandoli in una lista. Una volta letti tutti i dati, carica il livello tramite CreateLevel.Load(), passando la difficoltà, il contatore dei rami e i dati degli uccelli. Se il file non viene trovato, viene stampato un messaggio di errore.


#### **Parametri**
| Nome | Tipo   | Descrizione                |
|------|--------|----------------------------|
| ``  | ``  |    |

#### **Valore di Ritorno**
La funzione è void quindi non ritorna nulla.
