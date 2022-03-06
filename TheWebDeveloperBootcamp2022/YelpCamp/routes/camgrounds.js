const path = require("path");
//initialisation variables
const { renderNewCampgroundForm, index, getIndividualCampground, createNewCampground, getCampgroundUpdateForm, updateCampground, deleteCampground } = require("../controllers/campgrounds")
const express = require("express");
const multer = require("multer");
//Cloud storage
const { storage } = require("../cloudinary")
const upload = multer({storage})
//End of cloud storage
//This is for local storage
// var storageLocal = multer.diskStorage({
//     destination: function (req, file, callback) {
//         callback(null, './uploads');
//     },
    
//     filename: function (req, file, callback) {
//         var fname = file.originalname+ path.extname(file.originalname);
        
//         callback(null, fname);
        
//     }
// });
// const upload = multer({
//     storage:storageLocal
// })
//End of local storage
Campground = require("../models/campground");
methodOverride = require("method-override");
Joi = require("joi");
bodyParser = require("body-parser");
isLoggedIn = require("../public/javascript/middleware/isLogin")
reqProtector = require("../public/javascript/middleware/reqProtector")
validateCampgroundData = require("../public/javascript/middleware/validateCampgroundData")
wrapAsync = require("../public/javascript/middleware/errorWrapper")
//Configuration

router = express.Router();
app = express();
app.use(bodyParser.urlencoded({ extended: true }));
router.use(methodOverride("_method"))
//Route
router.get("/new", isLoggedIn ,renderNewCampgroundForm)
router.get("/", index)
router.get("/:id", getIndividualCampground)
router.post("", upload.array("image"),validateCampgroundData, wrapAsync(createNewCampground))
// router.post("",upload.array("image"), (req, res) => {
//     console.log(req.body, req.files)
// })
router.get("/:id/update", reqProtector, getCampgroundUpdateForm);
router.patch("/:id", updateCampground);
router.delete("/:id", deleteCampground)

module.exports = router;