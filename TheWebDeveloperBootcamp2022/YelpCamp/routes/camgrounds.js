const express = require("express");
const router = express.Router();
const Campground = require("../models/campground");
const methodOverride = require("method-override");

function wrapAsync(fn) {
    return function (req, res, next) {
        fn(req, res, next).catch(e => next(e));
    }
}
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
router.use(methodOverride("_method"))
router.get("/new", (req, res) => {
    res.render("campgrounds/new.ejs");
})

router.get("/", async (req, res) => {
    const result = await (Campground.find({}));
    res.render("campgrounds/index.ejs", { campgrounds: result })
})
router.get("/:id", async (req, res) => {
    const { id } = req.params;
    const foundCampground = await (Campground.findById(req.params.id).populate("reviews"));
    console.log(foundCampground)
    res.render("campgrounds/detail.ejs", { campground: foundCampground });
})


router.post("", validateCampgroundData, wrapAsync(async (req, res, next) => {

    const { name, location, price, description, image } = req.body;
    const object = new Campground({
        name: name,
        location: location,
        price: price,
        image: image,
        description: description
    })
    await (object.save());
    res.redirect("/campgrounds");

}))
router.get("/:id/update", async (req, res) => {
    const { id } = req.params;
    const foundCampground = await (Campground.findById(id));
    res.render("campgrounds/update.ejs", { campground: foundCampground });

})
router.patch("/:id", async (req, res) => {

    const { id } = req.params;
    const { name, location, price, description, image } = req.body;
    console.log(name, location, price, description);
    await (Campground.findByIdAndUpdate(id, { name: name, location: location, price: price, image: image, description: description }));
    res.redirect("/campgrounds");

})
router.delete("/:id", async (req, res) => {
    const { id } = req.params;
    const result = await (Campground.findByIdAndDelete(id));
    res.redirect("/campgrounds");
})
module.exports = router;