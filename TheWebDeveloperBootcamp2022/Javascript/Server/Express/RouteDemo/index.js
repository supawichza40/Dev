const express = require("express");
const app = express();
const dogRouters = require("./routes/dogs")
const shelterRouters = require("./routes/shelters")
const adminRouters = require("./routes/admin")

const requiredPassword = function (req,res,next) {
    const { password } = req.query;
    if (password === "chickennugget") {
        console.log("Login successful")
        next()
    }
    else {
        res.send("You are not authenticate")
    }
}
app.use("/", requiredPassword ,adminRouters);
app.listen(3000, (req,res) => {
    console.log("Listening to port 3000")
})

app.use("/aloha", shelterRouters);
app.get("/", (req, res) => {
    res.send("This is the home page.")
})
app.use("/", dogRouters);
