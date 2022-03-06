module.exports.renderNewCampgroundForm = (req, res) => {
        res.render("campgrounds/new.ejs");

    }
module.exports.index = async (req, res) => {
        console.log(req.user)
        const result = await (Campground.find({}));
        res.render("campgrounds/index.ejs", { campgrounds: result })
    }
module.exports.getIndividualCampground = async (req, res) => {
    const { id } = req.params;
    console.log(req.user)
    const foundCampground = ((await (Campground.findById(req.params.id)
        .populate(
            {
                path: "reviews",
                populate: { path: "author" }

            })
        .populate("author"))));
    console.log(foundCampground);
    if (foundCampground == null) {
        req.flash("error", "This page is no longer exist.")
        res.redirect("/campgrounds")
    }
    res.render("campgrounds/detail.ejs", { campground: foundCampground });
}

module.exports.createNewCampground = async (req, res, next) => {

    const { name, location, price, description } = req.body;
    const list_image = req.files.map(f=>({url:f.path,filename:f.filename}))
    const object = new Campground({
        name: name,
        location: location,
        price: price,
        images: list_image,
        description: description,
        author: req.user._id
    })
    await (object.save());
    req.flash("success", "Successfully create new campground!")
    res.redirect("/campgrounds");

}
module.exports.getCampgroundUpdateForm = async (req, res) => {
    const { id } = req.params;
    const foundCampground = await (Campground.findById(id));
    res.render("campgrounds/update.ejs", { campground: foundCampground });

}
module.exports.updateCampground = async (req, res) => {

    const { id } = req.params;
    const { name, location, price, description, image } = req.body;
    await (Campground.findByIdAndUpdate(id, { name: name, location: location, price: price, image: image, description: description }));
    res.redirect("/campgrounds");

}
module.exports.deleteCampground = async (req, res) => {
    const { id } = req.params;
    const result = await (Campground.findByIdAndDelete(id));
    res.redirect("/campgrounds");
}