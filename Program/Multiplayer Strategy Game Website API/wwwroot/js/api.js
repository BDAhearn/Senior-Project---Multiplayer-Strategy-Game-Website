const API_BASE = "https://localhost:7168/api/account";

async function post(url, data) {
    const response = await fetch(`${API_BASE}${url}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    });

    return response.json();
}

async function getSecure(url) {
    const token = localStorage.getItem("token");

    const response = await fetch(`${API_BASE}${url}`, {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${token}`
        }
    });

    return response;
}