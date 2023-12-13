
let selected = ""
let idDoc = ""
let isSearching = false
const inputNCommessa = document.getElementById("numeroCommessa")
const inputCliente = document.getElementById("cliente")
const tableRows = document.getElementsByClassName("riga")

Array.from(document.getElementById("cons-tbody").children).forEach(x => {
    x.addEventListener("click", () => {
        idDoc = x.getAttribute("idDoc")
        if (selected != "" && x.id != selected) {
            const childrens = Array.from(document.getElementById(selected).children)

            for (let i = 1; i < childrens.length; i++) {
                childrens[i].classList.remove("row-selected")
            }
        }
        selected = x.id
        const childrens = Array.from(x.children)
        for (let i = 1; i < childrens.length; i++) {
            childrens[i].classList.add("row-selected")
        }
    })
})

document.getElementById("filter-btn").addEventListener("click", () => {
    document.getElementById("filters-container").classList.toggle("show-filters")
});

/* inputNCommessa.addEventListener("input", (e) => {
    if (!isSearching) {
        isSearching = true
        setTimeout(() => {
            search("TBCP_N_COMM", e.target.value)
        }, 700)
        isSearching = false
    }
});

inputCliente.addEventListener("input", (e) => {
    if (!isSearching) {
        isSearching = true
        setTimeout(() => {
            search("TBCP_C_CLI", e.target.value)
        }, 700)
        isSearching = false
    }
}); */

document.getElementById("search-form").addEventListener("submit", (e) => {
    e.preventDefault()
    search("TBCP_N_COMM","TBCP_C_CLI", inputNCommessa.value, inputCliente.value)
});

function search(nComm, cCli, value1, value2) {
    if (value1 != "" || value2 != "") {
        for (const row of tableRows) {
                const field1 = row.querySelector(`[campo="${nComm}"]`)
                const field2 = row.querySelector(`[campo="${cCli}"]`)

                console.log("value1=" + value1 + "    value2=" + value2)
            if ((value1 != "" && field1.innerText.includes(value1)) && (value2 != "" && field2.innerText.includes(value2))) {
                row.classList.remove("d-none")
            } else {
                row.classList.add("d-none")
            }
        };
    } else {
        resetRows()
    }
}

function resetRows() {
    for (const row of tableRows) {
        row.classList.remove("d-none")
    };
}

function goToDoc(pageAction) {
    if (idDoc == "") {
        const modal = new bootstrap.Modal("#err-modal")
        modal.show()
        return
    }
    window.location.href = `../Crrg/${pageAction}/${idDoc}`
}