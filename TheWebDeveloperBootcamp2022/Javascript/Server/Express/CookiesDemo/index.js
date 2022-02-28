const express = require("express");
const app = express();
const cookieParser = require("cookie-parser");
app.use(cookieParser());
app.get("/setusername", (req, res) => {
    res.cookie("name", "Supavich");
    res.send("Sending cookie to the user browser from response.")
})
app.get("/", (req, res) => {
    res.send("This is home page.")
})
app.get("/greet", (req, res) => {
    console.log(req.cookies)
    const { name } = req.cookies;
    res.send(`Hello ${name}`)
})
app.listen(2500, () => {
    console.log("Listening on port 2500")
})