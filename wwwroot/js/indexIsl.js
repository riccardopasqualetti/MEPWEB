const logs = true; //per abilitare i logs

// Variabili che contengono le informazioni sulla commessa corrente e l'accordion corrente
let tbcpTstComm = "";
let tbcpPrfComm = "";
let tbcpAComm = "";
let tbcpNComm = "";
let comm = "";
let currentAccordionId = "";
let currentStato = "";
let currentIsl = "";
let currentConsuntivi = {};
const modal = new bootstrap.Modal("#addConsuntivo");
const altezzaAccordionVuoto = 93.84; //altezza dell'accordio con dentro solo l'intestazione della tabella

// Aggiunge un event listener su tutti i bottoni per aprire gli "accordion" dei consuntivi
Array.from(document.getElementsByClassName("open-btn")).forEach((x) => {
  x.parentElement.addEventListener("click", async (e) => {
    //salvo la isl selezionata per usarla in seguito
    currentIsl = x.parentElement.getAttribute("rowIsl");

    //prendo lo stato della isl selezionata (anfu, svil, deli)
    const rowStato = x.parentElement.getAttribute("islStato");

    //l'id dell'accordion è composto da "accordion-" numeroisl- statoisl
    currentAccordionId = `accordion-${currentIsl}-${rowStato}`;

    //salvo anche lo stato per usarlo in seguito
    currentStato = rowStato;

    const accordionBody = document.getElementById(currentAccordionId);
    accordionBody.classList.toggle("opens-opened");

    //se l'accordion è aperto allora mostra i consuntivi, altrimenti nascondi
    if (accordionBody.classList.contains("opens-opened")) {
      await showConsuntivi();
    } else {
      //metto l'overflow a hidden per non visualizzare la barra laterale quando l'accordio si chiude
      accordionBody.style.overflowY = "hidden";
      accordionBody.style.maxHeight = "0px";
    }
  });
});

// Event listener per il bottone di pulizia del campo note
document.getElementById("clear-text-btn").addEventListener("click", () => {
  document.getElementById("crrgNote").value = "";
  document.getElementById("crrgNote").focus();
});

// Event listener quando il form di aggiunta consuntivo viene inviato
document.getElementById("create-consuntivo-form").addEventListener("submit", async (e) => {
  e.preventDefault();
  await handleFormSubmit();
});

// Funzione per le chiamate api, prende in ingresso l'url, il metodo e l'oggetto da passare nel body (non in uso)
const http = {
  get: async (url) => {
    return await http.fetchApi(url, "GET");
  },
  post: async (url, obj) => {
    return await http.fetchApi(url, "POST", obj);
  },
  put: async (url, obj) => {
    return await http.fetchApi(url, "PUT", obj);
  },
  delete: async (url) => {
    return await http.fetchApi(url, "DELETE");
  },

  fetchApi: async (url, method, obj) => {
    const settings = {
      method: method,
      mode: "cors",
      headers: {
        "Content-Type": "application/json",
      },
    };

    console.log(method);
    if (method != "GET" && method != "DELETE") {
      if (!obj) {
        throw new Error("No object passed");
      }
      settings.body = JSON.stringify(obj);
    }

    const res = await fetch(`${window.location.origin}/${url}`, settings);
    Log(res);
    try {
      if (res.status === 200) {
        const data = await res.json();
        Log(data);
        return data;
      } else {
        Log(res.status);
        throw new Error("Error fetching data: " + res.detail);
      }
    } catch (err) {
      Log(err);
    }
  },
};

// Al click sul pulsante per vedere i consuntivi viene scatenata questa funzione che fa aprire l'accordion cambiandogli l'altezza massima in base
// a quanti consuntivi arrivano dalla chiamata
async function showConsuntivi() {
  const accordionBody = document.getElementById(currentAccordionId);
  accordionBody.style.maxHeight = "92.84px";
  currentConsuntivi = (await axios.get(`CrrgApi/GetAllByIsl/${currentIsl}`)).data;
  Log(currentConsuntivi);
  Log(currentAccordionId);
  Log(currentStato);
  const height = generateRows();
  accordionBody.style.maxHeight = 92.84 + height + 1 + "px";
  setTimeout(() => {
    accordionBody.style.overflowY = "auto";
  }, 350);
}

