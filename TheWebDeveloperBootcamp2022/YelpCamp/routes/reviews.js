const express = require("express");
const router = express.Router({ mergeParams:true});
const Campground = require("../models/campground")
const Review = require("../models/review");
var bodyParser = require("body-parser");
const app = express();
app.use(bodyParser.urlencoded({ extended: true }));

router.post("/", async (req, res) => {
    const { id } = req.params;
    const { body, rating } = req.body;
    const newReview = new Review({
        body: body,
        rating: rating,
        author: req.user._id
    })
    const foundCampground = await Campground.findById(id);
    foundCampground.reviews.push(newReview);
    await foundCampground.save();
    await newReview.save();
    req.flash("successReview", "Successfully create Review");
    res.redirect(`/campgrounds/${id}`);

})
router.delete("/reviews/:reviewID", async (req, res) => {
    const { id } = req.params
    const { reviewID } = req.params;
    const getCampgroundById = await Campground.findById(id);
    await Review.findByIdAndDelete(reviewID);
    getCampgroundById.reviews = getCampgroundById.reviews.filter(r => !(r._id.equals(reviewID)));
    await getCampgroundById.save();
    res.redirect(`/campgrounds/${id}`);


})


module.exports = router;