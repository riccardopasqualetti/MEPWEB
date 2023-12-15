
import * as types from "./types.js"
import { TableGenerator } from './TableGenerator.js'

const tempToken = "eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZDQkMtSFM1MTIiLCJraWQiOiJGNzcyRTI4RjMzNThEQjlERTU4NDFFODlGN0NCQ0UyMEM3RjdDNDgxIiwidHlwIjoiYXQrand0IiwiY3R5IjoiSldUIn0.BuPpaRMiivj0d362pRSNl4rFJ1TlQS0NGR96gx-TkV6A8vbQg_aHQqoob89RaqLveLHY3JmibGqtjhO_PDLoQHllr4bAhKmgQwm-6RrDsQsAHTPCsD_fhPtXXhgCtfTa0nNSqZTOiNjOdEtTr3PsJEPImK-SrbrYANo6OAELezqMBdYkdJdYaC3vcGUJt8X4JzXpnv9DUVnUA9wbCfbYqeoZhDAiM1EnRF72povFVC8WP8rk_2cKl0PDgccOZO8LzpGA2tYpsWsdXufcsrmbvGbQDw_9C2xxyY6uPe2KW_-yX6oIanMWAFeyMALE3cb6kGfXajJaKE0HrFbHDPWU2g.qRPAeMyKvVBwKF0EIHJL3g.3RddZkYvjxnf8Uh48XNj8emL-pIyfQKF_ql2BYW8oi6vXQLWRVMzo2_rrEnQG4WToOdYvhAgAFzVO2ADIZDWSpSEeCNlpo2nskasncs6xl482URVKRUoq3D967Z9A1ZyhtVqb1C6RIubydJoFNorsutpz-oEldGwpB0U63wUbChe66qjb5QTf1AcuhKfysbaa4GNuDgW_Z9Tk-sYMnUj2WEJqn-s1g8Prp7_kdhwoC7DQpdanrMajFAHMJ4IzBxwIx_mRGL3t_PIjjxhCj7ZYk7BTCtzhMCjZ5Wf3VThlDve4528xNmqFnIRCZRAqape_VPdlOWCw_YVGuEXkuHH_ffb2tqmFpt4bdEwk_E3Wxi89fvJt3pJ1KAYlA4jFhYbUFPfuvarMLJGun4RKQK8mHNMlW2_TWqDOeQguzbFXN8iwcHBQulh-vZWdzz3AqcpP3IMOzATsuTmtvomx8TZSF-V639hsSstbebnwRWN0-6CE8zR57Fvx0dCFOxzSs_z7dicP0tpzQ4HuFuvL1UBUCdx97sgh_ypttp5wS2p6MhOq8j3YB9nUyxilPTy3_UPg3vHKKFRwF5ythgEkjbNf33iKgI9hDNiR5cawwuMCa1TSQugnHDOgoSzvv9USC1zSmfgEMANMOJDk0z7kW0hgkWbHtvPqc74Cw9Faa6QwS50hmdHlv9O6Ki0iQvCcHx27kM-YesvbHrBhr6lMTwk4zTkbgCGsO0ZqPqXyjAt8VI6p6L7ng_1_JCfxSkzCFJUPsWRimqErR-fRBDf9RbgPxL4ZE_mrfQa1k8uK7PdOV2DDH9CYuB0W3titJZdg4eWccp0BCyXsomJlEHHlsnLiOYT5FHd9WHU0zp80tXxKJLeQ4QUf2vCkEkUTuClJvMgBgNloEEbrlFEEBhE7QePjok8A7qZlSgTLHd0wSTK09TON_nkO3VUS0_KPq-Fihct5XCxlHJl16-d8kJkYd52QyXmXAGZn_Al8pj2Jx_NPPjy1bAMUsbUypNWS6lLbxhU_LLt80z9Xo0kE5aQiLSpBaJWR7rISoXlFDp_1-ME7s-_uu5txp6es0b5xqIhMAZXjC2LUSIWrwF2IK6MRtMUriflQ5UTnmCNwl3NVfgcga3tgx-8S7_3E5HjRyPCXC6uEerVA0Udhw4Sugsmg8y61757faLol6HOy5AesIROsGetGmapv39uiwOKtPUmP5KImMOxsliqpBlOwD88Hsm2nI__2IJjaMr6dwdm6I3aWW1dcIMyuAahZQFN-bYvG3pymHSLrrQv2n6P1SwtrgCk6-pejCWb4UF-P5mYQBVB1Fi-vgjyo6pLMXc7SNcZlzFY.2uMXJRa_5-3To_dsDE3n_dAR63MVrWjDUZflaK1-M_o"

