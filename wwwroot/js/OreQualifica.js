
/* import * as types from "./types.js" */
/*import { TableGenerator } from './TableGenerator.js'*/

const tempToken = "eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZDQkMtSFM1MTIiLCJraWQiOiJGNzcyRTI4RjMzNThEQjlERTU4NDFFODlGN0NCQ0UyMEM3RjdDNDgxIiwidHlwIjoiYXQrand0IiwiY3R5IjoiSldUIn0.BuPpaRMiivj0d362pRSNl4rFJ1TlQS0NGR96gx-TkV6A8vbQg_aHQqoob89RaqLveLHY3JmibGqtjhO_PDLoQHllr4bAhKmgQwm-6RrDsQsAHTPCsD_fhPtXXhgCtfTa0nNSqZTOiNjOdEtTr3PsJEPImK-SrbrYANo6OAELezqMBdYkdJdYaC3vcGUJt8X4JzXpnv9DUVnUA9wbCfbYqeoZhDAiM1EnRF72povFVC8WP8rk_2cKl0PDgccOZO8LzpGA2tYpsWsdXufcsrmbvGbQDw_9C2xxyY6uPe2KW_-yX6oIanMWAFeyMALE3cb6kGfXajJaKE0HrFbHDPWU2g.qRPAeMyKvVBwKF0EIHJL3g.3RddZkYvjxnf8Uh48XNj8emL-pIyfQKF_ql2BYW8oi6vXQLWRVMzo2_rrEnQG4WToOdYvhAgAFzVO2ADIZDWSpSEeCNlpo2nskasncs6xl482URVKRUoq3D967Z9A1ZyhtVqb1C6RIubydJoFNorsutpz-oEldGwpB0U63wUbChe66qjb5QTf1AcuhKfysbaa4GNuDgW_Z9Tk-sYMnUj2WEJqn-s1g8Prp7_kdhwoC7DQpdanrMajFAHMJ4IzBxwIx_mRGL3t_PIjjxhCj7ZYk7BTCtzhMCjZ5Wf3VThlDve4528xNmqFnIRCZRAqape_VPdlOWCw_YVGuEXkuHH_ffb2tqmFpt4bdEwk_E3Wxi89fvJt3pJ1KAYlA4jFhYbUFPfuvarMLJGun4RKQK8mHNMlW2_TWqDOeQguzbFXN8iwcHBQulh-vZWdzz3AqcpP3IMOzATsuTmtvomx8TZSF-V639hsSstbebnwRWN0-6CE8zR57Fvx0dCFOxzSs_z7dicP0tpzQ4HuFuvL1UBUCdx97sgh_ypttp5wS2p6MhOq8j3YB9nUyxilPTy3_UPg3vHKKFRwF5ythgEkjbNf33iKgI9hDNiR5cawwuMCa1TSQugnHDOgoSzvv9USC1zSmfgEMANMOJDk0z7kW0hgkWbHtvPqc74Cw9Faa6QwS50hmdHlv9O6Ki0iQvCcHx27kM-YesvbHrBhr6lMTwk4zTkbgCGsO0ZqPqXyjAt8VI6p6L7ng_1_JCfxSkzCFJUPsWRimqErR-fRBDf9RbgPxL4ZE_mrfQa1k8uK7PdOV2DDH9CYuB0W3titJZdg4eWccp0BCyXsomJlEHHlsnLiOYT5FHd9WHU0zp80tXxKJLeQ4QUf2vCkEkUTuClJvMgBgNloEEbrlFEEBhE7QePjok8A7qZlSgTLHd0wSTK09TON_nkO3VUS0_KPq-Fihct5XCxlHJl16-d8kJkYd52QyXmXAGZn_Al8pj2Jx_NPPjy1bAMUsbUypNWS6lLbxhU_LLt80z9Xo0kE5aQiLSpBaJWR7rISoXlFDp_1-ME7s-_uu5txp6es0b5xqIhMAZXjC2LUSIWrwF2IK6MRtMUriflQ5UTnmCNwl3NVfgcga3tgx-8S7_3E5HjRyPCXC6uEerVA0Udhw4Sugsmg8y61757faLol6HOy5AesIROsGetGmapv39uiwOKtPUmP5KImMOxsliqpBlOwD88Hsm2nI__2IJjaMr6dwdm6I3aWW1dcIMyuAahZQFN-bYvG3pymHSLrrQv2n6P1SwtrgCk6-pejCWb4UF-P5mYQBVB1Fi-vgjyo6pLMXc7SNcZlzFY.2uMXJRa_5-3To_dsDE3n_dAR63MVrWjDUZflaK1-M_o"
let idDocumento = window.location.pathname.split("/")[3]

