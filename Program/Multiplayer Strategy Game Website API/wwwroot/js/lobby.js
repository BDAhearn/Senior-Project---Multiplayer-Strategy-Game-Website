async function createLobby() {
    const gameId = document.getElementById("gameSelect").value;
    const visibility = document.getElementById("visibility").value;
    const token = localStorage.getItem("token");

    const response = await fetch("https://localhost:7168/api/lobby", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },
        body: JSON.stringify({
            lobbyGameId: parseInt(gameId),
            lobbyVisibility: visibility
        })
    });

    const data = await response.json();
    console.log(data);

    if (response.ok) {
        document.getElementById("result").innerText =
            "Lobby created! ID: " + data.lobbyId;
    } else {
        document.getElementById("result").innerText =
            "Error creating lobby";
    }
}

async function loadGames() {
    const dropdown = document.getElementById("gameSelect");

    if (!dropdown) {
        console.error("gameSelect not found");
        return;
    }

    const response = await fetch("https://localhost:7168/api/game/available");
    const games = await response.json();

    dropdown.innerHTML = "";

    games.forEach(game => {
        const option = document.createElement("option");

        let label = game.gameName;

        if (game.gameStatus === "Testing") {
            label += " - testing";
        }

        if (game.gameStatus === "Closed") {
            option.disabled = true;
            label += " - closed";
        }

        option.value = game.gameId;
        option.text = label;

        dropdown.appendChild(option);
    });
}