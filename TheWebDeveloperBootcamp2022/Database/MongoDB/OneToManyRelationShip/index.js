// getting-started.js
const mongoose = require('mongoose');

main().catch(err => console.log(err));

async function main() {
    await mongoose.connect('mongodb://localhost:27017/relationshipSchema')
        .then(() => {
            console.log("CONNECTION OPEN!")
        })
        .catch(err => {
            console.log("Error");
        })
}



const productSchema = new mongoose.Schema({
    name: String,
    price: Number,
});
const Product = mongoose.model("Product", productSchema);

Product.insertMany([
    { name: "Orange", price: 1.99 },
    {name:"carrot",price:2.99}
])
const farmSchema = new mongoose.Schema({
    name: String,
    city: String,
    productss: [{type:mongoose.Schema.Types.ObjectId ,ref:"Product"}]
})
const Farm = mongoose.model("Farm", farmSchema);

const makeFarm = async () => {
    const farm = new Farm({ name: "Dearry Farm", city: "London,GB" });
    const melon = await Product.findOne({ name: "carrot" });
    farm.productss.push(melon);
    console.log(farm);
    await  farm.save();

}
makeFarm();
Farm.findOne({ name: "Dearry Farm" }).populate("productss").then(farm => console.log(farm));