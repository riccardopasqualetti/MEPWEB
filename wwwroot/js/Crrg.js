//location.reload(true);

divCrrgRifClienteContent = document.getElementById("divCrrgRifCliente").innerHTML;
divComCliContent         = document.getElementById("divComCli").innerHTML;
divCommContent           = document.getElementById("divComm").innerHTML;
//document.getElementById("divComCliBarra").innerHTML="";

async function Modalita() {
    document.getElementById("dropdown-commesse").innerHTML = ""
    switch (true) {
        case document.getElementById("modIsl").checked:
            
            // abilita divCrrgRifCliente
            document.getElementById("divCrrgRifCliente").classList.remove("d-none");
            // disabilita divComCli
            document.getElementById("divComCli").classList.add("d-none");
            // disabilita divComm
            document.getElementById("divComm").classList.add("d-none")

            // se la modalità è sempre la stessa significa che sta gestendo un errore 
            // e ricarica la commessa nel campo descrizione che è protetto e non eredita il valore.
            if (document.getElementById("MemoModalita").value == "modIsl") {
                //alert("ricarica commessa per ISL");
                ISLChanged();
            } else {
                document.getElementById("ComDropdownInput").value = ""
                document.getElementById("NTOper").innerHTML = ""
                document.getElementById("CommDesc").innerHTML = ""
                document.getElementById("dropdown-commesse").innerHTML = ""
            }

            document.getElementById("MemoModalita").value = "modIsl";
            break;

        case document.getElementById("modCli").checked:
            
            // disabilita divCrrgRifCliente
            document.getElementById("divCrrgRifCliente").classList.add("d-none");
            // abilita divComCli
            document.getElementById("divComCli").classList.remove("d-none");
            document.getElementById("CliDropdownInput").classList.remove("d-none")
            document.getElementById("CliDropdownInput").classList.remove("bad-field")
            document.getElementById("ComCCli").disabled = false;
            document.getElementById("ComCCli").hidden = true;
            // abilita divComm
            document.getElementById("divComm").classList.remove("d-none")
            document.getElementById("ComDropdownInput").classList.remove("d-none")
            document.getElementById("ComDropdownInput").classList.remove("bad-field")
            document.getElementById("CommCode").hidden = true
            

            if (document.getElementById("MemoModalita").value == "modCli") {
                await CliListChanged("modCli");
                await LoadTOper(document.getElementById("CommCode").value);
            } else {
                document.getElementById("ComCCli").value = ""
                document.getElementById("CommCode").value = ""
                document.getElementById("ComDropdownInput").value = ""
                document.getElementById("CommDesc").value = ""
                document.getElementById("NTOper").innerHTML = ""
                document.getElementById("CliDropdownInput").value = ""
            }

            document.getElementById("MemoModalita").value = "modCli";
            break;

        case document.getElementById("modGestInt").checked:
            // disabilita divCrrgRifCliente
            document.getElementById("divCrrgRifCliente").classList.add("d-none");
            // abilita divComCli
            document.getElementById("divComCli").classList.remove("d-none");
            document.getElementById("CliDropdownInput").classList.add("d-none")
            document.getElementById("ComCCli").value = "0153S018";
            document.getElementById("ComCCli").disabled = true;
            document.getElementById("ComCCli").hidden = false;
            // abilita divComm
            document.getElementById("divComm").classList.remove("d-none")
            document.getElementById("ComDropdownInput").classList.remove("d-none")
            document.getElementById("ComDropdownInput").classList.remove("bad-field")
            document.getElementById("CommCode").hidden = true

            await CliListChanged("modGestInt");
            if (document.getElementById("MemoModalita").value == "modGestInt") {
                await LoadTOper(document.getElementById("CommCode").value);
            } else {
                document.getElementById("CommCode").value = ""
                document.getElementById("ComDropdownInput").value = ""
                document.getElementById("CommDesc").value = ""
                document.getElementById("NTOper").innerHTML = ""
            }

            document.getElementById("MemoModalita").value = "modGestInt";

            break;

        case document.getElementById("modSvilInt").checked:
            // disabilita divCrrgRifCliente
            document.getElementById("divCrrgRifCliente").classList.add("d-none");
            // abilita divComCli
            document.getElementById("divComCli").classList.remove("d-none");    
            document.getElementById("CliDropdownInput").classList.add("d-none")
            document.getElementById("ComCCli").value = "0153S018";
            document.getElementById("ComCCli").disabled = true;
            document.getElementById("ComCCli").hidden = false;
            // abilita divComm
            document.getElementById("divComm").classList.remove("d-none")
            document.getElementById("ComDropdownInput").classList.remove("d-none")
            document.getElementById("ComDropdownInput").classList.remove("bad-field")
            document.getElementById("CommCode").hidden = true

            await CliListChanged("modSvilInt");
            if (document.getElementById("MemoModalita").value == "modSvilInt") {
                await LoadTOper(document.getElementById("CommCode").value);
            } else {
                document.getElementById("CommCode").value = ""
                document.getElementById("ComDropdownInput").value = ""
                document.getElementById("CommDesc").value = ""
                document.getElementById("NTOper").innerHTML = ""
            }

            document.getElementById("MemoModalita").value = "modSvilInt";

            break;

        case document.getElementById("modCom").checked: 
            //alert("com");
            // disabilita divCrrgRifCliente
            document.getElementById("divCrrgRifCliente").classList.add("d-none");
            // disabilita divComCli
            document.getElementById("divComCli").classList.add("d-none");
            // abilita divComm
            document.getElementById("CommCode").hidden = false
            document.getElementById("divComm").classList.remove("d-none");
            document.getElementById("ComDropdownInput").classList.add("d-none")

            // se la modalità è sempre la stessa significa che sta gestendo un errore 
            // e ricarica la commessa nel campo descrizione che è protetto e non eredita il valore.
            if (document.getElementById("MemoModalita").value == "modCom") {
                await CommChanged();
            } else {
                document.getElementById("ComCCli").value = ""
                document.getElementById("CommCode").value = ""
                document.getElementById("ComDropdownInput").value = ""
                document.getElementById("dropdown-commesse").innerHTML = ""
                document.getElementById("CommDesc").value = ""
                document.getElementById("NTOper").innerHTML = ""
            }

            document.getElementById("MemoModalita").value = "modCom";
            break;
    }   
}

