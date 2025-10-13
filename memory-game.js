// Memory Game
class MemoryGame {
    constructor() {
        this.cards = [];
        this.flippedCards = [];
        this.matchedPairs = 0;
        this.moves = 0;
        this.gameRunning = false;
        this.startTime = null;
        this.timer = null;
        this.difficulty = 'easy';
        this.symbols = ['🎮', '🎯', '🎲', '🎪', '🎨', '🎭', '🎪', '🎸', '🎺', '🎻', '🥁', '🎤', '🎧', '🎵', '🎶', '🎼', '🎹', '🎺', '🎻', '🎸', '🎷', '🎺', '🎻', '🎸', '🎷', '🎺', '🎻', '🎸', '🎷', '🎺'];
    }

    init() {
        this.setupEventListeners();
        this.checkStars();
        console.log('Memory game initialized!');
    }

    setupEventListeners() {
        // Start button
        const startBtn = document.getElementById('startMemoryBtn');
        if (startBtn) {
            startBtn.onclick = () => this.checkStars();
        }

        // Play again button
        const playAgainBtn = document.getElementById('playAgainBtn');
        if (playAgainBtn) {
            playAgainBtn.onclick = () => this.resetGame();
        }

        // Difficulty select
        const difficultySelect = document.getElementById('difficultySelect');
        if (difficultySelect) {
            difficultySelect.onchange = (e) => {
                this.difficulty = e.target.value;
            };
        }
    }

    checkStars() {
        // For now, just start the game directly
        // In a real implementation, you might check for earned stars
        this.startGame();
    }

    startGame() {
        console.log('Starting Memory game...');
        
        // Hide start screen and show game board
        const startScreen = document.getElementById('memoryStartScreen');
        const gameBoard = document.getElementById('memoryBoard');
        
        if (startScreen) startScreen.style.display = 'none';
        if (gameBoard) gameBoard.style.display = 'block';

        // Reset game state
        this.matchedPairs = 0;
        this.moves = 0;
        this.flippedCards = [];
        this.startTime = new Date();
        this.gameRunning = true;

        // Generate cards based on difficulty
        this.generateCards();

        // Start timer
        this.startTimer();

        // Update stats
        this.updateStats();
    }

    generateCards() {
        const grid = document.getElementById('memoryGrid');
        if (!grid) return;

        // Clear existing cards
        grid.innerHTML = '';

        // Set difficulty-based grid
        const difficultySettings = {
            easy: { rows: 4, cols: 4, totalCards: 16, colClass: 'col-3' },
            medium: { rows: 4, cols: 5, totalCards: 20, colClass: 'col-2' },
            hard: { rows: 5, cols: 6, totalCards: 30, colClass: 'col-2' }
        };

        const settings = difficultySettings[this.difficulty];
        const totalCards = settings.totalCards;
        const pairs = totalCards / 2;

        // Create array of symbols for this game
        const gameSymbols = this.symbols.slice(0, pairs);
        const cardSymbols = [...gameSymbols, ...gameSymbols]; // Duplicate for pairs

        // Shuffle the symbols
        for (let i = cardSymbols.length - 1; i > 0; i--) {
            const j = Math.floor(Math.random() * (i + 1));
            [cardSymbols[i], cardSymbols[j]] = [cardSymbols[j], cardSymbols[i]];
        }

        // Create cards using Bootstrap grid
        this.cards = [];
        for (let i = 0; i < totalCards; i++) {
            // Create column wrapper
            const col = document.createElement('div');
            col.className = `${settings.colClass} memory-col`;

            // Create card
            const card = document.createElement('div');
            card.className = 'memory-card';
            card.dataset.symbol = cardSymbols[i];
            card.dataset.index = i;
            card.onclick = () => this.flipCard(i);

            col.appendChild(card);
            grid.appendChild(col);

            this.cards.push({
                element: card,
                symbol: cardSymbols[i],
                isFlipped: false,
                isMatched: false
            });
        }
    }