//const baseURL = getBaseUrl()
function getBaseUrl() {
    for (const cookie of document.cookie.split("; ")) {
        if (cookie.split("=")[0] == "SC_Comp_Url") return decodeURIComponent(cookie.split("=")[1]) + "/"
    }
}
const baseURL = "https://localhost:7125/"
console.log(baseURL)

//const hanaLogin = {
//    username: "manager",
//    password: "SC@Rosss19",
//    company: "SBOM1_WM_WEB_HANA"
//}

//const mepLogin = {
//    username: "sata",
//    password: "demo",
//    company: "mep_demo"
//}

//let login;
//if (baseURL == "https://192.168.192.104:443/") {

//    login = hanaLogin
//} else if (baseURL == "https://192.168.192.113:443/") {

//    login = mepLogin
//} else {

//    login = hanaLogin
//}


/** 
 * Oggetto con i parametri per la generazione della tabella con API: MasterResource.
 * @type {types.ParamObject}
 *  */
const oreQualificaObj = {
    //credentials: login,
    authToken: tempToken,
    apiUrl: {
        get: {
            param: ["idDocumento"],
            url: [baseURL, "api/MepWeb_OreQualifica/GetAllPaged/${idDocumento}"]
        },
        post: {
            url: [baseURL, "api/MepWeb_OreQualifica"]
        },
        put: {
            url: [baseURL, "api/MepWeb_OreQualifica"]
        },
        delete: {
            param: ["idDocumento", "qualifica"],
            url: [baseURL, "api/MepWeb_OreQualifica"]
        }
    },
    paginationOptions: [640, 20, 50, 1280],
    modals: {
        create: { col: 1, maxFields: 5 }
    },
    fields: [
        {
            apiName: "idDocumento",
            displayedName: "Id Documento",
            type: "hidden"
        },
        {
            apiName: "qualifica",
            displayedName: "Qualifica",
            searchUrls: [
                {
                    url: "api/MepWeb_Qualiche",
                    searchFieldNames: ["codiceQualifica", "nomeQualifica"]
                }
            ]
        },
        {
            apiName: "oreAcquistate",
            displayedName: "Ore Acquistate",
            type: "number"
        },
        {
            apiName: "tipoFatturazione",
            displayedName: "Tipo Fatturazione",
            type: "dropdown",
            values: { o1: "Prepagate", o2: "Fatturate a consuntivo con check monte ore", o3: "Fatturate a consuntivo senza check monte ore" }
        },
        {
            apiName: "note",
            displayedName: "Note"
        }
    ]
}

/**
 * Funzione che viene passata nel campo additionalParameters degli ApiParams presenti nel ParamObject.
 * @param {types.AddParamObject} obj - Oggetto proprietà della classe TableGenerator che viene popolato con i parametri aggiuntivi.
 * @param {changePage.changePage} changePage - Metodo della classe TableGenerator che viene usato per cambiare la pagina della tabella.
 * @param {Function} generatePageBtns - Metodo della classe TableGenerator che viene usato per rigenerare i bottoni del cambio pagina.
 */

async function genDrop(obj, changePage, generatePageBtns) {

    let col = document.querySelector("#entries-col")
    let livelloP = document.createElement("p")
    livelloP.className = "m-0 ms-5 me-1"
    livelloP.innerText = "Livello: "
    col.appendChild(livelloP)
    let dropdown = document.createElement("select")
    dropdown.id = "selectLivello"
    dropdown.className = "form-select"
    dropdown.style.width = "max-content"
    dropdown.setAttribute("aria-label", "Levels")
    dropdown.addEventListener("change", async (e) => {
        obj.livello = dropdown.value
        let res = await changePage(1)
        let navPages = document.querySelector(`[aria-label="Page-navigation"]`)
        navPages.innerHTML = ""
        await generatePageBtns(res.totalPages, navPages)
        document.querySelectorAll(`select[name="livello"]`).forEach(x => {
            x.value = dropdown.value
        })

    })
    col.appendChild(dropdown)
    obj.livello = 1
    let options = ["Company", "Plant", "Department"]
    for (var i = 1; i < 4; i++) {
        let opt = document.createElement("option")
        opt.value = i
        opt.innerText = options[i - 1]
        dropdown.appendChild(opt)
    }

}

