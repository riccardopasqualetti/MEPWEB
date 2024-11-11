//location.reload(true);

divCrrgRifClienteContent = document.getElementById('divCrrgRifCliente').innerHTML;
divComCliContent = document.getElementById('divComCli').innerHTML;
divCommContent = document.getElementById('divComm').innerHTML;
//document.getElementById("divComCliBarra").innerHTML="";

async function Modalita() {
   document.getElementById('dropdown-commesse').innerHTML = '';
   //console.log("modalita:");

   switch (true) {
      case document.getElementById('modIsl').checked:
         // abilita divCrrgRifCliente
         document.getElementById('divCrrgRifCliente').classList.remove('d-none');
         // disabilita divComCli
         document.getElementById('divComCli').classList.add('d-none');
         // disabilita divComm
         document.getElementById('divComm').classList.add('d-none');

         if (document.getElementById('CrrgCSrl').value > 0) {
            document.getElementById('MemoModalita').value = 'modIsl';
            document.getElementById('modIsl').disabled = false;
            document.getElementById('modCli').disabled = true;
            document.getElementById('modGestInt').disabled = true;
            document.getElementById('modSvilInt').disabled = true;
            document.getElementById('modCom').disabled = true;
         }

         // se la modalità è sempre la stessa significa che sta gestendo un errore
         // e ricarica la commessa nel campo descrizione che è protetto e non eredita il valore.
         if (
            document.getElementById('MemoModalita').value == 'modIsl' ||
            document.getElementById('IsUpdate').value == 'True'
         ) {
            RefreshIslData();
         } else {
            // divCrrgRifCliente
            document.getElementById('CrrgRifCliente').value = '';
            document.getElementById('DescrIsl').value = '';
            document.getElementById('ISLCommDesc').value = '';

            // divComCli
            //document.getElementById("CliDropdownInput").value = "";
            //document.getElementById("description-clienti").value = "";

            // divComm
            //document.getElementById("ComDropdownInput").value = "";
            //document.getElementById("description-commesse").innerHTML = "";
            //document.getElementById("dropdown-commesse").innerHTML = "";

            DefaultCleanFields();
         }

         document.getElementById('MemoModalita').value = 'modIsl';
         break;

      case document.getElementById('modCli').checked:
         document.getElementById('CrrgCCaus').value = 'CORI';
         // disabilita divCrrgRifCliente
         document.getElementById('divCrrgRifCliente').classList.add('d-none');
         // abilita divComCli
         document.getElementById('divComCli').classList.remove('d-none');
         document.getElementById('CliDropdownInput').classList.remove('d-none');
         document.getElementById('CliDropdownInput').classList.remove('bad-field');
         document.getElementById('CliDropdownInput').disabled = false;
         document.getElementById('CliDropdownInput').hidden = false;
         // abilita divComm
         document.getElementById('divComm').classList.remove('d-none');
         document.getElementById('ComDropdownInput').classList.remove('d-none');
         document.getElementById('ComDropdownInput').classList.remove('bad-field');
         Array.from(document.getElementById('dropdown-clienti').children).forEach((x) => {
            x.classList.remove('d-none');
         });

         if (document.getElementById('MemoModalita').value == 'modCli') {
            await CliListChanged('modCli');
            await LoadTOper(document.getElementById('ComDropdownInput').value);
            await RefreshCommData('modCli');
         } else {
            // divCrrgRifCliente
            //document.getElementById("CrrgRifCliente").value = "";
            //document.getElementById("DescrIsl").value = "";
            //document.getElementById("ISLCommDesc").value = "";

            // divComCli
            document.getElementById('CliDropdownInput').value = '';
            document.getElementById('description-clienti').value = '';

            // divComm
            document.getElementById('ComDropdownInput').value = '';
            document.getElementById('description-commesse').value = '';
            //document.getElementById("dropdown-commesse").innerHTML = "";

            DefaultCleanFields();
         }

         document.getElementById('MemoModalita').value = 'modCli';
         break;

      case document.getElementById('modGestInt').checked:
         document.getElementById('CrrgCCaus').value = 'CORI';
         // disabilita divCrrgRifCliente
         document.getElementById('divCrrgRifCliente').classList.add('d-none');
         // abilita divComCli
         document.getElementById('divComCli').classList.remove('d-none');
         document.getElementById('CliDropdownInput').classList.remove('d-none');
         document.getElementById('CliDropdownInput').value = '0153S018';
         document.getElementById('description-clienti').value = '0153S018 - Sata Consulting S.r.l.';
         document.getElementById('CliDropdownInput').disabled = true;
         document.getElementById('CliDropdownInput').hidden = false;
         // abilita divComm
         document.getElementById('divComm').classList.remove('d-none');
         document.getElementById('ComDropdownInput').classList.remove('d-none');
         document.getElementById('ComDropdownInput').classList.remove('bad-field');

         await CliListChanged('modGestInt');
         if (document.getElementById('MemoModalita').value == 'modGestInt') {
            await LoadTOper(document.getElementById('ComDropdownInput').value);
            await RefreshCommData('modGestInt');
         } else {
            // divCrrgRifCliente
            //document.getElementById("CrrgRifCliente").value = "";
            //document.getElementById("DescrIsl").value = "";
            //document.getElementById("ISLCommDesc").value = "";

            // divComCli
            //document.getElementById("CliDropdownInput").value = "";
            //document.getElementById("description-clienti").value = "";

            // divComm
            document.getElementById('ComDropdownInput').value = '';
            document.getElementById('description-commesse').value = '';
            //document.getElementById("dropdown-commesse").innerHTML = "";

            DefaultCleanFields();
         }

         document.getElementById('MemoModalita').value = 'modGestInt';

         break;

      case document.getElementById('modSvilInt').checked:
         document.getElementById('CrrgCCaus').value = 'CORI';
         // disabilita divCrrgRifCliente
         document.getElementById('divCrrgRifCliente').classList.add('d-none');
         // abilita divComCli
         document.getElementById('divComCli').classList.remove('d-none');
         document.getElementById('CliDropdownInput').classList.remove('d-none');
         document.getElementById('CliDropdownInput').value = '0153S018';
         document.getElementById('description-clienti').value = '0153S018 - Sata Consulting S.r.l.';
         document.getElementById('CliDropdownInput').disabled = true;
         document.getElementById('CliDropdownInput').hidden = false;
         // abilita divComm
         document.getElementById('divComm').classList.remove('d-none');
         document.getElementById('ComDropdownInput').classList.remove('d-none');
         document.getElementById('ComDropdownInput').classList.remove('bad-field');

         await CliListChanged('modSvilInt');
         if (document.getElementById('MemoModalita').value == 'modSvilInt') {
            await LoadTOper(document.getElementById('ComDropdownInput').value);
            await RefreshCommData('modSvilInt');
         } else {
            // divCrrgRifCliente
            //document.getElementById("CrrgRifCliente").value = "";
            //document.getElementById("DescrIsl").value = "";
            //document.getElementById("ISLCommDesc").value = "";

            // divComCli
            //document.getElementById("CliDropdownInput").value = "";
            //document.getElementById("description-clienti").value = "";

            // divComm
            document.getElementById('ComDropdownInput').value = '';
            document.getElementById('description-commesse').value = '';
            //document.getElementById("dropdown-commesse").innerHTML = "";

            DefaultCleanFields();
         }

         document.getElementById('MemoModalita').value = 'modSvilInt';

         break;

      case document.getElementById('modCom').checked:
         document.getElementById('CrrgCCaus').value = 'CORI';
         // disabilita divCrrgRifCliente
         document.getElementById('divCrrgRifCliente').classList.add('d-none');
         // disabilita divComCli
         document.getElementById('divComCli').classList.add('d-none');
         // abilita divComm
         document.getElementById('divComm').classList.remove('d-none');
         document.getElementById('ComDropdownInput').classList.remove('d-none');
         //document.getElementById("ComDropdownInput").hidden = false;
         CliListChanged('modCom');

         if (document.getElementById('CrrgCSrl').value > 0) {
            document.getElementById('MemoModalita').value = 'modCom';
            document.getElementById('modIsl').disabled = true;
            document.getElementById('modCli').disabled = true;
            document.getElementById('modGestInt').disabled = true;
            document.getElementById('modSvilInt').disabled = true;
            document.getElementById('modCom').disabled = false;
         }

         // se la modalità è sempre la stessa significa che sta gestendo un errore
         // e ricarica la commessa nel campo descrizione che è protetto e non eredita il valore.
         if (document.getElementById('MemoModalita').value == 'modCom') {
            await RefreshCommData('modCom');
         } else {
            // divCrrgRifCliente
            //document.getElementById("CrrgRifCliente").value = "";
            //document.getElementById("DescrIsl").value = "";
            //document.getElementById("ISLCommDesc").value = "";

            // divComCli
            document.getElementById('CliDropdownInput').value = '';
            document.getElementById('description-clienti').value = '';

            // divComm
            document.getElementById('ComDropdownInput').value = '';
            document.getElementById('description-commesse').value = '';
            //document.getElementById("dropdown-commesse").innerHTML = "";

            DefaultCleanFields();
         }

         document.getElementById('MemoModalita').value = 'modCom';
         break;
   }
}

