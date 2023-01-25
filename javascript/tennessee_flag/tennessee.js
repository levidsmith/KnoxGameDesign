
//2023 Levi D. Smith - levidsmith.com
var c_tn_red = "#cc0000";
var c_tn_white = "#ffffff";
var c_tn_blue = "#002d65";
var SCALE = 4;


function startGame() {
    var canvas = document.getElementById("theCanvas");
    var ctx = canvas.getContext("2d");


    ctx.scale(SCALE,SCALE);

    ctx.fillStyle = c_tn_red;
    ctx.fillRect(0, 0, 222, 144);

    ctx.fillStyle = c_tn_white;
    ctx.fillRect(222, 0, 3, 144);

    ctx.fillStyle = c_tn_blue;
    ctx.fillRect(225, 0, 15, 144);

    ctx.translate(111, 72);

    ctx.fillStyle = c_tn_white;
    ctx.beginPath();
    ctx.arc(0, 0,39, 0, 2 * Math.PI);
    ctx.fill();

    ctx.fillStyle = c_tn_blue;
    ctx.beginPath();
    ctx.arc(0, 0,36, 0, 2 * Math.PI);
    ctx.fill();


    i = 0;

    while (i < 3) {
        r = 17;
        //2:30 = 5 * 360 / 24 = 75
        //120 = 360 / 3
        //12:00 = -90
        deg = 75 + (120 * i) - 90;
        degToRad = Math.PI / 180;
        xOffset = r * Math.cos(deg * degToRad);
        yOffset = r * Math.sin(deg * degToRad);

        ctx.setTransform();
        ctx.scale(SCALE, SCALE);
        ctx.translate(111, 72);
        ctx.translate(xOffset, yOffset);
        ctx.rotate( (182 + (i * 120)) * degToRad);

        ctx.fillStyle = c_tn_white;

        drawStar(ctx);
        i += 1;
    }

}

function drawStar(ctx1) {
    ctx1.beginPath();
    r = 16;

    j = 0;
    deg = (72 * j) + 270;
    degToRad = Math.PI / 180;
    ctx1.moveTo(r * Math.cos(deg * degToRad), r * Math.sin(deg * degToRad));
  
    j += 2;
    while (j < 12) {
        deg = (72 * j) + 270;
        ctx1.lineTo(r * Math.cos(deg * degToRad), r * Math.sin(deg * degToRad));
        j += 2;
    }
    ctx1.fill();

}