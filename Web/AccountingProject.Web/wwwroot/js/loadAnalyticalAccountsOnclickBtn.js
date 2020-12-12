let debitAnalyticalButtonElement = document.getElementById('DebitAnalyticalButton');
let creditAnalyticalButtonElement = document.getElementById('CreditAnalyticalButton');

debitAnalyticalButtonElement.addEventListener('click', loadAnalyticalAccounts);
creditAnalyticalButtonElement.addEventListener('click', loadAnalyticalAccounts);

function loadAnalyticalAccounts(event) {
    let parentDivElement = event.currentTarget.parentNode.parentNode.parentNode;    
    let mainAccountElement = parentDivElement.previousElementSibling.querySelector('select');
    let id = mainAccountElement.value;    
    let analyticalAccountElement = event.currentTarget.parentNode.previousElementSibling;        
    let xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            let analyticalAccounts = JSON.parse(this.responseText);
            analyticalAccountElement.innerHTML = '';
            if (analyticalAccounts.length > 0) {
                for (let i = 0; i < analyticalAccounts.length; i++) {
                    let optionElement = document.createElement('option');
                    optionElement.value = analyticalAccounts[i].id;
                    optionElement.textContent = analyticalAccounts[i].name;
                    analyticalAccountElement.appendChild(optionElement); 
                }
            }
            else {
                let optionElement = document.createElement('option');                
                analyticalAccountElement.appendChild(optionElement);               
            }
        }
    };
    xhr.open("GET", `/Api/AnalyticalsPerMainAccount/${id}`, true);
    xhr.send();
}
