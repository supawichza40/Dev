// username
// Comment

// username - comment
// bob - hello!

// CRUD System
// Create
// Post / comment

// Read
// Get / allcomments
// Get / all
// Get / showmecomments

// One way of doing CRUD RECOMMENDED
// NAME OUR RESOURCE THAT WILL BE USE IN ALL CRUD
//  e.g.comments plural(s)
// GET /comments - get all comments
// POST /comments - Create a new comment
// GET /comments/:id - get one comment from a user
// PATCH / comments /:id - update one comment from a user.
// DELETE /comments/:id - Destroy one comment.
const express = require("express");
const app = express();
const path = require("path");
const { v4: uuid } = require("uuid");
const methodOverride = require("method-override")
app.use(methodOverride("_method"));
app.use(express.urlencoded({ extended: true })) // for parsing application/x-www-form-urlencoded
app.set("views",path.join(__dirname,"views"))
app.set("view engine", "ejs");

const comments = [
    {
        id:uuid(),
        username: "Todd",
        comment: "lol that is funny"
    },
    {
        id: uuid(),
        username: "Dear",
        comment:"Hello world"
    },
    {
        id: uuid(),
        username: "Em",
        comment:"This is me!"
    }
]
app.get("/comments", (req, res) => {
    console.log("This is from here  ")
    res.render("comments/index.ejs",{comment:comments});
})
app.get("/comments/new", (req, res) => {
    res.render("comments/commentForm.ejs");
})
app.post("/comments", (req, res) => {
    console.log(req.body)
    const { id,username, comment } = req.body
    console.log(id, username, comment);
    
    const obj = {
        id :uuid(),
        username: username,
        comment:comment
    }
    console.log(obj)
    comments.push(obj)

    // res.render("comments/index.ejs", { comment: comments })
    //it is bad to do render or send when you are doing post request. therefore use redirect.
    // console.log(req.body);
    // res.send("It works")
    res.redirect("/comments")
})
app.patch("/comments/:id", (req, res) => {
    const { id } = req.params;
    console.log(req.params,req.body)
    const newCommentText = req.body.comment;
    const comment_in = comments.find(c => (c.id === id));
    console.log(comment_in);
    comment_in.comment = newCommentText;
    res.redirect("/comments")
})
app.get("/comments/:id", (req, res) => {
    const { id } = req.params;

    const result = comments.find(c => {
        return (c.id === id)
    });
    
    if (result == null) {
        res.render("comments/notfound.ejs")
    }
    else {
        res.render("comments/show.ejs", { comment: result } );
    }
})
//Get id to get the form to edit
app.get("/comments/:id/edit", (req, res) => {
    const { id } = req.params;

    console.log(id)
    const comment = comments.find(c => (c.id === id));
    res.render("comments/updateForm.ejs",{comment:comment})
})
app.delete("/comments/:id", (req, res) => {
    const { id } = req.params;
    for (let i = 0; i < comments.length; i++){
        if (comments[i].id === id) {
            comments.splice(i, 1);
        }
    }
    res.redirect("/comments")
})
app.listen(3000, () => {
    console.log("Listening on port 3000")
    console.log(comments[0].id)
})