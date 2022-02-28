const mongoose = require("mongoose");
const Campground = require("./models/campground");
main().catch(err => console.log(err));

async function main() {
    await mongoose.connect('mongodb://localhost:27017/CampGround');
}

const product = new Campground({
    name: "King of god",
    price: 12.99,
    location: "Los Angeles",
    description: "Place with lots of tree and you can stay with surrounding environment."
})
product.save();