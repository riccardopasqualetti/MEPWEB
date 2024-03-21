let selected = "";
let idDoc = "";
let isSearching = false;
const inputNCommessa = document.getElementById("numeroCommessa");
const inputCliente = document.getElementById("cliente");
const tableRows = document.getElementsByClassName("riga");
const response = JSON.parse(document.getElementById("jsonI").value).map((ogg) => {
  return { ...ogg, commessa: `${ogg.TBCP_TST_COMM}/${ogg.TBCP_PRF_COMM}/${ogg.TBCP_A_COMM}/${ogg.TBCP_N_COMM}` };
});
console.log(response);
const preoccupazione = Array.from(document.querySelectorAll("#aggiungi-nota .preoccupazione"));
const rows = document.querySelectorAll("#aggiungi-nota tbody tr");
const inputAvanzamento = document.querySelector("#modal-avanzamento input");

inputAvanzamento.addEventListener("input", (e) => {
  if (e.target.value > 100) e.target.value = 100;
  if (e.target.value < 0) e.target.value = 0;
});

for (const row of rows) {
  row.addEventListener("mouseover", (e) => {
    for (const child of row.children) {
      child.classList.add("row-selected");
    }
  });
  row.addEventListener("mouseout", (e) => {
    for (const child of row.children) {
      child.classList.remove("row-selected");
    }
  });
}

let isPreoccupazioneOpen = false;
const iSpan = document.querySelector("#modal-preoccupazione > div span:nth-of-type(2)");
iSpan.addEventListener("click", () => {
  if (isPreoccupazioneOpen) {
    closePreoccupazione();
  } else {
    isPreoccupazioneOpen = true;
    document.querySelector("#modal-preoccupazione > div span:nth-of-type(2) i").style.transform = "rotateY(180deg)";
    document.querySelector("#modal-preoccupazione > div").style.maxWidth = "131px";
  }
});

Array.from(document.getElementById("cons-tbody").children).forEach((row, i) => {
  row.addEventListener("click", () => {
    idDoc = row.getAttribute("idDoc");
    if (selected != "" && row.id != selected) {
      const childrens = Array.from(document.getElementById(selected).children);

      for (let i = 1; i < childrens.length; i++) {
        childrens[i].classList.remove("row-selected");
      }
    }
    selected = row.id;
    const childrens = Array.from(row.children);
    for (let i = 1; i < childrens.length; i++) {
      childrens[i].classList.add("row-selected");
    }
  });
});

preoccupazione.forEach((x) => {
  x.addEventListener("click", (e) => {
    if (!e.target.classList.contains("pointer-pointer")) {
      return;
    }
    e.target.classList.remove("pointer-pointer");

    const settato = Array.from(preoccupazione).find((x) => !x.classList.contains("pointer-pointer"));
    settato.classList.add("pointer-pointer");
    const classiCliccato = e.target.className;
    e.target.className = settato.className;
    settato.className = classiCliccato;
  });
});

document.getElementById("clear-text-btn").addEventListener("click", () => {
  const nota = document.getElementById("nota");
  nota.value = "";
  nota.focus();
});

document.getElementById("aggiungi-nota").addEventListener("submit", async (e) => {
  e.preventDefault();
  const data = new FormData(e.target);
  const value = Object.fromEntries(data.entries());
  console.log(value);
  if (preoccupazione) {
    const preoccupazioneSelezionata = preoccupazione.find((x) => !x.classList.contains("pointer-pointer"));
    preoccupazioneSelezionata.classList.contains("bg-verde") && (await updateCommessa(value.commessa_attuale, { preoccupazione: "G", Note: value.nota, Avanzamento: value.avanzamento }));
    preoccupazioneSelezionata.classList.contains("bg-giallo") && (await updateCommessa(value.commessa_attuale, { preoccupazione: "Y", Note: value.nota, Avanzamento: value.avanzamento }));
    preoccupazioneSelezionata.classList.contains("bg-rosso") && (await updateCommessa(value.commessa_attuale, { preoccupazione: "R", Note: value.nota, Avanzamento: value.avanzamento }));
  }
  console.log(response);
});

