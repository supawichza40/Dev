
const path = require("path");
const Product = require("./models/product")
// getting-started.js
const mongoose = require('mongoose');

main().catch(err => console.log(err));

async function main() {
    await mongoose.connect('mongodb://localhost:27017/farmStand');
}

const mango = new Product({
    name: "Mango",
    price: 2.99,
    category: "fruit"
})
const listOfFood = [
    {
        name: "Orange",
        price: 1.99,
        category:"fruit"
    },
    {
        name: "Milk",
        price: 0.99,
        category:"dairy"
    },
    {
        name: "Onion",
        price: 0.89,
        category:"vegetable"
        
    }
]
// mango.save().then(() => {
//     console.log("Successfully save")
// }).catch(e => {
//     console.log("Error")
// })
Product.insertMany(listOfFood).then(() => { 
    console.log("Successful")
}).catch(() => {
    console.log("Failure");
})
