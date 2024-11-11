/**
 * Setta gli event listener necessari per far funzionare la dropdown ma non gestisce la funzione per la creazione delle opzioni
 * @param {string} dropdownInputId - Id dell'input su cui fare la ricerca
 * @param {HTMLOP} options - Id dell'input su cui fare la ricerca
 * @param {Function?} fnSearch - Funzione di ricerca tra le options della dropdown
 */
function setupCustomDropdownsV2(dropdownInputId, options, fnSearch) {
   const input = document.getElementById(dropdownInputId);
   const borderStyle = document.createElement('div');
   borderStyle.classList.add('border-style');
   const dropdownContainer = document.createElement('div');
   const containerId = 'dropdown-' + input.id;
   dropdownContainer.id = containerId;
   dropdownContainer.classList.add(
      'd-none',
      'customDropdown-group',
      'dropdown-scrollbar',
      'customDropdown-container'
   );
   borderStyle.appendChild(dropdownContainer);
   input.parentElement.appendChild(borderStyle);

   options.unshift({ values: '[]', text: '' });
   options.forEach((opt) => dropdownContainer.appendChild(createOpt(opt.values, opt.text, null, dropdownInputId)));
   setupCustomDropdowns(dropdownInputId, containerId, null, fnSearch, null);
}

/**
 * Setta gli event listener necessari per far funzionare la dropdown ma non gestisce la funzione per la creazione delle opzioni
 * @param {string} dropdownInputId - Id dell'input su cui fare la ricerca
 * @param {string} dropdownContainerId - Id del container che conterrà le opzioni
 * @param {Function?} onInputFocusout - Funzione da eseguire al focusout dell'input
 */
function setupCustomDropdowns(dropdownInputId, dropdownContainerId, onInputFocusout, fnSearch, onEnterFn) {
   //apre la dropdown al focus sull'input
   document.getElementById(dropdownInputId).addEventListener('focusin', (e) => {
      //se all'apertura c'è già un valore nell'input viene filtrata la dropdown
      if (e.target.value != '') {
         fnSearch?.(e.target, dropdownContainerId);
      }
      showDropdown(dropdownContainerId);
   });

   document.getElementById(dropdownInputId).addEventListener('click', (e) => {
      if (document.activeElement.id == e.target.id) {
         showDropdown(dropdownContainerId);
      }
   });

   //se viene fatto click fuori dalla dropdown viene chiusa
   document.addEventListener('mousedown', (e) => {
      if (!e.target.classList.contains('customDropdown-group')) {
         hideDropdown(dropdownContainerId);
      }
   });

   //se l'input delle commesse della dropdown perde il focus la dropdown viene nascosta e viene chiamata la funzione che carica le operazioni
   document.getElementById(dropdownInputId).addEventListener('focusout', async (e) => {
      hideDropdown(dropdownContainerId);
      await onInputFocusout?.(e, dropdownInputId);
   });

   //se viene scritto qualcosa nell'input delle commesse della dropdown, il valore viene scritto anche nei veri input delle commesse
   document.getElementById(dropdownInputId).addEventListener('input', async (e) => {
      //e.target.value = e.target.value.toUpperCase()
      if (
         document.querySelector(
            '.customDropdown-group.dropdown-scrollbar.customDropdown-container.active-custom-dropdown.d-none'
         )
      )
         showDropdown(dropdownContainerId);
      document.getElementById(dropdownInputId).value = e.target.value;
      fnSearch?.(e.target, dropdownContainerId);
   });

   //per gestire i tasti su, giu e invio
   document.getElementById(dropdownInputId).addEventListener('keydown', (e) => {
      handleKeys(e, dropdownContainerId, dropdownInputId, onEnterFn, fnSearch);
   });
}

//per creare le options delle commesse che vengono create quando viene inserito il cliente
function createOpt(valore, text, afterOptCreate, inputId) {
   const opt = document.createElement('p');
   opt.className = 'm-0 customDropdown-group dropdown-opt';
   opt.setAttribute('valore', valore);
   opt.innerText = text;
   opt.addEventListener('mousedown', (e) => {
      const input = document.getElementById(inputId);
      input.value = e.target.innerText;
   });
   afterOptCreate?.(opt);
   return opt;
}

