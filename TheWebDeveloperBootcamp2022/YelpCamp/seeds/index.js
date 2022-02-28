const mongoose = require("mongoose");
const Campground = require("../models/campground");
const cities = require("./cities");
const { places, descriptors } = require("./seedHelpers");
const GetRandomNumber = function (maxNumber) {
    return (Math.floor(Math.random() * parseInt(maxNumber)));
}
// console.log(cities[0]);
// console.log(cities[0].city)
console.log(GetRandomNumber(10))
main().catch(err => console.log(err));

async function main() {
    await mongoose.connect('mongodb://localhost:27017/CampGround');
}
const seedDB = async () => {
    await (Campground.deleteMany({}));
    // const c = new Campground({
    //     name: "King of godsss|",
    //     price: 12.99,
    //     location: "Los Angeles",
    //     description: "Place with lots of tree and you can stay with surrounding environment."
    // })

    // await c.save();

    for (let index = 0; index < 10; index++) {
        let randomPrice = Math.floor((Math.random() * 100) + 10);
        const c = new Campground({
            name: `${descriptors[GetRandomNumber(descriptors.length)]} ${places[GetRandomNumber(places.length)]}`,
            location: `${cities[GetRandomNumber(1000)].city} ${cities[GetRandomNumber(1000)].state} `,
            // image: 'https://source.unsplash.com/collection/483251',
            image:'https://picsum.photos/200/300',
            description: "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Similique ea aperiam in deleniti ad dignissimos explicabo, ipsum numquam culpa laboriosam ullam voluptas labore dolore quaerat? Libero aperiam quae quos aspernatur.",
            price: randomPrice
            
        })
        await (c.save());
    }
}

seedDB();