// Genera le righe con i consuntivi
function generateRows() {
  const body = document.getElementById(`tbody-${currentIsl}-${currentStato}`);
  Log(`tbody-${currentIsl}-${currentStato}`);
  body.innerHTML = "";
  let height = 0;
  let lastRow;

  for (let i = 0; i < currentConsuntivi.length; i++) {
    const row = currentConsuntivi[i];

    const tr = document.createElement("tr");
    lastRow = tr;
    tr.classList.add("riga");
    tr.setAttribute("CrrgCSrl", row.crrgCSrl);
    body.appendChild(tr);
    tr.addEventListener("mouseover", () => {
      Array.from(document.getElementsByClassName("riga-hover")).forEach((td) => {
        td.classList.remove("riga-hover");
      });
      Array.from(tr.children).forEach((td) => {
        td.classList.add("riga-hover");
      });
    });
    tr.addEventListener("mouseout", () => {
      Array.from(tr.children).forEach((td) => {
        td.classList.remove("riga-hover");
      });
    });

    const tdRis = document.createElement("td");
    tdRis.style.width = "3%";
    tdRis.innerText = row.crrgCRis;
    tr.appendChild(tdRis);

    const tdData = document.createElement("td");
    tdData.style.width = "8%";
    const data = row.crrgDtt.split("T")[0].split("-");
    tdData.innerText = `${data[2]}/${data[1]}/${data[0]}`;
    tr.appendChild(tdData);

    const tdEffort = document.createElement("td");
    tdEffort.style.width = "6%";
    tdEffort.innerText = getDuration(row.crrgTmRunIncr);
    tr.appendChild(tdEffort);

    const tdIsl = document.createElement("td");
    tdIsl.style.width = "10%";
    tdIsl.innerText = row.crrgRifCliente;
    tr.appendChild(tdIsl);

    const tdCommessa = document.createElement("td");
    tdCommessa.style.width = "13%";
    tdCommessa.innerText = `${row.crrgTstDoc}/${row.crrgPrfDoc}/${row.crrgADoc}/${row.crrgNDoc}`;
    tr.appendChild(tdCommessa);

    const tdDescription = document.createElement("td");
    tdDescription.style.width = "53%";
    tdDescription.innerText = row.crrgNote;
    tr.appendChild(tdDescription);

    const td = document.createElement("td");
    td.classList.add("position-relative");
    td.style.width = "7%";
    const divIcons = document.createElement("div");
    divIcons.className = "icons-isl-btn d-flex";

    const iconaUpdate = document.createElement("i");
    iconaUpdate.className = "bi bi-pencil-fill pointer-pointer border-secondary border-end px-1";
    /* iconaUpdate.setAttribute("data-bs-toggle", "modal");
    iconaUpdate.setAttribute("data-bs-target", "#addConsuntivo"); */
    iconaUpdate.addEventListener("click", async () => {
      await populateForm("Modifica Consuntivo");
      await populateByConsuntivo(row.crrgCSrl, "update");
      document.getElementById("crrgCSrl").value = row.crrgCSrl;
      modal.show();
    });

    const iconaDelete = document.createElement("i");
    iconaDelete.className = "bi bi-trash-fill pointer-pointer px-1";
    iconaDelete.addEventListener("click", async () => {
      await deleteConsuntivo(row.crrgCSrl);
    });

    const iconaDuplicate = document.createElement("i");
    iconaDuplicate.className = "bi bi-trashbi bi-plus-circle pointer-pointer px-1 border-secondary border-end";
    iconaDuplicate.addEventListener("click", async () => {
      await populateForm("Duplica Consuntivo");
      await populateByConsuntivo(row.crrgCSrl);
      modal.show();
    });

    divIcons.appendChild(iconaDuplicate);
    divIcons.appendChild(iconaUpdate);
    divIcons.appendChild(iconaDelete);
    td.appendChild(divIcons);
    tr.appendChild(td);

    height += tr.getBoundingClientRect().height;
  }

  if (lastRow) {
    lastRow.classList.add("latest-row");
  }

  const tr = document.createElement("tr");
  const td = document.createElement("td");
  td.setAttribute("colspan", "7");
  const div = document.createElement("div");
  div.className = "white-icona-wrapper";
  const iconaAdd = document.createElement("i");
  iconaAdd.className = "bi bi-plus-circle-fill pointer-pointer";
  div.appendChild(iconaAdd);
  td.appendChild(div);
  tr.appendChild(td);
  body.appendChild(tr);

  iconaAdd.addEventListener("click", async () => {
    cleanModalErrors();
    await populateForm("Aggiungi Consuntivo");
    modal.show();
  });

  return height;
}