document.getElementById("filter-btn").addEventListener("click", () => {
  document.getElementById("filters-container").classList.toggle("show-filters");
});

document.getElementById("search-form").addEventListener("submit", (e) => {
  e.preventDefault();
  search("TBCP_COMM", "TBCP_C_CLI", inputNCommessa.value, inputCliente.value);
});

document.getElementById("reset-research").addEventListener("click", () => {
  resetRows();
});

function closePreoccupazione() {
  isPreoccupazioneOpen = false;
  document.querySelector("#modal-preoccupazione > div span:nth-of-type(2) i").style.transform = "rotateY(360deg)";
  document.querySelector("#modal-preoccupazione > div").style.maxWidth = "70px";
}

async function updateCommessa(comm, valueObj) {
  console.log(valueObj);
  let req = {
    tstComm: null,
    prfComm: null,
    aComm: null,
    nComm: null,
  };

  response.map((x) => {
    if (x.commessa === comm) {
      for (const key in valueObj) {
        console.log(x[key]);
        x[key] = valueObj[key];
      }

      req.tstComm = x.TBCP_TST_COMM;
      req.prfComm = x.TBCP_PRF_COMM;
      req.aComm = x.TBCP_A_COMM;
      req.nComm = x.TBCP_N_COMM;
      valueObj.Note && (req.nota = valueObj.Note);
      valueObj.preoccupazione && (req.Preoccupazione = valueObj.preoccupazione);
      valueObj.Avanzamento && (req.Avanzamento = valueObj.Avanzamento);
    }
    return x;
  });

  const res = await axios.post("/api/Tbcp/UpdateTbcpOpzionali", req);
  if (res.status != 200) {
    if (res.status == 400) {
      return;
    } else {
      alert("Richiesta non andata a buon fine");
      return;
    }
  }

  if (valueObj.Avanzamento) document.querySelector(`#cons-tbody [rowComm="${comm}"] [campo="Avanzamento"]`).innerText = valueObj.Avanzamento + "%";
  console.log(valueObj.preoccupazione);
  switch (valueObj.preoccupazione) {
    case "G":
      const campoPre = document.querySelector(`#cons-tbody [rowComm="${comm}"] [campo="Preoccupazione"] div`);
      console.log(campoPre);
      campoPre.classList.remove("bg-giallo", "bg-rosso");
      campoPre.classList.add("bg-verde");
      break;
    case "Y":
      const campoPre1 = document.querySelector(`#cons-tbody [rowComm="${comm}"] [campo="Preoccupazione"] div`);
      console.log(campoPre1);
      campoPre1.classList.remove("bg-verde", "bg-rosso");
      campoPre1.classList.add("bg-giallo");
      break;
    case "R":
      const campoPre2 = document.querySelector(`#cons-tbody [rowComm="${comm}"] [campo="Preoccupazione"] div`);
      console.log(campoPre2);
      campoPre2.classList.remove("bg-verde", "bg-giallo");
      campoPre2.classList.add("bg-rosso");
      break;
  }
}

