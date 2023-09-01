import axios from "axios";

const SERVER = "http://localhost:7019";
// const user = cookie.load("user")

export const endpoints = {
    "search-articles": "/Article/search-article"
}

export default axios.create({
    baseURL: SERVER
})