//Popola il form con i dati standard per poter inserire un consuntivo
async function populateForm(modalTitle) {
  document.getElementById("addConsuntivoModalLabel").innerHTML = modalTitle;
  const islData = document.getElementById(`accordion-${currentIsl}-${currentStato}`);

  //reset campi
  document.getElementById("NTOper").innerHTML = "";
  document.getElementById("ora").value = "00:00:00";
  document.getElementById("data").value = new Date().toISOString().split("T")[0];

  //valorizzazione campi commessa e composizione commessa completa
  tbcpTstComm = islData.getAttribute("TbcpTstComm");
  tbcpPrfComm = islData.getAttribute("TbcpPrfComm");
  tbcpAComm = islData.getAttribute("TbcpAComm");
  tbcpNComm = islData.getAttribute("TbcpNComm");
  comm = `${tbcpTstComm}/${tbcpPrfComm}/${tbcpAComm}/${tbcpNComm}`;

  //chiamata per ricevere i tipi di operazione validi per la commessa e popola la select
  const tipoOperazione = (await axios.get(`Olca/GetOlcaCitoByCommAsync/${comm}`)).data;
  for (const oper of tipoOperazione.olcaCitoList) {
    const option = document.createElement("option");
    option.value = `${oper.flussoOlca.olcaNOper}-${oper.flussoOlca.olcaTOper}`;
    option.innerText = `${oper.flussoOlca.olcaNOper} - ${oper.flussoOlca.olcaTOper} - ${oper.flussoCito.citoDescrizione}`;
    document.getElementById("NTOper").appendChild(option);
  }

  //prendo il dato flag dall'ISL attuale e lo "decodifico" per poi settarlo come valore nel form
  const flagCausale = islData.getAttribute("flag");
  const causali = {
    "1-ANFU": "ANFU",
    "2-SVIL": "SVIL",
    "3-DELI": "DELI",
    "7-NEW": "ANFU",
    "8-SOSP": "ANFU",
    "9-CLOSE": "DELI",
  };
  document.getElementById("crrgCCaus").value = causali[flagCausale];

  //valorizzazione del resto dei campi con i dati dell'ISL attuale
  document.getElementById("crrgNote").value = islData.getAttribute("islDesc");
  document.getElementById("crrgApp").value = islData.getAttribute("crrgApp");
  document.getElementById("crrgMod").value = islData.getAttribute("crrgMod");
  document.getElementById("crrgRifCliente").value = currentIsl;
  document.getElementById("crrgCSrl").value = 0;

  //valorizzazione del campo "attività" in base al tipo di commessa o al flag offerta
  const tatvFlgOfferta = islData.getAttribute("TatvFlgOfferta");
  if (tbcpPrfComm == "B" || tatvFlgOfferta > 0) {
    document.getElementById("crrgCmaatt4").checked = true; //Verbale
  } else {
    document.getElementById("crrgCmaatt1").checked = true; //n.d.
  }
}

