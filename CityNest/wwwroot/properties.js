document.addEventListener("DOMContentLoaded", function () {
    fetchProperties();  // Виводимо картки при завантаженні сторінки
});

async function fetchProperties() {
    const sortField = document.getElementById('sortField').value;
    const sortOrder = document.getElementById('sortOrder').value;

    try {
        const response = await fetch(`/api/properties?sortField=${sortField}&sortOrder=${sortOrder}`);

        if (!response.ok) {
            console.error("Помилка при отриманні даних:", response.statusText);
            return;
        }

        const properties = await response.json();
        displayProperties(properties);
    } catch (error) {
        console.error("Помилка при отриманні даних:", error);
    }
}

function displayProperties(properties) {
    const propertyList = document.getElementById("propertyList");
    propertyList.innerHTML = ''; // очищаємо попередні картки

    if (properties.length === 0) {
        propertyList.innerHTML = '<p>Немає доступних пропозицій.</p>';
    }

    properties.forEach(property => {
        const card = document.createElement("div");
        card.classList.add("property-card");

        card.innerHTML = `
            <img src="${property.Images[0]}" alt="${property.Title}">
            <div class="info">
                <h3>${property.Title}</h3>
                <p><strong>Місто:</strong> ${property.City}</p>
                <p><strong>Площа:</strong> ${property.Square} м²</p>
                <p><strong>Кімнати:</strong> ${property.Rooms}</p>
                <p class="price">${property.Price} ₴</p>
                <p class="description">${property.Description.substring(0, 100)}...</p>
            </div>
        `;

        propertyList.appendChild(card);
    });
}
