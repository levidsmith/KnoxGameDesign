
//2023 Levi D. Smith - levidsmith.com
function startGame() {
    var canvas = document.getElementById("theCanvas");
    var ctx = canvas.getContext("2d");
    ctx.fillStyle = "#C0C0C0";
    ctx.fillRect(0, 0, 1280, 720);

    ctx.fillStyle = "#0000FF";
    ctx.fillRect(0, 0, 64, 64);

}