function populateNotaForm({ comm }) {
  const form = document.getElementById("aggiungi-nota");

  closePreoccupazione();

  const select = form.querySelector("#commessa-attuale");
  select.addEventListener("change", (e) => {
    populateNotaForm({ comm: e.target.value });
  });
  const currentRow = response.find((r) => r.commessa === comm);
  if (currentRow.Note) {
    form.querySelector("#nota").value = currentRow.Note;
  } else {
    form.querySelector("#nota").value = "";
  }

  if (currentRow.Preoccupazione === "Y") {
    preoccupazione[0].className = "preoccupazione bg-giallo";
    preoccupazione[1].className = "preoccupazione bg-verde pointer-pointer";
    preoccupazione[2].className = "preoccupazione bg-rosso pointer-pointer";
  } else if (currentRow.Preoccupazione === "R") {
    preoccupazione[0].className = "preoccupazione bg-rosso";
    preoccupazione[1].className = "preoccupazione bg-verde pointer-pointer";
    preoccupazione[2].className = "preoccupazione bg-giallo pointer-pointer";
  } else {
    preoccupazione[0].className = "preoccupazione bg-verde";
    preoccupazione[1].className = "preoccupazione bg-giallo pointer-pointer";
    preoccupazione[2].className = "preoccupazione bg-rosso pointer-pointer";
  }

  const i = iSpan.querySelector("i");
  iSpan.addEventListener("mouseover", () => {
    i.style.color = "var(--whiter-text-color)";
  });
  iSpan.addEventListener("mouseout", () => {
    i.style.color = "";
  });

  const sameCustomerRows = response.filter((r) => r.TBCP_C_CLI === currentRow.TBCP_C_CLI);
  select.innerHTML = "";
  for (const row of sameCustomerRows) {
    const option = document.createElement("option");
    option.value = row.commessa;
    option.textContent = row.commessa + " " + row.TBCP_DESC;
    option.title = row.TBCP_DESC;
    select.appendChild(option);
  }

  select.value = comm;
  select.title = currentRow.TBCP_DESC;
  form.querySelector("#cliente-commessa").innerText = `Cliente: ${currentRow.ACLI_RAG_SOC_1} ${currentRow.TBCP_OFF_PREV ? "- " + currentRow.TBCP_OFF_PREV : ""}`;
  form.querySelector("#descrizione-commessa").innerText = currentRow.TBCP_DESC;

  //GEN
  document.getElementById("modal-gen-Acquistate").innerText = fo(currentRow.HHACQGEN);
  document.getElementById("modal-gen-Consuntivate").innerText = fo(currentRow.HHCRRGSOAEFF + currentRow.HHCRRGSYDEFF);
  document.getElementById("modal-gen-Differenza").innerText = fo(currentRow.HHACQSOA + currentRow.HHACQSYD + currentRow.HHACQGEN - (currentRow.HHCRRGSOAEFF + currentRow.HHCRRGSYDEFF));
  document.getElementById("modal-gen-HH001A").innerText = fo(currentRow.HH001ASOA + currentRow.HH001ASYD);
  document.getElementById("modal-gen-NV").innerText = fo(currentRow.HHCRRGSOAEFFNV + currentRow.HHCRRGSYDEFFNV);
  //SOA
  document.getElementById("modal-soa-Acquistate").innerText = fo(currentRow.HHACQSOA);
  document.getElementById("modal-soa-Consuntivate").innerText = fo(currentRow.HHCRRGSOAEFF);
  document.getElementById("modal-soa-Differenza").innerText = fo(currentRow.HHACQSOA - currentRow.HHCRRGSOAEFF);
  document.getElementById("modal-soa-HH001A").innerText = fo(currentRow.HH001ASOA);
  document.getElementById("modal-soa-NV").innerText = fo(currentRow.HHCRRGSOAEFFNV);
  //SYD
  document.getElementById("modal-syd-Acquistate").innerText = fo(currentRow.HHACQSYD);
  document.getElementById("modal-syd-Consuntivate").innerText = fo(currentRow.HHCRRGSYDEFF);
  document.getElementById("modal-syd-Differenza").innerText = fo(currentRow.HHACQSYD - currentRow.HHCRRGSYDEFF);
  document.getElementById("modal-syd-HH001A").innerText = fo(currentRow.HH001ASYD);
  document.getElementById("modal-syd-NV").innerText = fo(currentRow.HHCRRGSYDEFFNV);
  //GDE
  document.getElementById("modal-gde-Acquistate").innerText = fo(currentRow.HHACQGDE);
  document.getElementById("modal-gde-Consuntivate").innerText = fo(currentRow.HHCRRGPGMEFF + currentRow.HHCRRGPJMEFF + currentRow.HHCRRGBUCEFF);
  document.getElementById("modal-gde-Differenza").innerText = fo(currentRow.HHACQPGM + currentRow.HHACQPJM + currentRow.HHACQBUC - (currentRow.HHCRRGPGMEFF + currentRow.HHCRRGPJMEFF + currentRow.HHCRRGBUCEFF));
  document.getElementById("modal-gde-HH001A").innerText = fo(currentRow.HH001APGM + currentRow.HH001APJM + currentRow.HH001ABUC);
  document.getElementById("modal-gde-NV").innerText = fo(currentRow.HHCRRGPGMEFFNV + currentRow.HHCRRGPJMEFFNV + currentRow.HHCRRGBUCEFFNV);
  //PGM
  document.getElementById("modal-pgm-Acquistate").innerText = fo(currentRow.HHACQPGM);
  document.getElementById("modal-pgm-Consuntivate").innerText = fo(currentRow.HHCRRGPGMEFF);
  document.getElementById("modal-pgm-Differenza").innerText = fo(currentRow.HHACQPGM - currentRow.HHCRRGPGMEFF);
  document.getElementById("modal-pgm-HH001A").innerText = fo(currentRow.HH001APGM);
  document.getElementById("modal-pgm-NV").innerText = fo(currentRow.HHCRRGPGMEFFNV);
  //PJM
  document.getElementById("modal-pjm-Acquistate").innerText = fo(currentRow.HHACQPJM);
  document.getElementById("modal-pjm-Consuntivate").innerText = fo(currentRow.HHCRRGPJMEFF);
  document.getElementById("modal-pjm-Differenza").innerText = fo(currentRow.HHACQPJM - currentRow.HHCRRGPJMEFF);
  document.getElementById("modal-pjm-HH001A").innerText = fo(currentRow.HH001APJM);
  document.getElementById("modal-pjm-NV").innerText = fo(currentRow.HHCRRGPJMEFFNV);
  //BUC
  document.getElementById("modal-buc-Acquistate").innerText = fo(currentRow.HHACQBUC);
  document.getElementById("modal-buc-Consuntivate").innerText = fo(currentRow.HHCRRGBUCEFF);
  document.getElementById("modal-buc-Differenza").innerText = fo(currentRow.HHACQBUC - currentRow.HHCRRGBUCEFF);
  document.getElementById("modal-buc-HH001A").innerText = fo(currentRow.HH001ABUC);
  document.getElementById("modal-buc-NV").innerText = fo(currentRow.HHCRRGBUCEFFNV);

  console.log(currentRow);
  inputAvanzamento.value = currentRow.Avanzamento;
}

