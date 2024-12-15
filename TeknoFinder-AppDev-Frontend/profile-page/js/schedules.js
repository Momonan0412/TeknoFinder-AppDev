function openSchedule() {
    const scheduleContainer = document.getElementById('schedule-modal');
    scheduleContainer.style.display = 'block';

    fetch('http://localhost:5099/api/Schedules/Debugging', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => {
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        return response.json(); 
    })
    .then(data => {
        const table = createScheduleTable(data); // Generate the schedule table
        scheduleContainer.innerHTML = ''; // Clear any existing content in the container
        scheduleContainer.appendChild(table); // Append the new table to the container
    })
    .catch(error => {
        console.error("Error:", error); 
    });
}
function createScheduleTable(data) {
    console.log("Creating schedule table...");
    const table = document.createElement('table');
    table.classList.add('schedule-table');

    // Create table header
    const thead = document.createElement('thead');
    const headerRow = document.createElement('tr');
    const days = ['Time', 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    days.forEach((day) => {
        const th = document.createElement('th');
        th.textContent = day;
        headerRow.appendChild(th);
    });
    thead.appendChild(headerRow);
    table.appendChild(thead);

    // Create table body
    const tbody = document.createElement('tbody');
    for (let hour = 6; hour <= 21; hour++) {
        const row = document.createElement('tr');

        // Add the time column
        const timeCell = document.createElement('td');
        timeCell.textContent = `${hour}:00`;
        row.appendChild(timeCell);

        // Add the cells for each day
        for (let day = 0; day < 7; day++) {
            const cell = document.createElement('td');

            // Find matching schedule entries for this hour and day
            const schedule = data.find(item => {
                const startHour = parseInt(item.startsAt.split(':')[0], 10);
                return startHour === hour && item.day === day;
            });

            if (schedule) {
                // Populate cell with subject info
                cell.textContent = `${schedule.subjectTitle} (${schedule.section})`;
                cell.classList.add('highlighted'); 
            }

            row.appendChild(cell);
        }

        tbody.appendChild(row);
    }
    table.appendChild(tbody);

    return table;
}
