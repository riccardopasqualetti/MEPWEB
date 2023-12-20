


async function setupSearchIsl(elementId) {
    const searchInput = document.getElementById("isl-search-input");
    const bsModal = new bootstrap.Modal("#searchIslModal");

    await executeSearch(elementId, bsModal)

    bsModal.show()

    searchInput.addEventListener("input", (e) => {
        console.log(e.target.value)
    })
    
    document.getElementById("searchIslModal").addEventListener('hidden.bs.modal', () => {
        document.getElementById("isl-tbody").innerHTML = ""
    })

    document.getElementById("submit-research").addEventListener('click', async () => {
        await executeSearch()
    })
}

async function executeSearch(elementId, bsModal) {
    const islDisplay = document.getElementById("isl-tbody")
    islDisplay.innerHTML = ""

    const checkboxStatuses = getCheckboxesStatus()
    
    const isls = await fetchAll(checkboxStatuses)
    console.log(isls)
    for (const isl of isls) {
        islDisplay.appendChild(createRow(isl, elementId, bsModal))
    }
}

function getCheckboxesStatus() {
    const statuses =  {
        check_close: document.getElementById("isl-checkbox-close").checked,
        check_sosp: document.getElementById("isl-checkbox-sosp").checked,
        check_svil: document.getElementById("isl-checkbox-svil").checked,
        check_deli: document.getElementById("isl-checkbox-deli").checked,
        check_anfu: document.getElementById("isl-checkbox-anfu").checked,
        check_new: document.getElementById("isl-checkbox-new").checked
    }

    const chiavi = {
        check_close: "9-CLOSE",
        check_sosp: "8-SOSP",
        check_svil: "2-SVIL",
        check_deli: "3-DELI",
        check_anfu: "1-ANFU",
        check_new: "7-NEW"
    }

    let res = []

    for (const stat in statuses) {
        if (statuses[stat] == true) {
            res.push(chiavi[stat])
        }
    }

    return res
}

async function fetchAll(req) {

    const request = req

    const settings = {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(request)
    }

    const call = await fetch("api/VsPpMonitorIsl/GetIslByStato", settings)
    const response = await call.json()

    return response
}

function createRow(isl, elementId, bsModal) {
    const input = document.getElementById(elementId);

    const row = document.createElement("tr")
    row.classList.add("search-isl-riga")

    const tdRifcli = document.createElement("td")
    tdRifcli.innerText = isl.rifCli
    row.appendChild(tdRifcli)
    const tdStato = document.createElement("td")
    tdStato.innerText = isl.stato
    row.appendChild(tdStato)
    const tdAcliRagSoc1 = document.createElement("td")
    tdAcliRagSoc1.innerText = isl.acliRagSoc1
    row.appendChild(tdAcliRagSoc1)
    const tdTatvDesc = document.createElement("td")
    const p = document.createElement("p")
    p.innerText = isl.tatvDesc
    tdTatvDesc.appendChild(p)
    row.appendChild(tdTatvDesc)

    row.addEventListener("click", () => {
        input.value = tdRifcli.innerText
        bsModal.hide()
        input.onchange()
    })

    return row
}