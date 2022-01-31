import fetch from "node-fetch";


async function getWeatherData() {
    let api_key = "2cc1f898ce3fa66e7418d2be43abcb57";
    const data = await (fetch(`https://api.openweathermap.org/data/2.5/weather?q=london&units=metric&appid=2cc1f898ce3fa66e7418d2be43abcb57`)
        .then((response) => {
            return response.json();
        }))

    console.log(data.main.temp)
        
    console.log(data.weather[0].main);


}
getWeatherData();