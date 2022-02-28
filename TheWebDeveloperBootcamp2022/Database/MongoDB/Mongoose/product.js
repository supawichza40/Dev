// getting-started.js
const mongoose = require('mongoose');

main().catch(err => console.log(err));

async function main() {
    await mongoose.connect('mongodb://localhost:27017/PersonApp')
        .then(() => {
            console.log("CONNECTION OPEN!")
        })
        .catch(err => {
            console.log("Error");
        })
}
const productSchema = new mongoose.Schema({
    name: {
        type: String,
        required:true
    },
    price: {
        type: Number,
        required: false
    },
    stock: {
        online: {
            type: Number,
            required:false
        },
        shop: {
            type: Number,
            required:false
        }
    }
});

const personSchema = new mongoose.Schema({
    firstName: String,
    lastName:String
})
personSchema.virtual("fullName").get(function () {
    return `${this.firstName} ${this.lastName}`
})
const Person = mongoose.model("Person", personSchema);

const dear = new Person({ firstName: "Supavich", lastName: "Aussawa" });
console.log(dear.fullname)
dear.save();