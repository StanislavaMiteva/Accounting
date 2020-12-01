let debitMainAccountElement = document.getElementById('DebitMainAccountId');
let creditMainAccountElement = document.getElementById('CreditMainAccountId');

debitMainAccountElement.addEventListener('change', loadAnalyticalAccounts);
creditMainAccountElement.addEventListener('change', loadAnalyticalAccounts);

function loadAnalyticalAccounts(event) {
    let mainAccountElement = event.currentTarget;
    let analyticalAccountElement = mainAccountElement.parentNode.nextElementSibling.querySelector('select');
    let id = mainAccountElement.value;    
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
