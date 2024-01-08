//location.reload(true);

divCrrgRifClienteContent = document.getElementById("divCrrgRifCliente").innerHTML;
divComCliContent         = document.getElementById("divComCli").innerHTML;
divCommCodeDescContent   = document.getElementById("divCommCodeDesc").innerHTML;
divCommContent           = document.getElementById("divComm").innerHTML;
//divComCliBarra           = document.getElementById("divComCliBarra");
//document.getElementById("divComCliBarra").innerHTML="";

async function Modalita() {
    document.getElementById("NTOper").innerHTML = ""
    
    document.getElementById("dropdown-commesse").innerHTML = ""
    switch (true) {
        case document.getElementById("modIsl").checked:
            //alert("isl");
            
            // abilita divCrrgRifCliente
            document.getElementById("divCrrgRifCliente").innerHTML = divCrrgRifClienteContent;
            // disabilita divComCli
            document.getElementById("divComCli").innerHTML = "";
            // disabilita divCommCodeDesc
            document.getElementById("divCommCodeDesc").classList.add("d-none");
            // disabilita divComm
            document.getElementById("divComm").innerHTML = "";

            // se la modalità è sempre la stessa significa che sta gestendo un errore 
            // e ricarica la commessa nel campo descrizione che è protetto e non eredita il valore.
            if (document.getElementById("MemoModalita").value == "modIsl") {
                //alert("ricarica commessa per ISL");
                ISLChanged();
            } else {
                document.getElementById("myInput").value = ""
            }

            //document.getElementById("CrrgRifCliente").removeAttribute('disabled');

            document.getElementById("MemoModalita").value = "modIsl";
            break;

        case document.getElementById("modCli").checked:
            //alert("cli");
            
            // disabilita divCrrgRifCliente
            document.getElementById("divCrrgRifCliente").innerHTML = "";
            // abilita divComCli
            document.getElementById("divComCli").innerHTML = divComCliContent;
            // abilita divCommCodeDesc
            //document.getElementById("divCommCodeDesc").innerHTML = divCommCodeDescContent;
            document.getElementById("divCommCodeDesc").classList.remove("d-none")
            // disabilita divComm
            document.getElementById("divComm").innerHTML = divCommContent;
            document.getElementById("divComm").classList.add("d-none");

            //document.getElementById("CrrgRifCliente").setAttribute('disabled', 'disabled');

            if (document.getElementById("MemoModalita").value == "modCli") {
                await CliListChanged("modCli");
                document.getElementById("CommCodeDesc").value = document.getElementById("CommCode").value
                await LoadTOper(document.getElementById("CommCode").value);
            } else {
                document.getElementById("CommCodeDesc").value = ""
                document.getElementById("CommCode").value = ""
                document.getElementById("ComCCli").value = ""
                document.getElementById("myInput").value = ""
            }


            document.getElementById("MemoModalita").value = "modCli";
            break;

        case document.getElementById("modGestInt").checked:
            // disabilita divCrrgRifCliente
            document.getElementById("divCrrgRifCliente").innerHTML = "";
            // abilita divComCli
            document.getElementById("divComCli").innerHTML = divComCliContent;
            // abilita divCommCodeDesc
            //document.getElementById("divCommCodeDesc").innerHTML = divCommCodeDescContent;
            document.getElementById("divCommCodeDesc").classList.remove("d-none")
            // disabilita divComm
            document.getElementById("divComm").innerHTML = divCommContent;
            document.getElementById("divComm").classList.add("d-none");

            //document.getElementById("CrrgRifCliente").setAttribute('disabled', 'disabled');

            document.getElementById("ComCCli").value = "0153S018";
            document.getElementById("ComCCli").disabled = true;

            await CliListChanged("modGestInt");
            if (document.getElementById("MemoModalita").value == "modGestInt") {
                document.getElementById("CommCodeDesc").value = document.getElementById("CommCode").value
                await LoadTOper(document.getElementById("CommCode").value);
            } else {
                document.getElementById("CommCodeDesc").value = ""
                document.getElementById("CommCode").value = ""
                document.getElementById("myInput").value = ""
            }

            document.getElementById("MemoModalita").value = "modGestInt";

            break;

        case document.getElementById("modSvilInt").checked:
            // disabilita divCrrgRifCliente
            document.getElementById("divCrrgRifCliente").innerHTML = "";
            // abilita divComCli
            document.getElementById("divComCli").innerHTML = divComCliContent;
            // abilita divCommCodeDesc
            //document.getElementById("divCommCodeDesc").innerHTML = divCommCodeDescContent;
            document.getElementById("divCommCodeDesc").classList.remove("d-none")
            // disabilita divComm
            document.getElementById("divComm").innerHTML = divCommContent;
            document.getElementById("divComm").classList.add("d-none");

            //document.getElementById("CrrgRifCliente").setAttribute('disabled', 'disabled');

            document.getElementById("ComCCli").value = "0153S018";
            document.getElementById("ComCCli").disabled = true;

            await CliListChanged("modSvilInt");
            if (document.getElementById("MemoModalita").value == "modSvilInt") {
                document.getElementById("CommCodeDesc").value = document.getElementById("CommCode").value
                await LoadTOper(document.getElementById("CommCode").value);
            } else {
                document.getElementById("CommCodeDesc").value = ""
                document.getElementById("CommCode").value = ""
                document.getElementById("myInput").value = ""
            }

            document.getElementById("MemoModalita").value = "modSvilInt";

            break;

        case document.getElementById("modCom").checked: 
            //alert("com");
            // disabilita divCrrgRifCliente
            document.getElementById("divCrrgRifCliente").innerHTML = "";
            // disabilita divComCli
            document.getElementById("divComCli").innerHTML = "";
            // disabilita divCommCodeDesc
            //document.getElementById("divCommCodeDesc").innerHTML = "";
            document.getElementById("divCommCodeDesc").classList.add("d-none")
            // abilita divComm
            document.getElementById("divComm").innerHTML = divCommContent;

            document.getElementById("divComm").classList.remove("d-none");

            // se la modalità è sempre la stessa significa che sta gestendo un errore 
            // e ricarica la commessa nel campo descrizione che è protetto e non eredita il valore.
            if (document.getElementById("MemoModalita").value == "modCom") {
                //alert("ricarica descrizione commessa");
                await CommChanged();
            } else {
                document.getElementById("myInput").value = ""
            }

            document.getElementById("MemoModalita").value = "modCom";
            break;
    }   
}

