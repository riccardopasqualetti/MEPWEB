const MODAL_ID = 'version-modal-custom';
let modalInstance = null;

function ensureVersionModal() {
   if (document.getElementById(MODAL_ID)) return;

   const wrapper = document.createElement('div');
   wrapper.innerHTML = `
    <div class="modal fade" id="${MODAL_ID}" tabindex="-1" aria-hidden="true">
    <div class="modal-dialog modal-xl">
        <div class="modal-content" style="margin-top: 10vh">
            <div class="modal-header bg-sata-light">
                <h1 class="modal-title fs-5 sata-text-primary"></h1>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
            <div style="max-height: 68vh; overflow-y: auto;">
                  <div class="version-modal-additional-content">
                  </div>
                  <div class="version-modal-version-info align-items-baseline" class="mt-3" 
                     style=" display: grid; grid-template-columns: auto 1fr; column-gap: 1rem; row-gap: .3rem;">
                     <h6 class="m-0">Nome:</h6> <p class="m-0 version-nome"></p>
                     <h6 class="m-0">Versione Build:</h6> <p class="m-0 version-buildversion"></p>
                     <h6 class="m-0">Data e Ora Commit:</h6> <p class="m-0 version-commitdate"></p>
                     <h6 class="m-0">Hash Commit:</h6> <p class="m-0 version-commitsha"></p>
                     <h6 class="m-0">InformationalVersion:</h6> <p class="m-0 version-informationalversion"></p>
                     <h6 class="m-0">Versione Dll (interna):</h6> <p class="m-0 version-dllversion"></p>
                  </div>
                     <div class="version-modal-additional-content-2">
                  </div>
               </div>
            </div>
        </div>
    </div>
</div>
  `.trim();

   /*             <div class="d-flex gap-3 align-items-center p-3">
                  <button type="button" class="btn btn-sata-cancel" data-bs-dismiss="modal"
                           >${i18next.t('main.sannulla.long')}</button>
                  <div class="text-danger errorForm"></div>
            </div> */

   document.body.appendChild(wrapper.firstElementChild);
}

async function getAppsByCompany(companyId) {
   const response = await fetch(`/GruppiFiliali?handler=Activate&compId=${companyId}`);
   if (response.success) {
      const res = await response.json();
      return res.activationAppList;
   }
   throw new Error('Error fetching apps');
}

async function getApiVersionByCompany(companyId, signal) {
   try {
      const response = await fetch(`/GruppiFiliali?handler=ApiVersion&compId=${companyId}`, { signal });
      const parsedResponse = await response.json();
      return { success: response.ok, data: parsedResponse };
   } catch (error) {
      console.error('Error fetching API version:', error);
      return { success: false, data: null };
   }
}

async function getApiVersionByComapp(companyId, appCode, dbName) {
   try {
      const response = await fetch(`/GruppiFiliali?handler=ApiVersion&compId=${companyId}&appCode=${appCode}&dbName=${dbName}`);
      const parsedResponse = await response.json();
      return { success: response.ok, data: parsedResponse };
   } catch (error) {
      console.error('Error fetching API version:', error);
      return { success: false, data: null };
   }
}

async function getPlatformVersion() {
   try {
      const response = await fetch(`/api/Build/Platform`);
      const parsedResponse = await response.json();
      return { success: response.ok, data: parsedResponse };
   } catch (error) {
      console.error('Error fetching API version:', error);
      return { success: false, data: null };
   }
}

async function openVModalPlatform() {
   const apiVersionResponse = await getPlatformVersion();
   if (!apiVersionResponse.success) {
      alert('Errore nel recupero informazioni versione piattaforma');
      return;
   }
   openVersionModal({ title: 'Informazioni versione per SCWPlatform', buildResponse: apiVersionResponse.data });
}

/**
 * @param {string} appCode
 * @param {string} [dbName]
 * @param {string} [url]
 * @param {SystemInfoResponse|SapSystemInfoResponse|Mk1SystemInfoResponse|MepSystemInfoResponse} apiVersionResponse
 */
