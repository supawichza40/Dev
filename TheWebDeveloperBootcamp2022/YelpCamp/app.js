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
const Review = require("./models/review");
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
app.engine("ejs", ejsMate);
app.use(methodOverride("_method"));
app.use(express.json()) // for parsing application/json
app.use(express.urlencoded({ extended: true })) // for parsing application/x-www-form-urlencoded

app.set("views", path.join(__dirname, "views"));
app.set("view engine", "ejs");
app.get("/", (req, res) => {
    console.log("This is a home page");
    res.render("home.ejs")
})
app.get("/campgrounds/new", (req, res) => {
    res.render("campgrounds/new.ejs");
})
app.get("/campgrounds", async(req, res) => {
    const result = await (Campground.find({}));
    res.render("campgrounds/index.ejs",{campgrounds:result})
})
app.get("/campgrounds/:id", async(req, res) => {
    const { id } = req.params;
    const foundCampground = await (Campground.findById(req.params.id).populate("reviews"));
    console.log(foundCampground)
    res.render("campgrounds/detail.ejs", { campground: foundCampground });
})


app.post("/campgrounds",validateCampgroundData, wrapAsync(async (req, res, next) => {

    const { name, location, price, description,image } = req.body;
    const object = new Campground({
        name: name,
        location: location,
        price: price,
        image:image,
        description:description
    })
    await (object.save());
    res.redirect("/campgrounds");
    
}))
app.get("/campgrounds/:id/update", async(req, res) => {
    const { id } = req.params;
    const foundCampground = await (Campground.findById(id));
    res.render("campgrounds/update.ejs", { campground: foundCampground });

})
app.patch("/campgrounds/:id", async (req, res) => {
    
    const { id } = req.params;
    const { name, location, price, description,image } = req.body;
    console.log(name, location, price, description);
    await (Campground.findByIdAndUpdate(id, { name: name, location: location, price: price,image:image, description: description }));
    res.redirect("/campgrounds");
    
})
app.delete("/campgrounds/:id", async (req, res) => {
    const { id } = req.params;
    const result = await (Campground.findByIdAndDelete(id));
    res.redirect("/campgrounds");
})
//Review
app.post("/campgrounds/:id", async(req, res) => {
    const { id } = req.params;
    const { body, rating } = req.body;
    console.log(body, rating, id);
    const newReview = new Review({
        body: body,
        rating:rating
    })
    console.log(newReview)
    const foundCampground = await Campground.findById(id);
    foundCampground.reviews.push(newReview);
    console.log(foundCampground);
    await foundCampground.save();
    await newReview.save();
    res.redirect(`/campgrounds/${id}`);
    
})
app.delete("/campgrounds/:id/reviews/:reviewID", async(req, res) => {
    const { id } = req.params
    const { reviewID } = req.params;
    const getCampgroundById = await Campground.findById(id);
    await Review.findByIdAndDelete(reviewID);
    // console.log(getCampgroundById.reviews[8]._id.equals(reviewID));
    console.log(reviewID);
    console.log(getCampgroundById.reviews.filter(r => !(r._id.equals(reviewID))));
    getCampgroundById.reviews = getCampgroundById.reviews.filter(r => !(r._id.equals(reviewID)));
    await getCampgroundById.save();
    res.redirect(`/campgrounds/${id}`);
    

})
//End of Review
app.use(function (err, req, res, next) {
    console.log(err);
    console.dir(err);
    res.status(500).render("campgrounds/error.ejs",{err})
})
app.listen(3000, () => {
    console.log("Listening to port 3000")
})