function DefaultCleanFields() {
   document.getElementById('NTOper').innerHTML = '';
   //CrrgCmaatt
   //CrrgCCaus
   document.getElementById('CrrgApp').value = '';
   document.getElementById('CrrgMod').value = '';
   document.getElementById('CrrgNote').value = '';
   Array.from(document.getElementsByClassName('text-danger')).forEach((x) => (x.innerHTML = ''));
}

function ReloadCrrgCreateForm() {
   console.log('a');
   document.CrrgCreateForm.submit();
}

// ############################

// Ricerca ISL e caricamento solo delle descrizioni e valorizzazione NTOper
async function RefreshIslData() {
   var ISL = document.getElementById('CrrgRifCliente').value;
   var risposta = await LoadCommOfIsl(ISL);
   if (risposta.statusCode == 200) {
      islData = risposta.res;
      comm = islData.tbcpTstComm + '/' + islData.tbcpPrfComm + '/' + islData.tbcpAComm + '/' + islData.tbcpNComm;
      document.getElementById('ISLCommDesc').value = comm + ' - ' + islData.tbcpDesc;
      document.getElementById('DescrIsl').value = islData.tatvDesc;
      await LoadTOper(comm);
   }
}

// Valorizzazione di tutti i campi di dafault presi dalla ISL
async function ISLChanged() {
   var ISL = document.getElementById('CrrgRifCliente').value;
   if (ISL == '') {
      document.getElementById('ISLCommDesc').value = '';
      document.getElementById('DescrIsl').value = '';
      document.getElementById('CrrgApp').value = '';
      document.getElementById('CrrgMod').value = '';
      return;
   }
   var risposta = await LoadCommOfIsl(ISL);
   if (risposta.statusCode == 200) {
      islData = risposta.res;
      comm = islData.tbcpTstComm + '/' + islData.tbcpPrfComm + '/' + islData.tbcpAComm + '/' + islData.tbcpNComm;
      document.getElementById('ISLCommDesc').value = comm + ' - ' + islData.tbcpDesc;
      document.getElementById('DescrIsl').value = islData.tatvDesc;
      await LoadTOper(comm);
      const causali = {
         '1-ANFU': 'ANFU',
         '2-SVIL': 'SVIL',
         '3-DELI': 'DELI',
         '7-NEW': 'ANFU',
         '8-SOSP': 'ANFU',
         '9-CLOSE': 'DELI',
      };
      if (islData.tbcpPrfComm == 'B' || islData.tatvFlgOfferta > 0) {
         document.getElementById('CrrgCmaatt').value = '3';
      } else {
         document.getElementById('CrrgCmaatt').value = '0';
      }
      document.getElementById('CrrgCCaus').value = causali[islData.flag];
      document.getElementById('CrrgApp').value = islData.tatvCPartApp;
      document.getElementById('CrrgMod').value = islData.tatvCPart;
      document.getElementById('CrrgNote').value = islData.tatvDesc;
   } else {
      return risposta;
   }
}

