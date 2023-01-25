
//2023 Levi D. Smith - levidsmith.com
const UNIT_SIZE = 64;
const SCREEN_W = 20;
const SCREEN_H = 11;

class Player {
    constructor() {
        this.w = 1;
        this.h = 1;
        this.speed = 5;
        this.x = 10;
        this.y = 8;
        this.vel_x = 0;
        this.vel_y = 0;
    } 

    draw(ctx) {
        ctx.fillStyle = "#0000FF";
        ctx.fillRect(this.x * UNIT_SIZE, this.y * UNIT_SIZE, this.w * UNIT_SIZE, this.h * UNIT_SIZE);
    }

    update(deltaTime) {
        this.x += (this.vel_x * deltaTime);
        this.y += (this.vel_y * deltaTime);

        if (this.x < 0) {
            this.x = 0;
        } else if (this.x + this.w > SCREEN_W) {
            this.x = SCREEN_W  - this.w;
        }

        if (this.y < 0) {
            this.y = 0;
        } else if (this.y + this.h > SCREEN_H) {
            this.y = SCREEN_H - this.h;
        }
       
    }

    moveLeft() {
        this.vel_x = -5;
    }

    moveRight() {
        this.vel_x = 5;
    }

    moveUp() {
        this.vel_y = -5;
    }

    moveDown() {
        this.vel_y = 5;
    }


    stopMovingLeft() {
        if (this.vel_x < 0) {
            this.vel_x = 0;
        }
    }

    stopMovingRight() {
        if (this.vel_x > 0) {
            this.vel_x = 0;
        }
    }

    stopMovingUp() {
        if (this.vel_y < 0) {
            this.vel_y = 0;
        }
    }

    stopMovingDown() {
        if (this.vel_y > 0) {
            this.vel_y = 0;
        }
    }

    shoot() {
        b = new Bullet();
        b.x = this.x;
        b.y = this.y;
        b.vel_y = -8;

    }
}

class Enemy {
    constructor() {
        this.w = 1;
        this.h = 1;
        this.speed = 5;
        this.x = 3;
        this.y = 2;
        this.vel_x = 2;
        this.vel_y = 0;

        this.maxCountdown = 5;
        this.countdown = this.maxCountdown;
        
    } 

    draw(ctx) {
        ctx.fillStyle = "#008000";
        ctx.fillRect(this.x * UNIT_SIZE, this.y * UNIT_SIZE, this.w * UNIT_SIZE, this.h * UNIT_SIZE);
    }

    update(deltaTime) {
        this.x += (this.vel_x * deltaTime);
        this.y += (this.vel_y * deltaTime);

        this.countdown -= deltaTime;
        if (this.countdown <= 0) {
            this.vel_x = this.vel_x * -1;
            this.countdown += this.maxCountdown;
        }
       
    }


}

class Bullet {
    constructor() {
        this.w = 0.5;
        this.h = 0.5;
        this.x = 0;
        this.y = 0;
        this.vel_x = 0;
        this.vel_y = -8;

       // this.maxCountdown = 5;
       // this.countdown = this.maxCountdown;
        
    } 

    draw(ctx) {
        ctx.fillStyle = "#FF0000";
        ctx.fillRect(this.x * UNIT_SIZE, this.y * UNIT_SIZE, this.w * UNIT_SIZE, this.h * UNIT_SIZE);
    }

    update(deltaTime) {
        this.x += (this.vel_x * deltaTime);
        this.y += (this.vel_y * deltaTime);
        this.checkCollision();

       
    }

    checkCollision() {
        if (hasCollided(this.x, this.y, this.w, this.h, e.x, e.y, e.w, e.h)) {
            e = null;
            b = null;
        }
    }


}

//global variables
var canvas;
var ctx;
var timePrevious;

var p;
var e;
var b;


function startGame() {
    canvas = document.getElementById("theCanvas");
    ctx = canvas.getContext("2d");

    timePrevious = -1;
    window.requestAnimationFrame(update);

    p = new Player();
    e = new Enemy();
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
    if (e != null) {
        e.update(deltaTime);
    }
    if (b != null) {
        b.update(deltaTime);
    }
    window.requestAnimationFrame(update);
}

function draw() {
    //clear screen
    ctx.fillStyle = "#C0C0C0";
    ctx.fillRect(0, 0, SCREEN_W * UNIT_SIZE, SCREEN_H * UNIT_SIZE);

    p.draw(ctx);
    if (e != null) {
        e.draw(ctx);
    }
    if (b != null) {
        b.draw(ctx);
    }
}


document.onkeydown=function(e){
    keyPressed(e, (e||window.event).keyCode);
  }
  
document.onkeyup=function(e){
    keyReleased(e, (e||window.event).keyCode);
}

function keyPressed(e, iKey) {
    switch(iKey) {
        case 65:
        case 37:
            p.moveLeft();
            break;
        case 68:
        case 39:
            p.moveRight();
            break;
        case 87:
        case 38:
            p.moveUp();
            break;
        case 83:
        case 40:
            p.moveDown();
            break;
        case 32:
            p.shoot();
            break;
        }
}

function keyReleased(e, iKey) {
    switch(iKey) {
        case 65:
        case 37:
            p.stopMovingLeft();
            break;
        case 68:
        case 39:
            p.stopMovingRight();
            break;
        case 87:
        case 38:
            p.stopMovingUp();
            break;
        case 83:
        case 40:
            p.stopMovingDown();
            break;
    }
}

function hasCollided(x1, y1, w1, h1, x2, y2, w2, h2) {
    var hasCollided = true;
    if (x1 + w1 < x2 ||
        x1 > x2 + w2 ||
        y1 + h1 < y2 ||
        y1 > y2 + h2) {
            hasCollided = false;
    }



    return hasCollided;
}
    