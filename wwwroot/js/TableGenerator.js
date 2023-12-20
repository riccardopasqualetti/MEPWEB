
/**
 * 
 * @class
 * @classdesc Classe che gestisce la generazione della tabella e delle modali per l'API specificata nei parametri passati al costruttore.
 */
export class TableGenerator {

    /**
     * Costruttore della classe
     * @param  {types.ParamObject} paramObject - Oggetto contentente tutti i parametri necessari per la generazione.
     */
    constructor(paramObject) {
        /**
         * Proprietà privata della classe che contiene il numero della pagina corrente, inizialiazzato a 1.
         * @type {number}
         */
        this.currentPage = 1;
        this.paramObject = paramObject;
        /**
         * Proprietà della classe che contiene i parametri aggiuntivi.
         * @type {types.AddParamObject}
         */
        this.addParamObj = {}
        /**
         * Proprietà privata della classe che contiene la response dell'API in modo che sia globale.
         * @type {object}
         */
        this.apiResponse;
        this.maxPageButtons = 5;
        this.isTableCreated = false;
    }

    async logon() {
        const urlParams = {
            "grant_type": "password",
            "username": this.paramObject.credentials.username,
            "password": this.paramObject.credentials.password,
            "client_id": "test",
            "client_secret": "3700C5A7-DA54-4C60-802C-B879AC960335",
            "company": this.paramObject.credentials.company
        }

        const settings = {
            method: "POST",
            mode: "cors",
            body: new URLSearchParams(urlParams)
        }

        const url = `${this.paramObject.apiUrl.get.url[0]}api/Login/LoginSCC`
        const re = await this.fetchApi(url, settings)
        return re
    }

    /**
     * @function destroyTable
     * @description Metodo della classe TableGenerator che cancella la tabella.
     * @memberof destroyTable
     */

    destroyTable() {
        document.getElementById("tgen").innerHTML = ""
        document.getElementById("createRecord").remove();
        document.getElementById("updateRecord").remove();
        document.getElementById("deleteRecord").remove();
    }

    async openCreateModal() {
        let modal = new bootstrap.Modal("#createRecord");
        modal.show();
    }

    /**
     * @function destroyTable
     * @description Metodo della classe TableGenerator che fa comparire la modale di edit.
     * @param {Object} editObject - Oggetto con chiavi corrispondenti ai nomi dei campi presenti all'interno della modale di modifica(oltre che a quelli dell'api) e i valori sono quelli che verranno caricati nei campi
     * @memberof modalHandler
     */

    async openUpdateModal(editObject) {
        let modal = new bootstrap.Modal("#updateRecord");
        modal.show();
        for (const key in editObject) {
            const input = modal._dialog.querySelector(`*[name="${key}"]`)
            const field = this.paramObject.fields.find(field => field.apiName == key && field.modals != false)
            console.log("key: " + key)
            if (field.type == "checkbox") {
                input.checked = editObject[key] == "S"
            }
            else if (field.type == "decimalColor") {
                const color = this.getRGBFromNumber(editObject[key])
                input.value = editObject[key];
                input.style.backgroundColor = `rgb(${color.red},${color.green},${color.blue})`
                input.addEventListener("change", (e) => {
                    const color = this.getRGBFromNumber(e.target.value)
                    e.target.style.backgroundColor = `rgb(${color.red},${color.green},${color.blue})`
                })
            }
            else if (field.type == "datenormal") {

                let date = editObject[key] == null ? "null" : editObject[key].split("/").reverse().join("-")
                input.valueAsDate = new Date(date)

            } else if (field.type == "datelong") {

                let date = editObject[key] == null ? "null" : editObject[key].split("/").reverse().join("-")
                input.valueAsDate = new Date(date)

            } else {
                const mandatoryField = Array.from(modal._dialog.querySelectorAll(`#updateDialog *[name="${key}"]`)).find(x => x.type == "hidden")
                if (mandatoryField) mandatoryField.value = editObject[key]
                input.value = editObject[key] == "null" ? "" : editObject[key]
            }
        }
    }

    async openDeleteModal(deleteObj, name) {
        const nome = name ? name : ""
        let modal = new bootstrap.Modal("#deleteRecord");
        modal.show();
        modal._dialog.querySelector("#deleteText").innerText = "Confermare l'eliminazione del record"
        for (const key of this.paramObject.apiUrl.delete.param) {
            const input = modal._dialog.querySelector(`*[name="${key}"]`)
            input.value = deleteObj[key]
            
        }
    }

    /**
     * @function createModals
     * @description Metodo della classe TableGenerator che crea le modali e le finestre di ricerca per i rispettivi campi e gestisce i form di creazione, aggiornamento ed eliminazione.
     * @memberof createModals
     */

    async createModals() {

        /**
         * @function fetchGetResearchApi
         * @description Funzione interna a createModals, serve a chiamare l'API per ottenere i dati per popolare la finestra di ricerca.
         * @memberof createModals
         * @param {string} fieldUrl - URL per il campo interessato per il quale si vuole la finestra di ricerca.
         */

        const fetchGetResearchApi = async (fieldUrl) => {
            let settings = {
                method: "GET",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + this.paramObject.authToken
                }
            }

            let url = `${this.paramObject.apiUrl.get.url[0] + fieldUrl}`

            return await this.fetchApi(url, settings)
        }

        const fetchPutApi = async (requestBody) => {
            let settings = {
                method: "PUT",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + this.paramObject.authToken
                },
                body: JSON.stringify(requestBody)
            }
            let url = `${this.paramObject.apiUrl.put.url[0] + this.paramObject.apiUrl.put.url[1]}`
            let res = await this.fetchApi(url, settings)
            console.log(url)
            console.log(res)