function openVersionModalHelper(appCode, dbName = null, url = null, apiVersionResponse) {
   title = `Informazioni versione API per ${appCode}`;
   const { el: modal } = getInstance();
   modal.querySelector('.version-modal-additional-content').innerHTML = `${
      dbName
         ? `<div class="align-items-baseline" style="display: grid; grid-template-columns: auto 1fr; column-gap: 1rem; row-gap: .3rem;">
   
               <h6 class="m-0">Database:</h6>
               <p class="m-0">${dbName}</p>
               <h6 class="m-0">URL API:</h6>
               <p class="m-0">${url}</p>         
            </div>
            <hr>`
         : ''
   }`;

   const dllsContent = getDllsContent(apiVersionResponse.dlls);

   console.log('API Version Response:', apiVersionResponse);
   switch (apiVersionResponse.type) {
      case 'SAP':
         modal.querySelector('.version-modal-additional-content-2').innerHTML = `
      <hr>
      <div class="align-items-baseline" style="display: grid; grid-template-columns: auto 1fr; column-gap: 1rem; row-gap: .3rem;">
         <h6 class="m-0">Ambiente:</h6>
         <p class="m-0">${apiVersionResponse.type}</p>
         <h6 class="m-0">Versione:</h6>
         <p class="m-0">${apiVersionResponse.systemSap.version}</p>
         <h6 class="m-0">FP:</h6>
         <p class="m-0">${apiVersionResponse.systemSap.fp}</p>
         <h6 class="m-0">SHR Path:</h6>
         <p class="m-0">${apiVersionResponse.systemSap.shrPath}</p>
      </div>
      ${dllsContent}`;
         break;
      case 'MEP':
         modal.querySelector('.version-modal-additional-content-2').innerHTML = `
      <hr>
      <div class="align-items-baseline" style="display: grid; grid-template-columns: auto 1fr; column-gap: 1rem; row-gap: .3rem;">
         <h6 class="m-0">Ambiente:</h6>
         <p class="m-0">${apiVersionResponse.type}</p>
         <h6 class="m-0">Versione:</h6>
         <p class="m-0">${apiVersionResponse.mepProductVersion}</p>
      </div>
      ${dllsContent}`;
         break;
      case 'MK1':
         modal.querySelector('.version-modal-additional-content-2').innerHTML = `
      <hr>
      <div class="align-items-baseline" style="display: grid; grid-template-columns: auto 1fr; column-gap: 1rem; row-gap: .3rem;">
         <h6 class="m-0">Ambiente:</h6>
         <p class="m-0">SAP</p>
         <h6 class="m-0">Versione:</h6>
         <p class="m-0">${apiVersionResponse.systemSap.version}</p>
         <h6 class="m-0">FP:</h6>
         <p class="m-0">${apiVersionResponse.systemSap.fp}</p>
         <h6 class="m-0">SHR Path:</h6>
         <p class="m-0">${apiVersionResponse.systemSap.shrPath}</p>
      </div>
      <hr>
      <div class="align-items-baseline" style="display: grid; grid-template-columns: auto 1fr; column-gap: 1rem; row-gap: .3rem;">
         <h6 class="m-0">Ambiente:</h6>
         <p class="m-0">${apiVersionResponse.type}</p>
         <h6 class="m-0">Addon Version:</h6>
         <p class="m-0">${apiVersionResponse.systemMk1.mk1AddonVersion}</p>
         ${
            apiVersionResponse.systemMk1.machineNam
               ? `<h6 class="m-0">Machine Name:</h6>
         <p class="m-0">${apiVersionResponse.systemMk1.machineNam}</p>
         <h6 class="m-0">Company DB Name:</h6>
         <p class="m-0">${apiVersionResponse.systemMk1.compDbNam}</p>
         <h6 class="m-0">Ewa Sent Dat:</h6>
         <p class="m-0">${apiVersionResponse.systemMk1.ewaSentDat}</p>
         <h6 class="m-0">Ewa Sent Dat 1:</h6>
         <p class="m-0">${apiVersionResponse.systemMk1.ewaSentDat1}</p>
         <h6 class="m-0">ID:</h6>
         <p class="m-0">${apiVersionResponse.systemMk1.id}</p>
         <h6 class="m-0">Name:</h6>
         <p class="m-0">${apiVersionResponse.systemMk1.name}</p>
         <h6 class="m-0">Version:</h6>
         <p class="m-0">${apiVersionResponse.systemMk1.version}</p>
         <h6 class="m-0">Company Name:</h6>
         <p class="m-0">${apiVersionResponse.systemMk1.cmpName}</p>`
               : ''
         }
      </div>
      ${dllsContent}`;
         break;
   }
   /* {
    "type": "MK1",
    "systemMk1": {
        "mk1AddonVersion": "22.0.20251118.1152",
        "machineNam": null,
        "compDbNam": null,
        "ewaSentDat": null,
        "ewaSentDat1": null,
        "id": null,
        "name": null,
        "version": null,
        "cmpName": null
    },
    "systemSap": {
        "version": 1000300,
        "fp": "2508",
        "shrPath": "\\\\SC-SAP10SST\\B1_SHR"
    },
    "api": {
        "name": "SATA.API",
        "assemblyVersion": "1.0.0.0",
        "informationalVersion": "1.0.0\u002B5db3423aaca7a2c3c26b7ef61846cec1049747d4;date=2026-02-25T11:40:11\u002B01:00",
        "commitDate": "2026-02-25T11:40:11\u002B01:00",
        "commitSha": "5db3423aaca7a2c3c26b7ef61846cec1049747d4",
        "buildVersion": "1.0.0"
    },
    "dlls": [
        {
            "name": "m1dllfmrp2.dll",
            "productVersion": "20260225"
        },
        {
            "name": "m1dllfmrp0.dll",
            "productVersion": null
        }
    ]
} */

   openVersionModal({ title, buildResponse: apiVersionResponse.build, additionalContentFlg: true, additionalContentFlg2: true });
}

