const express = require("express");
const app = express();
const AppError = require("./errors/AppError");
// app.use((req, res,next) => {
//     console.log("This is my first middleware")
//     next();
// })

// app.use((req, res, next) => {
//     console.log("This is my second middleware")
//     next();
// })

//Create a middleware that is like morgan tiny
// app.use((req, res, next) => {
//     req.requestTime = Date.now();
//     console.log(`${req.method.toUpperCase()} ${req.path}`)
//     next();
// })
// app.use("/secret",(req, res, next) => {
//     const { password } = req.query;
//     if (password === "chickennugget") {
//         console.log("Successful authenticate");
//         next();
//     }
//     else {
//         res.send("You need a password to access this page.")
//     }
// })
const verifyPassword = function(req,res,next){
    const { password } = req.query;
    if (password === "chickennugget") {
        console.log("Successful authenticate");
        return next();
    }
    throw new AppError("password required", 401);
    res.send("You need a password to access this page.")
    
}
app.get("/", (req, res) => {
    console.log(`${req.method.toUpperCase()} ${req.path} ${Date.now()- req.requestTime}`)
    res.send("This is a home page.")
})
app.get("/secret",verifyPassword,(req, res) => {
    res.send("The secret is I am a god!")
})
app.get("/admin", (req, res) => {
    throw new AppError("You are not admin",403)
})
app.use((err, req, res, next) => {
    const { message = "Error", status = 404 } = err;
    res.status(status).send(message);
    // next(err);
})
app.listen(3000, () => {
    console.log("Listening on port 3000")
})