const express = require("express");
const app = express();
const User = require("./models/user");
const path = require("path");
const bcrypt = require("bcrypt");
const session = require("express-session");
const sessionRequirement = {
    secret: "Thisisnotagoodsecret",
    resave: true,
    saveUninitialized: true,
    cookie: {
        maxAge:1000*10
        
    }
}
app.use(session(sessionRequirement));
// getting-started.js
const mongoose = require('mongoose');
const e = require("express");
const { cookie } = require("express/lib/response");
const hashPassword = async (pw) => {
    const salt = await bcrypt.genSalt(12);
    const hash = await bcrypt.hash(pw, salt);
    return hash;
}
const login = async (pw, hashpw) => {
    const result = bcrypt.compare(pw, hashpw);
    if (result) {
        console.log("You are logged in");
        
    }
    else {
        console.log("Invalid password");
    }
    return result;
    
}
main().catch(err => console.log(err));

async function main() {
    await mongoose.connect('mongodb://localhost:27017/UserSignUp');
}
app.use(express.json()) // for parsing application/json
app.use(express.urlencoded({ extended: true })) // for parsing application/x-www-form-urlencoded
app.set("views", path.join(__dirname, "views"))
app.set("view engine", "ejs");
app.get("/", (req, res) => {
    res.send("This is home page")
})
app.post("/success", async(req, res) => {
    const { username, password } = req.body;
    const newUser = new User({
        username: username,
        password: await hashPassword(password)
    })
    await newUser.save()
    res.render("thankyou.ejs");
});
app.get("/signin", (req, res) => {
    res.render("signin.ejs")
})
app.post("/", async(req, res) => {
    const { username, password } = req.body;
    const foundUsername = await User.findOne({ username: username });
    console.log(foundUsername)
    if (foundUsername===null) {
        console.log("Invalid username!");
        res.redirect("/")
    } else {
        const validate = await login(password, foundUsername.password);
        if (validate) {
            console.log("Successfully sign in");
            req.session.user_id = foundUsername._id
            res.redirect("/secret")
        }
        else {
            console.log("Invalid username or password");
            res.redirect("/signin");
        }
        
    }
})
app.get("/secret", (req, res) => {
    console.log(req.session.user_id)
    if (!req.session.user_id) {
        res.redirect("/signin")
    }
    else {
        res.send("My secret is that you are a genius")
    }
})
app.get("/register", (req, res) => {
    res.render("register.ejs");
})
app.listen(3000, () => {
    console.log("Listening on port 3000")
})