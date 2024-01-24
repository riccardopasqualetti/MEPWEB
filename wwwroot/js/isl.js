let prevRowSelected;
let selectedIsl;

async function setupSearchIsl(elementId) {
  const searchInput = document.getElementById("isl-search-input");
  const bsModal = new bootstrap.Modal("#searchIslModal");
  const debouncedSearchData = debounce((ev) => searchData(ev), 500);

  await executeSearch(elementId, bsModal);

  bsModal.show();

  searchInput.addEventListener("input", (e) => {
    debouncedSearchData(e);
  });

  document.getElementById("searchIslModal").addEventListener("hidden.bs.modal", () => {
    document.getElementById("isl-tbody").innerHTML = "";
  });

  document.getElementById("submit-research").addEventListener("click", async () => {
    await executeSearch(elementId, bsModal);
  });

  document.getElementById("submit-isl-button").addEventListener("click", () => {
    const input = document.getElementById(elementId);
    input.value = selectedIsl;
    input.onchange();
    document.getElementById("NTOper").focus();
  });
}

const debounce = (mainFunction, delay) => {
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
};

function searchData(e) {
  const rows = document.getElementsByClassName("search-isl-riga");
  for (const row of rows) {
    value = row.querySelector(`*[field="rifCli"]`).innerText;
    if (!value.toLowerCase().includes(e.target.value.toLowerCase())) {
      row.classList.add("d-none");
    } else {
      row.classList.remove("d-none");
    }
  }
}

function filterRows(e) {
  console.log("1");
  /*   const rows = document.getElementsByClassName("search-isl-riga");
  for (const row of rows) {
    value = row.querySelector(`*[field="rifCli"]`).innerText;
    if (!value.toLowerCase().includes(e.target.value.toLowerCase())) {
      row.classList.add("d-none");
    } else {
      row.classList.remove("d-none");
    }
  } */
}

async function executeSearch(elementId, bsModal) {
  const islDisplay = document.getElementById("isl-tbody");
  islDisplay.innerHTML = "";

  const checkboxStatuses = getCheckboxesStatus();

  const isls = await fetchAll(checkboxStatuses);
  for (const isl of isls) {
    islDisplay.appendChild(createRow(isl, elementId, bsModal));
  }
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
  const request = req;

  const settings = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(request),
  };

  const call = await fetch("api/VsPpMonitorIsl/GetIslByStato", settings);
  const response = await call.json();

  return response;
}

function createRow(isl, elementId, bsModal) {
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