// Caricamento record ISL da vista vs_pp_monitor_isl
async function LoadCommOfIsl(ISL) {
   var url = 'api/VsPpMonitorIsl/GetByRifCli/' + ISL;
   try {
      const response = await fetch(url, {
         method: 'GET', // *GET, POST, PUT, DELETE, etc.
         mode: 'cors', // no-cors, *cors, same-origin
         cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
         credentials: 'same-origin', // include, *same-origin, omit
         headers: {
            'Content-Type': 'application/json',
            // 'Content-Type': 'application/x-www-form-urlencoded',
         },
      });

      if (response.status == 200) {
         let islData = await response.json();
         return {
            res: islData,
            statusCode: response.status,
            message: 'ISL trovata',
         };
      } else {
         return {
            res: null,
            statusCode: response.status,
            message: 'ISL non trovata',
         };
      }
   } catch (ex) {
      console.error(ex);
   }
}

// ############################

// Ricerca ISL e caricamento solo delle descrizioni e valorizzazione NTOper
async function RefreshCommData(modalita) {
   var com = document.getElementById('ComDropdownInput').value;
   var risposta = await LoadComm(com);
   if (risposta.statusCode == 200) {
      comData = risposta.res;
      if (com.match(/^[0-9]+$/) != null) {
         document.getElementById('ComDropdownInput').value = comData.commCompCode;
         document.getElementById('description-commesse').value = comData.commDesc;
      } else {
         document.getElementById('description-commesse').value = comData.commMasterData.tbcpDesc;
         if (modalita == 'modCli') {
            document.getElementById('CliDropdownInput').value = comData.commMasterData.tbcpCCli;
            document.getElementById('description-clienti').value =
               comData.commMasterData.tbcpCCli + ' - ' + comData.commMasterData.tbcpDesc;
         }
      }
      await LoadTOper(com);
      //document.getElementById("NTOper").value = document.getElementById("operazione1").innerHTML;
   }
}

