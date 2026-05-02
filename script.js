fetch('navbar.html')
  .then(res => res.text())
  .then(data => {
    document.getElementById('navbar').innerHTML = data;
  });
  
  flatpickr("#meu-calendario", {
    inline: true,
    locale: "pt",
    dateFormat: "Y-m-d"
});
