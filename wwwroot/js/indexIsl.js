// Variabili che contengono le informazioni sulla commessa corrente e l'accordion corrente
let tbcpTstComm = "";
let tbcpPrfComm = "";
let tbcpAComm = "";
let tbcpNComm = "";
let comm = "";
let currentAccordionId = "";
let currentStato = "";
let currentIsl = "";

// Aggiunge un event listener su tutti i bottoni per aprire gli "accordion" interni (quando clicchi per aprire una isl e vedere i consuntivi)
Array.from(document.getElementsByClassName("open-btn")).forEach((x) => {
  x.parentElement.addEventListener("mousedown", async (e) => {
    if (e.buttons != 1) {
      return;
    }
    currentIsl = x.parentElement.getAttribute("rowIsl");
    const rowStato = x.parentElement.getAttribute("islStato");
    currentAccordionId = `accordion-${currentIsl}-${rowStato}`;
    currentStato = rowStato;
    const accordionBody = document.getElementById(currentAccordionId);
    accordionBody.classList.toggle("opens-opened");

    if (accordionBody.classList.contains("opens-opened")) {
      await showConsuntivi();
    } else {
      accordionBody.style.maxHeight = "0px";
    }
  });
});

// Event listener quando il form di aggiunta consuntivo viene inviato
document.getElementById("create-consuntivo-form").addEventListener("submit", async (e) => {
  e.preventDefault();
  await handleFormSubmit();
});

async function fetchGET(url) {
  return await fetchApi(url, "GET");
}

async function fetchPOST(url, obj) {
  return await fetchApi(url, "POST", obj);
}

async function fetchPUT(url, obj) {
  return await fetchApi(url, "PUT", obj);
}

async function fetchDELETE(url) {
  return await fetchApi(url, "DELETE");
}

// Funzione per le chiamate, se viene passato un oggetto oltre all'url allora non è una GET
async function fetchApi(url, method, obj) {
  const settings = {
    method: method,
    mode: "cors",
    headers: {
      "Content-Type": "application/json",
    },
  };

  if (method != "GET" && method != "DELETE") {
    if (!obj) {
      throw new Error("No object passed");
    }
    settings.body = JSON.stringify(obj);
  }

  const res = await fetch(`${window.location.origin}/${url}`, settings);
  console.log(res);
  try {
    if (res.status === 200) {
      const data = await res.json();
      console.log(data);
      return data;
    } else {
      console.log(res.status);
      throw new Error("Error fetching data: " + res.detail);
    }
  } catch (err) {
    console.log(err);
  }
}

// Al click sul pulsante per vedere i consuntivi viene scatenata questa funzione che fa aprire l'accordion cambiandogli l'altezza massima in base
// a quanti consuntivi arrivano dalla chiamata
async function showConsuntivi() {
  const accordionBody = document.getElementById(currentAccordionId);
  //accordionBody.style.maxHeight = "92.84px";
  const res = await fetchGET(`api/CrrgApi/GetAllByIsl/${currentIsl}`);
  console.log(currentAccordionId);
  console.log(currentStato);
  const height = generateRows(res);
  accordionBody.style.maxHeight = 92.84 + height + 1 + "px";
}

// Genera le righe con i consuntivi
function generateRows(res) {
  const body = document.getElementById(`tbody-${currentIsl}-${currentStato}`);
  console.log(`tbody-${currentIsl}-${currentStato}`);
  body.innerHTML = "";
  let height = 0;

  for (let i = 0; i < res.length; i++) {
    const row = res[i];

    const tr = document.createElement("tr");
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
    tdData.innerText = `${data[2]}-${data[1]}-${data[0]}`;
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
    tdDescription.style.width = "56%";
    tdDescription.innerText = row.crrgNote;
    tr.appendChild(tdDescription);

    const td = document.createElement("td");
    td.style.width = "4%";
    const divIcons = document.createElement("div");
    divIcons.className = "icons-isl-btn";

    const iconaUpdate = document.createElement("i");
    iconaUpdate.className = "bi bi-pencil-fill pointer-pointer me-1";

    const iconaDelete = document.createElement("i");
    iconaDelete.className = "bi bi-trash-fill pointer-pointer";
    iconaDelete.addEventListener("click", async () => {
      await deleteConsuntivo(row.crrgCSrl);
    });

    //divIcons.appendChild(iconaUpdate);
    divIcons.appendChild(iconaDelete);
    td.appendChild(divIcons);
    tr.appendChild(td);

    height += tr.getBoundingClientRect().height;
  }

  const tr = document.createElement("tr");
  const td = document.createElement("td");
  td.setAttribute("colspan", "7");
  const div = document.createElement("div");
  div.className = "white-icona-wrapper";
  const iconaAdd = document.createElement("i");
  iconaAdd.className = "bi bi-plus-circle-fill pointer-pointer";
  iconaAdd.setAttribute("data-bs-toggle", "modal");
  iconaAdd.setAttribute("data-bs-target", "#addConsuntivo");
  //iconaAdd.setAttribute("riga");
  div.appendChild(iconaAdd);
  td.appendChild(div);
  tr.appendChild(td);
  tr.classList.add("last-row");
  body.appendChild(tr);

  iconaAdd.addEventListener("click", async () => {
    cleanModalFields();
    await populateForm();
  });

  return height;
}

