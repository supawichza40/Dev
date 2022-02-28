const express = require("express");
const app = express();
const session = require("express-session");
const flash = require("connect-flash");
const path = require("path");
app.set("views", path.join(__dirname, "views"));
app.use(session({secret:"thisisnotagoodsecret"}))
app.use(flash());
app.use((req, res, next) => {
    res.locals.message = req.flash("success");
    next();
})
app.get("/viewcount", (req, res) => {
    if (req.session.count) {
        req.session.count += 1;
    }
    else {
        req.session.count = 1;
    }
    res.send(`You have visited this page ${req.session.count} time/s`)
})
app.get("/", (req, res) => {
    req.flash("success","This is flash")
    res.render("home.ejs",)
})

app.listen(3000, () => {
    console.log("Listening on port 3000");
})