const express = require("express");
const adminRouter = express.Router();

adminRouter.get("/admin/topSecret", (req, res) => {
    res.send("This is top secret");
})

module.exports = adminRouter;