// Valorizzazione di tutti i campi di default presi dalla Commessa
async function CommChanged() {
   var com = document.getElementById('ComDropdownInput').value;
   var risposta = await LoadComm(com);
   if (risposta.statusCode == 200) {
      comData = risposta.res;

      if (com.match(/^[0-9]+$/) != null) {
         document.getElementById('ComDropdownInput').value = comData.commCompCode;
         document.getElementById('description-commesse').value = comData.commDesc;
      } else {
         document.getElementById('description-commesse').value = comData.commMasterData.tbcpDesc;
      }
      if (com[4] == 'B') {
         document.getElementById('CrrgCmaatt').value = '3';
      } else {
         document.getElementById('CrrgCmaatt').value = '0';
      }
      await LoadTOper(document.getElementById('ComDropdownInput').value);
   } else {
      document.getElementById('description-commesse').value = '';
      return risposta;
   }
}

// Caricamento record Commessa da tabella flusso_tbcp
async function LoadComm(com) {
   if (com.match(/^[0-9]+$/) != null) {
      var url = '/api/Tbcp/GetCommByNumAsync/' + com;
   } else {
      var url = '/api/Tbcp/GetCommByCompCode1Async/' + com;
   }

   try {
      const response = await fetch(url, {
         method: 'GET', // *GET, POST, PUT, DELETE, etc.
         mode: 'cors', // no-cors, *cors, same-origin
         cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
         credentials: 'same-origin', // include, *same-origin, omit
         headers: {
            'Content-Type': 'application/json',
            // 'Content-Type': 'application/x-www-form-urlencoded',
         },
      });
      if (response.status == 200) {
         let comData = await response.json();

         return {
            res: comData,
            statusCode: response.status,
            message: 'ISL trovata',
         };
      } else {
         return {
            res: null,
            statusCode: response.status,
            message: 'ISL non trovata',
         };
      }
   } catch (ex) {
      console.error(ex);
   }
}

