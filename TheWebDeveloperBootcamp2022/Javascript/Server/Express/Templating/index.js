const express = require("express");
const app = express();
const path = require("path");
app.use(express.static("public"))
//new version
app.use(express.static(path.join(__dirname,"public")))
app.set("view engine", "ejs");
//setting up new directory path..
app.set("views",path.join(__dirname,"/views"))
app.get("/", (req, res) => {
    console.log("User requesting home page")
    res.render("home.ejs")
})
app.get("/random", (req, res) => {
    //let treat this as a database
    const cat = ["Blue","White","Steph","Robert"]
    let oddOrEvens = "";
    console.log("Random number generator");
    const randNumber = Math.floor(Math.random() * 10);
    if (randNumber % 2 == 0) {
        oddOrEvens = "Even";
    }
    else {
        oddOrEvens = "Odd";
    }
    const data = {
        rand: randNumber,
        oddOrEven: oddOrEvens,
        cats: cat
    }
    res.render("random.ejs",{ data:data})
})

app.get("/s/:animals", (req, res) => {
    const { animals } = req.params;
    console.log(req.params)
    console.log(`Search for ${animals}`);
    res.render("animal.ejs",{animal:animals})
})
app.listen(3000, () => {
    console.log("listening to port 3000");
})