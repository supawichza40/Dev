const Joi = require("joi")
module.exports = validateCampgroundData = function (req, res, next) {
    const campgroundSchemaJOI = Joi.object({
        name: Joi.string().required(),
        price: Joi.number().required(),
        location: Joi.string().required(),
        image: Joi.string().required(),
        description: Joi.string().required()
    })
    const { error } = campgroundSchemaJOI.validate(req.body);
    //this will catch if joi provide any error.
    if (error) {
        const msg = error.details.map(el => el.message).join(",");
        throw new AppError(404, msg);
    }
    //if there is no error, then will call the next middleware.
    else {
        next();
    }
}