function ReloadCrrgCreateForm() {
    console.log("a")
    document.CrrgCreateForm.submit();
}

async function ISLChanged() { 
    var ISL = document.getElementById("CrrgRifCliente").value
    var url = "api/VsPpMonitorIsl/GetByRifCli/" + ISL
    try {
        const response = await fetch(url, {
            method: "GET", // *GET, POST, PUT, DELETE, etc.
            mode: "cors", // no-cors, *cors, same-origin
            cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
            credentials: "same-origin", // include, *same-origin, omit
            headers: {
                "Content-Type": "application/json",
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
        })
        
        if (response.status == 200) {
            let islData = await response.json()
            console.log(islData)
            comm = islData.tbcpTstComm + '/' + islData.tbcpPrfComm + '/' + islData.tbcpAComm + '/' + islData.tbcpNComm;
            document.getElementById("ISLCommDesc").value = comm + ' - ' + islData.tbcpDesc;
            document.getElementById("DescrIsl").value = islData.tatvDesc;
            const tipo = {
                "1-ANFU": "ANFU",
                "2-SVIL": "SVIL",
                "3-DELI": "DELI"
            }
            document.getElementById("CrrgCCaus").value = tipo[islData.flag]
            document.getElementById("CrrgApp").value = islData.tatvCPartApp;            
            document.getElementById("CrrgMod").value = islData.tatvCPart;
            await LoadTOper(comm);
        }
        else {
            return {
                res: null,
                statusCode: response.status,
                message: "ISL non trovata"
            }
        }          

    } catch (ex) {
        console.log(ex)
    }
}


async function CliListChanged(modalita) {
    //document.getElementById("divComCliBarra").innerHTML = divComCliBarra;
    var cli = document.getElementById("ComCCli").value
    var url = "/api/VsCommAperteXCli/GetCommAllByCliAsync/" + cli
    //alert(url);
    try {
        const response = await fetch(url, {
            method: "GET", // *GET, POST, PUT, DELETE, etc.
            mode: "cors", // no-cors, *cors, same-origin
            cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
            credentials: "same-origin", // include, *same-origin, omit
            headers: {
                "Content-Type": "application/json",
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
        })


        if (response.status == 200) {
            let cliData = await response.json();
            let cliNumber = cliData.length;

            document.getElementById("dropdown-commesse").innerHTML = "";
            const opt = document.createElement("p")
            opt.className = "m-0 dropdown-opt"
            opt.setAttribute("valore", ``)
            opt.innerText = ""
            opt.style.height = "22px"
            document.getElementById("dropdown-commesse").appendChild(opt)
            opt.addEventListener("click", () => {
                document.getElementById("ComDropdownInput").value = opt.getAttribute("valore")
            })
            for (var i = 0; i < cliNumber; i++) {
                if ((modalita == "modCli") || (modalita == "modGestInt" && "FGZ".includes(cliData[i].orpbPrfDoc)) || modalita == "modSvilInt" && "AH".includes(cliData[i].orpbPrfDoc)) {
                    const opt = createOpt(cliData[i])
                    document.getElementById("dropdown-commesse").appendChild(opt)
                }
            };

            addHoverClassToOpt(document.getElementById("dropdown-commesse").children[0])
        }
        else {
            return {
                res: null,
                statusCode: response.status,
                message: "ISL non trovata"
            }
        }

    } catch (ex) {
        console.log(ex)
    }
    //document.getElementById("divComCliBarra").innerHTML = "";
}

function setupCustomDropdowns(dropdownInputId, dropdownContainerId, realInput) {

    //apre la dropdown al focus sull'input
    document.getElementById(dropdownInputId).addEventListener("focusin", (e) => {

        //se all'apertura c'è già un valore nell'input viene filtrata la dropdown
        if (e.target.value != "") {
            filterDropdown(e.target, dropdownContainerId)
        }
        showDropdown(dropdownContainerId)
    })

    //se viene fatto click fuori dalla dropdown viene chiusa
    document.addEventListener("mousedown", (e) => {
        if (!e.target.classList.contains("customDropdown-group")) {
            hideDropdown(dropdownContainerId)
        }
    })

    //se l'input delle commesse della dropdown perde il focus la dropdown viene nascosta e viene chiamata la funzione che carica le operazioni
    document.getElementById(dropdownInputId).addEventListener("focusout", async (e) => {
        hideDropdown(dropdownContainerId)
        if (!e.target.classList.contains("bad-field") && e.target.value != "") {
            if (dropdownInputId == "ComDropdownInput") {
                await CommChanged()
            } else {
                await CliListChanged("modCli")
            }
        }
    })

    //se viene scritto qualcosa nell'input delle commesse della dropdown, il valore viene scritto anche nei veri input delle commesse
    document.getElementById(dropdownInputId).addEventListener("input", async (e) => {
        //e.target.value = e.target.value.toUpperCase()
        document.getElementById(realInput).value = e.target.value
        filterDropdown(e.target, dropdownContainerId)
    })

    //per gestire i tasti su, giu e invio
    document.getElementById(dropdownInputId).addEventListener("keydown", (e) => {
        handleKeys(e, dropdownContainerId, dropdownInputId, realInput)
    })
}

function showDropdown(containerId) {
    document.getElementById(containerId).classList.remove("d-none")
    document.getElementById(containerId).classList.add("active-custom-dropdown")
    document.getElementById(containerId).addEventListener("mousemove", (e) => {
        if (e.target.classList.contains("dropdown-opt")) {
            addHoverClassToOpt(e.target)
        }
    })
}

//gestisce l'aggiunta della classe per l'effetto di 'riga selezionata'
function addHoverClassToOpt(target) {
    if (!target) {
        return
    }
    Array.from(document.getElementsByClassName("opt-hover")).forEach(x => x.classList.remove("opt-hover"))
    target.classList.add("opt-hover")
}

function hideDropdown(containerId) {
    if (!containerId) {
        return
    }
    document.getElementById(containerId).classList.add("d-none")
}

//riceve l'input da cui prende il testo e l'id della dropdown da filtrare
function filterDropdown(input, dropdownId) {
    const value = input.value
    const dropdown = document.getElementById(dropdownId)
    const elements = Array.from(dropdown.children)

    elements.forEach(el => {
        const elementText = el.innerText.toUpperCase().replaceAll(" ", "")

        if (!elementText.includes(value.toUpperCase().replaceAll(" ", ""))) {
            el.classList.add("d-none")
        } else {
            el.classList.remove("d-none")
        }
    })

    //filtra le opzioni della dropdown, se non ne trova significa che il valore dell'input non è presente nella dropdown quindi fa il bordo rosso
    const isValueAccepted = elements.filter(x => x.getAttribute("valore") == value)
    if (isValueAccepted.length < 1) {
        input.classList.add("bad-field")
    } else {
        input.classList.remove("bad-field")
    }

    //aggiunge l'hover al primo elemento di quelli visibili
    addHoverClassToOpt(elements.find(x => !x.classList.contains("d-none")))
}

//gestisce freccia su, giu e invio sulla dropdown
function handleKeys(e, dropdownId, inputId, inputIdToSet) {
    const dropdown = document.getElementById(dropdownId)
    const input = document.getElementById(inputId)
    const shownElements = Array.from(dropdown.querySelectorAll(".dropdown-opt:not(.d-none)"))

    //se non ci sono elementi visibili esce
    if (shownElements < 1) {
        return
    }
    const optionHeight = shownElements[0].getBoundingClientRect().height
    let selectedIndex = 0;

    if (e.keyCode == 40) { //freccia giu
        for (let i = 0; i < shownElements.length; i++) {

            if (i == shownElements.length - 1) {
                selectedIndex = i
                break
            }
            if (shownElements[i].classList.contains("opt-hover")) {
                addHoverClassToOpt(shownElements[i + 1])
                selectedIndex = i + 1
                break
            }
        }

    } else if (e.keyCode == 38) { //freccia su
        for (let i = shownElements.length - 1; i > 0; i--) {

            if (i == 0) {
                selectedIndex = 0
                break
            }
            if (shownElements[i].classList.contains("opt-hover")) {
                addHoverClassToOpt(shownElements[i - 1])
                selectedIndex = i - 1
                break
            }
        }

    } else if (e.keyCode == 13) { //invio
        e.preventDefault();
        const value = shownElements.find(x => x.classList.contains("opt-hover")).getAttribute("valore")
        input.value = value
        document.getElementById(inputIdToSet).value = value
        filterDropdown(e.target, dropdownId)
        hideDropdown(dropdownId)
    }

    //fa scorrere in giù la scrollbar se l'opzione selezionata è sotto
    const selectedOptionOffset = selectedIndex * optionHeight;
    const containerHeight = dropdown.offsetHeight;

    if (selectedOptionOffset < dropdown.scrollTop) {
        dropdown.scrollTop = selectedOptionOffset;
    } else if (selectedOptionOffset + optionHeight > dropdown.scrollTop + containerHeight) {
        dropdown.scrollTop = selectedOptionOffset + optionHeight - containerHeight;
    }
}

//per creare le options delle commesse che vengono create quando viene inserito il cliente
function createOpt(cliData, i) {
    const opt = document.createElement("p")
    opt.className = "m-0 customDropdown-group dropdown-opt"
    opt.setAttribute("valore", `${cliData.orpbTstDoc}/${cliData.orpbPrfDoc}/${cliData.orpbADoc}/${cliData.orpbNDoc}`)
    opt.innerText = cliData.commDescDd
    opt.addEventListener("mousedown", async () => {
        document.getElementById("ComDropdownInput").value = opt.getAttribute("valore")
        document.getElementById("CommCode").value = opt.getAttribute("valore")
        document.getElementById("dropdown-commesse").classList.add("d-none")
        await CommChanged()
    })
    return opt
}

//per gestire le options dei clienti che vengono create direttamente da c# alla creazione della pagina 
function cliOpt(value) {
    document.getElementById("CliDropdownInput").value = value
    document.getElementById("ComCCli").value = value
    document.getElementById("dropdown-clienti").classList.add("d-none")
    const fn = async () => await CliListChanged("modCli")
    fn()
}

async function CommListChanged() {    
    await LoadTOper(document.getElementById("CommCode").value);
}


async function CommChanged() {
    var com = document.getElementById("CommCode").value;
    if (com.match(/^[0-9]+$/) != null) {
        var url = "/api/Tbcp/GetCommByNumAsync/" + com
    } else {
        var url = "/api/Tbcp/GetCommByCompCode1Async/" + com
    }    
    
    try {
        const response = await fetch(url, {
            method: "GET", // *GET, POST, PUT, DELETE, etc.
            mode: "cors", // no-cors, *cors, same-origin
            cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
            credentials: "same-origin", // include, *same-origin, omit
            headers: {
                "Content-Type": "application/json",
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
        })

        if (response.status == 200) {
            let comData = await response.json()
            if (com.match(/^[0-9]+$/) != null) {
                document.getElementById("CommCode").value = comData.commCompCode;
                document.getElementById("CommDesc").value = comData.commDesc;
            } else {
                document.getElementById("CommDesc").value = comData.commMasterData.tbcpDesc;
            }            
            await LoadTOper(document.getElementById("CommCode").value);
        }
        else {
            document.getElementById("CommDesc").value = "";
            return {
                res: null,
                statusCode: response.status,
                message: "Commessa non trovata"
            }
        }


    } catch (ex) {
        console.log(ex)
    }
}


async function LoadTOper(commCode) {    
    var url = "/api/Olca/GetOlcaCitoByCommAsync/" + commCode;    
    try {
        const response = await fetch(url, {
            method: "GET", // *GET, POST, PUT, DELETE, etc.
            mode: "cors", // no-cors, *cors, same-origin
            cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
            credentials: "same-origin", // include, *same-origin, omit
            headers: {
                "Content-Type": "application/json",
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
        })

        if (response.status == 200) {            
            let operData = await response.json();
            let operList = operData.olcaCitoList;
            let operNumber = operList.length            
            document.getElementById("NTOper").empty;
            if (operNumber > 1) {
                options = '<option value=""></option>';
            } else {
                options = "";
            }
            for (var i = 0; i < operNumber; i++) {
                options += '<option value="' + operList[i].flussoOlca.olcaNOper + '-' + operList[i].flussoOlca.olcaTOper + '">' + operList[i].flussoOlca.olcaNOper + ' - ' + operList[i].flussoOlca.olcaTOper + ' - ' + operList[i].flussoCito.citoDescrizione + '</option>';
            };
            document.getElementById("NTOper").innerHTML = options;
            document.getElementById("NTOper").value = document.getElementById("operazione").innerHTML
        }
        else {
            return {
                res: null,
                statusCode: response.status,
                message: "Operazioni non trovate per la commessa"
            }
        }

    } catch (ex) {
        console.log(ex)
    }
}


MomoModalita = document.getElementById("MemoModalita").value;
switch (true) {
    case MomoModalita == "modIsl":
        document.getElementById("modIsl").checked = true; break;
    case MomoModalita == "modCli":
        document.getElementById("modCli").checked = true; break;
    case MomoModalita == "modCom":
        document.getElementById("modCom").checked = true; break;
    case MomoModalita == "modSvilInt":
        document.getElementById("modSvilInt").checked = true; break;
    case MomoModalita == "modGestInt":
        document.getElementById("modGestInt").checked = true; break;
}

document.addEventListener("DOMContentLoaded", async function () {        
    document.getElementById("modIsl").addEventListener("change", async () => await Modalita())
    document.getElementById("modCli").addEventListener("change", async () => await Modalita())
    document.getElementById("modGestInt").addEventListener("change", async () => await Modalita())
    document.getElementById("modSvilInt").addEventListener("change", async () => await Modalita())
    document.getElementById("modCom").addEventListener("change", async () => await Modalita())

    //gli viene passato l'input su cui fare la ricerca, il container che conterrà le opzioni e l'input 'vero' che verrà valorizzato
    //setta gli event listener necessari per far funzionare la dropdown ma non gestisce la funzione per la creazione delle opzioni
    setupCustomDropdowns("ComDropdownInput", "dropdown-commesse", "CommCode")
    setupCustomDropdowns("CliDropdownInput", "dropdown-clienti", "ComCCli")

    await Modalita();
});


