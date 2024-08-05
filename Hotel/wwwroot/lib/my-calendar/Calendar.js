function Start(){
    const currentDate = new Date();
    let currentMonth = currentDate.getMonth();
    generateCalendar(currentMonth);
    let currentMonthLabel = document.getElementById('currentMonth');
    currentMonthLabel.textContent = currentMonth;
    updateMonthName(currentMonth);
}

const months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

    function getMonthName(monthNumber) {
        return months[monthNumber];
}

function updateMonthName(monthNumber) {
    let monthName = document.getElementById('monthName');
    monthName.textContent = getMonthName(monthNumber);
}

function PreviewsMonth(){
    let currentMonthLabel = document.getElementById('currentMonth');
    let currentMonth = Number(currentMonthLabel.textContent) -1;
    if (currentMonth < 0) {
        currentMonth = 11;
    }
    generateCalendar(currentMonth);
    currentMonthLabel.textContent = currentMonth;
    updateMonthName(currentMonth);
}

function NextMonth(){
    let currentMonthLabel = document.getElementById('currentMonth');
    let currentMonth = Number(currentMonthLabel.textContent) +1;
    if (currentMonth==12) {
        currentMonth = 0;
    }
    generateCalendar(currentMonth);
    currentMonthLabel.textContent = currentMonth;
    updateMonthName(currentMonth);
}

function OnClick(td) {
    //td = document.querySelectorAll('tbody td')
    //td.style.background == "blue";
    //td.textContent = 'b';
}
function generateCalendar(month) {
    let rooms = document.querySelectorAll('#rooms #roomNumbers a');
    const currentDate = new Date();
    const currentYear = currentDate.getFullYear();
    let currentMonth = month;
    let days = new Date(currentYear, month + 1, 0).getDate();
    let start = document.querySelector('thead tr');
    th0 = document.createElement('th');
    start.appendChild(th0);
    start.innerHTML = "";// Clear existing calendar content

    for (let day = 0; day <= days; day++) {
        let th = document.createElement('th'); 
        th.textContent=day; 
        start.appendChild(th); 
        if (day == 0) {
            th.textContent = '  ';
        }
    }
    let period = document.querySelector('tbody');
    period.innerHTML = ""; // Clear existing room rows
    rooms.forEach(r => {
        let room = document.createElement('tr');
        let roomNumber = document.createElement('td');
        roomNumber.textContent = r.textContent;
        room.appendChild(roomNumber);
        period.appendChild(room);
        for (d = 1; d <= days; d++) {
            let daytd = document.createElement('td');
            daytd.textContent = ' ';
            room.appendChild(daytd);
        }
        getBookings(room);
    });
    function getBookings(room) {
            let bookings = document.querySelectorAll('#bookings #booking');
            bookings.forEach(booking => {
                let children = booking.children;
                let roomNumber = children[0].textContent;
                let currentRoom = room.children[0].textContent;
                if (roomNumber == currentRoom) {
                    let dateFrom = new Date(children[2].value);
                    let bookingMonthStart = dateFrom.getMonth();
                    let bookingDayStart = dateFrom.getDate();
                    let dateTo = new Date(children[3].value);
                    let bookingMonthEnd = dateTo.getMonth();
                    let bookingDayEnd = dateTo.getDate()-1;
                    let days = room.querySelectorAll('td');
                    if (currentMonth === bookingMonthStart && currentMonth === bookingMonthEnd) {
                        for (i = bookingDayStart; i <= bookingDayEnd; i++) {
                            days[i].style.backgroundColor = "tomato";
                        }
                    }
                    else if (currentMonth === bookingMonthStart && currentMonth < bookingMonthEnd) {
                        for (i = bookingDayStart; i < days.length; i++) {
                            days[i].style.backgroundColor = "tomato";
                        }
                    }
                    else if (bookingMonthStart < currentMonth && bookingMonthEnd === currentMonth) {
                        for (var i = 1; i <= bookingDayEnd; i++) {
                            days[i].style.backgroundColor = "tomato";
                        }
                    }
                }

        }
        )
        //});
    }
    document.querySelectorAll('td').forEach(td => {
        td.addEventListener('click', function () {
            OnClick(this);
        });
    });
}
    Start();