function ReloadCrrgCreateForm() {
    console.log("a")
    document.CrrgCreateForm.submit();
}


//function SetCommCode() {
//    var ISL = document.getElementById("CrrgRifCliente").value
//    if (ISL != null && ISL != '') {
//        var url1 = "https://localhost:44300/api/Tatv/GetCommByISL/" + ISL
//        var url = "https://localhost:44300/api/Tatv/GetCommByISL/ISL-0108-1000"
//        alert(url);
//        //document.getElementById("CommCode").value = "pippo";
//        $.getJSON(url, function (comm) {
//            alert("ret: " + comm.CrrgTstDoc);
//          //  if (comm != null && !jQuery.isEmptyObject(comm)) {
//          //      commcode = comm.CrrgNDoc;
//          //      document.getElementById("CommCode").value = commcode;
//          //  }
//        });
//        alert("get");
//    }
//}
async function ISLChanged() { 
    var ISL = document.getElementById("CrrgRifCliente").value
    var url = "/api/Tatv/GetISLByCodeAsync/" + ISL
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
            comm = islData.islMasterData.tatvTstComm + '/' + islData.islMasterData.tatvPrfComm + '/' + islData.islMasterData.tatvAComm + '/' + islData.islMasterData.tatvNComm;
            document.getElementById("ISLCommDesc").value = islData.islMasterData.tatvTstComm + '/' + islData.islMasterData.tatvPrfComm + '/' + islData.islMasterData.tatvAComm + '/' + islData.islMasterData.tatvNComm + ' - ' + islData.islCommData.commMasterData.tbcpDesc;
            document.getElementById("CrrgApp").value = islData.islMasterData.tatvCPartApp;            
            document.getElementById("CrrgMod").value = islData.islMasterData.tatvCPart;            
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
            //document.getElementById("CommCodeDesc").empty;
            //options = '<option value=""></option>';
            //for (var i = 0; i < cliNumber; i++) {
            //    if ((modalita == "modCli") || (modalita == "modGestInt" && "FGZ".includes(cliData[i].orpbPrfDoc)) || modalita == "modSvilInt" && "AH".includes(cliData[i].orpbPrfDoc)) {
            //        options += `<option value="${cliData[i].orpbTstDoc}/${cliData[i].orpbPrfDoc}/${cliData[i].orpbADoc}/${cliData[i].orpbNDoc}">${cliData[i].commDescDd}</option>`;
            //    }
            //};
            //document.getElementById("CommCodeDesc").innerHTML = options;
            document.getElementById("dropdown-commesse").innerHTML = "";
            const opt = document.createElement("p")
            opt.className = "m-0 dropdown-opt"
            opt.setAttribute("valore", ``)
            opt.innerText = ""
            opt.style.height = "22px"
            document.getElementById("dropdown-commesse").appendChild(opt)
            opt.addEventListener("click", () => {
                document.getElementById("CommCodeDesc").value = opt.getAttribute("valore")
                document.getElementById("myInput").value = opt.getAttribute("valore")
            })
            for (var i = 0; i < cliNumber; i++) {
                if ((modalita == "modCli") || (modalita == "modGestInt" && "FGZ".includes(cliData[i].orpbPrfDoc)) || modalita == "modSvilInt" && "AH".includes(cliData[i].orpbPrfDoc)) {
                    document.getElementById("dropdown-commesse").appendChild(createOpt(cliData[i]))
                }
            };

            //document.getElementById("dropdown-commesse").children[0].classList.add("selectedDropItem")
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

function showDropdown() {
    document.getElementById("dropdown-commesse").classList.remove("d-none")
    document.getElementById("dropdown-commesse").classList.add("active-custom-dropdown")
    document.getElementById("dropdown-commesse").addEventListener("mousemove", (e) => {
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

function hideDropdown() {
    document.getElementById("dropdown-commesse").classList.add("d-none")
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
function handleKeys(e, dropdownId, inputId) {
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
        document.getElementById("CommCodeDesc").value = value
        document.getElementById("CommCode").value = value
        hideDropdown()
    }

    const selectedOptionOffset = selectedIndex * optionHeight;
    const containerHeight = dropdown.offsetHeight;

    if (selectedOptionOffset < dropdown.scrollTop) {
        dropdown.scrollTop = selectedOptionOffset;
    } else if (selectedOptionOffset + optionHeight > dropdown.scrollTop + containerHeight) {
        dropdown.scrollTop = selectedOptionOffset + optionHeight - containerHeight;
    }
}

function createOpt(cliData, i) {
    const opt = document.createElement("p")
    opt.className = "m-0 dropCom-group dropdown-opt"
    opt.setAttribute("valore", `${cliData.orpbTstDoc}/${cliData.orpbPrfDoc}/${cliData.orpbADoc}/${cliData.orpbNDoc}`)
    opt.innerText = cliData.commDescDd
    opt.addEventListener("mousedown", async () => {
        document.getElementById("myInput").value = opt.getAttribute("valore")
        document.getElementById("CommCodeDesc").value = opt.getAttribute("valore")
        document.getElementById("CommCode").value = opt.getAttribute("valore")
        document.getElementById("dropdown-commesse").classList.add("d-none")
        await CommChanged()
    })
    return opt
}

async function CommListChanged() {    
    //document.getElementById("CommCode").value = document.getElementById("CommCodeDesc").value;
    //var dropdown = document.getElementById("CommCodeDesc");
    //document.getElementById("CommDesc").value = dropdown.options[dropdown.selectedIndex].text.substring(20);
    document.getElementById("CommCode").value = document.getElementById("CommCodeDesc").value
    await LoadTOper(document.getElementById("CommCodeDesc").value);
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
    document.getElementById("CommCodeDesc").addEventListener("focusout", async () => {
        await CommChanged()
    })

    //apre la dropdown al focus sull'input
    document.getElementById("myInput").addEventListener("focusin", (e) => {

        //se all'apertura c'è già un valore nell'input viene filtrata la dropdown
        if (e.target.value != "") {
            filterDropdown(e.target, "dropdown-commesse")
        }
        showDropdown()
    })

    //se viene fatto click fuori dalla dropdown viene chiusa
    document.addEventListener("mousedown", (e) => {
        if (!e.target.classList.contains("dropCom-group")) {
            hideDropdown()
        }
    })

    //se l'input delle commesse della dropdown perde il focus la dropdown viene nascosta e viene chiamata la funzione che carica le operazioni
    document.getElementById("myInput").addEventListener("focusout", async (e) => {
        hideDropdown()
        if (!e.target.classList.contains("bad-field") && e.target.value != "") {
            await CommChanged()
        }
    })

    //se viene scritto qualcosa nell'input delle commesse della dropdown, il valore viene scritto anche nei veri input delle commesse
    document.getElementById("myInput").addEventListener("input", async (e) => {
        e.target.value = e.target.value.toUpperCase()
        document.getElementById("CommCodeDesc").value = e.target.value
        document.getElementById("CommCode").value = e.target.value
        filterDropdown(e.target, "dropdown-commesse")
    })

    //per gestire i tasti su, giu e invio
    document.getElementById("myInput").addEventListener("keydown", (e) => {
        handleKeys(e, "dropdown-commesse", "myInput")
    })

    await Modalita();
});