function getDllsContent(dlls) {
   const content = `<hr>
      <div class="align-items-baseline" style="display: grid; grid-template-columns: auto 1fr; column-gap: 1rem; row-gap: .5rem;">
         <h6 class="m-0">DLL installate nell'ambiente</h6>
         <p class="m-0"></p>
         <div class="align-items-baseline" style="display: grid; grid-template-columns: auto 1fr; column-gap: 1rem; row-gap: .3rem;">
         ${dlls
            .map(
               (dll) => `<h6 class="m-0">${dll.name}:</h6>
               <p class="m-0">${dll.productVersion}</p>`,
            )
            .join('')}
            </div>
      </div>`;

   return content;
}

function openVersionModalApplicativo({ title, buildResponse }) {
   const { el: modal } = getInstance();
   if (!Array.isArray(buildResponse.dlls) || buildResponse.dlls.length == 0) {
      openVersionModal({ title, buildResponse, additionalContentFlg: false, additionalContentFlg2: false });
   } else {
      modal.querySelector('.version-modal-additional-content-2').innerHTML = getDllsContent(buildResponse.dlls);
      openVersionModal({ title, buildResponse, additionalContentFlg: false, additionalContentFlg2: true });
   }
}

/**
 *
 * @param {Object} param
 * @param {string} param.title Titolo da mostrare nel modal.
 * @param {BuildResponse} param.buildResponse Informazioni di build da mostrare nel modal.
 * @param {boolean} [param.additionalContentFlg=false] Flag per indicare se mostrare o meno i contenuti aggiuntivi (es. url, dbName).
 */
