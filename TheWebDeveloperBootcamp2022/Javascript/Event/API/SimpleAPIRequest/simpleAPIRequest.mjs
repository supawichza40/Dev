

async function getBitcoinDataFromWeb() {
    const body = document.querySelector("body");
    const h1TickerName = document.querySelector("h1");
    const data = await(fetch('https://api.cryptonator.com/api/full/btc-usd')
        .then(response => {
            return response.json();
        })).then(data => {
            setInterval(() => {
                console.log(`Ticker : ${data.ticker.base} - ${data.ticker.target}`);
                console.log(`Price : $${data.ticker.price}`)
                console.log(`Volume: ${data.ticker.volume} units`)
                h1TickerName.textContent = `Ticker : ${data.ticker.base} - ${data.ticker.target}`;
                
                const p_of_price = document.querySelector("#price")
                p_of_price.textContent = `Price : $${data.ticker.price}`
                body.appendChild(p_of_price);


                const p_of_volumn = document.querySelector("#volumn")
                p_of_volumn.textContent = `Volume: ${data.ticker.volume} units`
                body.appendChild(p_of_volumn);

                var currentTime = new Date();
                const h1Time = document.querySelector("#currentTime");
                h1Time.innerHTML = `Time: ${currentTime}`
            }, 1000);



        })




}

getBitcoinDataFromWeb();