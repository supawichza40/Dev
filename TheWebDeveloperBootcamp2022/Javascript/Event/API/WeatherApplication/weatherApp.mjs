
const weekday = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
const monthNames = ["January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
];
const h2Temp = document.querySelector("h2");
async function getWeatherData() {
    //
    let api_key = "2cc1f898ce3fa66e7418d2be43abcb57";
    const data = await (fetch(`https://api.openweathermap.org/data/2.5/weather?q=london&units=metric&appid=2cc1f898ce3fa66e7418d2be43abcb57`)
        .then((response) => {
            return response.json();
        }))
    
    const time = new Date();
    const status = document.querySelector("#status");
    const h1Date = document.querySelector("#date");
    const h1Time = document.querySelector("#time");
    const location = document.querySelector("#location")

    h1Date.textContent = `${weekday[time.getDay().toString()]} ${time.getDate()} ${monthNames[time.getMonth().toString()]} ${time.getFullYear()} `
    h1Time.textContent = `${time.getHours()}:${time.getMinutes()}.${time.getSeconds()}`
    status.textContent = data.weather[0].main;
    location.textContent = data.name;
    h2Temp.innerHTML = `${data.main.temp}  &#8451;`
    console.log(data.weather[0].main);
    console.log(data.main.temp)
    if (data.weather[0].main === "Clouds") {
        
        document.body.style.backgroundImage = "url('media/images/cloud.jpg')"
        document.body.style.backgroundSize = "cover";
        document.body.style.backgroundRepeat = "no-repeat";
        

        
    }
        
}
getWeatherData();