const express = require("express");
const app = express();
const path = require("path");
const Product = require("./models/product")
const Farm = require("./models/farm")
// getting-started.js
const mongoose = require('mongoose');
app.set("views", path.join(__dirname, "views"))
app.set("view engine", "ejs");
app.use(express.json()) // for parsing application/json
app.use(express.urlencoded({ extended: true })) // for parsing application/x-www-form-urlencoded
var methodOverride = require("method-override");
app.use(methodOverride("_method"));

main().catch(err => console.log(err));

async function main() {
    await mongoose.connect('mongodb://localhost:27017/farmStand');
}
//FARM ROUTE
app.get("/farms/new", (req, res) => {
    res.render("farms/new.ejs")
})
app.post("/farms", async (req, res) => {
    const { name, city, email } = req.body;
    const newFarm = new Farm({
        name: name,
        city: city,
        email:email
    })
    await newFarm.save();
    res.redirect("/farms");
})
app.get("/farms", async(req, res) => {
    const result = await Farm.find({});
    console.log(result);
    
    res.render("farms/allFarms.ejs",{farms:result})
})
app.get("/farms/:id", async (req, res) => {
    
    const { id } = req.params;
    const result = await Farm.findById(id);
    const result_expand = await(result.populate("products"));
    console.log(result_expand);
    res.render("farms/detail.ejs",{farm:result_expand})

})
app.get("/farms/:id/products/new", (req, res) => {
    const { id } = req.params;
    res.render("farms/products/new.ejs",{id:id})
})
// app.get("/farms/:id/products", async(req, res) => {
//     const { id } = id;
//     const result = await()
// })
app.post("/farms/:id/products", async(req, res) => {
    const { name, price, category } = req.body;
    // console.log(req.body)
    const { id } = req.params;
    const getFarm = await Farm.findById(id); 
    
    const newProduct = new Product({
        name: name,
        price: price,
        category: category,
        farm : getFarm    
    })
    await (newProduct.save());
    getFarm.products.push(newProduct._id);
    await getFarm.save();
    getFarm.populate("products").then(farm => console.log(farm))
    
})
app.get("/farms/:id/products", (req, res) => {
    const { id } = req.params;
    const foundFarm = Farm.findById(id);
    
})
app.delete("/farms/:id", async(req, res) => {
    const { id } = req.params;
    await (Farm.findByIdAndDelete(id));
    res.redirect("/farms")
})

//END FARM ROUTE
//PRODUCT ROUTE
app.get("/products", async(req, res) => {
    const { category } = req.query;
    
    const { search } = req.body;

    if (category == null || category == "") {
        
        Product.find({}).populate("farm").then((data) => {
            res.render("products/index.ejs", { data: data });
        })
    }
    else {
        const data = await (Product.find({ category: category }).populate("farm"));
        res.render("products/index.ejs", { data: data });
    }
})
app.get("/products/new", (req, res) => {
    // console.log("New product")
    res.render("products/create.ejs")
    
})
app.get("/products/:id", async(req, res) => {
    const { id } = req.params;
    const result = await (Product.findById(id));
    res.render("products/detail.ejs",{product:result})
})
app.post("/products", async(req, res) => {
    const { name, price, category, farm } = req.body;


    const farmFound = await Farm.findOne({ name: farm });


    const product = new Product({ name: name, price: price, category: category, farm: farmFound._id });
    farmFound.products.push(product);

    await farmFound.save();
    await product.save();
    res.redirect("/products");
    
})
app.get("/products/:id/update", async(req, res) => {
    const { id } = req.params;
    const data = await (Product.findById(id));
    console.log(data);
    res.render("products/update.ejs",{product:data})
})
app.patch("/products/:id", async(req, res) => {
    const { name, price, category } = req.body;
    const { id } = req.params;
    const response = await(Product.findByIdAndUpdate(id, { name: name, price: price, category: category }));
    console.log(response);
    res.redirect("/products");
})
app.delete("/products/:id", async(req, res) => {
    const { id } = req.params;
    const result = await (Product.findByIdAndDelete(id));
    console.log("Delete data")
    console.log(result);
    res.redirect("/products");
})
//PRODUCT ROUTE
app.listen(3000, (req, res) => {
    console.log("Listening to port 3000");
})