async function populateForm() {
  const islData = document.getElementById(`accordion-${currentIsl}-${currentStato}`);
  //reset campi
  document.getElementById("NTOper").innerHTML = "";
  document.getElementById("ora").value = "00:00:00";
  document.getElementById("data").value = new Date().toISOString().split("T")[0];

  tbcpTstComm = islData.getAttribute("TbcpTstComm");
  tbcpPrfComm = islData.getAttribute("TbcpPrfComm");
  tbcpAComm = islData.getAttribute("TbcpAComm");
  tbcpNComm = islData.getAttribute("TbcpNComm");

  comm = `${tbcpTstComm}/${tbcpPrfComm}/${tbcpAComm}/${tbcpNComm}`;
  const tipoOperazione = await fetchGET(`api/Olca/GetOlcaCitoByCommAsync/${comm}`);

  for (const oper of tipoOperazione.olcaCitoList) {
    const option = document.createElement("option");
    option.value = `${oper.flussoOlca.olcaNOper}-${oper.flussoOlca.olcaTOper}`;
    option.innerText = `${oper.flussoOlca.olcaNOper} - ${oper.flussoOlca.olcaTOper} - ${oper.flussoCito.citoDescrizione}`;
    document.getElementById("NTOper").appendChild(option);
  }

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
  document.getElementById("crrgNote").value = islData.getAttribute("islDesc");
  document.getElementById("crrgApp").value = islData.getAttribute("crrgApp");
  document.getElementById("crrgMod").value = islData.getAttribute("crrgMod");
  document.getElementById("crrgRifCliente").value = currentIsl;

  const tatvFlgOfferta = islData.getAttribute("TatvFlgOfferta");
  if (tbcpPrfComm == "B" || tatvFlgOfferta > 0) {
    document.getElementById("crrgCmaatt4").checked = true;
  } else {
    document.getElementById("crrgCmaatt1").checked = true;
  }
}

// Scatta quando viene inviato il form di aggiunta consuntivo, crea la request e la invia al controller, se ci sono errori li fa comparire nel campo che li ha causati
async function handleFormSubmit() {
  const request = getConsuntivoObj();

  const res = await fetchPOST("api/CrrgApi/Create", request);

  if (res.succeeded == "S") {
    document.getElementById("isl-error-container").innerText = "Operazione Eseguita";
    Array.from(document.getElementsByClassName("text-danger")).forEach((x) => (x.innerText = ""));
    document.getElementById("close-modal").click();
    await showConsuntivi();
    return;
  }
  cleanModalFields();

  for (const error of res.errors) {
    const key = Object.keys(error)[0];
    const id = `${key[0].toLowerCase() + key.substring(1)}-error`;
    document.getElementById(id).innerText = error[key];
  }
}

function getConsuntivoObj() {
  const form = document.getElementById("create-consuntivo-form");
  const data = new FormData(form);
  const value = Object.fromEntries(data.entries());
  const time = value.crrgTmRunIncrHMS.toString().split(":");

  value.isUpdate = false;
  value.crrgPosDoc = 0;
  value.crrgPrgDoc = 0;
  value.crrgTstDoc = tbcpTstComm;
  value.crrgPrfDoc = tbcpPrfComm;
  value.crrgADoc = tbcpAComm;
  value.crrgNDoc = tbcpNComm;
  value.commCode = comm;
  value.crrgTOper = "";
  value.crrgTmRunIncrHMS = new Date(Date.UTC(2023, 1, 1, time[0], time[1], time[2])).toISOString();
  value.crrgCSrl = 0;
  value.crrgNOper = 0;
  value.crrgCdl;
  value.crrgCRis;
  value.crrgCdl = "_" + value.crrgCRis;

  console.log(value);
  return value;
}

// Delete del consuntivo tramite CrrgCSrl
async function deleteConsuntivo(crsl) {
  console.log("Eliminazione consuntivo: " + crsl);
  const res = await fetchDELETE(`api/CrrgApi/Delete/${crsl}`);

  let height = 0;

  Array.from(document.getElementById(`tbody-${currentIsl}-${currentStato}`).children).forEach((x) => {
    if (x.getAttribute("CrrgCSrl") == crsl) {
      x.remove();
    } else {
      height += x.getBoundingClientRect().height;
    }
  });
  document.getElementById(currentAccordionId).style.maxHeight = 92.84 + height + 1 + "px";
}

//per pulire i campi di errore
function cleanModalFields() {
  document.getElementById("isl-error-container").innerText = "";
  Array.from(document.getElementsByClassName("text-danger")).forEach((x) => (x.innerText = ""));
}

function getDuration(duration) {
  return new Date(duration * 1000).toISOString().substring(11, 19);
}
