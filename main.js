// Main application logic
class GameApp {
    constructor() {
        this.currentGame = null;
        this.init();
    }

    init() {
        this.setupNavigation();
        this.showWelcomeScreen();
        console.log('Game App initialized!');
    }

    setupNavigation() {
        // Game navigation buttons
        const flappyBtn = document.querySelector('button[onclick="showGame(\'flappy\')"]');
        const memoryBtn = document.querySelector('button[onclick="showGame(\'memory\')"]');

        if (flappyBtn) {
            flappyBtn.addEventListener('click', () => this.showGame('flappy'));
        }

        if (memoryBtn) {
            memoryBtn.addEventListener('click', () => this.showGame('memory'));
        }
    }

    showGame(gameType) {
        // Hide all game sections
        const gameSections = document.querySelectorAll('.game-section');
        gameSections.forEach(section => {
            section.style.display = 'none';
        });

        // Hide welcome screen
        const welcomeScreen = document.getElementById('welcomeScreen');
        if (welcomeScreen) {
            welcomeScreen.style.display = 'none';
        }

        // Show selected game
        const targetGame = document.getElementById(gameType + 'Game');
        if (targetGame) {
            targetGame.style.display = 'block';
            this.currentGame = gameType;
            console.log(`Switched to ${gameType} game`);
        }

        // Reset games when switching
        this.resetCurrentGame();
    }

    resetCurrentGame() {
        if (this.currentGame === 'flappy') {
            // Reset Flappy Bird game
            if (window.flappyGame) {
                window.flappyGame.gameRunning = false;
                // Show start button
                const startBtn = document.getElementById('startFlappyBtn');
                if (startBtn) {
                    startBtn.style.display = 'inline-block';
                }
                // Hide game over screen
                const gameOverScreen = document.getElementById('gameOverScreen');
                if (gameOverScreen) {
                    gameOverScreen.style.display = 'none';
                }
                // Reset canvas
                if (window.flappyGame.canvas && window.flappyGame.ctx) {
                    window.flappyGame.ctx.fillStyle = '#87CEEB';
                    window.flappyGame.ctx.fillRect(0, 0, window.flappyGame.canvas.width, window.flappyGame.canvas.height);
                }
            }
        } else if (this.currentGame === 'memory') {
            // Reset Memory game
            if (window.memoryGame) {
                window.memoryGame.resetGame();
            }
        }
    }

    showWelcomeScreen() {
        // Hide all game sections
        const gameSections = document.querySelectorAll('.game-section');
        gameSections.forEach(section => {
            section.style.display = 'none';
        });

        // Show welcome screen
        const welcomeScreen = document.getElementById('welcomeScreen');
        if (welcomeScreen) {
            welcomeScreen.style.display = 'block';
        }

        this.currentGame = null;
    }

    // Public method to show games (called from HTML onclick)
    showGame(gameType) {
        this.showGame(gameType);
    }
}

// Global functions for HTML onclick handlers
function showGame(gameType) {
    if (window.gameApp) {
        window.gameApp.showGame(gameType);
    }
}

// Initialize app when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    window.gameApp = new GameApp();
});
