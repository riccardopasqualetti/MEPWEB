﻿//location.reload(true);

divCrrgRifClienteContent = document.getElementById("divCrrgRifCliente").innerHTML;
divComCliContent         = document.getElementById("divComCli").innerHTML;
divCommCodeDescContent   = document.getElementById("divCommCodeDesc").innerHTML;
divCommContent           = document.getElementById("divComm").innerHTML;
//divComCliBarra           = document.getElementById("divComCliBarra");
//document.getElementById("divComCliBarra").innerHTML="";

async function Modalita() {
    document.getElementById("NTOper").innerHTML = ""
    switch (true) {
        case document.getElementById("modIsl").checked:
            //alert("isl");
            
            // abilita divCrrgRifCliente
            document.getElementById("divCrrgRifCliente").innerHTML = divCrrgRifClienteContent;
            // disabilita divComCli
            document.getElementById("divComCli").innerHTML = "";
            // disabilita divCommCodeDesc
            document.getElementById("divCommCodeDesc").innerHTML = "";
            // disabilita divComm
            document.getElementById("divComm").innerHTML = "";

            // se la modalità è sempre la stessa significa che sta gestendo un errore 
            // e ricarica la commessa nel campo descrizione che è protetto e non eredita il valore.
            if (document.getElementById("MemoModalita").value == "modIsl") {
                //alert("ricarica commessa per ISL");
                ISLChanged();
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
            document.getElementById("divCommCodeDesc").innerHTML = divCommCodeDescContent;
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
            }


            document.getElementById("MemoModalita").value = "modCli";
            break;

        case document.getElementById("modGestInt").checked:
            // disabilita divCrrgRifCliente
            document.getElementById("divCrrgRifCliente").innerHTML = "";
            // abilita divComCli
            document.getElementById("divComCli").innerHTML = divComCliContent;
            // abilita divCommCodeDesc
            document.getElementById("divCommCodeDesc").innerHTML = divCommCodeDescContent;

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
            }

            document.getElementById("MemoModalita").value = "modGestInt";

            break;

        case document.getElementById("modSvilInt").checked:
            // disabilita divCrrgRifCliente
            document.getElementById("divCrrgRifCliente").innerHTML = "";
            // abilita divComCli
            document.getElementById("divComCli").innerHTML = divComCliContent;
            // abilita divCommCodeDesc
            document.getElementById("divCommCodeDesc").innerHTML = divCommCodeDescContent;
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
            document.getElementById("divCommCodeDesc").innerHTML = "";
            // abilita divComm
            document.getElementById("divComm").innerHTML = divCommContent;

            document.getElementById("divComm").classList.remove("d-none");

            // se la modalità è sempre la stessa significa che sta gestendo un errore 
            // e ricarica la commessa nel campo descrizione che è protetto e non eredita il valore.
            if (document.getElementById("MemoModalita").value == "modCom") {
                //alert("ricarica descrizione commessa");
                CommChanged();
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
            document.getElementById("CommCodeDesc").empty;
            options = '<option value=""></option>';
            for (var i = 0; i < cliNumber; i++) {
                if ((modalita == "modCli") || (modalita == "modGestInt" && "FGZ".includes(cliData[i].orpbPrfDoc)) || modalita == "modSvilInt" && "AH".includes(cliData[i].orpbPrfDoc)) {
                    options += `<option value="${cliData[i].orpbTstDoc}/${cliData[i].orpbPrfDoc}/${cliData[i].orpbADoc}/${cliData[i].orpbNDoc}">${cliData[i].commDescDd}</option>`;
                } 
            };
            document.getElementById("CommCodeDesc").innerHTML = options;
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

async function CommListChanged() {    
    //document.getElementById("CommCode").value = document.getElementById("CommCodeDesc").value;
    //var dropdown = document.getElementById("CommCodeDesc");
    //document.getElementById("CommDesc").value = dropdown.options[dropdown.selectedIndex].text.substring(20);
    document.getElementById("CommCode").value = document.getElementById("CommCodeDesc").value
    await LoadTOper(document.getElementById("CommCodeDesc").value);
}


async function CommChanged() {
    var com = document.getElementById("CommCode").value;
    console.log(com);    
    if (com.match(/^[0-9]+$/) != null) {
        console.log('numero');
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
            console.log(comData);
            if (com.match(/^[0-9]+$/) != null) {
                console.log(comData.CommCompCode);
                document.getElementById("CommCode").value = comData.commCompCode;
                document.getElementById("CommDesc").value = comData.commDesc;
            } else {
                document.getElementById("CommDesc").value = comData.commMasterData.tbcpDesc;
            }            
            LoadTOper(document.getElementById("CommCode").value);
        }
        else {
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

    await Modalita();
});


