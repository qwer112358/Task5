document.addEventListener('DOMContentLoaded', function () {
    const regionSelect = document.getElementById('region');
    const errorSlider = document.getElementById('errorSlider');
    const errorInput = document.getElementById('errorInput');
    const seedInput = document.getElementById('seedInput');
    const randomSeedButton = document.getElementById('randomSeed');
    const exportButton = document.getElementById('exportToCsv');
    const tableBody = document.querySelector('#data-table tbody');
    const tableWrapper = document.getElementById('data-table-wrapper');
    const warningMessage = document.createElement('div'); 
    warningMessage.style.color = 'red';
    warningMessage.style.display = 'none'; 
    document.body.appendChild(warningMessage); 

    let pageNumber = 0;
    let isLoading = false;

    function fetchData(append = false) {
        if (isLoading) return;

        const region = regionSelect.value;
        const errorCount = parseFloat(errorInput.value);
        const seed = parseInt(seedInput.value);

        isLoading = true;

        fetch('/Data/generate', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ region, errorCount, seed, pageNumber })
        })
            .then(response => response.json())
            .then(users => {
                if (!append) tableBody.innerHTML = '';

                users.forEach(user => {
                    const row = document.createElement('tr');
                    row.innerHTML = `
                    <td>${user.number}</td>
                    <td>${user.identifier}</td>
                    <td>${user.fullName}</td>
                    <td>${user.address}</td>
                    <td>${user.phone}</td>
                `;
                    tableBody.appendChild(row);
                });

                isLoading = false;
                pageNumber++;
            })
            .catch(() => {
                isLoading = false;
            });
    }

    tableWrapper.addEventListener('scroll', () => {
        if (isLoading) return;
        if (tableWrapper.scrollTop + tableWrapper.clientHeight >= tableWrapper.scrollHeight) {
            fetchData(true);
        }
    });

    regionSelect.addEventListener('change', () => {
        pageNumber = 0;
        fetchData();
    });

    errorSlider.addEventListener('input', () => {
        errorInput.value = errorSlider.value;
        pageNumber = 0;
        fetchData();
    });

    errorInput.addEventListener('input', () => {
        if (errorInput.value > 1000) errorInput.value = 1000;
        if (errorInput.value < 0) errorInput.value = 0;
        errorSlider.value = errorInput.value;
        pageNumber = 0;
        fetchData();
    });

    seedInput.addEventListener('input', () => {
        pageNumber = 0;
        fetchData();
    });

    randomSeedButton.addEventListener('click', () => {
        let maxSeed = 1000000;
        seedInput.value = Math.floor(Math.random() * maxSeed);
        pageNumber = 0;
        fetchData();
    });

    exportButton.addEventListener('click', () => {
        if (tableBody.rows.length === 0) {
            warningMessage.textContent = 'Please generate the data first before exporting!';
            warningMessage.style.display = 'block'; 
            setTimeout(() => {
                warningMessage.style.display = 'none'; 
            }, 3000);
            return; 
        }

        const region = regionSelect.value;
        const errorCount = parseFloat(errorInput.value);
        const seed = parseInt(seedInput.value);

        fetch('/Data/export', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                region,
                errorCount,
                seed,
                pageNumber
            })
        })
            .then(response => response.blob())
            .then(blob => {
                const link = document.createElement('a');
                link.href = URL.createObjectURL(blob);
                link.download = `${region}_error_${errorCount}_seed_${seed}_users.csv`;
                link.click();
            });
    });

    fetchData();
});
