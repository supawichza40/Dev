//OLD API request
// import fetch from "node-fetch";

// var XMLHttpRequest = require('xhr2');
// const req = new XMLHttpRequest();
// req.onload = function () {
//     const data = JSON.parse(this.responseText);
//     console.log(`Old data: ${data.ticker.price}`);

// };
// req.onerror = function () {
//     console.log("Old API request error");

// }
// req.open("get", "https://api.cryptonator.com/api/ticker/btc-usd", true);
// req.setRequestHeader("Accept", "application/json")
// req.send();

//New API request

// async function RequestAPIData(){
//     const req = await (fetch("https://api.cryptonator.com/api/ticker/btc-usd").then(
//         response => {
//             console.log(response);
//             console.log();
//             return response.json();
//         }
//     ).then(data => {
//         console.log(data);
//     }))
// }
// RequestAPIData();

//old use require
//new use import +mjs, both in conflict.

//Best API using library AXIOS
const data = axios.get("https://api.cryptonator.com/api/ticker/btc-usd")
    .then(res =>
        console.log(res.data.ticker.price))
    .catch(e => {
        console.log("Error");
        console.log(e)
    });


const fetchBitcoinData = async () => {
    try {
        const res = await (axios.get("https://api.cryptonator.com/api/ticker/btc-usd")) ;
        
        console.log(res.data.ticker.price);
    }
    catch(e) {
        console.log(e)
    }
}

const header = {
    headers:{Accept:'application/json'}
}
const fetchDadJoke = async (element) => {
    try {
        const res = await (axios.get("https://icanhazdadjoke.com/",header));
        console.log(res.data.joke);
        return res.data.joke;
        
    } catch (error) {
        console.log(error);
    }
}
const jokeButton = document.addEventListener("click", async(event) => {
    const h2Joke = document.querySelector("#joke");
    h2Joke.textContent = await (fetchDadJoke());
    

})