const express = require("express");
const router = express.Router({mergeParams:true});
const User = require("../models/user")
const bodyParser = require("body-parser");
const app = express()
const passport = require("passport")
app.use(bodyParser.urlencoded({extended:true}))
router.get("/register", (req, res) => {
    res.render("users/register.ejs")
})
router.post("/success", async(req, res,next) => {
    // console.log(req.body)
    const { email, username, password } = req.body;
    const user = new User({ email: email, username: username })
    const newUser = await (User.register(user, password));
    req.logIn(newUser, err => {
        if (err) return next(err);
        console.log("Welcome to Campgrounds")
        req.flash("userSuccess", "You are successful register! Welcome to Campground")
        res.redirect("/campgrounds")
    });
    
})
router.get("/signin", (req, res) => {
    res.render("users/signin.ejs")
})
router.post("/successful",passport.authenticate("local",{failureFlash:true,failureRedirect:"/login"}), (req, res) => {
    console.log("Welcome to Campgrounds");
    req.flash("successLogin", "You have succesfully log in");
    const redirectUrl = req.session.returnTo || "/campgrounds";
    
    res.redirect(redirectUrl)
})
router.get("/signoff", (req, res) => {
    req.logOut();
    res.redirect("/campgrounds")
})


module.exports = router;