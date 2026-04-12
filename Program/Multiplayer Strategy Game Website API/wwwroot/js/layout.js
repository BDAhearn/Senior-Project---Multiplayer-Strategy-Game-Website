async function loadHeader() {
    const response = await fetch("partials/header.html");
    const html = await response.text();
    document.getElementById("header").innerHTML = html;
}

async function loadName() {
    const token = localStorage.getItem("token");

    if (!token) {
        document.getElementById("welcomeUser").innerText = "Guest";
        return;
    }

    const payload = parseJwt(token);

    document.getElementById("welcomeUser").innerText =
        payload.name || payload.sub;
}

function parseJwt(token) {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
        atob(base64)
            .split('')
            .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
            .join('')
    );
    console.log(JSON.parse(jsonPayload));
    return JSON.parse(jsonPayload);
}