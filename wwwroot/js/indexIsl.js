Array.from(document.getElementsByClassName("open-btn")).forEach((x) => {
  x.addEventListener("click", async () => {
    const rowIsl = x.parentElement.getAttribute("rowIsl");
    const accordionBody = document.getElementById(`accordion-${rowIsl}`);
    accordionBody.classList.toggle("opens-opened");
    if (accordionBody.classList.contains("opens-opened")) {
      accordionBody.style.maxHeight = "92.84px";
      const res = await fetchApi(`MepWeb_Isl/${rowIsl}`);
      const height = generateRows(res, rowIsl);
      accordionBody.style.maxHeight = 92.84 + height + 1 + "px";
    } else {
      accordionBody.style.maxHeight = "0px";
    }
  });
});

async function fetchApi(url) {
  const res = await fetch(url);
  console.log(res);
  try {
    if (res.status === 200) {
      const data = await res.json();
      console.log(data);
      return data;
    } else {
      console.log(res.statusCode);
      return [];
    }
  } catch (err) {
    console.log(err);
  }
}

function generateRows(res, isl) {
  const body = document.getElementById(`tbody-${isl}`);
  body.innerHTML = "";
  let height = 0;

  for (let i = 0; i < res.length; i++) {
    const row = res[i];

    const tr = document.createElement("tr");
    body.appendChild(tr);

    const tdRis = document.createElement("td");
    tdRis.innerText = row.crrgCRis;
    tr.appendChild(tdRis);

    const tdData = document.createElement("td");
    tdData.innerText = row.crrgDtt;
    tr.appendChild(tdData);

    const tdEffort = document.createElement("td");
    tdEffort.innerText = row.crrgTmRunIncr / 3600 == 1 ? row.crrgTmRunIncr / 3600 + " ora" : row.crrgTmRunIncr / 3600 + " ore";
    tr.appendChild(tdEffort);

    const tdIsl = document.createElement("td");
    tdIsl.innerText = row.crrgRifCliente;
    tr.appendChild(tdIsl);

    const tdCommessa = document.createElement("td");
    tdCommessa.innerText = `${row.crrgTstDoc}/${row.crrgPrfDoc}/${row.crrgADoc}/${row.crrgNDoc}`;
    tr.appendChild(tdCommessa);

    const tdDescription = document.createElement("td");
    tdDescription.innerText = row.crrgNote;
    tr.appendChild(tdDescription);

    const td = document.createElement("td");
    const iconaDelete = document.createElement("i");
    iconaDelete.className = "bi bi-trash-fill";
    td.appendChild(iconaDelete);
    tr.appendChild(td);

    height += tr.getBoundingClientRect().height;
  }

  const tr = document.createElement("tr");
  const td = document.createElement("td");
  td.setAttribute("colspan", "7");
  const div = document.createElement("div");
  div.className = "white-icona-wrapper";
  const iconaAdd = document.createElement("i");
  iconaAdd.className = "bi bi-plus-circle-fill pointer-pointer";
  iconaAdd.setAttribute("data-bs-toggle", "modal");
  iconaAdd.setAttribute("data-bs-target", "#addConsuntivo");
  //iconaAdd.setAttribute("riga");
  div.appendChild(iconaAdd);
  td.appendChild(div);
  tr.appendChild(td);
  body.appendChild(tr);

  iconaAdd.addEventListener("click", (e) => {
    const rowN = e.target.getAttribute("riga");

    document.getElementById("crrgCSrl").value = res[rowN][crrgCSrl];
    document.getElementById("crrgCRis").value = res[rowN][crrgCRis];
    document.getElementById("crrgCdl").value = res[rowN][crrgCdl];
    document.getElementById("crrgRifCliente").value = res[rowN][crrgRifCliente];
    document.getElementById("crrgTstDoc").value = res[rowN][crrgTstDoc];
    document.getElementById("crrgPrfDoc").value = res[rowN][crrgPrfDoc];
    document.getElementById("crrgADoc").value = res[rowN][crrgADoc];
    document.getElementById("crrgNDoc").value = res[rowN][crrgNDoc];
    document.getElementById("crrgNOper").value = res[rowN][crrgNOper];
    document.getElementById("crrgTOper").value = res[rowN][crrgTOper];
    document.getElementById("crrgCmaatt").value = res[rowN][crrgCmaatt];
    document.getElementById("crrgCCaus").value = res[rowN][crrgCCaus];
    document.getElementById("crrgApp").value = res[rowN][crrgApp];
    document.getElementById("crrgMod").value = res[rowN][crrgMod];
    document.getElementById("crrgMod").value = res[rowN][crrgMod];
  });

  return height;
}
