
// create connection
var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7080/hubs/unit").withAutomaticReconnect().build();


// Connecting to method that Hub invokes
connection.on("updateTotalViews", (value) => {
    var newCountSpan = document.getElementById("amountViews");
    newCountSpan.innerText = value.toString();
})

connection.on("updateUnit", (name, description, temperature) => {

    var nameElement = document.getElementById("nameUnit");
    var descriptionElement = document.getElementById("descriptionUnit");
    var temperatureElement = document.getElementById("temperatureUnit");
    
    nameElement.innerText = name.toString();
    descriptionElement.innerText = description.toString();
    temperatureElement.innerText = temperature.toString();
})

// Calling method on the hub
function new_Window_Loaded_On_Client() {
    connection.send("NewWindowLoaded");
}

function new_Unit_Values_On_Client() {
    connection.send("NewUnitValues");
}


// Start connection
function fulfilled() {
    console.log("Connection to hub successful");
    new_Window_Loaded_On_Client() 
    new_Unit_Values_On_Client()
}
function rejected() {
   // Logging
}

connection.start().then(fulfilled, rejected);





