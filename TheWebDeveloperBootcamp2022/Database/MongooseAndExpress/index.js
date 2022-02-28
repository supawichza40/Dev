const express = require("express");
const app = express();
const path = require("path");
const Product = require("./models/product")
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

app.get("/products", async(req, res) => {
    const { category } = req.query;
    
    const { search } = req.body;
    console.log(req.body)

    if (category == null || category == "") {
        
        Product.find({}).then((data) => {
            
            res.render("products/index.ejs", { data: data });
        })
    }
    else {
        const data = await (Product.find({ category: category }));
        res.render("products/index.ejs", { data: data });
    }
})
app.get("/products/create", (req, res) => {
    res.render("products/create.ejs")
})
app.get("/products/:id", async(req, res) => {
    const { id } = req.params;
    const result = await (Product.findById(id));
    res.render("products/detail.ejs",{product:result})
})
app.post("/products", (req, res) => {
    const { name, price, category } = req.body;

    const product = new Product({ name: name, price: price, category: category });
    product.save();
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
app.listen(3000, (req, res) => {
    console.log("Listening to port 3000");
})

