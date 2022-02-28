// getting-started.js
const mongoose = require('mongoose');

main().catch(err => console.log(err));

async function main() {
    await mongoose.connect('mongodb://localhost:27017/MovieApp')
        .then(() => {
            console.log("CONNECTION OPEN!")
        })
        .catch(err => {
            console.log("Error");
        })
}
const moviewSchema = new mongoose.Schema({
    title: String,
    year: Number,
    score: Number,
    rating: String
});

const Movie = mongoose.model("Movie", moviewSchema);
const amadeus = new Movie({ title: "Amadeus", year: 1997, score: 9.8, rating: "R" });
const amadillo = new Movie({ title: "Amadillo", year: 1997, score: 9.8, rating: "R" });
amadeus.save();
amadillo.save();

Movie.insertMany([
    { title: "Amadillo", year: 1515, score: 5.2, rating: "PG" },
    {title:"King",year:1532,score:1.2,rating:"R"}
])
    .then(() => {
    console.log("It Work!")
    })
    .catch(err => {
        console.log("Error");
})
console.log(amadeus,amadillo);