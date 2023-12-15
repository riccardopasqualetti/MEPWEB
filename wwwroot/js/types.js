/**
 * @namespace types
 */

/**
* @typedef {Object} ParamObject - Oggetto dei parametri da passare alla classe TableGenerator per poter costruire una tabella con varie funzionalità.
* @property {string?} authToken - Token di sicurezza che viene utilizzato per la comunicazione con l'API. Questo parametro è alternativo alle credenziali.
* @property {types.CredentialsObject?} credentials - Credenziali dell'utente per l'accesso all'API. Questo parametro è necessario in caso di assenza dell'authToken.
* @property {types.ApiUrlObject} apiUrl - Contiene le api per le chiamate che verranno eseguite dal generatore.
* @property {number[]} paginationOptions - Lista delle opzioni di paginazioni disponibili per l'utente (limiti di righe per pagina).
* @property {types.ModalParameters} modals - Opzioni per la generazione delle modali di default.
* @property {types.FieldParameters[]} fields - Array con la lista dei campi della tabella e le informazioni per visualizzarli.
* @memberof types
*/

/**
 * @typedef {Object} CredentialsObject - Contiene le credenziali di accesso all'API.
 * @property {string} username - Username dell'utente per l'accesso all'API.
 * @property {string} password - Password dell'utente per l'accesso all'API.
 * @property {string} company - Company per l'accesso all'API.
 * @memberof types
 */

/**
 * @typedef {Object} ApiUrlObject - Contiene gli URL per l'accesso all'API.
 * @property {types.ApiParams} get - URL ed eventualmente parametri opzionali per effettuare la GET all'API.
 * @property {types.ApiParams} post - URL per effettuare la chiamata POST all'API.
 * @property {types.ApiParams} put - URL e il nome della colonna con la chiave del record per effettuare la PUT all'API.
 * @property {types.ApiParams} delete - URL e il nome della colonna con la chiave del record per effettuare la DELETE all'API.
 * @memberof types
 */

/**
 * @typedef {Object} ApiParams - Parametri per effettuare una richiesta all'API.
 * @property {string[]} url - Array con alla posizione 0 l'URL di base per comunicare con l'API e alla posizione 1 l'endpoint per la richiesta.
 * @property {string?} param - Nome della colonna con la chiave del record per effettuare la richiesta (PUT o DELETE).
 * @property {string[]?} additionalParameters - Parametri addizionali per la richiesta (es. livello per flusso_cily).
 * @memberof types
 */

/**
 * @typedef {Object} ModalParameters - Contiene le impostazioni per la creazione delle modali.
 * @property {types.ModalSettings} create - Impostazioni per la creazione della modale di inserimento di un record (CREATE).
 * @memberof types
 */

/**
 * @typedef {Object} ModalSettings - Impostazioni specifiche per la creazione di una modale.
 * @property {number} col - Numero di colonne che dovrà avere la modale.
 * @property {number} maxFields - Numero di campi massimo che una colonna potrà avere.
 * @memberof types
 */

/**
 * @typedef {Object} FieldParameters - Parametri di un campo.
 * @property {string} apiName - Nome visualizzato nella response della GET dell'API.
 * @property {string} displayedName - Password dell'utente per l'accesso all'API.
 * @property {string?} type - Company per l'accesso all'API.
 * @property {string?} values - Company per l'accesso all'API.
 * @property {boolean?} values - Company per l'accesso all'API.
 * @property {types.searchUrls[]?} values - Company per l'accesso all'API.
 * @memberof types
 */

/**
 * @typedef {Object} SearchUrls - Opzioni per la chiamata della finestra di ricerca.
 * @property {string} url - URL per la chiamata get della finestra di ricerca.
 * @property {string[]} searchFieldNames - Array contenente i campi rilevanti da visualizzare nella finestra di ricerca (codice, descrizione).
 * @property {function?} ref - Funzione aggiuntiva per il calcolo dei parametri per la finestra di ricerca.
 * @memberof types
 */

/**
 * @typedef {Object} AddParamObject - Contiene n campi aggiuntivi con chiave = nome del campo.
 * @memberof types
 */

/**
 * @namespace changePage
 */

/**
 * @namespace createModals
 */

/**
 * @namespace bodyGenerator
 */

/**
 * @namespace createTableMenu
 */

/**
 * @namespace addMenuListenersToRows
 */

/**
 * @namespace getRGBFromNumber
 */

/**
 * @namespace fetchApi
 */

/**
 * @namespace fetchGetApi
 */

/**
 * @namespace createTable
 */

/**
 * @namespace destroyTable
 */

/**
 * @namespace modalHandler
 */

export {}