const express = require("express");
const router = express.Router();
router.get("/shelters", (req, res) => {
    res.send("All shelters");

})

router.get("/shelters/:id", (req, res) => {
    res.send("viewing one shelter");

})
router.get("/shelters/:id/edit", (req, res) => {
    res.send("Edit one shelter");

})

module.exports = router;