/** 
 * Oggetto con i parametri per la generazione della tabella con API: MasterCompanyLayout.
 * @type {types.ParamObject} 
 * */
const addettoQualificaObj = {
    //credentials: login,
    authToken: tempToken,
    functions: [genDrop],
    apiUrl: {
        get: {
            param: ["idDocumento"],
            url: [baseURL, "api/MepWeb_AddettoQualifica/GetAllPaged/${idDocumento}"]
        },
        post: {
            url: [baseURL, "api/MepWeb_AddettoQualifica"]
        },
        put: {
            url: [baseURL, "api/MepWeb_AddettoQualifica"]
        },
        delete: {
            param: ["id"],
            url: [baseURL, "api/MepWeb_AddettoQualifica"]
        }
    },
    paginationOptions: [20, 2, 5, 10, 50],
    modals: {
        create: { col: 1, maxFields: 3 }
    },
    fields: [
        {
            apiName: "idDoc",
            displayedName: "Id Documento",
            type: "hidden"
        },
        {
            apiName: "cRisorsa",
            displayedName: "Codice Risorsa"
        },
        {
            apiName: "grpcdl",
            displayedName: "Qualifica",
            searchUrls: [
                {
                    url: "api/MepWeb_Qualifiche",
                    searchFieldNames: ["codiceQualifica", "nomeQualifica"]
                }
            ]
        }
    ]
}

/** 
 * Oggetto con i parametri per la generazione della tabella con API: MasterWorkCenter.
 * @type {types.ParamObject}
 *  */
const registroRicaricheObj = {
    //credentials: login,
    authToken: tempToken,
    apiUrl: {
        get: {
            param: ["idDocumento"],
            url: [baseURL, "api/MepWeb_OreQualifica/GetAllPaged/${idDocumento}"]
        },
        post: {
            url: [baseURL, "api/MepWeb_OreQualifica"]
        },
        put: {
            url: [baseURL, "api/MepWeb_OreQualifica"]
        },
        delete: {
            param: ["id"],
            url: [baseURL, "api/MepWeb_OreQualifica"]
        }
    },
    paginationOptions: [20, 50, 100, 200, 400, 800],
    modals: {
        create: { col: 2, maxFields: 4 }
    },
    fields: [
        {
            apiName: "idDocumento",
            displayedName: "Id Documento",
            type: "hidden"
        },
        {
            apiName: "id",
            displayedName: "Id",
            type: "hidden"
        },
        {
            apiName: "qualifica",
            displayedName: "Qualifica",
            searchUrls: [
                {
                    url: "api/MepWeb_Qualifiche",
                    searchFieldNames: ["codiceQualifica", "nomeQualifica"]
                }
            ]
        },
        {
            apiName: "riferimentoOfferta",
            displayedName: "Riferimento Offerta",
        },
        {
            apiName: "dataRicarica",
            displayedName: "Data Ricarica",
            type: "datelong"
        },
        {
            apiName: "oreAcquistate",
            displayedName: "Ore Acquistate",
            type: "number"
        },
        {
            apiName: "note",
            displayedName: "Note"
        }
    ]
}

/* const optionsObj = {
    cily: ["Cily", cilyObj],
    workCenter: ["WorkCenter", workCenterObj],
    resources: ["Risorse", resourcesObj]
} */

/**
 * Un'istanza di TableGenerator utilizzata per eseguire azioni specifiche.
 * @type {types.TableGenerator}
 */
let tableGeneratorInstance = new TableGenerator(oreQualificaObj);

await tableGeneratorInstance.createModals()
await tableGeneratorInstance.createTable()

//let sel = await generateOptions()

async function generateOptions(selezionata) {
    const container = document.getElementById("container-options")
    const select = document.createElement("select")
    select.className = "form-select"

    for (const opt in optionsObj) {
        const option = document.createElement("option")
        option.innerText = optionsObj[opt][0]
        option.value = opt
        if (opt == selezionata) {
            option.selected = true
        }
        select.appendChild(option)
    }

    select.addEventListener("change", async (e) => {
        await tableGeneratorInstance.destroyTable()

        tableGeneratorInstance = new TableGenerator(optionsObj[e.target.value][1]);
        await tableGeneratorInstance.createTable()
        await generateOptions(e.target.value)
    })

    container.appendChild(select)
    return select

}