    flipCard(index) {
        if (!this.gameRunning) return;

        const card = this.cards[index];
        if (card.isFlipped || card.isMatched || this.flippedCards.length >= 2) return;

        // Flip the card
        card.isFlipped = true;
        card.element.classList.add('flipped');
        card.element.textContent = card.symbol;
        this.flippedCards.push(index);

        // Check for match when two cards are flipped
        if (this.flippedCards.length === 2) {
            this.moves++;
            this.updateStats();
            setTimeout(() => {
                this.checkForMatch();
            }, 1000);
        }
    }

    checkForMatch() {
        const [index1, index2] = this.flippedCards;
        const card1 = this.cards[index1];
        const card2 = this.cards[index2];

        if (card1.symbol === card2.symbol) {
            // Match found!
            card1.isMatched = true;
            card2.isMatched = true;
            card1.element.classList.add('matched');
            card2.element.classList.add('matched');
            this.matchedPairs++;

            // Check if game is complete
            if (this.matchedPairs === this.cards.length / 2) {
                this.completeGame();
            }
        } else {
            // No match - flip cards back
            card1.isFlipped = false;
            card2.isFlipped = false;
            card1.element.classList.remove('flipped');
            card2.element.classList.remove('flipped');
            card1.element.textContent = '';
            card2.element.textContent = '';
        }

        this.flippedCards = [];
    }

    updateStats() {
        const movesElement = document.getElementById('memoryMoves');
        const matchesElement = document.getElementById('memoryMatches');
        const timeElement = document.getElementById('memoryTime');

        if (movesElement) {
            movesElement.textContent = this.moves;
        }

        if (matchesElement) {
            matchesElement.textContent = this.matchedPairs;
        }

        if (timeElement && this.startTime) {
            const elapsed = Math.floor((new Date() - this.startTime) / 1000);
            const minutes = Math.floor(elapsed / 60);
            const seconds = elapsed % 60;
            timeElement.textContent = `${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
        }
    }

    startTimer() {
        this.timer = setInterval(() => {
            this.updateStats();
        }, 1000);
    }

    stopTimer() {
        if (this.timer) {
            clearInterval(this.timer);
            this.timer = null;
        }
    }

    completeGame() {
        this.gameRunning = false;
        this.stopTimer();

        // Show completion screen
        const gameBoard = document.getElementById('memoryBoard');
        const completeScreen = document.getElementById('memoryCompleteScreen');
        const finalMoves = document.getElementById('finalMoves');
        const finalTime = document.getElementById('finalTime');

        if (gameBoard) gameBoard.style.display = 'none';
        if (completeScreen) completeScreen.style.display = 'block';

        if (finalMoves) {
            finalMoves.textContent = this.moves;
        }

        if (finalTime) {
            const elapsed = Math.floor((new Date() - this.startTime) / 1000);
            const minutes = Math.floor(elapsed / 60);
            const seconds = elapsed % 60;
            finalTime.textContent = `${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
        }

        console.log('Memory game completed!', {
            moves: this.moves,
            time: Math.floor((new Date() - this.startTime) / 1000)
        });
    }

    resetGame() {
        console.log('Resetting Memory game...');
        
        // Hide completion screen and show start screen
        const completeScreen = document.getElementById('memoryCompleteScreen');
        const startScreen = document.getElementById('memoryStartScreen');
        
        if (completeScreen) completeScreen.style.display = 'none';
        if (startScreen) startScreen.style.display = 'block';

        // Reset game state
        this.cards = [];
        this.flippedCards = [];
        this.matchedPairs = 0;
        this.moves = 0;
        this.gameRunning = false;
        this.startTime = null;
        this.stopTimer();
        this.updateStats();
    }
}

// Initialize Memory game when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    window.memoryGame = new MemoryGame();
    window.memoryGame.init();
});