async function CliListChanged(modalita) {
   var cli = document.getElementById('CliDropdownInput').value;
   if (modalita == 'modCom') {
      var url = '/api/VsCommAperteXCli/GetAllComm';
   } else {
      var url = '/api/VsCommAperteXCli/GetCommAllByCliAsync/' + cli;
   }
   try {
      const response = await fetch(url, {
         method: 'GET', // *GET, POST, PUT, DELETE, etc.
         mode: 'cors', // no-cors, *cors, same-origin
         cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
         credentials: 'same-origin', // include, *same-origin, omit
         headers: {
            'Content-Type': 'application/json',
            // 'Content-Type': 'application/x-www-form-urlencoded',
         },
      });

      if (response.status == 200) {
         let cliData = await response.json();
         let cliNumber = cliData.length;

         document.getElementById('dropdown-commesse').innerHTML = '';
         const opt = document.createElement('p');
         opt.className = 'm-0 dropdown-opt';
         opt.setAttribute('valore', ``);
         opt.innerText = '';
         opt.style.height = '22px';
         document.getElementById('dropdown-commesse').appendChild(opt);
         opt.addEventListener('click', () => {
            document.getElementById('ComDropdownInput').value = opt.getAttribute('valore');
         });
         for (var i = 0; i < cliNumber; i++) {
            if (
               modalita == 'modCli' ||
               (modalita == 'modGestInt' && 'FGZ'.includes(cliData[i].orpbPrfDoc)) ||
               (modalita == 'modSvilInt' && 'AH'.includes(cliData[i].orpbPrfDoc)) ||
               modalita == 'modCom'
            ) {
               const opt = createOptCrrg(cliData[i]);
               document.getElementById('dropdown-commesse').appendChild(opt);
            }
         }

         addHoverClassToOpt(document.getElementById('dropdown-commesse').children[0]);
      } else {
         return {
            res: null,
            statusCode: response.status,
            message: 'ISL non trovata',
         };
      }
   } catch (ex) {
      console.error(ex);
   }
}

//per valorizzare le options dei clienti che vengono create direttamente da c# alla creazione della pagina
function cliOpt(value, description) {
   document.getElementById('description-clienti').value = description;
   document.getElementById('CliDropdownInput').value = value;
   //document.getElementById("CliDropdownInput").value = value;
   document.getElementById('dropdown-clienti').classList.add('d-none');
   (async () => await CliListChanged('modCli'))();
   const input = document.getElementById('CliDropdownInput');
   filterDropdown(input, 'dropdown-clienti');
}

async function CommListChanged() {
   await LoadTOper(document.getElementById('ComDropdownInput').value);
}

function onEnterFnCrrg(selected) {
   const value = selected.getAttribute('valore');
   if (selected.getAttribute('descrizione')) {
      document.getElementById('description-' + dropdownId.split('-')[1]).value =
         selected.getAttribute('descrizione');
   }
   return value;
}

function createOptCrrg(cliData) {
   const ndoc = cliData.orpbNDoc.toString().padStart(6, '0');
   const valore = `${cliData.orpbTstDoc}/${cliData.orpbPrfDoc}/${cliData.orpbADoc}/${ndoc}`;
   const text = cliData.commDescDd;
   const afterOptCreate = async (opt) => {
      opt.addEventListener('mousedown', async () => {
         document.getElementById('ComDropdownInput').value = opt.getAttribute('valore');
         document.getElementById('dropdown-commesse').classList.add('d-none');
         await CommChanged();
         const input = document.getElementById('ComDropdownInput');
         filterDropdown(input, 'dropdown-commesse');
      });
   };
   return createOpt(valore, text, afterOptCreate);
}

async function LoadTOper(ComDropdownInput) {
   var url = '/api/Olca/GetOlcaCitoByCommAsync/' + ComDropdownInput;
   try {
      const response = await fetch(url, {
         method: 'GET', // *GET, POST, PUT, DELETE, etc.
         mode: 'cors', // no-cors, *cors, same-origin
         cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
         credentials: 'same-origin', // include, *same-origin, omit
         headers: {
            'Content-Type': 'application/json',
            // 'Content-Type': 'application/x-www-form-urlencoded',
         },
      });

      if (response.status == 200) {
         let operData = await response.json();
         let operList = operData.olcaCitoList;
         let operNumber = operList.length;
         document.getElementById('NTOper').empty;
         if (operNumber > 1) {
            options = '<option value=""></option>';
            for (var i = 0; i < operNumber; i++) {
               options +=
                  '<option value="' +
                  operList[i].flussoOlca.olcaNOper +
                  '-' +
                  operList[i].flussoOlca.olcaTOper +
                  '">' +
                  operList[i].flussoOlca.olcaNOper +
                  ' - ' +
                  operList[i].flussoOlca.olcaTOper +
                  ' - ' +
                  operList[i].flussoCito.citoDescrizione +
                  '</option>';
            }
         } else {
            options = `<option value="${operList[0].flussoOlca.olcaNOper}-${operList[0].flussoOlca.olcaTOper}">${operList[0].flussoOlca.olcaNOper}-${operList[0].flussoOlca.olcaTOper}-${operList[0].flussoCito.citoDescrizione}</option>`;
            document.getElementById('NTOper').innerHTML = options;
            document.getElementById(
               'NTOper'
            ).value = `${operList[0].flussoOlca.olcaNOper}-${operList[0].flussoOlca.olcaTOper}`;
            return;
         }

         if (document.getElementById('operazione').innerHTML == '') {
            document.getElementById('operazione').innerHTML = document.getElementById('operazione1').innerHTML;
         }

         document.getElementById('NTOper').innerHTML = options;
         document.getElementById('NTOper').value = document.getElementById('operazione').innerHTML;
      } else {
         return {
            res: null,
            statusCode: response.status,
            message: 'Operazioni non trovate per la commessa',
         };
      }
   } catch (ex) {
      console.error(ex);
   }
}