            return res
        }

        const fetchPostApi = async (requestBody) => {
            let settings = {
                method: "POST",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + this.paramObject.authToken
                },
                body: JSON.stringify(requestBody)
            }
            let url = `${this.paramObject.apiUrl.post.url[0]}${this.paramObject.apiUrl.post.url[1]}`
            let res = await this.fetchApi(url, settings)
            console.log(url)
            console.log(res)

            return res
        }

        const fetchDeleteApi = async (data) => {
            let settings = {
                method: "DELETE",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + this.paramObject.authToken
                }
            }

            const paramCodes = this.paramObject.apiUrl.delete.param
            let params = ""

            if (this.paramObject.apiUrl.delete.type == "querystring") {
                params += "?"
                for (let i = 0; i < paramCodes.length; i++) {
                    params += `${paramCodes[i]}=${data[paramCodes[i]]}`
                    if (i < paramCodes.length - 1) {
                        params += "&"
                    }
                }
            } else {
                params += "/"
                for (let i = 0; i < paramCodes.length; i++) {
                    params += `${data[paramCodes[i]]}`
                    if (i < paramCodes.length - 1) {
                        params += "/"
                    }
                }
            }
            console.log("params: " + params)

            let url = `${this.paramObject.apiUrl.delete.url[0] + this.paramObject.apiUrl.delete.url[1]}${params}`
            console.log(url)
            let res = await this.fetchApi(url, settings)
            console.log(res)

            return res
        }

        /**
         * @function populateResearchObject
         * @description Funzione interna a createModals, serve a popolare l'oggetto che contiene le response per le finestre di ricerca.
         * @memberof createModals
         */

        const populateResearchObject = async (fieldName) => {
            const field = this.paramObject.fields.filter(field => field.searchUrls && field.apiName == fieldName)[0]
            
            for (const srcParam of field.searchUrls) {
                const refInputs = document.querySelectorAll(`.modal *[name="${srcParam.field}"]`)
                /* for (const input of refInputs) {

                    const livello = srcParam.ref(document.querySelector(`select[name="${srcParam.field}"]`).value)
                    const url = srcParam.url
                    researchResponses[field.apiName] = await fetchGetResearchApi(url + "/" + livello)
                } */
                if (refInputs.length == 0) {
                    if (researchResponses[field.apiName]) return
                    researchResponses[field.apiName] = await fetchGetResearchApi(srcParam.url)
                }
            }
            
        }

        //da rivedere in un futuro
        const disableCilyInput = (livelloPassato) => {
            const livello = livelloPassato || this.addParamObj.livello
            document.querySelectorAll(`select[name="livello"]`).forEach(x => {
                x.value = livello
            })
            if (livello == "1") {
                document.querySelectorAll(`input[name="codicePadre"]`).forEach(x => x.disabled = true)
            } else {
                document.querySelectorAll(`input[name="codicePadre"]`).forEach(x => x.disabled = false)
            }
        }

        /**
         * @function generateResearchWindow
         * @description Crea la finestra di ricerca per l'input desiderato.
         * @param {HTMLDivElement} inputContainer - Elemento che contiene l'input.
         * @param {types.SearchUrls} field - Campo passato dal parmObject con le informazioni per il campo per cui si vuole la ricerca.
         * @param {HTMLInputElement} inputField - Campo input della modale per il quale si vuole la finestra di ricerca. 
         * @param {string} modalId - Id della modale (e.g. create update)
         * @memberof createModals
         */

        const generateResearchWindow = async (inputContainer, field, inputField, modalId) => {

            const wrap = document.createElement("div")
            wrap.className = "reswrap"
            inputContainer.appendChild(wrap)

            const research = document.createElement("div")
            research.className = "research-div"
            wrap.classList.toggle("d-none")
            wrap.appendChild(research)

            const row = document.createElement("div")
            row.className = "row"
            research.appendChild(row)
            const col = document.createElement("div")
            col.className = "col"
            row.appendChild(col)
            const chiudi = document.createElement("div")
            chiudi.backgroundColor = "red"
            chiudi.innerHTML = `<svg xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 512 512"><!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. --><path d="M256 512A256 256 0 1 0 256 0a256 256 0 1 0 0 512zM175 175c9.4-9.4 24.6-9.4 33.9 0l47 47 47-47c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9l-47 47 47 47c9.4 9.4 9.4 24.6 0 33.9s-24.6 9.4-33.9 0l-47-47-47 47c-9.4 9.4-24.6 9.4-33.9 0s-9.4-24.6 0-33.9l47-47-47-47c-9.4-9.4-9.4-24.6 0-33.9z"/></svg>`
            chiudi.className = "close-research"
            chiudi.addEventListener("click", () => {
                wrap.classList.toggle("d-none")
            })
            col.appendChild(chiudi)

            const row1 = document.createElement("div")
            row1.className = "row"
            research.appendChild(row1)
            const col1 = document.createElement("div")
            col1.className = "col g-0"
            col1.style.marginLeft = "1rem"
            row1.appendChild(col1)

            const table = document.createElement("table")
            table.className = "table table-striped"
            col1.appendChild(table)
            const thead = document.createElement("thead")
            table.appendChild(thead)
            const tr = document.createElement("tr")
            thead.appendChild(tr)
            const campi = ["Codice", "Descrizione"]
            campi.forEach(fi => {
                const th = document.createElement("th")
                th.innerText = fi
                tr.appendChild(th)
            })

            const tbody = document.createElement("tbody")
            tbody.id = "research-table-body"
            table.appendChild(tbody)

            const row2 = document.createElement("div")
            row2.className = "row"
            research.appendChild(row2)
            const col2 = document.createElement("div")
            col2.className = "col g-0"
            col2.style.marginLeft = "1rem"
            row2.appendChild(col2)

            let researchLoader = document.createElement('span')
            researchLoader.id = "rLoader"
            researchLoader.className = "layout-loader d-block"
            col2.appendChild(researchLoader)

            const modal = document.querySelector(`#${modalId}`)
            inputContainer.appendChild(wrap)
            inputField.addEventListener("focus", async (e) => { //quimod

                //controllo che la modale abbia l'attributo "shown" impostato a true prima di visualizzare la researchWindow in modo da poter calcolare l'altezza giusta
                //l'attributo shown della modale viene impostato true quando ha finito l'animazione di apertura, mentre false quando viene chiusa
                if (modal.getAttribute("shown") == "true") {
                    wrap.classList.remove("d-none")
                    const modalDimensions = modal.getBoundingClientRect()
                    const isWrapHeightOutside = wrap.getBoundingClientRect().height + wrap.getBoundingClientRect().y
                    if (modalDimensions.height < isWrapHeightOutside) {
                        wrap.style.transform = `translateY(${modalDimensions.height - isWrapHeightOutside}px)`
                    }
                    const isWrapWidthOutside = wrap.getBoundingClientRect().width + wrap.getBoundingClientRect().x
                    if (modalDimensions.width < isWrapWidthOutside) {
                        wrap.style.transform = `translateX(${modalDimensions.width - isWrapWidthOutside}px)`
                    }

                    await populateResearchObject(e.target.name)
                    researchLoader.classList.remove("d-block")
                    tbody.innerHTML = ""
                    populateResearchWindow(field, tbody, inputField)
                }
            })
            inputField.classList.add("research-input")
        }

        const populateResearchWindow = (field, researchBody) => {
            researchBody = researchBody || document.getElementById("research-table-body")

            researchResponses[field.apiName].forEach((x, i) => {
                const tr = document.createElement("tr")
                tr.tabIndex = i

                tr.addEventListener("focus", (e) => {
                    inputField.value = e.target.children[0].textContent
                    wrap.classList.add("d-none")
                })
                researchBody.appendChild(tr)

                field.searchUrls[0].searchFieldNames.forEach(f => {
                    const td = document.createElement("td")
                    td.innerText = x[f]
                    tr.appendChild(td)
                })
            })
        }

        /**
         * @function generateInputs
         * @description Funzione dedicata alla creazione degli input della modale.
         * @param {HTMLDivElement} col - Colonna della modale in cui verrà creato l'input.
         * @param {number} colIndex - Indice della colonna.
         * @param {string} dialogId - Id del modalDialog.
         * @param {string} modalId - Id della modale.
         * @memberof createModals
         */

        const generateInputs = async (col, colIndex, dialogId, modalId) => {

            const index = colIndex * this.paramObject.modals.create.maxFields
            const max = (colIndex * this.paramObject.modals.create.maxFields) + this.paramObject.modals.create.maxFields
            const fields = this.paramObject.fields.filter(x => x.modals != false)

            for (let i = index; i < max; i++) {

                const field = fields[i]
                if (!field) break

                const inputContainer = document.createElement('div')
                inputContainer.style.position = 'relative'
                col.appendChild(inputContainer)
                let label = document.createElement('label')
                label.innerText = field.displayedName.replace(/(\r\n|\n|\r)/gm, "")
                label.classNames = "fs-14"
                label.for = dialogId + "-input" + i
                inputContainer.appendChild(label)
                var inputField;

                switch (field.type) {

                    case "checkbox":
                        let formCheck = document.createElement("div")
                        formCheck.className = "form-check"
                        inputContainer.appendChild(formCheck)

                        inputField = document.createElement("input")
                        inputField.type = "checkbox"
                        inputField.checked = false
                        inputField.className = "form-check-input"
                        formCheck.appendChild(inputField)
                        break

                    case "dropdown":

                        inputField = document.createElement("select")
                        inputField.className = "form-select"
                        inputContainer.appendChild(inputField)

                        for (let opt of Object.entries(field.values)) {
                            const option = document.createElement("option")
                            option.value = opt[0].replace("o", "")
                            option.innerText = opt[1]
                            inputField.appendChild(option)
                        }

                        inputField.addEventListener("change", (e) => {
                            disableCilyInput(e.target.value)
                        })
                        break

                    case "decimalColor":

                        inputField = document.createElement("select")
                        inputField.className = "form-select"
                        inputField.addEventListener("change", (e) => {
                            const rgb = this.getRGBFromNumber(field.values[e.target.value])
                            e.target.style.backgroundColor = `rgb(${rgb.red},${rgb.green},${rgb.blue})`
                        })
                        inputContainer.appendChild(inputField)

                        for (let opt of Object.values(field.values)) {
                            const rgb = this.getRGBFromNumber(opt)
                            const option = document.createElement("option")
                            option.value = opt
                            option.style.backgroundColor = `rgb(${rgb.red},${rgb.green},${rgb.blue})`
                            inputField.appendChild(option)
                        }
                        break

                    case "datelong":

                        inputField = document.createElement("input")
                        inputField.type = "datetime"
                        inputField.className = "form-control"
                        inputContainer.appendChild(inputField)
                        break

                    case "datenormal":

                        inputField = document.createElement("input")
                        let date = new Date()
                        inputField.type = "date"
                        inputField.className = "form-control"
                        inputField.min = "01-01-1800"
                        inputField.max = "01-01-2100"
                        inputField.value = date.toISOString().substring(0, 10)
                        inputContainer.appendChild(inputField)
                        break

                    case "number":

                        inputField = document.createElement("input")
                        inputField.type = "number"
                        inputField.className = "form-control"
                        inputField.value = 0
                        inputContainer.appendChild(inputField)
                        break

                    default:

                        inputField = document.createElement("input")
                        inputField.className = "form-control mb-2"
                        inputField.type = "text"
                        inputContainer.appendChild(inputField)
                }

                if (field.searchUrls) {
                    await generateResearchWindow(inputContainer, field, inputField, modalId)
                }

                inputField.autocomplete = "off"
                inputField.id = dialogId + "input" + i
                inputField.name = field.apiName
                inputField.classList.add(dialogId + "-input")
            }
        }

        /**
         * @function generateFields         
         * @description Funzione dedicata alla creazione delle colonne e degli input della modale.
         * @param {string} dialoglId 
         * @param {string} modalId 
         * @memberof createModals
         */

        const generateFields = async (dialoglId, modalId) => {

            const classi = {
                c1: "max-w-25",
                c2: "max-w-40",
                c3: "max-w-55",
                c4: "max-w-70",
                c6: "max-w-90"
            }
            const dialog = document.querySelector(`#${dialoglId}`);
            const mainRow = document.querySelector(`#${dialoglId} #main-row`);
            const colWidth = 12 / this.paramObject.modals.create.col
            dialog.classList.add(classi["c" + this.paramObject.modals.create.col])

            for (let i = 0; i < this.paramObject.modals.create.col; i++) {

                const col = document.createElement('div')
                col.className = `col-md-${colWidth} px-2`
                col.id = dialoglId + "-col" + i

                await generateInputs(col, i, dialoglId, modalId)

                mainRow.appendChild(col)
            }
        }

        let researchResponses = {};
        const main = document.querySelector("#tgen")

        //<div class="modal fade" id="createRecord" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true"></div>
        let modal = document.createElement("div")
        modal.className = "modal fade user-none"
        modal.id = "createRecord"
        modal.tabIndex = -1
        modal.setAttribute("aria-labelledby", "exampleModalLabel")
        modal.setAttribute("aria-hidden", "true")
        main.appendChild(modal)

        modal.innerHTML =
            `<div class="modal-dialog" id="createDialog">
                <div class="modal-content">
                <form id="create-form">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="createModalTitle">Crea un nuovo record</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row" id="main-row">
                                
                        </div>
                    </div>
                    <div class="modal-footer justify-content-between">
                        <div id="response-message-container-create" class="response-message-container d-flex align-content-center">
                        </div>
                        <div>
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Annulla</button>
                            <button type="submit" class="btn btn-primary" id="submit-form-create">Inserisci record</button>
                        </div>
                    </div>
                    </form>
                </div>
            </div>`

        await generateFields("createDialog", modal.id)

        let form = document.querySelector('#create-form')
        //let submitButton = document.querySelector('#submit-form-create')
        this.paramObject.apiUrl.post.param.forEach(e => {
            let inputCreate = document.createElement("input")
            inputCreate.type = "hidden"
            inputCreate.id = "create-input-" + e
            inputCreate.name = e
            form.appendChild(inputCreate)
        })

        form.addEventListener('submit', async (e) => {
            e.preventDefault();
            const data = new FormData(e.target);
            const value = Object.fromEntries(data.entries());
            console.log(value)

            for (let field of Object.keys(value)) {
                if (value[field] == "null") value[field] = null

                let fi = this.paramObject.fields.find(x => x.apiName == field)
                if (fi.type == "checkbox") {
                    if (value[field] == "on") {
                        value[field] = "S"
                    } else {
                        value[field] = "N"
                    }
                }
            }

            value.codiceDitta = "01"
            let res = await fetchPostApi(value)
            console.log(res)
            const statusResponse = generateResponseMessage(res, "create")
            if (statusResponse == "success") await this.changePage(this.currentPage)
        })

        modal.addEventListener('hidden.bs.modal', async () => {
            modal.querySelector('#main-row').innerHTML = ""
            await generateFields("createDialog", modal.id)
        })


        let deleteModal = document.createElement("div")
        deleteModal.className = "modal fade user-none"
        deleteModal.id = "deleteRecord"
        deleteModal.tabIndex = -1
        deleteModal.setAttribute("aria-labelledby", "deleteRecordLabel")
        deleteModal.setAttribute("aria-hidden", "true")
        main.appendChild(deleteModal)
        //<div class="modal fade" id="deleteRecord" tabindex="-1" aria-labelledby="deleteRecordLabel" aria-hidden="true">
        deleteModal.innerHTML =
            `<div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-blue">
                            <h1 class="modal-title fs-5 text-white" id="deleteRecordLabel"><b>Delete Record</b></h1>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <p class="mb-3" id="deleteText"></p>
                            <div id="response-message-container-delete" class="response-message-container d-flex align-content-center">
                            </div>
                        </div>
                        <form id="delete-record-form" action="" method="">
                            <div class="modal-footer justify-content-between">
                                <div>
                                    <button type="button" id="delete-btn-revert" class="btn btn-danger" data-bs-dismiss="modal">Cancel</button>
                                    <button type="submit" id="delete-btn-yes" class="btn btn-success">Confirm</button>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>`
        
        const deleteForm = document.querySelector('#delete-record-form')
        this.paramObject.apiUrl.delete.param.forEach(x => {
            const inputDelete = document.createElement("input")
            inputDelete.type = "hidden"
            inputDelete.id = "delete-input-" + x
            inputDelete.name = x
            deleteForm.appendChild(inputDelete)
        })
        

        deleteForm.addEventListener('submit', async (e) => {
            e.preventDefault();

            const data = new FormData(e.target);

            const value = Object.fromEntries(data.entries());
            console.log(value)
            let res = await fetchDeleteApi(value)

            const statusResponse = generateResponseMessage(res, "delete")

            if (statusResponse == "success") await this.changePage(this.currentPage)

        })

        //UPDATE modal
        let updateModal = document.createElement("div")
        updateModal.className = "modal fade user-none"
        updateModal.id = "updateRecord"
        updateModal.tabIndex = -1
        updateModal.setAttribute("aria-hidden", "true")
        main.appendChild(updateModal)

        updateModal.innerHTML =
            `<div class="modal-dialog" id="updateDialog">
                <div class="modal-content">
                    <form id="update-form">
                        <div class="modal-header">
                            <h1 class="modal-title fs-5" id="updateModalTitle"></h1>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row" id="main-row">
                                    
                            </div>
                        </div>
                        <div class="modal-footer justify-content-between">
                            <div id="response-message-container-update" class="response-message-container d-flex align-content-center">
                            </div>
                            <div>
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Annulla</button>
                                <button type="submit" class="btn btn-primary" id="submit-form-create">Aggiorna record</button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>`

        document.querySelector("#updateDialog")

        Array.from(document.querySelectorAll(".modal")).forEach(x => {
            x.addEventListener('show.bs.modal', () => {
                disableCilyInput()
                let messageContainer = document.querySelectorAll('.response-message-container');
                messageContainer.forEach(x => x.innerHTML = "")
            })
            //imposto l'attributo shown a true al completamento del caricamento della modale in modo da poterlo controllare in seguito
            x.addEventListener("shown.bs.modal", () => {
                x.setAttribute("shown", true)
            })
            //imposto l'attributo shown a false quando viene nascosta
            x.addEventListener("hidden.bs.modal", () => {
                x.setAttribute("shown", false)
            })
        })

        await generateFields("updateDialog", updateModal.id)

        let updateForm = document.querySelector('#update-form')
        this.paramObject.apiUrl.put.param.forEach(e => {
            let inputUpdate = document.createElement("input")
            inputUpdate.type = "hidden"
            inputUpdate.id = "update-input-" + e
            inputUpdate.name = e
            updateForm.appendChild(inputUpdate)
        })

        updateForm.addEventListener('submit', async (e) => {
            e.preventDefault();
            const data = new FormData(e.target);
            const value = Object.fromEntries(data.entries());
            console.log("data entries value: " + value);

            for (let field of Object.keys(value)) {

                if (value[field] == "null" || this.paramObject.fields.find(x => (x.type == "datelong" || x.type == "datenormal") && x.apiName == field)) value[field] = null
                let fi = this.paramObject.fields.find(x => x.apiName == field)
                if (fi.type == "checkbox") {
                    if (value[field] == "on") {
                        value[field] = "S"
                    } else {
                        value[field] = "N"
                    }
                }
            }

            value.codiceDitta = "01"
            console.log("value2: " + value);
            let res = await fetchPutApi(value)

            const statusResponse = generateResponseMessage(res, "update")

            if (statusResponse == "success") await this.changePage(this.currentPage)
        })

        /**
         * @function generateResponseMessage         
         * @description Funzione dedicata alla lettura dello stato della response e popolazione del campo della modale in cui viene visualizzato l'esito della chiamata.
         * @param {Object} response - Response dell'API. 
         * @param {string} responseMethod - Metodo HTTP con cui viene chiamata.
         * @returns {string} - Stato della risposta (error, success).
         * @memberof createModals
         */

        function generateResponseMessage(response, responseMethod) {

            let messageContainer = document.querySelector('#response-message-container-' + responseMethod);
            messageContainer.innerHTML = ""
            let stato;
            let message = document.createElement("p")
            if (response.status) {
                message.className = "table-error"
                message.innerText = response.detail
                stato = "error"
            } else {

                message.className = "table-success"
                message.innerText = response.response
                stato = "success"
            }

            message.classList.add("m-0")
            messageContainer.appendChild(message)
            return stato
        }
    }

    /**
     * @function bodyGenerator         
     * @description Metodo della classe dedicato alla popolazione del body della tabella.
     * @param {Object} apiRes - Response GET dell'API. 
     * @param {HTMLTableSectionElement} tbody - Body della tabella.
     * @memberof bodyGenerator
     */

    bodyGenerator(apiRes, tbody) {
        /**
         * @function createRow         
         * @description Funzione dedicata alla creazione di una row della tabella.
         * @param {number} index - Indice del ciclo di creazione delle row.
         * @returns {HTMLTableRowElement} - Row creata.
         * @memberof bodyGenerator
         */
        const createRow = (index) => {
            let row = document.createElement("tr")
            row.tabIndex = index
            let rowNumber = document.createElement("td")
            rowNumber.style.fontWeight = "bold"
            rowNumber.innerText = index + 1
            row.appendChild(rowNumber)

            const editTd = document.createElement("td") 
            row.appendChild(editTd)

            const editBtn = document.createElement("button")
            editBtn.id = "editRecordBtn"
            editBtn.className = "btn-general-table"
            editBtn.type = 'button'
            editBtn.innerHTML = `<svg height="16" width="16" viewBox="0 0 512 512"><path d="M471.6 21.7c-21.9-21.9-57.3-21.9-79.2 0L362.3 51.7l97.9 97.9 30.1-30.1c21.9-21.9 21.9-57.3 0-79.2L471.6 21.7zm-299.2 220c-6.1 6.1-10.8 13.6-13.5 21.9l-29.6 88.8c-2.9 8.6-.6 18.1 5.8 24.6s15.9 8.7 24.6 5.8l88.8-29.6c8.2-2.7 15.7-7.4 21.9-13.5L437.7 172.3 339.7 74.3 172.4 241.7zM96 64C43 64 0 107 0 160V416c0 53 43 96 96 96H352c53 0 96-43 96-96V320c0-17.7-14.3-32-32-32s-32 14.3-32 32v96c0 17.7-14.3 32-32 32H96c-17.7 0-32-14.3-32-32V160c0-17.7 14.3-32 32-32h96c17.7 0 32-14.3 32-32s-14.3-32-32-32H96z"/></svg>`
            editBtn.setAttribute("data-bs-toggle", "modal")
            editBtn.setAttribute("data-bs-target", "#updateRecord")
            editBtn.addEventListener("click", () => {
                this.paramObject.apiUrl.put.param.forEach(v => {
                    let updateValue = editBtn.parentElement.parentElement.querySelector(`td[fieldname="${v}"]`).getAttribute("valore")
                    console.log(updateValue)
                    let updateInput = document.querySelector(`#update-input-` + v)
                    updateInput.value = updateValue
                    updateInput.type = "hidden"
                })

                let updateModalTitle = document.querySelector("#updateModalTitle")
                updateModalTitle.innerText = "Update the following record"

                const fields = this.paramObject.fields.filter(x => x.modals != false)

                const values = Array.from(editBtn.parentElement.parentElement.children).filter(x => x.getAttribute("fieldname") && x != null)

                for (let i = 0; i < fields.length; i++) {
                    const td = values.find(v => v.getAttribute("fieldname") == fields[i].apiName)
                    const input = document.querySelector(`#updateDialog *[name="${fields[i].apiName}"]`)
                    const valore = td.getAttribute("valore")

                    if (td.getAttribute("fieldname") == this.paramObject.apiUrl.put.param) {
                        input.disabled = true
                    }
                    if (fields[i].type == "checkbox") {

                        input.checked = valore == "S"
                    }
                    else if (fields[i].type == "decimalColor") {
                        const color = this.getRGBFromNumber(valore)
                        input.style.backgroundColor = `rgb(${color.red},${color.green},${color.blue})`
                        input.addEventListener("change", (e) => {
                            const color = this.getRGBFromNumber(e.target.value)
                            e.target.style.backgroundColor = `rgb(${color.red},${color.green},${color.blue})`
                        })
                    }
                    else if (fields[i].type == "datenormal") {

                        let date = valore.split("/").reverse().join("-")
                        input.valueAsDate = new Date(date)

                    } else {
                        input.value = valore == "null" ? "" : valore
                    }
                }
            })
            editTd.appendChild(editBtn)

            const deleteTd = document.createElement("td")
            row.appendChild(deleteTd)
            
            const deleteBtn = document.createElement("button")
            deleteBtn.id = "deleteRecordBtn"
            deleteBtn.className = "btn-general-table"
            deleteBtn.type = "button"
            deleteBtn.innerHTML = `<svg height="16" width="14" viewBox="0 0 448 512"><path d="M135.2 17.7C140.6 6.8 151.7 0 163.8 0H284.2c12.1 0 23.2 6.8 28.6 17.7L320 32h96c17.7 0 32 14.3 32 32s-14.3 32-32 32H32C14.3 96 0 81.7 0 64S14.3 32 32 32h96l7.2-14.3zM32 128H416V448c0 35.3-28.7 64-64 64H96c-35.3 0-64-28.7-64-64V128zm96 64c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16z"/></svg>`
            deleteBtn.setAttribute("data-bs-toggle", "modal")
            deleteBtn.setAttribute("data-bs-target", "#deleteRecord")
            deleteBtn.addEventListener("click", () => {
                this.paramObject.apiUrl.delete.param.forEach(x => {
                    console.log(x)
                    console.log(deleteBtn.parentElement)
                    const deleteValue = deleteBtn.parentElement.parentElement.querySelector(`td[fieldname="${x}"]`).getAttribute("valore")
                    console.log(deleteValue)
                    const inputDelete = document.querySelector("#delete-input-" + x)
                    inputDelete.value = deleteValue
                    inputDelete.type = "hidden"
                })

                document.querySelector("#deleteText").innerText = "Confermare l'eliminazione del record?"
            })
            deleteTd.appendChild(deleteBtn)

            return row
        }
        let getRGBFromNumber = this.getRGBFromNumber

        for (let i = 0; i < apiRes.length; i++) {

            let row = createRow(i)

            for (let field of this.paramObject.fields) {

                let td = createCell(apiRes[i], field)

                row.appendChild(td)
            }

            tbody.appendChild(row)
            tbody.scrollTop = 0;
            document.querySelector("#lateral-scroll").scrollLeft = 0;
        }

        //this.addMenuListenersToRows()

        /**
         * @function createCell         
         * @description Funzione dedicata alla creazione di una cella di una row.
         * @param {Object} apiData - Response GET dell'API.
         * @param {types.FieldParameters} field - Configurazione del campo da creare dall'oggetto di configurazione iniziale.
         * @returns {HTMLTableCellElement} - Cella creata.
         * @memberof bodyGenerator
         */

        function createCell(apiData, field) {
            let td = document.createElement("td")
            td.setAttribute("fieldName", field.apiName)
            td.setAttribute("valore", apiData[field.apiName])

            switch (field.type) {

                case "checkbox":
                    return createCheckbox(apiData[field.apiName], field.class)

                case "dropdown":
                    td.innerText = field.values["o" + apiData[field.apiName]] || ""
                    break

                case "decimalColor":
                    let rgb = getRGBFromNumber(apiData[field.apiName])
                    td.style.backgroundColor = `rgb(${rgb.red},${rgb.green},${rgb.blue})`
                    break

                case "datelong":
                    td.innerText = formatDatelong(apiData[field.apiName])
                    td.setAttribute("valore", td.innerText)
                    break

                case "datenormal":
                    td.innerText = formatDatenormal(apiData[field.apiName])
                    td.setAttribute("valore", td.innerText)
                    break

                case "hidden":
                    td.classList.add("d-none")
                    break

                default:
                    td.innerText = apiData[field.apiName]

            }

            if (field.class) td.className += ` ${field.class}`

            return td

            /**
             * @function createCheckbox         
             * @description Funzione dedicata alla creazione di una cella con all'interno una checkbox.
             * @param {string | number} value - Valore per quel campo che verrà convertito in true o false per la checkbox.
             * @param {string} classCSS - Classi aggiuntive assegnabili nell'oggetto di configurazione.
             * @returns {HTMLTableCellElement} - Ritorna la cella con dentro la checkbox.
             * @memberof bodyGenerator
             */

            function createCheckbox(value, classCSS) {

                let formCheck = document.createElement("div")
                formCheck.className = "form-check"
                if (classCSS) formCheck.className += ` ${field.class}`
                td.appendChild(formCheck)

                let checkbox = document.createElement("input")
                checkbox.type = "checkbox"
                checkbox.checked = value == "S"
                checkbox.disabled = true
                checkbox.className = "form-check-input"
                formCheck.appendChild(checkbox)
                return td
            }

            /**
             * @function formatDatelong         
             * @description Funzione dedicata alla formattazione della data dall'anno ai secondi.
             * @param {date} value - Data da formattare.
             * @returns {string} - Stringa con la data formattata.
             * @memberof bodyGenerator
             */

            function formatDatelong(value) {
                const date = new Date(value);

                const days = String(date.getDate()).padStart(2, '0')
                const months = String(date.getMonth() + 1).padStart(2, '0')
                const years = date.getFullYear()
                const hours = String(date.getHours()).padStart(2, '0')
                const minutes = String(date.getMinutes()).padStart(2, '0')
                const seconds = String(date.getSeconds()).padStart(2, '0')

                return `${days}/${months}/${years}\n${hours}:${minutes}:${seconds}`;
            }

            /**
             * @function formatDatenormal         
             * @description Funzione dedicata alla formattazione della data dall'anno ai giorni.
             * @param {date} value - Data da formattare.
             * @returns {string} - Stringa con la data formattata.
             * @memberof bodyGenerator
             */

            function formatDatenormal(value) {
                const date = new Date(value);

                const days = String(date.getDate()).padStart(2, '0')
                const months = String(date.getMonth() + 1).padStart(2, '0')
                const years = date.getFullYear()

                return `${days}/${months}/${years}`;
            }
        }
    }

    /**
     * @function createTableMenu
     * @description Metodo dedicato alla creazione del menu che appare al click destro su una row.
     * @memberof createTableMenu
     */

    createTableMenu() {
        document.addEventListener("click", (e) => {
            let menu = document.querySelector("#tableMenu")
            if (menu) {
                menu.remove()
            }
        })

        //this.addMenuListenersToRows()
    }

    /**
     * @function addMenuListenersToRows
     * @description Metodo dedicato all'assegnazione degli eventListeners alle row per l'apparizione del menu al click destro e alla logica dei tasti del menu.
     * @memberof addMenuListenersToRows
     */

    addMenuListenersToRows() {
        let tbodyTrs = document.querySelectorAll("tbody tr")
        tbodyTrs.forEach(x => {
            x.addEventListener("contextmenu", (e) => {
                e.preventDefault()
                let mouseposx = e.clientX
                let mouseposy = e.clientY

                let menu = document.querySelector("#tableMenu")
                if (menu) {
                    menu.remove()
                }
                //type = "button" class="btn bg-blue text-white rounded-5 m-1"
                menu = document.createElement("div")
                menu.classList.add("tableMenu")
                menu.id = "tableMenu"
                menu.style.position = "absolute"
                menu.style.left = mouseposx + "px"
                menu.style.top = (mouseposy - 47.5) + "px"
                document.body.appendChild(menu)

                let btn1 = document.createElement("button")
                btn1.type = "button"
                btn1.className = "btn bg-blue rounded-5 m-1"
                btn1.innerHTML = `<svg height="1em" viewBox="0 0 512 512"><path d="M471.6 21.7c-21.9-21.9-57.3-21.9-79.2 0L362.3 51.7l97.9 97.9 30.1-30.1c21.9-21.9 21.9-57.3 0-79.2L471.6 21.7zm-299.2 220c-6.1 6.1-10.8 13.6-13.5 21.9l-29.6 88.8c-2.9 8.6-.6 18.1 5.8 24.6s15.9 8.7 24.6 5.8l88.8-29.6c8.2-2.7 15.7-7.4 21.9-13.5L437.7 172.3 339.7 74.3 172.4 241.7zM96 64C43 64 0 107 0 160V416c0 53 43 96 96 96H352c53 0 96-43 96-96V320c0-17.7-14.3-32-32-32s-32 14.3-32 32v96c0 17.7-14.3 32-32 32H96c-17.7 0-32-14.3-32-32V160c0-17.7 14.3-32 32-32h96c17.7 0 32-14.3 32-32s-14.3-32-32-32H96z"/></svg>`
                btn1.setAttribute("data-bs-toggle", "modal")
                btn1.setAttribute("data-bs-target", "#updateRecord")
                btn1.addEventListener("click", () => {
                    this.paramObject.apiUrl.put.param.forEach(v => {
                        let updateValue = e.target.parentElement.querySelector(`td[fieldname="${v}"]`).getAttribute("valore")
                        console.log(updateValue)
                        let updateInput = document.querySelector(`#update-input-` + v)
                        updateInput.value = updateValue
                        updateInput.type = "hidden"
                    })

                    let updateModalTitle = document.querySelector("#updateModalTitle")
                    updateModalTitle.innerText = "Update the following record"

                    const fields = this.paramObject.fields.filter(x => x.modals != false)

                    const values = Array.from(e.target.parentElement.children).filter(x => x.getAttribute("fieldname") && x != null)

                    for (let i = 0; i < fields.length; i++) {
                        const td = values.find(v => v.getAttribute("fieldname") == fields[i].apiName)
                        const input = document.querySelector(`#updateDialog *[name="${fields[i].apiName}"]`)
                        const valore = td.getAttribute("valore")

                        if (td.getAttribute("fieldname") == this.paramObject.apiUrl.put.param) {
                            input.disabled = true
                        }
                        if (fields[i].type == "checkbox") {

                            input.checked = valore == "S"
                        }
                        else if (fields[i].type == "decimalColor") {
                            const color = this.getRGBFromNumber(valore)
                            input.style.backgroundColor = `rgb(${color.red},${color.green},${color.blue})`
                            input.addEventListener("change", (e) => {
                                const color = this.getRGBFromNumber(e.target.value)
                                e.target.style.backgroundColor = `rgb(${color.red},${color.green},${color.blue})`
                            })
                        }
                        else if (fields[i].type == "datenormal") {

                            let date = valore.split("/").reverse().join("-")
                            input.valueAsDate = new Date(date)

                        } else {
                            input.value = valore == "null" ? "" : valore
                        }


                    }


                })
                menu.appendChild(btn1)

                /* let btn2 = document.createElement("button")
                btn2.type = "button"
                btn2.className = "btn bg-blue rounded-5 m-1"
                btn2.innerHTML = `<svg height="1em" viewBox="0 0 448 512"><<path d="M135.2 17.7C140.6 6.8 151.7 0 163.8 0H284.2c12.1 0 23.2 6.8 28.6 17.7L320 32h96c17.7 0 32 14.3 32 32s-14.3 32-32 32H32C14.3 96 0 81.7 0 64S14.3 32 32 32h96l7.2-14.3zM32 128H416V448c0 35.3-28.7 64-64 64H96c-35.3 0-64-28.7-64-64V128zm96 64c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16z"/></svg>`
                btn2.setAttribute("data-bs-toggle", "modal")
                btn2.setAttribute("data-bs-target", "#deleteRecord")
                
                btn2.addEventListener("click", () => {
                    this.paramObject.apiUrl.delete.param.forEach(x => {
                        console.log(x)
                        const deleteValue = e.target.parentElement.querySelector(`td[fieldname="${x}"]`).getAttribute("valore")
                        console.log(deleteValue)
                        const inputDelete = document.querySelector("#delete-input-" + x)
                        inputDelete.value = deleteValue
                        inputDelete.type = "hidden"
                    })

                    document.querySelector("#deleteText").innerText = "Confermare l'eliminazione del record?"
                })
                menu.appendChild(btn2) */
            })
        })
    }

    /**
     * @function getRGBFromNumber
     * @description Metodo dedicato alla conversione dei colori in formato decimal al formato RGB.
     * @param {number} colorNumber 
     * @returns {Object} - Un oggetto con 3 proprietà (reg, green, blue)
     * @memberof getRGBFromNumber
     */

    getRGBFromNumber(colorNumber) {
        const blue = colorNumber >> 16
        const green = (colorNumber >> 8) & 255
        const red = colorNumber & 255
        return { red, green, blue }
    }

    beginSearch(e) {
        const val = e ? e.target.value : document.getElementById("searchRecord").value
        let searchField = document.querySelector(`[aria-label="Research-Fields"]`).value
        let research = Array.from(document.querySelectorAll(`tbody td[fieldname="${searchField}"]`))
            .filter(x => x.innerText.toLowerCase().includes(val.toLowerCase()))
            .map(x => x.parentElement)

        document.querySelectorAll("tbody tr").forEach(x => {
            if (!research.includes(x)) {
                x.style.display = "none"
            } else {
                x.style.display = "table-row"
            }
        })
    }

    /**
     * @function changePage
     * @description - Gestisce il cambio di pagina visualizzata all'interno della tabella e gestisce la visualizzazione corretta dei bottoni di navigazione.
     * @param {number} page - Numero di pagina sulla quale si vuole andare.
     * @returns {Object} - Response GET dell'API della pagina scelta.
     * @memberof changePage
     */
    
    changePage = async (page) => {
        if (!this.isTableCreated) return
        this.currentPage = page
        let prevPageBtn = document.querySelector(`.btn[selected="true"]`)
        prevPageBtn.removeAttribute("selected")
        let btn = document.querySelector(`.btn[page="${page}"]`)
        if (btn.classList.contains("hide-element")) {
            if (prevPageBtn.value > page) {
                showNextPageButtons(this.maxPageButtons, page)
            } else {
                showPreviousPageButtons(this.maxPageButtons, page)
            }
        }

        let previousBtn = document.querySelector("#previous-btn")
        let nextBtn = document.querySelector("#next-btn")

        if (btn.value > 1) {
            previousBtn.removeAttribute("disabled")
        } else {
            previousBtn.setAttribute("disabled", "true")
        }

        let allBtns = Array.from(document.querySelectorAll(".page-button"))
        //let allVisibleButtons = document.querySelectorAll(".page-button:not(.hide-element)")

        if (btn.value != allBtns[allBtns.length - 1].value) {
            nextBtn.removeAttribute("disabled")
        } else {
            nextBtn.setAttribute("disabled", "true")
        }

        btn.setAttribute("selected", "true")
        let pageLimit = document.querySelector("#selectEntries").value
        let res = await this.fetchGetApi(btn.value, pageLimit)
        let tbody = document.querySelector("tbody")
        tbody.innerHTML = ""
        this.bodyGenerator(res.response, tbody)

        let from = page == 1 ? 1 : pageLimit * (page - 1) + 1
        let to = pageLimit
        if (page != 1) {
            to = from + +pageLimit > res.totalRecords ? res.totalRecords : from + +pageLimit - 1
        }

        document.querySelector("#showNumber").innerText = `Showing ${from} to ${to} of the ${res.totalRecords} entries`

        this.beginSearch()

        return res

        /**
         * @function showNextPageButtons
         * @description Funzione che gestisce la visualizzazione dei pulsanti della navigazione di pagina quando si va avanti di pagina.
         * @param {number} nOfButtons - Massimo numero di pulsanti da far visualizzare. 
         * @param {number} page - Numero di pagina corrente.
         * @memberof changePage
         */

        function showNextPageButtons(nOfButtons, page) {

            let allBtns = Array.from(document.querySelectorAll(".page-button"))
            let btn = document.querySelector(`.btn[page="${page}"]`)
            let btnPosition = allBtns.indexOf(btn)

            for (let i = btnPosition; i <= btnPosition + nOfButtons; i++) {

                if (!allBtns[i]) {
                    break
                }
                allBtns[i].classList.add("hide-element")
                allBtns[i].parentElement.classList.add("hide-element")
            }
            for (let i = btnPosition; i >= btnPosition - (nOfButtons - 1); i--) {

                if (!allBtns[i]) break
                allBtns[i].classList.remove("hide-element")
                allBtns[i].parentElement.classList.remove("hide-element")
            }

            showHideDotsButtons(page)
        }

        /**
         * @function showPreviousPageButtons
         * @description Funzione che gestisce la visualizzazione dei pulsanti della navigazione di pagina quando si va indietro di pagina.
         * @param {number} nOfButtons - Massimo numero di pulsanti da far visualizzare. 
         * @param {number} page - Numero di pagina corrente.
         * @memberof changePages
         */
        function showPreviousPageButtons(nOfButtons, page) {

            let allBtns = Array.from(document.querySelectorAll(".page-button"))
            let btn = document.querySelector(`.btn[page="${page}"]`)
            let btnPosition = allBtns.indexOf(btn)

            for (let i = btnPosition; i >= btnPosition - nOfButtons; i--) {

                if (!allBtns[i]) {
                    break
                }
                allBtns[i].classList.add("hide-element")
                allBtns[i].parentElement.classList.add("hide-element")
            }
            for (let i = btnPosition; i <= btnPosition + (nOfButtons - 1); i++) {

                if (!allBtns[i]) {
                    break
                }
                allBtns[i].classList.remove("hide-element")
                allBtns[i].parentElement.classList.remove("hide-element")
            }

            showHideDotsButtons(page)
        }

        /**
         * @function showHideDotsButtons
         * @description Funzione che gestisce la visualizzazione dei pulsanti con i 3 punti per skippare di più pagine alla volta.
         * @param {number} page - Numero di pagina corrente. 
         * @memberof changePage
         */

        function showHideDotsButtons(page) {

            let allBtns = Array.from(document.querySelectorAll(".page-button"))
            let btn = document.querySelector(`.btn[page="${page}"]`)
            let buttonPosition = allBtns.indexOf(btn)
            let allVisibleButtons = document.querySelectorAll(".page-button:not(.hide-element)")

            let previousMore = document.querySelector("#previous-more")
            if (allVisibleButtons[0].value == allBtns[0].value) {

                previousMore.classList.add("hide-element")
                previousMore.parentElement.classList.add("hide-element")
            } else {

                previousMore.classList.remove("hide-element")
                previousMore.parentElement.classList.remove("hide-element")
            }

            let skipMore = document.querySelector("#skip-more")
            if (allVisibleButtons[allVisibleButtons.length - 1].value == allBtns[allBtns.length - 1].value) {

                skipMore.classList.add("hide-element")
                skipMore.parentElement.classList.add("hide-element")
            } else {

                skipMore.classList.remove("hide-element")
                skipMore.parentElement.classList.remove("hide-element")
            }
        }
    }

    /**
     * @function fetchApi
     * @description Metodo di base per chiamare l'api.
     * @param {string} url - URL da chiamare.
     * @param {RequestInit} settings - Impostazioni per la chiamata.
     * @returns {Response | undefined} - Response dell'API.
     * @memberof fetchApi
     */

    async fetchApi(url, settings) {
        let response;

        try {
            response = await fetch(url, settings)

            if (!response.ok) {
                const errorData = await response.json();
                return errorData
            }
        } catch (e) {
            console.log("e: " + e)
        }

        const res = await response.json()
        console.log(res)
        return res
        
    }

    /**
     * @function fetchGetApi
     * @description Metodo per fare una GET.
     * @param {number | string} pageNum - Numero della pagina.
     * @param {number | string} pageLimit - Grandezza della pagina. 
     * @returns {Response | undefined} - Response GET dell' API.
     * @memberof fetchGetApi
     */

    async fetchGetApi(pageNum, pageLimit) {
        const additionalParameters = this.paramObject.apiUrl.get.additionalParameters
        let stringParams = ""
        if (additionalParameters) {
            for (let params of additionalParameters) {
                stringParams += `&Livello=${this.addParamObj[params]}`
            }
        }

        let settings = {
            method: "GET",
            mode: "cors",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + this.paramObject.authToken,
                'Access-Control-Allow-Origin': '*'
            }
        }

        let url = `${this.paramObject.apiUrl.get.url[0] + this.paramObject.apiUrl.get.url[1]}?Page=${pageNum}&Limit=${pageLimit}${stringParams}`
        console.log("get url: " + url)

        return await this.fetchApi(url, settings)
    }

    /**
     * @function createTable
     * @description Funzione principale da cui inizia la creazione della tabella.
     * @memberof createTable
     */

    async createTable() {

        //inizio
        let paramObject = this.paramObject
        this.isTableCreated = true;

        /**
         * @function generateAdditionalParameters
         * @description Chiama le funzioni aggiuntive se passata in configurazione.
         * @memberof createTable
         */

        const generateAdditionalParameters = async () => {
            if (this.paramObject.functions) {
                for (const f of this.paramObject.functions) {
                    await f(this.addParamObj, this.changePage, generatePageBtns, this.currentPage)
                }
            }
        }

        /**
         * @function generateHeaders
         * @description Genera gli headings della tabella tramite l'oggetto di configurazione
         * @param {HTMLTableSectionElement} parent - Elemento padre a cui appendere la row con gli headers creati.
         * @memberof createTable
         */

        const generateHeaders = (parent) => {

            let heads = paramObject.fields

            let trHeaders = document.createElement("tr")
            parent.appendChild(trHeaders)

            let th = document.createElement("th")
            th.scope = "col"
            trHeaders.appendChild(th)

            let p = document.createElement("p")
            p.className = "m-0"
            p.innerText = "Riga"
            th.appendChild(p)

            const editRecordTh = document.createElement("th")
            editRecordTh.scope = "col"
            trHeaders.appendChild(editRecordTh)

            let editRecordP = document.createElement("p")
            editRecordP.className = "m-0"
            editRecordP.innerText = "Modifica"
            editRecordTh.appendChild(editRecordP)

            const deleteRecordTh = document.createElement("th")
            deleteRecordTh.scope = "col"
            trHeaders.appendChild(deleteRecordTh)

            let deleteRecordP = document.createElement("p")
            deleteRecordP.className = "m-0"
            deleteRecordP.innerText = "Elimina"
            deleteRecordTh.appendChild(deleteRecordP)

            heads.forEach((x, i) => {

                let th = document.createElement("th")
                th.scope = "col"
                let index = i
                trHeaders.appendChild(th)
                const sort = document.createElement("div")
                /* sort.addEventListener("click", () => { */
/*                     let rows = [...document.querySelectorAll("tbody tr")]
                    rows = rows.sort((a, b) => a[x.apiName] - b[x.apiName])
                    console.log(rows)
                    const tbody = document.querySelector("tbody")
                    tbody.innerHTML = ""
                    rows.forEach(element => {
                        tbody.appendChild(element)
                    });
                    //this.addMenuListenersToRows() */
                    /* var table, rows, switching, i, x, y, shouldSwitch;
                    table = document.querySelector("#containerLayoutAziendale table");
                    switching = true;

                    while (switching) {
                        switching = false;
                        rows = table.rows;

                        for (i = 1; i < rows.length - 1; i++) {
                        shouldSwitch = false;
                        x = rows[i].getElementsByTagName("td")[index];
                        y = rows[i + 1].getElementsByTagName("td")[index];

                            if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                                shouldSwitch = true;
                                break;
                            }
                        }

                        if (shouldSwitch) {
                        rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
                        switching = true;
                        }
                    }
                }) */
               /*  sort.classList.add("sorter", "pointer-pointer") */
                let p = document.createElement("p")
                p.className = "m-0"
                p.style.width = "max-content"
                p.style.display = "inline-block"
                p.innerText = x.displayedName
                if (x.type == "hidden") th.classList.add("d-none")
                sort.appendChild(p)
                
                /* sort.innerHTML += `<svg xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 320 512"><!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. --><path d="M137.4 41.4c12.5-12.5 32.8-12.5 45.3 0l128 128c9.2 9.2 11.9 22.9 6.9 34.9s-16.6 19.8-29.6 19.8H32c-12.9 0-24.6-7.8-29.6-19.8s-2.2-25.7 6.9-34.9l128-128zm0 429.3l-128-128c-9.2-9.2-11.9-22.9-6.9-34.9s16.6-19.8 29.6-19.8H288c12.9 0 24.6 7.8 29.6 19.8s2.2 25.7-6.9 34.9l-128 128c-12.5 12.5-32.8 12.5-45.3 0z"/></svg>`
                sort.style.display = "inline-block" */
                th.appendChild(sort)
            })
        }

        /**
         * @function generateSearchInput
         * @description Genera l'input della ricerca.
         * @param {HTMLDivElement} parent - Elemento padre a cui appendare l'input creato.
         * @memberof createTable
         */

        const generateSearchInput = (parent) => {
            let searchInput = document.createElement("input")
            searchInput.type = "text"
            searchInput.className = "form-control"
            searchInput.id = "searchRecord"
            searchInput.placeholder = "Search..."
            parent.appendChild(searchInput)

            searchInput.addEventListener("input", (e) => {
                this.beginSearch(e)
            })
        }

        /**
         * @function createPageBtn
         * @description Crea il pulsante per cambiare pagina con il numero di pagina.
         * @param {number} i - Numero di pagina del pulsante.
         * @param {HTMLUListElement} ul - Oggetto HTML in cui appendere gli oggetti HTML <li> contenenti i pulsanti creati.
         * @param {HTMLButtonElement} previousBtn - Bottone "Previous".
         * @memberof createTable
         */

        const createPageBtn = async (i, ul, previousBtn) => {

            let li = document.createElement("li")
            li.className = "page-item border border-secondary border-opacity-25 rounded-0"
            ul.appendChild(li)

            let btn = document.createElement("button")
            btn.type = "button"
            btn.className = "btn btn-light rounded-0 page-button"
            btn.setAttribute("page", i)
            btn.innerText = i
            btn.value = i
            if (i > this.maxPageButtons) {
                btn.classList.add("hide-element")
                li.classList.add("hide-element")
            }

            if (i == 1) {
                btn.setAttribute("selected", "true")
                previousBtn.setAttribute("disabled", "true")
            }

            btn.addEventListener("click", async (e) => {
                await this.changePage(e.target.value)
            })

            li.appendChild(btn)
        }

        /**
         * @function generatePagOptions
         * @description Genera la dropdown da cui è possibile scegliere il numero di righe per pagina.
         * @memberof createTable
         */
        
        const generatePagOptions = async () => {

            let entriesSelect = document.querySelector("#selectEntries");
            let tLoader = document.querySelector("#tLoader");
            entriesSelect.addEventListener("change", async () => {

                tLoader.classList.remove("d-none");
                //(entriesSelect.value * currentPage) > apiResponse.totalRecords ? 
                document.querySelector("#showNumber").innerText = `Showing 1 to ${entriesSelect.value} of the ${apiResponse.totalRecords} entries`

                let res = await this.changePage(1)
                let navPages = document.querySelector(`[aria-label="Page-navigation"]`)
                navPages.innerHTML = ""
                await generatePageBtns(res.totalPages, navPages)
                tLoader.classList.add("d-none");
            })

            paramObject.paginationOptions.forEach((option) => {
                let opt = document.createElement("option")
                opt.value = option
                opt.innerText = option
                entriesSelect.appendChild(opt)
            })
        }

        /**
         * @function generatePageBtns
         * @description Gestisce la creazuibe di tutti i pulsanti di navigazione di pagina.
         * @param {number} totalPages - Numero totale di pagine con la grandezza scelta.
         * @param {HTMLElement} parent - Elemento padre a cui appendere la lista creata nella funzione.
         * @memberof createTable
         */

        const generatePageBtns = async (totalPages, parent) => {

            let ul = document.createElement("ul")
            ul.className = "pagination m-3"
            parent.appendChild(ul)

            let previous = document.createElement("li")
            previous.className = "page-item border border-secondary border-opacity-25 rounded-start"
            ul.appendChild(previous)

            let previousBtn = document.createElement("button")
            previousBtn.type = "button"
            previousBtn.id = "previous-btn"
            previousBtn.className = "btn btn-light rounded-0 rounded-start"
            previousBtn.innerText = "Previous"
            previousBtn.addEventListener("click", async () => {
                let page = document.querySelector(`.btn[selected="true"]`).value
                await this.changePage(+page - 1)
            })
            previous.appendChild(previousBtn)

            if (totalPages > this.maxPageButtons) {

                let li = document.createElement("li")
                li.className = "page-item border border-secondary border-opacity-25 rounded-0 hide-element"
                ul.appendChild(li)

                let btn = document.createElement("button")
                btn.type = "button"
                btn.className = "btn btn-light rounded-0 hide-element"
                //btn.setAttribute("page", i)
                btn.innerText = "..."
                btn.value = "..."
                btn.id = "previous-more"
                btn.addEventListener("click", async () => {
                    let allShownBtns = document.querySelectorAll(".page-button:not(.hide-element)")
                    let first = allShownBtns[0]
                    await this.changePage(+first.value - 1)
                })

                li.appendChild(btn)
            }

            for (let i = 1; i <= totalPages; i++) {

                createPageBtn(i, ul, previousBtn)

            }

            if (totalPages > this.maxPageButtons) {

                let li = document.createElement("li")
                li.className = "page-item border border-secondary border-opacity-25 rounded-0"
                ul.appendChild(li)

                let btn = document.createElement("button")
                btn.type = "button"
                btn.id = "skip-more"
                btn.className = "btn btn-light rounded-0"
                //btn.setAttribute("page", i)
                btn.innerText = "..."
                btn.value = "..."
                btn.addEventListener("click", async () => {
                    let allShownBtns = document.querySelectorAll(".page-button:not(.hide-element)")
                    let last = allShownBtns[allShownBtns.length - 1]
                    await this.changePage(+last.value + 1)
                })

                li.appendChild(btn)
            }

            let next = document.createElement("li")
            next.className = "page-item border border-secondary border-opacity-25 rounded-end"
            ul.appendChild(next)

            let nextBtn = document.createElement("button")
            nextBtn.type = "button"
            nextBtn.id = "next-btn"
            nextBtn.className = "btn btn-light rounded-0 rounded-end"
            nextBtn.innerText = "Next"
            nextBtn.disabled = totalPages < 2
            nextBtn.addEventListener("click", async () => {
                let page = document.querySelector(`.btn[selected="true"]`).value
                await this.changePage(+page + 1)
            })
            next.appendChild(nextBtn)

        }

        /**
         * @function generateTable
         * @description Genera la tabella.
         * @returns {Object} - Response GET dell'API.
         * @memberof createTable 
        */

        const generateTable = async () => {

            let main = document.querySelector("#tgen") //default div where the table is created

            let container = document.createElement("div")
            container.id = "containerLayoutAziendale"
            container.className = "container-fluid"
            main.appendChild(container)

            let mainRow = document.createElement("div")
            mainRow.className = "row"
            container.appendChild(mainRow)

            let mainCol = document.createElement("div")
            mainCol.className = "col rounded-3 pt-3"
            mainCol.id = "table-background"
            mainRow.appendChild(mainCol)

            let rowDati = document.createElement("div")
            rowDati.className = "row align-items-center mb-1"
            mainCol.appendChild(rowDati)
            
            ////////////////////////////////////////////////////////////////
            
            const colopti = document.createElement("div")
            colopti.className = "col-md-3 d-flex justify-content-start"
            colopti.id = "container-options"
            rowDati.appendChild(colopti)
            
            ////////////////////////////////////////////////////////////////
            let tableLoader = document.createElement("span")
            tableLoader.id = "tLoader"
            tableLoader.className = "layout-loader"
            colopti.appendChild(tableLoader)

            let colSearch = document.createElement("div")
            colSearch.className = "col-md-6 d-flex justify-content-center"
            rowDati.appendChild(colSearch)

            generateSearchFields(colSearch) //generates the dropdown for selecting the search fields

            generateSearchInput(colSearch) //generates the input with a listener to search through the displayed recordss

            let colAddBtn = document.createElement("div")
            colAddBtn.className = "col-md-3 d-flex  justify-content-center"
            rowDati.appendChild(colAddBtn)

            let addBtnWrap = document.createElement("div")
            addBtnWrap.className = "add-btn-wrapper pointer-pointer d-flex align-items-center border"
            addBtnWrap.setAttribute("data-bs-toggle", "modal")
            addBtnWrap.setAttribute("data-bs-target", "#createRecord")
            addBtnWrap.addEventListener("click", () => {
                this.paramObject.apiUrl.post.param.forEach(c => {
                    console.log(c)
                    const createValue = document.querySelector(`td[fieldname="${c}"]`).getAttribute("valore")
                    console.log(createValue)
                    const inputCreate = document.querySelector("#create-input-" + c)
                    inputCreate.value = createValue
                    inputCreate.type = "hidden"
                })
            })
            colAddBtn.appendChild(addBtnWrap)

            let addButton = document.createElement("div")
            addButton.className = "me-3 add-record-button d-flex align-items-center justify-content-center"
            addButton.innerHTML = `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><path d="M256 512A256 256 0 1 0 256 0a256 256 0 1 0 0 512zM232 344V280H168c-13.3 0-24-10.7-24-24s10.7-24 24-24h64V168c0-13.3 10.7-24 24-24s24 10.7 24 24v64h64c13.3 0 24 10.7 24 24s-10.7 24-24 24H280v64c0 13.3-10.7 24-24 24s-24-10.7-24-24z"/></svg>`
            addButton.style.display = "inline"
            addBtnWrap.appendChild(addButton)

            let addP = document.createElement("p")
            addP.innerText = "Add new record"
            addP.className = "m-0"
            addBtnWrap.appendChild(addP)

            let row3 = document.createElement("div")
            row3.className = "row h-58"
            row3.id = "lateral-scroll"
            mainCol.appendChild(row3)

            let col4 = document.createElement("div")
            col4.className = "col"
            row3.appendChild(col4)

            let table = document.createElement("table")
            table.className = "table table-striped"
            col4.appendChild(table)

            let thead = document.createElement("thead")
            thead.className = "z-1"
            table.appendChild(thead)

            generateHeaders(thead) //generates the headers of the table

            let tbody = document.createElement("tbody")
            table.appendChild(tbody)

            let rowEntries = document.createElement("div")
            rowEntries.className = "row align-items-center mt-3"
            mainCol.appendChild(rowEntries)

            let colSelectEntries = document.createElement("div")
            colSelectEntries.id = "entries-col"
            colSelectEntries.className = "col-md-4 d-flex align-items-center"
            rowEntries.appendChild(colSelectEntries)

            let showEntries = document.createElement("p")
            showEntries.innerText = "Show"
            showEntries.className = "m-0"
            colSelectEntries.appendChild(showEntries)

            let entriesSelect = document.createElement("select")
            entriesSelect.id = "selectEntries"
            entriesSelect.className = "form-select mx-1"
            entriesSelect.setAttribute("aria-label", "Number-of-Entries")
            colSelectEntries.appendChild(entriesSelect)

            let showEntries2 = document.createElement("p")
            showEntries2.innerText = "entries"
            showEntries2.className = "m-0"
            colSelectEntries.appendChild(showEntries2)

            generatePagOptions() //generates the dropdown for selecting the entries per page

            let colShowEntries = document.createElement("div")
            colShowEntries.className = "col-md-3 d-flex align-items-center justify-content-center"
            rowEntries.appendChild(colShowEntries)

            let showingNumber = document.createElement("p")
            showingNumber.id = "showNumber"
            showingNumber.className = "m-0"
            colShowEntries.appendChild(showingNumber)

            

            let col7 = document.createElement("div")
            col7.className = "col-md-5 d-flex justify-content-end"
            rowEntries.appendChild(col7)

            let nav = document.createElement("nav")
            nav.setAttribute("aria-label", "Page-navigation")
            col7.appendChild(nav)

            await generateAdditionalParameters(generatePageBtns)

            if (!this.paramObject.authToken) {
                console.log("in attesa di login")
                const login = await this.logon()
                console.log("login fatto")
                this.paramObject.authToken = login.access_token
            }

            let apiResponse = await this.fetchGetApi(1, paramObject.paginationOptions[0]);
            await generatePageBtns(apiResponse.totalPages, nav) //generates buttons with page number
            this.bodyGenerator(apiResponse.response, tbody) //generates the rows of the table
            tableLoader.classList.add('d-none');
            showingNumber.innerText = `Showing 1 to ${paramObject.paginationOptions[0]} of the ${apiResponse.totalRecords} entries`
            return apiResponse
        }

        let apiResponse = await generateTable();
        this.apiResponse = apiResponse
        this.createTableMenu()

        

        /**
         * @function generateSearchFields
         * @description Genera la dropdown con i campi per cui è possibile ricercare nella tabella.
         * @param {HTMLDivElement} parent - Elemento padre a cui appendere la dropdown generata.
         * @memberof createTable
         */

        function generateSearchFields(parent) {
            let searchSelect = document.createElement("select")
            searchSelect.className = "form-select me-2"
            searchSelect.style.width = "max-content"
            searchSelect.setAttribute("aria-label", "Research-Fields")
            parent.appendChild(searchSelect)

            paramObject.fields.filter(x => x.type != "checkbox" && x.type != "decimalColor").forEach(f => {
                let opt = document.createElement("option")
                opt.value = f.apiName
                opt.innerText = f.displayedName
                searchSelect.appendChild(opt);
            })
        }
    }

}
