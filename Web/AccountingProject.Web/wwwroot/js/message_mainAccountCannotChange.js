let mainAccountElement = document.getElementById('MainAccount');
mainAccountElement.addEventListener('click', loadAnalyticalAccounts);

function loadAnalyticalAccounts(event) {
    let mainAccountElement = event.currentTarget;
    let divElement=mainAccountElement.parentNode;
    let li = document.createElement("li");
    li.textContent = "Main account cannot be changed";
    li.classList.add('text-danger');
    divElement.prepend(li);
    setTimeout(function () {
        li.style.display = "none";
    }, 2000); 
}