const baseURL = "/"

/** 
* Oggetto con i parametri per la generazione della tabella con API: MasterResource.
* @type {types.ParamObject}
*  */
const oreQualificaObj = {
    //credentials: login,
    authToken: tempToken,
    apiUrl: {
        get: {
            url: [baseURL, `api/MepWeb_OreQualifica/${idDocumento}/GetAllPaged`]
        },
        post: {
            param: ["idDocumento"],
            url: [baseURL, "api/MepWeb_OreQualifica"]
        },
        put: {
            param: ["idDocumento", "qualifica"],
            url: [baseURL, "api/MepWeb_OreQualifica"]
        },
        delete: {
            param: ["idDocumento", "qualifica"],
            url: [baseURL, "api/MepWeb_OreQualifica"]
        }
    },
    paginationOptions: [640, 20, 50, 1280],
    modals: {
        create: { width: "c1" }
    },
    fields: [
        {
            apiName: "idDocumento",
            displayedName: "Id Documento",
            type: "hidden",
            modals: false
        },
        {
            apiName: "qualifica",
            displayedName: "Qualifica",
            searchUrls: [
                {
                    url: "api/MepWeb_Qualifiche",
                    searchFieldNames: ["cod", "descrizioneRidotta"]
                }
            ],
            type: "dropdown",
            update: false,
            modalPosition :{
                row: 1,
                nCols: 12
            }
        },
        {
            apiName: "oreAcquistate",
            displayedName: "Ore Acquistate",
            type: "number",
            modals: false
        },
        {
            apiName: "tipoFatturazione",
            displayedName: "Tipo Fatturazione",
            type: "dropdown",
            values: { o1: "Prepagate", o2: "Fatturate a consuntivo con check monte ore", o3: "Fatturate a consuntivo senza check monte ore" },
            modalPosition :{
                row: 2,
                nCols: 12
            }
        },
        {
            apiName: "note",
            displayedName: "Note",
            modalPosition :{
                row: 3,
                nCols: 12
            }
        },
        {
            apiName: "registroRicariche",
            displayedName: "Ricariche",
            type: "button",
            href: `../RegistroRicariche/${idDocumento}/{}`,
            varValue: "qualifica",
            svg: `<svg height="16" width="16" viewBox="0 0 512 512"><path d="M40 48C26.7 48 16 58.7 16 72v48c0 13.3 10.7 24 24 24H88c13.3 0 24-10.7 24-24V72c0-13.3-10.7-24-24-24H40zM192 64c-17.7 0-32 14.3-32 32s14.3 32 32 32H480c17.7 0 32-14.3 32-32s-14.3-32-32-32H192zm0 160c-17.7 0-32 14.3-32 32s14.3 32 32 32H480c17.7 0 32-14.3 32-32s-14.3-32-32-32H192zm0 160c-17.7 0-32 14.3-32 32s14.3 32 32 32H480c17.7 0 32-14.3 32-32s-14.3-32-32-32H192zM16 232v48c0 13.3 10.7 24 24 24H88c13.3 0 24-10.7 24-24V232c0-13.3-10.7-24-24-24H40c-13.3 0-24 10.7-24 24zM40 368c-13.3 0-24 10.7-24 24v48c0 13.3 10.7 24 24 24H88c13.3 0 24-10.7 24-24V392c0-13.3-10.7-24-24-24H40z"/></svg>`,
            modals: false
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
/* } */

//const idDocumento = getDocumento()



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