function showDropdown(containerId) {
   const dropdown = document.getElementById(containerId);
   dropdown.classList.remove('d-none');
   dropdown.classList.add('active-custom-dropdown');
   addHoverClassToOpt(Array.from(dropdown.children).find((x) => !x.classList.contains('d-none')));
   dropdown.addEventListener('mousemove', (e) => {
      if (e.target.classList.contains('dropdown-opt')) {
         addHoverClassToOpt(e.target);
      }
   });
}

//gestisce l'aggiunta della classe per l'effetto di 'riga selezionata'
function addHoverClassToOpt(target) {
   if (!target) {
      return;
   }
   Array.from(document.getElementsByClassName('opt-hover')).forEach((x) => x.classList.remove('opt-hover'));
   target.classList.add('opt-hover');
}

function hideDropdown(containerId) {
   if (!containerId) {
      return;
   }
   document.getElementById(containerId).classList.add('d-none');
}

//gestisce freccia su, giu e invio sulla dropdown
function handleKeys(e, dropdownId, inputId, onEnterFn, fnSearch) {
   const dropdown = document.getElementById(dropdownId);
   const input = document.getElementById(inputId);
   const shownElements = Array.from(dropdown.querySelectorAll('.dropdown-opt:not(.d-none)'));

   //se non ci sono elementi visibili esce
   if (shownElements < 1) {
      return;
   }
   const optionHeight = shownElements[0].getBoundingClientRect().height;
   let selectedIndex = 0;
   if (e.keyCode == 40) {
      //freccia giu
      for (let i = 0; i < shownElements.length; i++) {
         if (i == shownElements.length - 1) {
            selectedIndex = i;
            break;
         }
         if (shownElements[i].classList.contains('opt-hover')) {
            addHoverClassToOpt(shownElements[i + 1]);
            selectedIndex = i + 1;
            break;
         }
      }
   } else if (e.keyCode == 38) {
      //freccia su
      for (let i = shownElements.length - 1; i > 0; i--) {
         if (i == 0) {
            selectedIndex = 0;
            break;
         }
         if (shownElements[i].classList.contains('opt-hover')) {
            addHoverClassToOpt(shownElements[i - 1]);
            selectedIndex = i - 1;
            break;
         }
      }
   } else if (e.keyCode == 13) {
      //invio
      if (
         document.querySelector(
            '.customDropdown-group.dropdown-scrollbar.customDropdown-container.active-custom-dropdown.d-none'
         )
      )
         return;
      e.preventDefault();
      const selected = shownElements.find((x) => x.classList.contains('opt-hover'));
      const value = onEnterFn?.(selected) ?? selected.innerText;
      input.value = value;
      fnSearch?.(e.target, dropdownId);
      hideDropdown(dropdownId);
   }
   //else if (e.keyCode == 9) {
   //    //tab
   //    e.preventDefault();
   //    const value = shownElements.find((x) => x.classList.contains("opt-hover")).getAttribute("valore");
   //    input.value = value;
   //    document.getElementById(inputIdToSet).value = value;
   //    filterDropdown(e.target, dropdownId);
   //    hideDropdown(dropdownId);
   //}

   //fa scorrere in giù la scrollbar se l'opzione selezionata è sotto
   const selectedOptionOffset = selectedIndex * optionHeight;
   const containerHeight = dropdown.offsetHeight;

   if (selectedOptionOffset < dropdown.scrollTop) {
      dropdown.scrollTop = selectedOptionOffset;
   } else if (selectedOptionOffset + optionHeight > dropdown.scrollTop + containerHeight) {
      dropdown.scrollTop = selectedOptionOffset + optionHeight - containerHeight;
   }
}

/**
 * Restituisce un identificativo univoco
 * @returns {string} Un identificativo univoco
 */
function getUid() {
   return (Math.random() + 1).toString(36).substring(2);
}