//riceve l'input da cui prende il testo e l'id della dropdown da filtrare
function filterDropdown(input, dropdownId) {
   const value = input.value;
   const dropdown = document.getElementById(dropdownId);
   const elements = Array.from(dropdown.children);

   elements.forEach((el) => {
      const elementText = el.innerText.toUpperCase().replaceAll(' ', '');

      if (!elementText.includes(value.toUpperCase().replaceAll(' ', ''))) {
         el.classList.add('d-none');
      } else {
         el.classList.remove('d-none');
      }
   });

   //filtra le opzioni della dropdown, se non ne trova significa che il valore dell'input non è presente nella dropdown quindi fa il bordo rosso
   const isValueAccepted = elements.filter((x) => x.getAttribute('valore') == value);
   if (isValueAccepted.length < 1) {
      input.classList.add('bad-field');
   } else {
      input.classList.remove('bad-field');
   }

   //aggiunge l'hover al primo elemento di quelli visibili
   addHoverClassToOpt(elements.find((x) => !x.classList.contains('d-none')));
}

document.getElementById('modCom').checked = true;
MomoModalita = document.getElementById('MemoModalita').value;
switch (true) {
   case MomoModalita == 'modIsl':
      document.getElementById('modIsl').checked = true;
      break;
   case MomoModalita == 'modCom':
      document.getElementById('modCom').checked = true;
      break;
   case MomoModalita == 'modCli':
      document.getElementById('modCli').checked = true;
      break;
   case MomoModalita == 'modSvilInt':
      document.getElementById('modSvilInt').checked = true;
      break;
   case MomoModalita == 'modGestInt':
      document.getElementById('modGestInt').checked = true;
      break;
}

document.getElementById('modIsl').disabled = false;
document.getElementById('modCli').disabled = false;
document.getElementById('modGestInt').disabled = false;
document.getElementById('modSvilInt').disabled = false;
document.getElementById('modCom').disabled = false;

document.addEventListener('DOMContentLoaded', async function () {
   document.getElementById('modIsl').addEventListener('change', async () => await Modalita());
   document.getElementById('modCli').addEventListener('change', async () => await Modalita());
   document.getElementById('modGestInt').addEventListener('change', async () => await Modalita());
   document.getElementById('modSvilInt').addEventListener('change', async () => await Modalita());
   document.getElementById('modCom').addEventListener('change', async () => await Modalita());
   document.getElementById('input-risorsa').addEventListener('input', (e) => {
      e.target.value = e.target.value.toUpperCase();
   });
   document.getElementById('CrrgTmRunIncrHMS').addEventListener('input', (e) => {
      console.log(e);
      document.getElementById('CrrgTmRunIncrHMSProd').value = e.target.value;
   });

   const onInputFocusout = async (e, dropdownInputId) => {
      if (!e.target.classList.contains('bad-field') && e.target.value != '') {
         if (dropdownInputId == 'ComDropdownInput') {
            await CommChanged();
         } else {
            await CliListChanged('modCli');
         }
      }
   };

   setupCustomDropdowns('ComDropdownInput', 'dropdown-commesse', onInputFocusout, filterDropdown, onEnterFnCrrg);
   setupCustomDropdowns('CliDropdownInput', 'dropdown-clienti', onInputFocusout, filterDropdown, onEnterFnCrrg);

   await Modalita();
});
