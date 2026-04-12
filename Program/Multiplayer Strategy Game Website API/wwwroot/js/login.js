async function login() {
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    const result = await post("/login", {
        userName: username,
        password: password,
    });

    if (result.token) {
        saveToken(result.token);
        window.location.href = "index.html";
    } else {
        alert("Login failed");
    }
}