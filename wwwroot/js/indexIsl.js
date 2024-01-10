Array.from(document.getElementsByClassName("open-btn")).forEach((x) => {
  x.addEventListener("click", async () => {
    const rowIsl = x.parentElement.getAttribute("rowIsl");
    const accordionBody = document.getElementById(`accordion-${rowIsl}`);
    accordionBody.classList.toggle("opens-opened");
    if (accordionBody.classList.contains("opens-opened")) {
      const res = await fetchApi(`MepWeb_Isl/${rowIsl}`);
      generateRows(res, rowIsl);
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

  for (const row of res) {
    const tr = document.createElement("tr");
    body.appendChild(tr);

    const tdRis = document.createElement("td");
    tdRis.innerText = row.crrgCRis;
    tr.appendChild(tdRis);

    const tdData = document.createElement("td");
    tdData.innerText = row.crrgDtt;
    tr.appendChild(tdData);

    const tdEffort = document.createElement("td");
    tdEffort.innerText = row.crrgTmRunIncr;
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
    const icona = document.createElement("i");
    icona.className = "bi bi-trash-fill";
    td.appendChild(icona);
    tr.appendChild(td);
  }
}
