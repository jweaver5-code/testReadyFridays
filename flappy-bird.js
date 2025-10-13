// Flappy Bird Game
class FlappyBirdGame {
    constructor() {
        this.canvas = null;
        this.ctx = null;
        this.bird = {
            x: 50,
            y: 250,
            width: 30,
            height: 30,
            velocity: 0
        };
        this.pipes = [];
        this.score = 0;
        this.gameRunning = false;
        this.gravity = 0.5;
        this.jumpPower = -8;
        this.pipeSpeed = 2;
        this.pipeWidth = 60;
        this.pipeGap = 150;
        this.frameCount = 0;
    }

    init() {
        this.canvas = document.getElementById('flappyCanvas');
        if (!this.canvas) return;
        
        this.ctx = this.canvas.getContext('2d');
        this.setupEventListeners();
        this.draw();
    }

    setupEventListeners() {
        // Start button
        const startBtn = document.getElementById('startFlappyBtn');
        if (startBtn) {
            startBtn.onclick = () => this.startGame();
        }

        // Restart button
        const restartBtn = document.getElementById('restartBtn');
        if (restartBtn) {
            restartBtn.onclick = () => this.restartGame();
        }

        // Keyboard controls
        document.addEventListener('keydown', (e) => this.handleKeyPress(e));
        
        // Mouse/touch controls
        this.canvas.addEventListener('click', (e) => this.handleClick(e));
    }

    startGame() {
        console.log('Starting Flappy Bird game...');
        
        // Hide start button
        const startBtn = document.getElementById('startFlappyBtn');
        if (startBtn) {
            startBtn.style.display = 'none';
        }

        // Reset game state
        this.bird.y = 250;
        this.bird.velocity = 0;
        this.pipes = [];
        this.score = 0;
        this.gameRunning = true;
        this.frameCount = 0;

        // Start game loop
        this.gameLoop();
    }

    restartGame() {
        console.log('Restarting Flappy Bird game...');
        
        // Hide game over screen
        const gameOverScreen = document.getElementById('gameOverScreen');
        if (gameOverScreen) {
            gameOverScreen.style.display = 'none';
        }

        // Start new game
        this.startGame();
    }

    handleKeyPress(e) {
        if (e.code === 'Space' && this.gameRunning) {
            e.preventDefault();
            this.bird.velocity = this.jumpPower;
        }
    }

    handleClick(e) {
        if (this.gameRunning) {
            this.bird.velocity = this.jumpPower;
        }
    }

    gameLoop() {
        if (!this.gameRunning) return;
        
        this.update();
        this.draw();
        requestAnimationFrame(() => this.gameLoop());
    }

    update() {
        // Update bird
        this.bird.velocity += this.gravity;
        this.bird.y += this.bird.velocity;

        // Add new pipes
        if (this.pipes.length === 0 || this.pipes[this.pipes.length - 1].x < this.canvas.width - 200) {
            this.addPipe();
        }

        // Update pipes
        for (let i = this.pipes.length - 1; i >= 0; i--) {
            this.pipes[i].x -= this.pipeSpeed;
            
            // Remove pipes that are off screen
            if (this.pipes[i].x + this.pipeWidth < 0) {
                this.pipes.splice(i, 1);
                this.score++;
                this.updateScore();
            }
        }

        // Check collisions
        if (this.checkCollision()) {
            this.gameOver();
        }
    }

    addPipe() {
        const pipeHeight = Math.random() * (this.canvas.height - this.pipeGap - 100) + 50;
        this.pipes.push({
            x: this.canvas.width,
            topHeight: pipeHeight,
            bottomY: pipeHeight + this.pipeGap
        });
    }

    checkCollision() {
        // Check ground and ceiling
        if (this.bird.y + this.bird.height > this.canvas.height || this.bird.y < 0) {
            return true;
        }

        // Check pipes
        for (let pipe of this.pipes) {
            if (this.bird.x < pipe.x + this.pipeWidth && this.bird.x + this.bird.width > pipe.x) {
                if (this.bird.y < pipe.topHeight || this.bird.y + this.bird.height > pipe.bottomY) {
                    return true;
                }
            }
        }
        return false;
    }

    draw() {
        // Clear canvas
        this.ctx.fillStyle = '#87CEEB';
        this.ctx.fillRect(0, 0, this.canvas.width, this.canvas.height);

        // Draw bird
        this.ctx.fillStyle = '#FFD700';
        this.ctx.fillRect(this.bird.x, this.bird.y, this.bird.width, this.bird.height);

        // Draw pipes
        this.ctx.fillStyle = '#228B22';
        for (let pipe of this.pipes) {
            // Top pipe
            this.ctx.fillRect(pipe.x, 0, this.pipeWidth, pipe.topHeight);
            // Bottom pipe
            this.ctx.fillRect(pipe.x, pipe.bottomY, this.pipeWidth, this.canvas.height - pipe.bottomY);
        }

        // Draw score
        this.ctx.fillStyle = '#000';
        this.ctx.font = '24px Arial';
        this.ctx.fillText(`Score: ${this.score}`, 10, 30);
    }

    updateScore() {
        const scoreElement = document.getElementById('currentScore');
        if (scoreElement) {
            scoreElement.textContent = this.score;
        }
    }

    gameOver() {
        this.gameRunning = false;
        
        // Show game over screen
        const gameOverScreen = document.getElementById('gameOverScreen');
        const finalScore = document.getElementById('finalScore');
        
        if (gameOverScreen) {
            gameOverScreen.style.display = 'block';
        }
        
        if (finalScore) {
            finalScore.textContent = this.score;
        }
        
        console.log('Game Over! Final Score:', this.score);
    }
}

// Initialize Flappy Bird game when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    window.flappyGame = new FlappyBirdGame();
    window.flappyGame.init();
});
