Array.from(document.getElementsByClassName("open-btn")).forEach((x) => {
  x.addEventListener("click", () => {
    document.getElementById(`container-${x.parentElement.getAttribute("rowIsl")}`).classList.toggle("opens-opened");
  });
});
