module.exports =  isLoggedIn = (req, res, next) => {
    if (!req.isAuthenticated()) {
        req.session.returnTo = req.originalUrl
        req.flash("error","You are not sign in")
        res.redirect("/signin")
    }
    else {
        next();
        
    }
}