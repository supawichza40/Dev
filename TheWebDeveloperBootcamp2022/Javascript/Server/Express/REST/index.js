const express = require("express");
const app = express();
// app.use(express.urlencoded({ extended: true }))
app.use(express.json()) 
app.set("view engine", "ejs");

app.get("/tacos", (req, res) => [
    res.send("Get/ tacos")
])
app.post("/tacos", (req, res) => {
    console.log(req.body)
    res.send("Post/ tacos")
})
app.get("/form", (req, res) => {
    res.render("getpost.ejs")
})
app.listen(3000, () => {
    console.log("Listening to port 3000")
})