function fo(stringa) {
  return stringa?.toFixed(2).padStart(1, "0").replace(".", ",");
}

function search(nComm, cCli, value1, value2) {
  value1 = value1.trim().toLowerCase();
  value2 = value2.trim().toLowerCase();
  if (value1 != "" || value2 != "") {
    for (const row of tableRows) {
      const field1 = row.querySelector(`[campo="${nComm}"]`).innerText.toLowerCase().split("/")[3];
      const field2 = row.querySelector(`[campo="${cCli}"]`).innerText.toLowerCase();

      if (field1.includes(value1) && field2.includes(value2)) {
        //console.log("  " + value1 + " " + value2 + "             " + field1 + " " + field2)
        row.classList.remove("d-none");
      } /* else if (field1.includes(value1) && value1 != "") {
                row.classList.remove("d-none")
            } else if (field2.includes(value2) && value2 != "") {
                row.classList.remove("d-none")
            } */ else {
        row.classList.add("d-none");
      }
    }
  } else {
    resetRows();
  }
}

function resetRows() {
  for (const row of tableRows) {
    row.classList.remove("d-none");
  }
  inputNCommessa.value = "";
  inputCliente.value = "";
}

function goToDoc(pageAction) {
  if (idDoc == "") {
    const modal = new bootstrap.Modal("#err-modal");
    modal.show();
    return;
  }
  window.open(`../Crrg/${pageAction}/${idDoc}`);
}
