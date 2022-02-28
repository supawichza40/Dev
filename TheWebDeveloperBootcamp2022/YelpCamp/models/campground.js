const mongoose = require("mongoose");
const Review = require("./review");

const campgroundSchema = new mongoose.Schema({
    name: {
        type: String,
        
    },
    price: {
        type: Number,
        
    },
    location: {
        type: String,
        
    },
    image: {
        type:String
    },
    description: {
        type:String
    },
    reviews: [{
        type: mongoose.Schema.Types.ObjectId,
        ref: "Review"
    }]
});
campgroundSchema.post("findOneAndDelete", async function (campground) {
    console.log("Post middleware");
    console.log(campground);
    console.log("Review before");
    console.log(await Review.find({}));
    if (campground.reviews.length) {
        console.log("There is review to delete");
        await Review.deleteMany({ _id: { $in: campground.reviews } });
    }
    console.log("Review After");
    console.log(await Review.find({}));


})
module.exports = mongoose.model("Campground", campgroundSchema);