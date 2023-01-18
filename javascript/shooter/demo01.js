
//2023 Levi D. Smith - levidsmith.com
function startGame() {
    var canvas = document.getElementById("theCanvas");
    var ctx = canvas.getContext("2d");
    ctx.fillStyle = "#C0C0C0";
    ctx.fillRect(0, 0, 1280, 720);

    ctx.font = "128px Arial";

    ctx.fillStyle = "#FF0000";
    ctx.fillText("Knox", 32, 128 + (128 * 0));

    ctx.fillStyle = "#00FF00";
    ctx.fillText("Game", 32, 128 + (128 * 1));

    ctx.fillStyle = "#00FFFF";
    ctx.fillText("Design", 32, 128 + (128 * 2));

}