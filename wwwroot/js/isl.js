let prevRowSelected;
let selectedIsl;
let isSearching = false;
let currentSearch;
const searchInput = document.getElementById("isl-search-input");
const bsModal = new bootstrap.Modal("#searchIslModal");
const debouncedSearchData = debounce((ev) => searchData(ev), 500);
const debouncedExecuteSearch = debounce(async (isl) => executeSearch(isl), 500);

searchInput.addEventListener("input", (e) => {
  debouncedExecuteSearch(e.target.value);
});

document.getElementById("searchIslModal").addEventListener("hidden.bs.modal", () => {
    document.getElementById("isl-tbody").innerHTML = "";
    searchInput.value = "";
});

document.getElementById("submit-research").addEventListener("click", async () => {
  await executeSearch();
});

document.getElementById("submit-isl-button").addEventListener("click", () => {
    if (selectedIsl) {
        const input = document.getElementById("CrrgRifCliente");
        input.value = selectedIsl;
        input.onchange();
        document.getElementById("NTOper").focus();
    }
});
async function setupSearchIsl() {
  bsModal.show();
  await executeSearch();
}

function debounce(mainFunction, delay) {
  // Declare a variable called 'timer' to store the timer ID
  let timer;
  // Return an anonymous function that takes in any number of arguments
  return function (...args) {
    // Clear the previous timer to prevent the execution of 'mainFunction'
    clearTimeout(timer);
    // Set a new timer that will execute 'mainFunction' after the specified delay
    timer = setTimeout(() => {
      mainFunction(...args);
    }, delay);
  };
}

function searchData(e) {
  /* const isls = await fetchAll(checkboxStatuses);
  for (const isl of isls) {
    islDisplay.appendChild(createRow(isl, elementId, bsModal));
  } */
  const rows = document.getElementsByClassName("search-isl-riga");
  console.log(rows[10]);
  for (const row of rows) {
    value = row.children[0].innerText;
    if (!value.toLowerCase().includes(e.target.value.toLowerCase())) {
      row.classList.add("d-none");
    } else {
      row.classList.remove("d-none");
    }
  }
}

async function executeSearch(isl) {
  if (isSearching) {
    return debouncedExecuteSearch(isl);
  }
  console.log("Searching...");
  isSearching = true;
  const islDisplay = document.getElementById("isl-tbody");
  islDisplay.innerHTML = "";

  createLoader();

  const checkboxStatuses = getCheckboxesStatus();
  const request = {
    flags: checkboxStatuses,
  };

  if (isl) {
    request.isl = isl;
  }

  const isls = await fetchAll(request);

  for (const isl of isls) {
    islDisplay.appendChild(createRow(isl));
  }
  deleteLoader();
  isSearching = false;
}

function deleteLoader() {
  document.getElementById("tr-loader").remove();
}

function createLoader() {
  const trLoaderContainer = document.createElement("tr");
  trLoaderContainer.id = "tr-loader";
  const tdLoaderContainer = document.createElement("td");
  tdLoaderContainer.classList.add("p-0");
  tdLoaderContainer.style.backgroundColor = "var(--bs-modal-bg)";
  tdLoaderContainer.setAttribute("colspan", "4");
  const loaderContainer = document.createElement("div");
  loaderContainer.className = "d-flex justify-content-center align-items-center";
  loaderContainer.style.backgroundColor = "var(--bs-modal-bg)";
  const researchLoader = document.createElement("span");
  researchLoader.id = "rLoader";
  researchLoader.className = "layout-loader d-block";
  document.getElementById("isl-tbody").appendChild(trLoaderContainer);
  trLoaderContainer.appendChild(tdLoaderContainer);
  tdLoaderContainer.appendChild(loaderContainer);
  loaderContainer.appendChild(researchLoader);
}

function getCheckboxesStatus() {
  const statuses = {
    check_close: document.getElementById("isl-checkbox-close").checked,
    check_sosp: document.getElementById("isl-checkbox-sosp").checked,
    check_svil: document.getElementById("isl-checkbox-svil").checked,
    check_deli: document.getElementById("isl-checkbox-deli").checked,
    check_anfu: document.getElementById("isl-checkbox-anfu").checked,
    check_new: document.getElementById("isl-checkbox-new").checked,
  };

  const chiavi = {
    check_close: "9-CLOSE",
    check_sosp: "8-SOSP",
    check_svil: "2-SVIL",
    check_deli: "3-DELI",
    check_anfu: "1-ANFU",
    check_new: "7-NEW",
  };

  let res = [];

  for (const stat in statuses) {
    if (statuses[stat] == true) {
      res.push(chiavi[stat]);
    }
  }

  return res;
}

async function fetchAll(req) {
  const call = await axios.post("api/VsPpMonitorIsl/GetIslByStato", JSON.stringify(req), { headers: { "Content-Type": "application/json" } });

  return call.data;
}

function createRow(isl) {
  const row = document.createElement("tr");
  row.classList.add("search-isl-riga");

  const tdRifcli = document.createElement("td");
  tdRifcli.innerText = isl.rifCli;
  tdRifcli.setAttribute("field", "rifCli");
  row.appendChild(tdRifcli);
  const tdStato = document.createElement("td");
  tdStato.innerText = isl.stato;
  row.appendChild(tdStato);
  const tdAcliRagSoc1 = document.createElement("td");
  tdAcliRagSoc1.innerText = isl.acliRagSoc1;
  row.appendChild(tdAcliRagSoc1);
  const tdTatvDesc = document.createElement("td");
  const p = document.createElement("p");
  p.innerText = isl.tatvDesc;
  tdTatvDesc.appendChild(p);
  row.appendChild(tdTatvDesc);

  row.addEventListener("click", () => {
    if (prevRowSelected) {
      prevRowSelected.removeAttribute("selected");
    }
    prevRowSelected = row;
    row.setAttribute("selected", "true");
    selectedIsl = tdRifcli.innerText;
  });

  return row;
}
