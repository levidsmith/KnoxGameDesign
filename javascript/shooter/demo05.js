
//2023 Levi D. Smith - levidsmith.com
const UNIT_SIZE = 64;

class Player {
    constructor() {
        this.w = 1;
        this.h = 1;
        this.x = 5.0;
        this.y = 5.0;
        this.vel_x = 5.0;
        this.vel_y = 0.0;
    } 

    draw(ctx) {
        ctx.fillStyle = "#0000FF";
        ctx.fillRect(this.x * UNIT_SIZE, this.y * UNIT_SIZE, this.w * UNIT_SIZE, this.h * UNIT_SIZE);
    }

    update(deltaTime) {
        ctx.fillStyle = "#FF0000";
        ctx.font = "32px Arial";

        ctx.fillText("x: " + this.x + " deltaTime: " + deltaTime, 32, 32);
        ctx.fillText((this.vel_x * deltaTime), 32, 64);
    
        this.x += (this.vel_x * deltaTime);
        this.y += (this.vel_y * deltaTime);
       
    }
}

//global variables
var canvas;
var ctx;
var p;
var timePrevious;

function startGame() {
    canvas = document.getElementById("theCanvas");
    ctx = canvas.getContext("2d");

    timePrevious = -1;
    window.requestAnimationFrame(update);

    p = new Player();
}

function update(timeCurrent) {
    if (timePrevious >= 0) {
        deltaTime = (timeCurrent - timePrevious) / 1000;
    } else {
        deltaTime = 0;
    }
    timePrevious = timeCurrent;
    draw();
    p.update(deltaTime);
    window.requestAnimationFrame(update);
}

function draw() {
    //clear screen
    ctx.fillStyle = "#C0C0C0";
    ctx.fillRect(0, 0, 1280, 720);

    p.draw(ctx);
}
