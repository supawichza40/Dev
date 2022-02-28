const mongoose = require('mongoose');
const Product = require('./product');
const { Schema } = mongoose;

const farmSchema = new Schema({
    name: {
        type: String,
        require: [true,"Farm must have a name"]
    },
    city: {
        type: String,
        
    },
    email: {
        type: String,
        required:[true,"Email required"]
    },
    products: [{
        type:Schema.Types.ObjectId,ref:"Product"
    }]
})
farmSchema.post("findOneAndDelete", async function (farm) {
    if (farm.products.length > 0) {
        Product.deleteMany({_id:{ $in:farm.products}})
    }
})
const Farm = new mongoose.model("Farm", farmSchema);
module.exports = Farm;