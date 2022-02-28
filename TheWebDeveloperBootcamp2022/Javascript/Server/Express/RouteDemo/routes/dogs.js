const { application } = require("express");
const express = require("express");
const router = express.Router();

router.get("/dogs", (req, res) => {
    res.send("This is all dogs page")
})

router.get("/dogs/:id", (req, res) => {
    res.send("This is specific dog")
})
router.get("/dogs/:id/edit", (req, res) => {
    res.send("Editing dog page");
})
module.exports = router;