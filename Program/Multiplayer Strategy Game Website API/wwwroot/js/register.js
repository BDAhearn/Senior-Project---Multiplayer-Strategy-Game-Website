async function register() {
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    const result = await post("/register", {
        playerName: username,
        playerPasswordHash: password
    });

    alert("Account created!");
    window.location.href = "login.html";
}