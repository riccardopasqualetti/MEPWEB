// [231009] Pagina "Importa da Teamwork": cambio periodo, import e allineamento senza ricaricare la pagina.
// Il server restituisce le griglie già pronte (partial _Griglie) e, per le azioni, un JSON con l'esito.
(function () {
    'use strict';

    const contenitore = document.getElementById('tw-griglie');
    if (!contenitore) return;

    const formPeriodo = document.getElementById('tw-form-periodo');
    const area = document.getElementById('tw-area');
    const spinner = document.getElementById('tw-spinner');
    const btnAggiorna = document.getElementById('tw-btn-aggiorna');
    const urlGriglie = contenitore.dataset.urlGriglie;
    const modaleEsito = new bootstrap.Modal(document.getElementById('tw-modal-esito'));
    const modaleConferma = new bootstrap.Modal(document.getElementById('tw-modal-conferma'));

    // ------------------------------------------------------------------ utilità

    function periodo() {
        return { dal: formPeriodo.elements['dal'].value, al: formPeriodo.elements['al'].value };
    }

    /** Griglie attenuate con spinner sopra; il pulsante Aggiorna mostra lo spinner ed è disattivato. */
    function occupato(attivo) {
        area.classList.toggle('tw-occupato', attivo);
        spinner.classList.toggle('d-none', !attivo);
        btnAggiorna.disabled = attivo;
    }

    function testo(s) {
        const span = document.createElement('span');
        span.textContent = s == null ? '' : String(s);
        return span.innerHTML;
    }

    /** Notifica breve in basso a destra: le conferme spariscono da sole, gli errori restano finché non si chiudono. */
    function notifica(messaggio, ok) {
        const el = document.createElement('div');
        el.className = 'toast align-items-center border-0 ' + (ok ? 'text-bg-success' : 'text-bg-danger');
        el.setAttribute('role', ok ? 'status' : 'alert');
        el.innerHTML =
            '<div class="d-flex"><div class="toast-body">' + testo(messaggio) + '</div>' +
            '<button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Chiudi"></button></div>';
        document.getElementById('tw-toast-container').appendChild(el);
        el.addEventListener('hidden.bs.toast', function () { el.remove(); });
        new bootstrap.Toast(el, { autohide: ok, delay: 5000 }).show();
    }

    /** Fetch che riconosce la sessione scaduta (il filtro di sessione risponde con la pagina di login). */
    async function chiama(url, opzioni) {
        const risposta = await fetch(url, Object.assign({ credentials: 'same-origin' }, opzioni));
        if (risposta.redirected && /\/login\/login/i.test(risposta.url)) {
            throw new Error('Sessione scaduta: ricarica la pagina ed effettua di nuovo l\'accesso.');
        }
        if (!risposta.ok) {
            throw new Error('Errore del server (' + risposta.status + ').');
        }
        return risposta;
    }

    async function postForm(form) {
        const risposta = await chiama(form.action, { method: 'POST', body: new FormData(form) });
        const tipo = risposta.headers.get('content-type') || '';
        if (tipo.indexOf('application/json') < 0) {
            throw new Error('Risposta inattesa dal server: ricarica la pagina.');
        }
        return risposta.json();
    }

    // ------------------------------------------------------------------ griglie

    async function aggiornaGriglie() {
        const p = periodo();
        occupato(true);
        try {
            const url = urlGriglie + '?dal=' + encodeURIComponent(p.dal) + '&al=' + encodeURIComponent(p.al);
            const risposta = await chiama(url, { headers: { 'Accept': 'text/html' } });
            contenitore.innerHTML = await risposta.text();
            // L'indirizzo della pagina segue il periodo, così un aggiornamento del browser mostra lo stesso periodo
            history.replaceState(null, '', formPeriodo.action + '?dal=' + encodeURIComponent(p.dal) + '&al=' + encodeURIComponent(p.al));
        } catch (e) {
            notifica(e.message, false);
        } finally {
            occupato(false);
        }
    }

    formPeriodo.addEventListener('submit', function (ev) {
        ev.preventDefault();
        aggiornaGriglie();
    });

    // ------------------------------------------------------------------ import

    function mostraEsitoImport(esito) {
        const importate = esito.importate || [];
        const errori = esito.errori || [];

        const boxOk = document.getElementById('tw-esito-importate');
        const listaOk = document.getElementById('tw-esito-importate-lista');
        listaOk.innerHTML = importate.map(function (r) {
            return '<li class="py-1 border-bottom d-flex align-items-center gap-2">' +
                '<span class="flex-grow-1">' + testo(r.nome) + '</span>' +
                '<span class="text-nowrap fw-bold">→ <span class="badge text-bg-light fs-6 align-middle">' + testo(r.crrgCSrl) + '</span></span>' +
                '</li>';
        }).join('');
        document.getElementById('tw-esito-importate-n').textContent = importate.length;
        boxOk.classList.toggle('d-none', importate.length === 0);

        const boxKo = document.getElementById('tw-esito-errori');
        document.getElementById('tw-esito-errori-lista').innerHTML = errori.map(function (e) { return '<li class="py-1">' + testo(e) + '</li>'; }).join('');
        document.getElementById('tw-esito-errori-n').textContent = errori.length;
        boxKo.classList.toggle('d-none', errori.length === 0);

        modaleEsito.show();
    }

    async function importa(form) {
        if (!form.querySelector('.tw-sel:checked')) {
            notifica('Seleziona almeno una registrazione da importare.', false);
            return;
        }
        occupato(true);
        try {
            const esito = await postForm(form);
            if (esito.errore) {
                notifica(esito.errore, false);
                return;
            }
            mostraEsitoImport(esito);
        } catch (e) {
            notifica(e.message, false);
            return;
        } finally {
            occupato(false);
        }
        await aggiornaGriglie();
    }

    // ------------------------------------------------------------------ allineamento

    let formDaConfermare = null;

    async function allinea(form) {
        occupato(true);
        try {
            const esito = await postForm(form);
            notifica(esito.messaggio, esito.ok);
            if (!esito.ok) return;
        } catch (e) {
            notifica(e.message, false);
            return;
        } finally {
            occupato(false);
        }
        await aggiornaGriglie();
    }

    document.getElementById('tw-conferma-ok').addEventListener('click', function () {
        const form = formDaConfermare;
        formDaConfermare = null;
        modaleConferma.hide();
        if (form) allinea(form);
    });

    // ------------------------------------------------------------------ eventi sulle griglie (ricaricate dinamicamente)

    contenitore.addEventListener('submit', function (ev) {
        const form = ev.target;
        if (form.id === 'tw-form-import') {
            ev.preventDefault();
            importa(form);
        } else if (form.classList.contains('tw-form-allinea')) {
            ev.preventDefault();
            if (form.dataset.conferma) {
                formDaConfermare = form;
                document.getElementById('tw-conferma-testo').textContent = form.dataset.conferma;
                modaleConferma.show();
            } else {
                allinea(form);
            }
        }
    });

    contenitore.addEventListener('change', function (ev) {
        if (ev.target.id === 'tw-sel-tutte') {
            contenitore.querySelectorAll('.tw-sel').forEach(function (c) { c.checked = ev.target.checked; });
        }
    });
})();
