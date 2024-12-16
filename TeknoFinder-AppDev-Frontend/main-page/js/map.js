console.log("JavaScript is linked successfully!");

var imageUrl = '../maps/NGE_test.png';
        var imageBounds = [[0,0], [234,153]]; 
        var travel = [];
        const map = L.map('map').setView([0, 0], 13);
        const UserLayer = L.layerGroup().addTo(map);
        const PathLayer = L.layerGroup().addTo(map);
        const Overlays = {
            "Path Markers": PathLayer,
            "User Markers": UserLayer
        }
        L.imageOverlay(imageUrl, imageBounds).addTo(map);
        L.control.layers(null, Overlays).addTo(map);



        map.on('click', function(e) {
            var latlng = e.latlng; // Get the latitude and longitude of the clicked point
            console.log("You clicked at: " + latlng.lat + ", " + latlng.lng);

            // Optionally add a marker at the clicked position
            var newMarker = CreateUserMarker(latlng.lat, latlng.lng).addTo(map);
        });

        var PathNodes = [];
        
        function CreateUserMarker(lat, lng){
            var customIcon = L.icon({
                iconUrl: '../assets/marker.png',
                iconSize: [32, 43],
                iconAnchor: [16, 43],
                popupAnchor: [0, -43]
            });

            var marker = L.marker([lat, lng], { icon: customIcon }).addTo(map);
            //not very cursed popup code now for the sake of UI/UX
            var pElement = document.createElement('p');
            pElement.textContent = 'I am a user generated Marker!';
            var popupContent = document.createElement('div');
            var clearButton = document.createElement('button');
            clearButton.innerHTML = "Clear Me";
            clearButton.onclick = function () {
                RemoveMarker(marker); //use delete for saved waypoints
            };

            var saveButton = document.createElement('button');
            saveButton.innerHTML = "Save Me";
            saveButton.onclick = function () {
                setMarkerName(marker); //use edit for saved waypoints
            };

            popupContent.appendChild(pElement);
            popupContent.appendChild(clearButton);
            popupContent.appendChild(saveButton);

            marker.bindPopup(popupContent);
            return marker;
        }

        function AddWaypoint(){
            const name = document.getElementById('name').value;
            const coordX = document.getElementById('coordX').value;
            const coordY = document.getElementById('coordY').value;
            
            console.log('Sending waypoint:', { name, coordX, coordY });
                
                fetch(`http://localhost:5099/api/Waypoints`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        waypointName: name,
                        waypointType: "UserPin",
                        pointX: coordX,
                        pointY: coordY,
                        Building: "NGE", 
                        Floor: 0,
                    })
                })
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`HTTP error! Status: ${response.status}`);
                    }
                    return response.json();
                })
                .then(json => console.log(json))
                .catch(error => console.error('Error:', error))
            
        }

        function GetWaypoint(){
                fetch(`http://localhost:5099/api/Waypoints`,{
                    method:'GET',
                    headers:{
                        'Content-Type': 'application/json',
                    }
                })
                .then(response => response.json())
                .then(markers => {
                    console.log(markers)
                    let count = 0
                    markers.forEach(marker => {
                    const { pointX, pointY, waypointName, waypointType } = marker; // Destructure the data

                    var customIcon = L.icon({
                        iconUrl: '../assets/pathnode_icon.png',
                        iconSize: [16, 16]
                    });
                    

                    if(waypointType == 'PathNode'){
                        PathNodes[count] = L.marker([pointX, pointY], { icon: customIcon }).addTo(map)
                        .bindPopup(`<b>${waypointName}</b>`);
                        PathLayer.addLayer(PathNodes[count]);
                        console.log(`Index #${count}: ${PathNodes[count].getLatLng()}`);
                        count++
                    }else{
                        var usermarker = CreateUserMarker(pointX, pointY);
                        UserLayer.addLayer(usermarker);
                    }
                    });
                })
        }
        
        function DeleteWaypoint(x, y){
            fetch(`http://localhost:5099/api/Waypoints`,{
                    method:'GET',
                    headers:{
                        'Content-Type': 'application/json',
                    }
                })
                .then(response => response.json())
                .then(markers => {
                    markers.forEach(marker => {
                        if(marker.PointX == x && marker.PointY == y){
                            console.log(user.id);  // Display user id in console
                        }
                    });
                })
        }

        function setMarkerName(marker) {
            var userInput = prompt('Please enter a number:');
            if (userInput !== null) {
                alert('You entered: ' + userInput);
            }

            document.getElementById('name').value = userInput;
            document.getElementById('coordX').value = marker.getLatLng().lng;
            document.getElementById('coordY').value = marker.getLatLng().lat;
            AddWaypoint();
        }

        function RemoveMarker(marker){
            console.log(marker.getLatLng());
            map.removeLayer(marker);
        }
        
            // Modal handling
            const modal = document.getElementById("myModal");
            const openModalBtn = document.getElementById("openModalBtn");
            
            function openModal() {
                modal.style.display = "block";
            }

            /*
            function closeModal() {
                modal.style.display = "none";
            }
            */

    // Function to handle option selection and show corresponding letters
    function getPathNode(letter){
        switch (letter.toUpperCase()) {
            case 'A':
                return PathNodes[12];
            case 'B':
                return PathNodes[2];
            case 'C':
                return PathNodes[3];
            case 'D':
                return PathNodes[1];
            case 'E':
                return PathNodes[4];
            case 'F':
                return PathNodes[0];
            case 'G':
                return PathNodes[5];
            case 'H':
                return PathNodes[6];
            case 'I':
                return PathNodes[7];
            case 'J':
                return PathNodes[8];
            case 'K':
                return PathNodes[9];
            case 'L':
                return PathNodes[10];
            case 'M':
                return PathNodes[11];
            default:
                return null;  // Or some default value if the letter is invalid
        }
    }
    function getLetters(option) {
        let letters = [];
        switch(option) {
            case '1':
                letters = ['A', 'M', 'L'];
                DrawPath(letters);
                break;
            case '2':
                letters = ['A', 'M', 'L', 'K'];
                DrawPath(letters);
                break;
            case '3':
                letters = ['A', 'M', 'L', 'K', 'J'];
                DrawPath(letters);
                break;
            case '4':
                letters = ['A', 'M', 'L', 'K', 'J', 'H'];
                DrawPath(letters);
                break;
            case '5':
                letters = ['A', 'M', 'L', 'K', 'J', 'H', 'I'];
                DrawPath(letters);
                break;
            case '6':
                letters = ['A', 'B', 'C'];
                DrawPath(letters);
                break;
            case '7':
                letters = ['A', 'B', 'D', 'E'];
                DrawPath(letters);
                break;
            case '8':
                letters = ['A', 'B', 'D', 'F', 'G'];
                DrawPath(letters);
                break;
            default:
                console.log('Invalid option');
                return;
        }
    }

    function confirmSelection() {
        const dropdown = document.getElementById("dropdown");
        const selectedOption = dropdown.value; 
        getLetters(selectedOption); 
        //closeModal(); 
    }

    function removeAllPolylines() {
        travel.forEach(polyline => {
            map.removeLayer(polyline);  //removes all the path in UI
        });
        travel = [];  //resets the path
    }

    function DrawPath(Array_letters){
        if(travel){
            removeAllPolylines();
        }
        

        console.log(`Array: ${Array_letters}`);
        try{
            for (let i = 0; i < Array_letters.length - 1; i++) {
                const num1 = Array_letters[i];
                const num2 = Array_letters[i + 1];
                console.log(`Pair: ${num1}, Node: ${num2}`);
    
                var marker1 = getPathNode(num1);
                var marker2 = getPathNode(num2);
    
                var marker1coords = marker1.getLatLng();
                var marker2coords = marker2.getLatLng();
                
                travel[i] = L.polyline([marker1coords, marker2coords]).addTo(map);
            }
        }catch(error){
            if (error instanceof TypeError) {
                alert('Load the waypoints first by clicked "Saved"');
            } else {
                alert('An unexpected error occurred');
            }
        }
        
    }