//Popola il form con i dati del consuntivo selezionato (per update o duplicate)
async function populateByConsuntivo(srl, mode) {
  //pulisce i campi di errore
  cleanModalErrors();

  //prendo il consuntivo corrente dalla response che contiene i consuntivi della commessa
  const consuntivo = currentConsuntivi.find((x) => x.crrgCSrl == srl);

  //valorizzazione dei campi con i dati del consuntivo corrente
  document.getElementById("ora").value = getDuration(consuntivo.crrgTmRunIncr);
  if (mode == "update") {
    //se siamo in update valorizzo anche la data
    document.getElementById("data").value = consuntivo.crrgDtt.split("T")[0];
  }

  for (const opt of document.getElementById("NTOper").options) {
    opt.selected = opt.value.split("-")[1] == consuntivo.crrgTOper;
  }
  document.getElementById(`crrgCmaatt${+consuntivo.crrgCmaatt + 1}`).checked = true;
  document.getElementById("crrgCCaus").value = consuntivo.crrgCCaus;
  document.getElementById("crrgNote").value = consuntivo.crrgNote;
}

// Scatta quando viene inviato il form di aggiunta consuntivo, crea la request e la invia al controller, se ci sono errori li fa comparire nel campo che li ha causati
async function handleFormSubmit() {
  const request = getConsuntivoObj();

  const res = (await axios.post("CrrgApi/Create", request)).data;

  if (res.succeeded == "S") {
    document.getElementById("isl-error-container").innerText = "Operazione Eseguita";
    Array.from(document.getElementsByClassName("text-danger")).forEach((x) => (x.innerText = ""));
    document.getElementById("close-modal").click();
    await showConsuntivi();
    return;
  }

  cleanModalErrors();

  for (const error of res.errors) {
    const key = Object.keys(error)[0];
    const id = `${key[0].toLowerCase() + key.substring(1)}-error`;
    document.getElementById(id).innerText = error[key];
  }
}

//Prende i dati del form della modale e setta quelli di default, ritorna un oggetto con i dati del consuntivo
function getConsuntivoObj() {
  const form = document.getElementById("create-consuntivo-form");
  const data = new FormData(form);
  const value = Object.fromEntries(data.entries());
  const time = value.crrgTmRunIncrHMS.toString().split(":");
  const srl = document.getElementById("crrgCSrl").value;

  //se il CSrl è diverso da 0 significa che siamo in update
  value.isUpdate = srl != "0";
  value.crrgPosDoc = 0;
  value.crrgPrgDoc = 0;
  value.crrgTstDoc = tbcpTstComm;
  value.crrgPrfDoc = tbcpPrfComm;
  value.crrgADoc = tbcpAComm;
  value.crrgNDoc = tbcpNComm;
  value.commCode = comm;
  value.crrgTOper = "";
  value.crrgTmRunIncrHMS = new Date(Date.UTC(2023, 1, 1, time[0], time[1], time[2])).toISOString();
  value.crrgNOper = 0;
  value.crrgCdl;
  value.crrgCRis;
  value.crrgCdl = "_" + value.crrgCRis;

  Log(value);
  return value;
}

// Delete del consuntivo tramite CrrgCSrl
async function deleteConsuntivo(crsl) {
  Log("Eliminazione consuntivo: " + crsl);
  await axios.delete(`CrrgApi/Delete/${crsl}`);

  //dopo la chiamata per l'eliminazione ricalcola l'altezza che deve avere l'accordion dei consuntivi
  let height = 0;

  for (const child of document.getElementById(`tbody-${currentIsl}-${currentStato}`).children) {
    if (child.getAttribute("CrrgCSrl") == crsl) {
      child.remove();
    } else {
      height += child.getBoundingClientRect().height;
    }
  }
  document.getElementById(currentAccordionId).style.maxHeight = altezzaAccordionVuoto + height + "px";
}

//per pulire i campi di errore
function cleanModalErrors() {
  document.getElementById("isl-error-container").innerText = "";
  Array.from(document.getElementsByClassName("text-danger")).forEach((x) => (x.innerText = ""));
}

//Da decimali a formato 00:00:00
function getDuration(duration) {
  return new Date(duration * 1000).toISOString().substring(11, 19);
}

//Logga se la variabile logs = true
function Log(val) {
  if (logs) {
    console.log(val);
  }
}
