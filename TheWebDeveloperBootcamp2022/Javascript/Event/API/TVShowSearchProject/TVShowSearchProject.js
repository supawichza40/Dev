// const imageLists = document.querySelector(".imageLists");
// imageLists.innerHTML = "";
// const image = document.createElement("img");
// image.alt = "dfssdf";
// imageLists.appendChild(image);

const imageShowSubmitBtn = document.querySelector("#tvShowSubmitButton")
imageShowSubmitBtn.addEventListener("click", async(event) => {
    event.preventDefault();
    const userSearchInput = document.querySelector("#searchBox");
    console.log(userSearchInput.value)
    const defaultURL = "https://api.tvmaze.com/search/shows?q=";
    const data = await(getURLData(defaultURL, userSearchInput.value));
    const imageLists = document.querySelector(".imageLists");
    imageLists.innerHTML = "";

    for (const d of data) {
        try {
            const image = document.createElement("img");
            image.src = d.show.image.medium
            imageLists.appendChild(image);
            
        } catch (error) {
            const image = document.createElement("img");
            image.alt = d.show.name;
            imageLists.appendChild(image);
        }
    }




})
const getURLData = async (url, searchName) => {
    try {
        const res = await (axios.get(`${url}${searchName}`));
        for (const data of res.data) {
            try {
                console.log(data.show.image.medium)
                
            } catch (error) {
                console.log("Invalid image")
            }
        }
        return res.data;
    } catch (error) {
        console.log("Invalid result, please try again")
    }
}
    
