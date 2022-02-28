const express = require("express");
const router = express.Router();
router.post("/", async (req, res) => {
    console.log(req.params,req.body)
    const { id } = req.params;
    const { body, rating } = req.body;
    console.log(body, rating, id);
    const newReview = new Review({
        body: body,
        rating: rating
    })
    console.log(newReview)
    const foundCampground = await Campground.findById(id);
    foundCampground.reviews.push(newReview);
    console.log(foundCampground);
    await foundCampground.save();
    await newReview.save();
    res.redirect(`/campgrounds/${id}`);

})


module.exports = router;