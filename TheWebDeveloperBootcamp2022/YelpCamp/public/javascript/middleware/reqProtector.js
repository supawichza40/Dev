const Campground = require("../../../models/campground")
module.exports = reqProtector = async(req, res, next) => {
    
    console.log(req.params);
    const foundCampground = await ((await Campground.findById(req.params.id).populate("author")));
    if (foundCampground.author.equals(req.user)) {
        console.log("same user")
        next()
    }
    else {
        console.log("different user");
        req.flash("error","Only Author for this campground can access this page.")
        res.redirect("/campgrounds")
    }

}