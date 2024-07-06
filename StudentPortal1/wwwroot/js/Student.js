document.addEventListener('DOMContentLoaded', function () {
    // Function to get history from localStorage
    function getHistory(key) {
        const history = localStorage.getItem(key);
        return history ? JSON.parse(history) : [];
    }

    // Function to save history to localStorage
    function saveHistory(key, history) {
        localStorage.setItem(key, JSON.stringify(history));
    }

    // Function to update the history display
    // Function to update the history display
    function updateHistoryDisplay(historyKey) {
        const historySpan = document.getElementById(historyKey);
        const history = getHistory(historyKey);
        if (history.length > 0) {
            historySpan.innerHTML = '<strong>History:</strong><br>' + history.map((item, index) => {
                return `<div>${item} <button class="remove-history" data-key="${historyKey}" data-index="${index}">x</button></div>`;
            }).join('');
        } else {
            historySpan.innerHTML = ''; // Clear the history display if no history available
        }
    }

    // Function to handle removing a history item
    function removeHistoryItem(key, index) {
        const history = getHistory(key);
        history.splice(index, 1); // Remove the item at the given index
        saveHistory(key, history);
        updateHistoryDisplay(key);
    }

    // Function to handle textbox focus
    function handleTextboxFocus(inputId, historyKey) {
        const input = document.getElementById(inputId);
        input.addEventListener('focus', function () {
            // Show the history when the input is focused
            const historySpan = document.getElementById(historyKey);
            historySpan.style.display = 'block'; // Show history span
            historySpan.style.backgroundColor = 'black'; // Change background color
            updateHistoryDisplay(historyKey);
        });
    }

    // Attach event listeners to each input
    handleTextboxFocus('nameInput', 'nameHistory');
    handleTextboxFocus('ageInput', 'ageHistory');

    // Event delegation to handle removing history items
    document.body.addEventListener('click', function (event) {
        if (event.target.classList.contains('remove-history')) {
            const key = event.target.getAttribute('data-key');
            const index = event.target.getAttribute('data-index');
            removeHistoryItem(key, index);
        }
    });
});
