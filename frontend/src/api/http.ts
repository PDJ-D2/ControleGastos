import axios from "axios";

export const http = axios.create({
    baseURL: "http://localhost:5110",
    headers: {
        "Content-Type": "application/json",
    },
});
