
//2023 Levi D. Smith - levidsmith.com
class Player {
    constructor() {
        this.w = 64;
        this.h = 64;
        this.x = (1280 - this.w) / 2;
        this.y = (720 * 2 / 3);
        this.vel_x = 0;
        this.vel_y = 0;
    } 

    draw(ctx) {
        ctx.fillStyle = "#0000FF";
        ctx.fillRect(this.x, this.y, this.w, this.h);
    }

    update() {
        this.x += this.vel_x;
        this.y += this.vel_y;

    }
}

function startGame() {
    var canvas = document.getElementById("theCanvas");
    var ctx = canvas.getContext("2d");
    ctx.fillStyle = "#C0C0C0";
    ctx.fillRect(0, 0, 1280, 720);

    p = new Player();
    p.draw(ctx);

    

}