const express = require("express");
const app = express();
const path = require("path");
const mongoose = require("mongoose");
const Campground = require("./models/campground");
const methodOverride = require("method-override");
const ejsMate = require("ejs-mate");
const { error } = require("console");
const AppError = require("./views/error");
const Joi = require("joi");
var bodyParser = require("body-parser");
const Review = require("./models/review");
const camgroundRoute = require("./routes/camgrounds");
const reviewRoute = require("./routes/reviews");
const userRoute = require("./routes/users");
const session = require("express-session");
const flash = require("connect-flash");
const passport = require("passport");
const localStrategy = require("passport-local");
const User = require("./models/user");
const sessionConfig = {
    secret: "this should be a secret",
    resave: true,
    saveUninitialized: true,
    // cookie: { secure: true }
    cookie: {
        //so the expire is use if expire reach user will be force to log out.
        httpOnly:true,
        expires: Date.now() + 1000 * 60 * 60 * 24 * 7,//Expire a week from now.
        maxAge: 1000 * 60 * 60 * 24 * 7
        
    }
}

app.use(session(sessionConfig))
app.use(passport.initialize())
app.use(passport.session())
passport.use(new localStrategy(User.authenticate()))
passport.serializeUser(User.serializeUser());
passport.deserializeUser(User.deserializeUser());
app.use(flash());
app.use(bodyParser.urlencoded({ extended: true }));
app.engine("ejs", ejsMate);
app.use(methodOverride("_method"));
app.use(express.json()) // for parsing application/json
app.use(express.urlencoded({ extended: true })) // for parsing application/x-www-form-urlencoded

app.use(express.static(path.join(__dirname,"public")))
app.set("views", path.join(__dirname, "views"));
app.set("view engine", "ejs");
app.use((req, res, next) => {
    console.log(req.session);
    res.locals.currentuser = req.user;
    res.locals.campgroundMessage = req.flash("success");
    res.locals.reviewMessage = req.flash("successReview");
    res.locals.error = req.flash("error");
    res.locals.userSuccessRegister = req.flash("userSuccess")
    res.locals.successLogin = req.flash("successLogin");
    res.locals.successes = req.flash("successes")
    next();
})
app.use("/campgrounds", camgroundRoute);
app.use("/campgrounds/:id", reviewRoute);
app.use("/", userRoute);
//we do not need to write try and catch everywhere
function wrapAsync(fn) {
    return function (req, res, next) {
        fn(req, res, next).catch(e => next(e));
    }
}
//so we do not have to create/write joi schema everywhere
const validateCampgroundData = function (req, res, next) {
    const campgroundSchemaJOI = Joi.object({
        name: Joi.string().required(),
        price: Joi.number().required(),
        location: Joi.string().required(),
        image: Joi.string().required(),
        description: Joi.string().required()
    })
    const { error } = campgroundSchemaJOI.validate(req.body);
    //this will catch if joi provide any error.
    if (error) {
        const msg = error.details.map(el => el.message).join(",");
        throw new AppError(404, msg);
    }
    //if there is no error, then will call the next middleware.
    else {
        next();
    }
}
main().catch(err => console.log(err));

async function main() {
    await mongoose.connect('mongodb://localhost:27017/CampGround');
}
app.get("/", (req, res) => {
    console.log("This is a home page");
    res.render("home.ejs")
})

app.get("/count", (req, res) => {
    if (req.session.count>0) {
        req.session.count += 1;
    }
    else {
        req.session.count = 1;
    }
    console.log("req.session");
    res.send(`This is my ${req.session.count} times`)
})
app.get("/fakeuser", async(req, res) => {
    const user = new User({ email: "supawichzaa@gmail.com", username: "supawichza40002333333" });
    const newUser = await (User.register(user, "chicken"));
    console.log(newUser);
})
app.use(function (err, req, res, next) {
    console.log(err);
    console.dir(err);
    res.status(500).render("campgrounds/error.ejs",{err})
})


app.listen(3000, () => {
    console.log("Listening to port 3000")
})