function openVersionModal({ title, buildResponse, additionalContentFlg = false, additionalContentFlg2 = false }) {
   const { el: modal, bsInstance } = getInstance();
   if (!additionalContentFlg) modal.querySelector('.version-modal-additional-content').innerHTML = '';
   if (!additionalContentFlg2) modal.querySelector('.version-modal-additional-content-2').innerHTML = '';
   modal.querySelector('.modal-title').textContent = title ?? 'title';
   /* 
      {
         "name": "SATA.API",
         "assemblyVersion": "1.0.0.0",
         "informationalVersion": "1.0.0+289ac4322f8aca7e515ebc4c27112bb7fdd074bf;date=2026-02-05T15:36:19+01:00",
         "commitDate": "2026-02-05T15:36:19+01:00",
         "commitSha": "289ac4322f8aca7e515ebc4c27112bb7fdd074bf",
         "buildVersion": "1.0.0"
      }
      */
   modal.querySelector('.version-nome').textContent = buildResponse.name;
   modal.querySelector('.version-buildversion').textContent = buildResponse.buildVersion;
   modal.querySelector('.version-commitdate').textContent = new Date(buildResponse.commitDate).toLocaleString();
   modal.querySelector('.version-commitsha').textContent = buildResponse.commitSha;
   modal.querySelector('.version-informationalversion').textContent = buildResponse.informationalVersion;
   modal.querySelector('.version-dllversion').textContent = buildResponse.assemblyVersion;
   bsInstance.show();
}

function getInstance() {
   ensureVersionModal();
   const el = document.getElementById(MODAL_ID);

   // crea 1 sola instance
   if (!modalInstance)
      modalInstance = window.bootstrap.Modal.getOrCreateInstance(el, {
         backdrop: true,
         keyboard: true,
      });

   return { el, bsInstance: modalInstance };
}

/**
 * Informazioni di build della dll api/platform/moduli.
 * @typedef {Object} BuildResponse
 * @property {string|null} name Nome assembly/applicazione.
 * @property {string|null} assemblyVersion Versione assembly.
 * @property {string|null} informationalVersion Versione informativa (semver, tag build).
 * @property {string|null} commitDate Data commit build.
 * @property {string|null} commitSha SHA del commit.
 * @property {string|null} buildVersion Versione build CI/CD.
 */

/**
 * Informazioni base del sistema.
 * Contiene le informazioni di build dell'API.
 *
 * @typedef {Object} SystemInfoResponse
 * @property {"SAP"|"MK1"|"MEP"} type
 * @property {BuildResponse} api Informazioni build API.
 */

/**
 * Informazioni ambiente SAP.
 *
 * @typedef {Object} SapInfoResponse
 * @property {number} version Versione SAP Business One.
 * @property {string|null} fp Feature Pack (es. FP2408).
 * @property {string} shrPath Path condiviso SAP.
 */

/**
 * @typedef {SystemInfoResponse & {
 *   type: "SAP",
 *   systemSap: SapInfoResponse
 * }} SapSystemInfoResponse
 */

/**
 * Informazioni base recuperate dal repository MK1.
 *
 * @typedef {Object} Mk1InfoRepositoryResponse
 * @property {string|null} machineNam Nome macchina.
 * @property {string|null} compDbNam Nome database company.
 * @property {string|null} ewaSentDat Data invio EWA (hardware).
 * @property {string|null} ewaSentDat1 Data invio EWA (addon/applicativo).
 * @property {number|null} id Identificativo record.
 * @property {string|null} name Nome applicazione.
 * @property {string|null} version Versione applicazione.
 * @property {string|null} cmpName Nome descrittivo company.
 */

/**
 * @typedef {Mk1InfoRepositoryResponse & {
 *   mk1AddonVersion: string
 * }} Mk1InfoResponse
 */

/**
 * @typedef {SapSystemInfoResponse & {
 *   type: "MK1",
 *   systemMk1: Mk1InfoResponse
 * }} Mk1SystemInfoResponse
 */

/**
 * @typedef {SystemInfoResponse & {
 *   type: "MEP",
 *   mepProductVersion: string|null
 * }} MepSystemInfoResponse
 */
