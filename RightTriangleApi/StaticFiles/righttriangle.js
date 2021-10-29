
const columnWidth = 100;
const columnRes = 10;
const gridWidth = 600;
const canvas = document.getElementById("triangleGrid");
const ctx = canvas.getContext("2d");

let previousCoordinates;

init();

function init() {        
    ctx.beginPath();
    initTriangleGrid();

    populateCoordinatesDropDowns(document.getElementById("ddlV1x"));
    populateCoordinatesDropDowns(document.getElementById("ddlV1y"));
    populateCoordinatesDropDowns(document.getElementById("ddlV2x"));
    populateCoordinatesDropDowns(document.getElementById("ddlV2y"));
    populateCoordinatesDropDowns(document.getElementById("ddlV3x"));
    populateCoordinatesDropDowns(document.getElementById("ddlV3y"));
}

function initTriangleGrid() {
    for (var i = columnWidth; i <= gridWidth; i += columnWidth) {
        ctx.moveTo(0, i);
        ctx.lineTo(gridWidth, i);
        ctx.moveTo(i, 0);
        ctx.lineTo(i, gridWidth);
        
        let x = gridWidth - i;
        ctx.moveTo(x, 0);
        ctx.lineTo(gridWidth, i);

        ctx.moveTo(0, x);
        ctx.lineTo(i, gridWidth);

        ctx.stroke();
    }

    document.getElementById("btnCoordinates").addEventListener("click", btnCoordinatesClick);
    document.getElementById("btnPosition").addEventListener("click", btnPositionClick);
}

function drawTriangle(coordinates, clearTriangle) {
    
    var canvas = document.getElementById("triangleGrid");
    var ctx = canvas.getContext("2d");
    
    ctx.beginPath();
    ctx.moveTo(coordinates.V1[0] * columnRes, coordinates.V1[1] * columnRes);
    ctx.lineTo(coordinates.V2[0] * columnRes, coordinates.V2[1] * columnRes);
    ctx.lineTo(coordinates.V3[0] * columnRes, coordinates.V3[1] * columnRes);
    ctx.lineTo(coordinates.V1[0] * columnRes, coordinates.V1[1] * columnRes);

    if (clearTriangle) {
        ctx.fillStyle = "#FFFFFF";
        ctx.fill();
        ctx.stroke();
    }
    else {
        ctx.fillStyle = "#0096FF";
        ctx.fill();
    }    

    previousCoordinates = coordinates;
}

function calculateCoordinates() {

    let row = document.getElementById("ddlRow").value;
    let column = document.getElementById("ddlColumn").value;
    let url = window.location.origin + "/righttriangle/GetCoordinates?row=" + row + "&column=" + column;

    fetch(url)
        .then(response => response.json())
        .then(coordinates => {
            drawTriangle(coordinates, false);
            document.getElementById("lblCoordinates").innerHTML = JSON.stringify(coordinates);
        })
        .catch((error) => {
            document.getElementById("lblCoordinates").innerHTML = "";
            console.error('Error:', error);
        });
}

function calculatePosition() {

    let v1x = document.getElementById("ddlV1x").value;
    let v1y = document.getElementById("ddlV1y").value;
    let v2x = document.getElementById("ddlV2x").value;
    let v2y = document.getElementById("ddlV2y").value;
    let v3x = document.getElementById("ddlV3x").value;
    let v3y = document.getElementById("ddlV3y").value;

    let coordinates = { V1: [v1x, v1y], V2: [v2x, v2y], V3: [v3x, v3y] };

    let url = window.location.origin + "/righttriangle/GetPosition?v1x=" + v1x + "&v1y=" + v1y + "&v2x=" + v2x + "&v2y=" + v2y + "&v3x=" + v3x + "&v3y=" + v3y;

    fetch(url)
        .then(response => response.json())
        .then(position => {
            drawTriangle(coordinates, false);
            document.getElementById("lblPosition").innerHTML = position.row + position.column;
        })
        .catch((error) => {
            document.getElementById("lblPosition").innerHTML = "";
            console.error('Error:', error);
        });
}

function btnCoordinatesClick() {    

    if (previousCoordinates != undefined) {
        drawTriangle(previousCoordinates, true);
    }

    calculateCoordinates();
}

function btnPositionClick() {    

    if (previousCoordinates != undefined) {
        drawTriangle(previousCoordinates, true);
    }

    calculatePosition();
}

function populateCoordinatesDropDowns(dropDown) {
    for (var i = 0; i <= gridWidth / columnRes; i += columnRes) {
        dropDown.appendChild(new Option(i, i));
    }
}



