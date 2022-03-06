const mongoose = require("mongoose");
const Campground = require("../models/campground");
const cities = require("./cities");
const { places, descriptors } = require("./seedHelpers");
const GetRandomNumber = function (maxNumber) {
    return (Math.floor(Math.random() * parseInt(maxNumber)));
}
// console.log(cities[0]);
// console.log(cities[0].city)
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
            images: [
                {
                    url: 'https://res.cloudinary.com/kingofgodz/image/upload/v1646598960/YelpCamp/qvlpupwipwygkjapu17u.jpg',
                    filename:"YelpCamp/anab3x972rsprbw1nsqe"
                },
                {
                    url: 'https://res.cloudinary.com/kingofgodz/image/upload/v1646598960/YelpCamp/jfeksfg4mxo0sbcmnfdu.jpg',
                    filename:"YelpCamp/anab3x9sdfasdfasdffe"
                }
            ],
            description: "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Similique ea aperiam in deleniti ad dignissimos explicabo, ipsum numquam culpa laboriosam ullam voluptas labore dolore quaerat? Libero aperiam quae quos aspernatur.",
            price: randomPrice,
            author: "622101f1c62f896b12f1fd4e",
            reviews:[]
            
        })
        await (c.save());
    }
}

seedDB();
console.log("Done")


